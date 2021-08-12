using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;
using CameraWorks;

namespace Player
{
    public class PlayerController : MonoBehaviourPun
    {
        public float mspeed = 0.2f;
        public bool LPress;
        public bool RPress;
        public bool isJumping = false;

        public float m_DoubleClickSecond = 0.25f;
        private bool m_IsOneClick = false;
        private double m_Timer = 0;
        public GameObject LB;
        public GameObject RB;

        public SpriteRenderer PlayerSprite;

        #region MonoBehaviour Callbacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
            CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();
            PlayerSprite = transform.GetComponent<SpriteRenderer>();

            if (_cameraWork != null)
            {
                if (photonView.IsMine)
                {
                    _cameraWork.OnStartFollowing();
                }
            }
            else
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
            }
        }

        void Awake()
        {
            GetComponent<Rigidbody2D>().gravityScale = 1f;
            if (photonView.IsMine)
            {
                LB = GameObject.Find("LButton");
                RB = GameObject.Find("RButton");

                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerDown;
                entry.callback.AddListener((eventData) => { LButtonDown(); });
                LB.GetComponent<EventTrigger>().triggers.Add(entry);

                EventTrigger.Entry entry2 = new EventTrigger.Entry();
                entry2.eventID = EventTriggerType.PointerUp;
                entry2.callback.AddListener((eventData) => { LButtonUp(); });
                LB.GetComponent<EventTrigger>().triggers.Add(entry2);

                EventTrigger.Entry entry3 = new EventTrigger.Entry();
                entry3.eventID = EventTriggerType.PointerDown;
                entry3.callback.AddListener((eventData) => { RButtonDown(); });
                RB.GetComponent<EventTrigger>().triggers.Add(entry3);

                EventTrigger.Entry entry4 = new EventTrigger.Entry();
                entry4.eventID = EventTriggerType.PointerUp;
                entry4.callback.AddListener((eventData) => { RButtonUp(); });
                RB.GetComponent<EventTrigger>().triggers.Add(entry4);

            }
        }

        #endregion

        // Update is called once per frame
        void FixedUpdate()
        {
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
            {
                return;
            }

            if (GetComponent<CameraWork>().enabled == false)
            {
                m_IsOneClick = false;
                LPress = false;
                RPress = false;
                return;
            }

            if (m_IsOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond))
            {
                m_IsOneClick = false;
            }

            if (LPress == true)
            {
                //MoveLeft();
                photonView.RPC("MoveLeft", RpcTarget.All);
            }
            if (RPress == true)
            {
                //MoveRight();
                photonView.RPC("MoveRight", RpcTarget.All);
            }

            MoveKeyboard();
            //CheckGround();

        }

        public void MoveKeyboard()
        {

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                if (!m_IsOneClick)
                {
                    m_Timer = Time.time;
                    m_IsOneClick = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                if (m_IsOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
                {
                    Jump();
                    m_IsOneClick = false;
                }
            }

            if (Input.GetKey(KeyCode.A))
            {
                MoveLeft();
                //photonView.RPC("MoveLeft", RpcTarget.All);
            }
            if (Input.GetKey(KeyCode.D))
            {
                MoveRight();
                //photonView.RPC("MoveRight", RpcTarget.All);
            }

        }

        public void LButtonUp()
        {
            if (!m_IsOneClick && !RPress)
            {
                m_Timer = Time.time;
                LPress = false;
                m_IsOneClick = true;
            }
        }
        public void LButtonDown()
        {
            if (RPress)
                return;

            LPress = true;
            if (m_IsOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
            {
                Jump();
                //photonView.RPC("Jump", RpcTarget.All);
                m_IsOneClick = false;
            }
        }
        public void RButtonUp()
        {
            if (!m_IsOneClick && !LPress)
            {
                m_Timer = Time.time;
                RPress = false;
                m_IsOneClick = true;
            }
        }
        public void RButtonDown()
        {
            if (LPress)
                return;

            RPress = true;
            if (m_IsOneClick && ((Time.time - m_Timer) < m_DoubleClickSecond))
            {
                Jump();
                //photonView.RPC("Jump", RpcTarget.All);
                m_IsOneClick = false;
            }
        }
        [PunRPC]
        void MoveLeft()
        {
            PlayerSprite.flipX = true;
            transform.Translate(new Vector3(-mspeed * Time.deltaTime, 0, 0));
        }
        [PunRPC]
        void MoveRight()
        {
            PlayerSprite.flipX = false;
            transform.Translate(new Vector3(mspeed * Time.deltaTime, 0, 0));

        }
        [PunRPC]
        void DestroySelf()
        {
            if(photonView.IsMine)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }

        }
        [PunRPC]
        void Jump()
        {
            if (isJumping == false)
            {
                isJumping = true;
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                GetComponent<Rigidbody2D>().AddForce(Vector3.up * 260f);
            }
        }

        void OnCollisionStay2D(Collision2D col)
        {
            if (col.transform.tag == "Ground")
            {
                if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) <= 0.05f)
                    isJumping = false;
            }
        }
        void CheckGround()
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, Vector3.left * 0.9f, Color.red);
            Debug.DrawRay(transform.position, Vector3.right * 0.9f, Color.red);

            if (isJumping == true)
            {
                if (Physics.Raycast(transform.position, Vector3.left, out hit, 0.9f) || Physics.Raycast(transform.position, Vector3.right, out hit, 0.9f))
                {
                    if (hit.transform.tag == "Ground")
                    {
                        Debug.Log("You cant Jump");
                        isJumping = true;
                        return;
                    }
                }

            }

        }
    }
}
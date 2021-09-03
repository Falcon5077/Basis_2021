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

        public Vector2 spawnPos;

        [PunRPC]
        public void Restart()
        {
            // 중복 호출을 막기 위한 if문
            if (photonView.IsMine)
            {
                // 라이프 1 감소
                Game_Manager.instance.life--;
            }

            // 라이프가 0 이상이면
            if (Game_Manager.instance.life > 0)
            {
                // 플레이어 위치 초기화
                transform.position = spawnPos;

                // 카메라 위치 동기화 On
                if (photonView.IsMine)
                    GetComponent<CameraWork>().enabled = true;  

                Game_Manager.instance.Clear();

                // 탈출 값 초기화
            }
            else
            {
                Game_Manager.instance.LoadLobby();
            }
        }
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

        private void Update()
        {
            // Esc를 눌렀다면
            if (Input.GetKeyDown(KeyCode.Escape))   
            {
                // 로컬이 마스터클라이언트(host)인지 체크
                if (PhotonNetwork.IsMasterClient)   
                {
                    // Restart 함수를 실행하라고 플레이어 전체에게 송신
                    photonView.RPC("Restart", RpcTarget.All);   
                }
            }
        }

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
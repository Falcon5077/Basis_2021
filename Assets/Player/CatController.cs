using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class CatController : MonoBehaviourPun
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

    void Awake()
    {
     //   GetComponent<Rigidbody2D>().gravityScale = 1f;

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

    // called first
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(GameObject.Find("LButton") == true)
            Awake();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (m_IsOneClick && ((Time.time - m_Timer) > m_DoubleClickSecond))
        {
            m_IsOneClick = false;
        }

        if (LPress == true)
        {
            MoveLeft();
            if (RPress == true)
            {
                RPress = false;
            }
            //photonView.RPC("MoveRight", RpcTarget.All);
        }
        if (RPress == true)
        {
            MoveRight();
            if (LPress == true)
            {
                LPress = false;
            }
            //photonView.RPC("MoveRight", RpcTarget.All);
        }

        MoveKeyboard();
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
                photonView.RPC("Jump", RpcTarget.All);
                m_IsOneClick = false;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            photonView.RPC("MoveLeft", RpcTarget.All);
        }
        if (Input.GetKey(KeyCode.D))
        {
            photonView.RPC("MoveRight", RpcTarget.All);
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
        transform.Translate(new Vector3(-mspeed * Time.deltaTime, 0, 0));
    }
    [PunRPC]
    void MoveRight()
    {
        transform.Translate(new Vector3(mspeed * Time.deltaTime, 0, 0));

    }
    
    [PunRPC]
    void Jump()
    {
        if (isJumping == false)
        {
            isJumping = true;
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * 300f);
           // StartCoroutine("JButtonUp");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.transform.tag == "Ground")
        {
            if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) <= 0.05f)
                isJumping = false;
        }
    }

}

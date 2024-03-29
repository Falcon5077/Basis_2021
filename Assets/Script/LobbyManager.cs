﻿using Photon.Pun; // 유니티용 포톤 컴포넌트들
using Photon.Realtime; // 포톤 서비스 관련 라이브러리
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 마스터(매치 메이킹) 서버와 룸 접속을 담당
public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1"; // 게임 버전
    public Button createButton; // 룸 접속 버튼
    public Button joinButton; // 룸 접속 버튼
    public InputField inputField; // 방 번호 필드

    public int SceneNumber;
    public static bool Connection = false;
    [SerializeField]
    private byte maxPlayersPerRoom = 4; // 방 최대 플레이어
    bool isConnecting;

    // 게임 실행과 동시에 마스터 서버 접속 시도
    private void Start()
    {
        // 접속에 필요한 정보(게임 버전) 설정
        PhotonNetwork.GameVersion = gameVersion;
        // 설정한 정보를 가지고 마스터 서버 접속 시도
        PhotonNetwork.ConnectUsingSettings();

    } 

    public override void OnConnectedToMaster()
    {
        Connection = true;
    }

    // 마스터 서버 접속 실패시 자동 실행
    public override void OnDisconnected(DisconnectCause cause)
    {

        // 마스터 서버로의 재접속 시도
        PhotonNetwork.ConnectUsingSettings();
    }

    private void Update()
    {
        if(Connection == true && !isConnecting)
        {

            createButton.interactable = true;
            joinButton.interactable = true;
        }
    }
    // 룸 생성 시도
    public void Create()
    {
        isConnecting = true;
        // 중복 접속 시도를 막기 위해, 접속 버튼 잠시 비활성화
        createButton.interactable = false;
        joinButton.interactable = false;

        // 마스터 서버에 접속중이라면
        if (PhotonNetwork.IsConnected)
        {
            // 룸 접속 실행
            //PhotonNetwork.JoinRandomRoom();
            string code = Random.Range(0, 10).ToString() + Random.Range(0, 10).ToString() + Random.Range(0, 10).ToString() + Random.Range(0, 10).ToString();
            PhotonNetwork.CreateRoom(code, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }
        else
        {
            // 마스터 서버로의 재접속 시도
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public void Join()
    {
        isConnecting = true;
        // 중복 접속 시도를 막기 위해, 접속 버튼 잠시 비활성화
        createButton.interactable = false;
        joinButton.interactable = false;

        // 마스터 서버에 접속중이라면
        if (PhotonNetwork.IsConnected)
        {
            if (inputField.text != "")
            {
                PhotonNetwork.JoinRoom(inputField.text);
            }
            else
            {
                Debug.Log("코드를 입력해주세요");
            }

            createButton.interactable = true;
            joinButton.interactable = true;
        }
        else
        {
            // 마스터 서버로의 재접속 시도
            //PhotonNetwork.ConnectUsingSettings();
        }
    }

    // (빈 방이 없어)랜덤 룸 참가에 실패한 경우 자동 실행
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // 접속 상태 표시 "빈 방이 없음, 새로운 방 생성..."
        // 최대 4명을 수용 가능한 빈방을 생성
        PhotonNetwork.CreateRoom("Lobby", new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        createButton.interactable = true;
        joinButton.interactable = true;
        Debug.Log("코드를 확인해주세요");
    }
    // 룸에 참가 완료된 경우 자동 실행
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(SceneNumber);
    }
}





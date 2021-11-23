using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public Text networkInfoText;
    public InputField usernameInput;
    public InputField roomNumInput;
    
    public Text roomplayerCnt;
    public Text playerList;
    public GameObject multiPanel;
    public GameObject menu1Panel;
    public GameObject waitPanel;
    public GameObject titleText;

    public byte maxPlayer = 4;
    int roomNum;
    private bool connect = false;

    void Update() {
        if (connect)
        {
            networkInfoText.text = PhotonNetwork.NetworkClientState.ToString();
            SetWaitPanel();
        }
    }

    void SetWaitPanel()
    {
        roomplayerCnt.text = "최대 인원: " + PhotonNetwork.CurrentRoom.MaxPlayers;
        string list = "";
        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            list += PhotonNetwork.PlayerList[i].NickName + "\n";
        }
    }

    // 서버 접속
    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    // 연결시 실행
    public override void OnConnectedToMaster()
    {
        print("서버접속완료");
        connect = true;
        PhotonNetwork.LocalPlayer.NickName = usernameInput.text;
        waitPanel.SetActive(true);
    }

    // 연결 끊기
    public void Disconnect()
    {
        titleText.SetActive(true);
        menu1Panel.SetActive(true);
        multiPanel.SetActive(false);
        PhotonNetwork.Disconnect();
        waitPanel.SetActive(false);
    }
    public override void OnDisconnected(DisconnectCause cause) {
        print("서버 연결 끊김");
        networkInfoText.text = "서버의 연결이 끊어졌습니다.";
        waitPanel.SetActive(false);
    }

    // 로비 접속
 //   public void JoinRobby() => PhotonNetwork.JoinLobby();
 //   public override void OnJoinedLobby() => print("로비 접속");

    // 방 생성/입장
//    public void CreateRoom() => PhotonNetwork.CreateRoom(roomNumInput.text, new RoomOptions { MaxPlayers = maxPlayer });
//    public void JoinRoom() => PhotonNetwork.JoinRoom(roomNumInput.text);
    public void JoinOrCreateRoom() => PhotonNetwork.JoinOrCreateRoom(roomNumInput.text, new RoomOptions { MaxPlayers = maxPlayer }, null);
    public void LeaveRoom() => PhotonNetwork.LeaveRoom();

    public override void OnCreatedRoom() => print("방만들기 완료");
    public override void OnJoinedRoom() => print("방 참가 완료");
    public override void OnCreateRoomFailed(short returnCode, string message) => print("방만들기 실패");
}

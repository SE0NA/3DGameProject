using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public Text networkInfoText;
    public InputField usernameInput;
    public InputField roomNumInput;
    
    public Text playerList;

    public GameObject selectPanel;

    public byte maxPlayer = 4;
    int roomNum;
    private bool connect = false;

    public Button joinBtn;

    SendStageInfo _sendStageInfo;

    private void Start()
    {
        _sendStageInfo = FindObjectOfType<SendStageInfo>();
        selectPanel.SetActive(false);
        Connect();
    }
    void Update() {
        if (connect)
        {
            SetWaitPanel();
        }
    }
    
    void SetWaitPanel()
    {
        string list = "";
        for(int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            list += PhotonNetwork.PlayerList[i].NickName + "\n";
        }
        playerList.text = list;
    }

    public void SelectStagePanel()
    {
        //if (PhotonNetwork.IsMasterClient)
        //{
            selectPanel.SetActive(true);
        //}
    }

    public void SelectStage(int stage)
    {
        switch (stage)
        {
            case 5:
                _sendStageInfo.selectStage = StageLevel.stage5x5;
                break;
            case 7:
                _sendStageInfo.selectStage = StageLevel.stage7x7;
                break;
            case 9:
                _sendStageInfo.selectStage = StageLevel.stage9x9;
                break;
        }
        networkInfoText.text = "게임을 시작합니다...";
        //    photonView.RPC("GameStart", RpcTarget.All);
        GameStart();
    }
    //[PunRPC]
    void GameStart()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    // 서버 접속
    public void Connect() {
        PhotonNetwork.ConnectUsingSettings();
        networkInfoText.text = "서버 접속 중...";
    }

    // 연결시 실행
    public override void OnConnectedToMaster()
    {
        print("서버접속완료");
        networkInfoText.text = "서버 접속 완료!";
        connect = true;
    }

    // 연결 끊기
    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }
    public override void OnDisconnected(DisconnectCause cause) {
        print("서버 연결 끊김");
        networkInfoText.text = "서버의 연결이 끊어졌습니다.";
        joinBtn.enabled = true;
    }
    
    // 방 생성/참가
    public void JoinOrCreateRoom()
    {
        if(roomNumInput.text.Equals("") || usernameInput.text.Equals(""))
        {
            networkInfoText.text = "입력 란을 확인해주십시오.";
            return;
        }
        PhotonNetwork.JoinOrCreateRoom(roomNumInput.text, new RoomOptions { MaxPlayers = maxPlayer }, null);
        joinBtn.enabled = false;
        PhotonNetwork.LocalPlayer.NickName = usernameInput.text;
    }
    public void LeaveRoom() => PhotonNetwork.LeaveRoom();

    public override void OnCreatedRoom() => print("방만들기 완료");
    public override void OnJoinedRoom() => print("방 참가 완료");
    public override void OnCreateRoomFailed(short returnCode, string message) => print("방만들기 실패");

    public void BackBtn()
    {
        LeaveRoom();
        Disconnect();
        SceneManager.LoadScene("Main");
    }
}

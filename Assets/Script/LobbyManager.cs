using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.Assertions.Must;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField, Header("對戰按鈕")] private Button btnBattle;
    [SerializeField, Header("連線畫面")] private GameObject connectPicture;
    [SerializeField, Header("連線人數")] private Text textCountPlayer;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("<color=blue>1.已進入控制台</color>");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("<color=yellow>2.成功連線至大廳</color>");
        btnBattle.interactable = true;
    }

    public void StartConnect()
    {
        print("<color=brown>3.開始連線...</color>");
        connectPicture.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        print("<color=red>4.加入隨機房間失敗</color>");

        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20;
        PhotonNetwork.CreateRoom("", ro);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("<color=green>5.開房者進入房間</color>");

        int currentCount = PhotonNetwork.CurrentRoom.PlayerCount;
        int maxCount = PhotonNetwork.CurrentRoom.MaxPlayers;
        textCountPlayer.text = "連線人數" + currentCount + "/" + maxCount;

        LoadGameSence(currentCount, maxCount);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        print("<color=wite>6.玩家進入房間</color>");

        int currentCount = PhotonNetwork.CurrentRoom.PlayerCount;
        int maxCount = PhotonNetwork.CurrentRoom.MaxPlayers;
        textCountPlayer.text = "連線人數" + currentCount + "/" + maxCount;

        LoadGameSence(currentCount, maxCount);

    }

    private void LoadGameSence(int currentCount, int maxCount)
    {
        if(currentCount == maxCount)
        {
            PhotonNetwork.LoadLevel("遊戲場景");
        }
    }
}

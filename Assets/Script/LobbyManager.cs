using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.Assertions.Must;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField, Header("��ԫ��s")] private Button btnBattle;
    [SerializeField, Header("�s�u�e��")] private GameObject connectPicture;
    [SerializeField, Header("�s�u�H��")] private Text textCountPlayer;

    private void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print("<color=blue>1.�w�i�J����x</color>");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("<color=yellow>2.���\�s�u�ܤj�U</color>");
        btnBattle.interactable = true;
    }

    public void StartConnect()
    {
        print("<color=brown>3.�}�l�s�u...</color>");
        connectPicture.SetActive(true);
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        print("<color=red>4.�[�J�H���ж�����</color>");

        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20;
        PhotonNetwork.CreateRoom("", ro);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("<color=green>5.�}�Ъ̶i�J�ж�</color>");

        int currentCount = PhotonNetwork.CurrentRoom.PlayerCount;
        int maxCount = PhotonNetwork.CurrentRoom.MaxPlayers;
        textCountPlayer.text = "�s�u�H��" + currentCount + "/" + maxCount;

        LoadGameSence(currentCount, maxCount);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        print("<color=wite>6.���a�i�J�ж�</color>");

        int currentCount = PhotonNetwork.CurrentRoom.PlayerCount;
        int maxCount = PhotonNetwork.CurrentRoom.MaxPlayers;
        textCountPlayer.text = "�s�u�H��" + currentCount + "/" + maxCount;

        LoadGameSence(currentCount, maxCount);

    }

    private void LoadGameSence(int currentCount, int maxCount)
    {
        if(currentCount == maxCount)
        {
            PhotonNetwork.LoadLevel("�C������");
        }
    }
}

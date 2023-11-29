using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class launcher : MonoBehaviourPunCallbacks
{
    public static launcher instance;
    public GameObject LoadingScene;
    public TMP_Text LoadingText;

    public GameObject CreateRoomScreen;
    public TMP_InputField roomNameInput;

    public GameObject createdRoomScreen;
    public TMP_Text roomNameText;

    // Start is called before the first frame update
    private void Start()
    {
        instance = this;
        LoadingScene.SetActive(true);
        LoadingText.text = "Connecting to Server....";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        LoadingText.text = "Joining Lobby";
    }

    public override void OnJoinedLobby()
    {
        LoadingScene.SetActive(false);
    }

    public void OpenCreateRoomScreen()
    {
        CreateRoomScreen.SetActive(true);
    }

    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(roomNameInput.text))
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 10;
            PhotonNetwork.CreateRoom(roomNameInput.text);

            LoadingScene.SetActive(true);
            LoadingText.text = " Creating Room";

        }
    }


    public override void OnCreatedRoom()
    {
        LoadingScene.SetActive(false);
        createdRoomScreen.SetActive(true);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
    }


    public void LeaveRoom()
    {
        createdRoomScreen.SetActive(false);
        LoadingScene.SetActive(true);
        LoadingText.text = " Leaving Room....";

        PhotonNetwork.LeaveRoom();
    }
}
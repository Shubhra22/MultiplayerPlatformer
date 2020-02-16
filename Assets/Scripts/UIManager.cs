using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviourPunCallbacks
{
    public GameObject userNameScreen;
    public GameObject connectScreen;

    public Button userNameBtn;
    public InputField userNameInput;

    public Button createRoomBtn;
    public InputField createRoomInput;
    
    public Button joinRoomBtn;
    public InputField joinRoomInput;
    // Start is called before the first frame update
    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        userNameBtn.onClick.AddListener(OnClickSubmitName);
        createRoomBtn.onClick.AddListener(()=>OnClickCreateRoom(createRoomInput.text));
        joinRoomBtn.onClick.AddListener(()=>OnClickJoinRoom(joinRoomInput.text));
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Connected to Lobby");
        userNameScreen.SetActive(true);
    }

    private void OnClickSubmitName()
    {
        string un = userNameInput.text;
        if (un != "")
        {
            PhotonNetwork.NickName = un;
            userNameScreen.SetActive(false);
            connectScreen.SetActive(true);
        }
        else
        {
            Debug.Log("User name can not be empty");
            // Show error box
        }
    }

    private void OnClickJoinRoom(string roomName)
    {
        if (roomName.Length > 3)
        {
            //RoomOptions roomOptions = new RoomOptions();
            //roomOptions.MaxPlayers = 4;
            //PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
            PhotonNetwork.JoinRoom(roomName);
        }
        else
        {
            Debug.Log("Room name must contain at least 3 character");
        }
    }

    public override void OnJoinedRoom()
    {
        //play game
        PhotonNetwork.LoadLevel("Level1");
    }

    private void OnClickCreateRoom(string roomName)
    {
        if (roomName.Length > 3)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 4;
            PhotonNetwork.CreateRoom(roomName, roomOptions, null);
        }
        else
        {
            Debug.Log("Room name must contain at least 3 character");
        }
    }
}

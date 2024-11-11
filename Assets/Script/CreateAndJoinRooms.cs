using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class ConnectAndJoinRooms : MonoBehaviourPunCallbacks
{

    // public PhotonView playerPrefab;
    // public Transform spawnPoint;
    public float cooldownTime = 3.0f;
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    public TextMeshProUGUI messageText;


    public void CreateRoom()
    {
        // Configurar las opciones de la sala
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4; // Límite de 4 jugadores en la sala

        PhotonNetwork.CreateRoom(createInput.text, roomOptions);
        Debug.Log("room creado");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    //    PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {


         messageText.text = "La sala está llena o no existe."; // Muestra el mensaje en la UI }
    }

}

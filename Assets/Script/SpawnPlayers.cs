using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{


    public PhotonView playerPrefab;


    public Transform spawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        // PhotonNetwork.ConnectUsingSettings();
        Debug.Log("conectado al room");


        PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
    }

   // public override void OnConnectedToMaster()
   // {
  //      Debug.Log("conectado al Master");
  //      PhotonNetwork.JoinRandomOrCreateRoom();
  //  }

   // public override void OnJoinedRoom()
    //{
    //    Debug.Log("conectado al room");


    //    PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);


   // }


}

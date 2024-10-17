using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{


    public PhotonView playerPrefab1;
    public PhotonView playerPrefab2;

    public int cont =0;


    public Transform spawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        // PhotonNetwork.ConnectUsingSettings();
        Debug.Log("conectado al room");

        if ((cont%2) == 1)
        {
            PhotonNetwork.Instantiate(playerPrefab1.name, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            PhotonNetwork.Instantiate(playerPrefab2.name, spawnPoint.position, spawnPoint.rotation);
        }

       
    }

    void OnPhotonInstantiate()
    {
        // e.g. store this gameobject as this player's charater in Player.TagObject
        cont = cont + 1;
       // cont = cont % 2;
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

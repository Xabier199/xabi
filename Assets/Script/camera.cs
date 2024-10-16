using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;



public class CamaraScript : MonoBehaviourPunCallbacks
    {
     public GameObject Square; //tenemos creado el objeto John
     void Update()
     {
        if (photonView.IsMine)
        {
            Vector3 position = transform.position;
            position.x = Square.transform.position.x;
            transform.position = position;
        }
     }
}

    


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonFrecuencia : MonoBehaviour
{
    private void Start()
    {
        // Configura la frecuencia de envío a 30 paquetes por segundo
        PhotonNetwork.SendRate = 60;

        // Configura la frecuencia de serialización a 15 paquetes por segundo
        PhotonNetwork.SerializationRate = 60;
    }
}
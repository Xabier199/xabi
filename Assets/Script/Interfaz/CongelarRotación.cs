using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CongelarRotación : MonoBehaviourPunCallbacks
{
    private Quaternion initialRotation;

    void Start()
    {
        if (photonView.IsMine)
        {
            initialRotation = transform.rotation;
        }                                           // Guardamos la rotación inicial del objeto hijo

    }

    void LateUpdate()
    {
        if (photonView.IsMine)
        {
            // Reseteamos la rotación del objeto hijo a su rotación inicial en cada frame
            transform.rotation = initialRotation;
        }
    }
}

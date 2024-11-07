using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ataque : MonoBehaviourPunCallbacks
{

    private float cd;
    public float iniciocd;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {



            if (cd <= 0)
            {
                cd = iniciocd;
            }
            else
            {
                cd -= Time.deltaTime;
            }

        }
    }
}
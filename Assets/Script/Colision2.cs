using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionMultijugador : MonoBehaviourPunCallbacks
{
    private PhotonView playerPhotonView;  // Referencia al PhotonView del jugador
    private GameObject playerObject;      // Referencia al GameObject del jugador
    private float subiendoAnterior;
    public float umbral = 0.01f;          // Umbral para detectar el cambio en Y
    private Collider2D objectCollider;

    [SerializeField]
    private int playerViewID; // Asigna el ViewID del jugador al que queremos seguir

    void Start()
    {
        objectCollider = GetComponent<Collider2D>(); // Obtener el Collider del propio objeto
    }

    void Update()
    {
        // Verifica si el objeto del jugador ya está asignado
        if (playerPhotonView == null)
        {
            // Buscar el PhotonView usando el ViewID
            playerPhotonView = PhotonView.Find(playerViewID);

            // Asegúrate de que el PhotonView ha sido encontrado
            if (playerPhotonView != null)
            {
                playerObject = playerPhotonView.gameObject; // Obtener el GameObject del jugador
                subiendoAnterior = playerObject.transform.position.y;
                Debug.Log("Jugador encontrado con PhotonView, posición inicial en Y: " + subiendoAnterior);
            }
            return; // Espera hasta que se encuentre el objeto
        }

        // Si el jugador ya ha sido encontrado, proceder con la detección de movimiento
        float subiendoActual = playerObject.transform.position.y;

        // Depuración: Verificar las posiciones actuales y anteriores
        Debug.Log("Posición actual del jugador: " + subiendoActual + ", Posición anterior: " + subiendoAnterior);

        // Comparamos las posiciones con un umbral para evitar problemas de precisión
        if (subiendoActual > subiendoAnterior + umbral)
        {
            if (objectCollider.enabled)
            {
                objectCollider.enabled = false; // Desactiva la colisión
                Debug.Log("Está subiendo, colisión desactivada");
            }
        }
        else if (subiendoActual < subiendoAnterior - umbral)
        {
            if (!objectCollider.enabled)
            {
                objectCollider.enabled = true; // Activa la colisión
                Debug.Log("Está bajando, colisión activada");
            }
        }

        // Actualizar la posición anterior después de la comparación
        subiendoAnterior = subiendoActual;
    }
}
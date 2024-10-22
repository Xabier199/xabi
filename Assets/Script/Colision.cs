using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Colision : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public GameObject Player;
    private float subiendoAnterior;
    [SerializeField]
    public float umbral = 0.001f;  // Umbral para detectar el cambio significativo en Y

    private Collider2D objectCollider; // Para controlar las colisiones

    void Start()
    {
        subiendoAnterior = Player.transform.position.y;
        objectCollider = gameObject.GetComponent<Collider2D>(); // Obtiene el componente Collider
        Debug.Log("Posición inicial del jugador: " + subiendoAnterior);
    }

    // Update is called once per frame
    void Update()
    {
     


        float subiendoActual = Player.transform.position.y;
        
        Debug.Log("Posición actual: " + subiendoActual + ", Posición anterior: " + subiendoAnterior);


        // Comparamos las posiciones con un umbral para evitar problemas de precisión
        if (subiendoActual > subiendoAnterior + umbral)
        {
            if (objectCollider.enabled) // Verifica si el collider está activo
            {
                objectCollider.enabled = false; // Desactiva el collider (sin colisiones)
                Debug.Log("Está subiendo, colisión desactivada");
            }
        }
        else if (subiendoActual < subiendoAnterior - umbral)
        {
            if (!objectCollider.enabled) // Verifica si el collider está desactivado
            {
                objectCollider.enabled = true; // Activa el collider (colisiones activas)
                Debug.Log("Está bajando, colisión activada");
            }
        }

        // Actualizar la posición anterior solo después de la comparación
        subiendoAnterior = subiendoActual;
    }
}

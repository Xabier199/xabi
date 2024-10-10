using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement2D : MonoBehaviour
{
    // Velocidad de movimiento del jugador
    public float speed = 5f;

    // Update se llama una vez por frame
    void Update()
    {
        // Obtén el input horizontal (A/D o flechas izquierda/derecha)
        float horizontal = Input.GetAxis("Horizontal");

        // Obtén el input vertical (W/S o flechas arriba/abajo)
        float vertical = Input.GetAxis("Vertical");

        // Crear un vector de movimiento en 2D
        Vector2 movement = new Vector2(horizontal, vertical);

        // Mover al jugador en el espacio 2D usando ese vector
        transform.Translate(movement * speed * Time.deltaTime);
    }
}

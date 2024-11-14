using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MuteButton : MonoBehaviour
{
    private bool isMuted = false;

    // Función para alternar el estado de muteo
    public void ToggleMute()
    {
        // Cambia el estado de muteo
        isMuted = !isMuted;

        // Aplica el volumen según el estado
        AudioListener.volume = isMuted ? 0.0f : 1.0f;

        // Opcional: Imprimir el estado actual en la consola
        Debug.Log(isMuted ? "Sonido en mute" : "Sonido activado");
    }
}

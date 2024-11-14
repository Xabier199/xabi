using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Función para desmutear el sonido
    public void Unmute()
    {
        // Ajusta el volumen global al máximo (1.0f para volumen completo)
        AudioListener.volume = 1.0f;

        // Opcional: Imprimir en la consola para verificar la acción
        Debug.Log("Sonido desmuteado");
    }
}

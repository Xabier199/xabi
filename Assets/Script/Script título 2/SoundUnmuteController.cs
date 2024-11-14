using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Funci�n para desmutear el sonido
    public void Unmute()
    {
        // Ajusta el volumen global al m�ximo (1.0f para volumen completo)
        AudioListener.volume = 1.0f;

        // Opcional: Imprimir en la consola para verificar la acci�n
        Debug.Log("Sonido desmuteado");
    }
}

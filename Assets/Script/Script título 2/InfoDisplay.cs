using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoDisplay : MonoBehaviour
{
    // Información a mostrar
    public string infoMessage = "Este es el mensaje de información";

    // Función para mostrar la información en la consola
    public void ShowInfo()
    {
        Debug.Log(infoMessage);
    }
}

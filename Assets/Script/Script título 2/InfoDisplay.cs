using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoDisplay : MonoBehaviour
{
    // Informaci�n a mostrar
    public string infoMessage = "Este es el mensaje de informaci�n";

    // Funci�n para mostrar la informaci�n en la consola
    public void ShowInfo()
    {
        Debug.Log(infoMessage);
    }
}

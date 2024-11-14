using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanelControl : MonoBehaviour
{
    public GameObject infoPanel; // El panel de informaci�n

    // Funci�n para alternar la visibilidad del panel de informaci�n
    public void ToggleInfoPanel()
    {
        if (infoPanel != null)
        {
            bool isActive = infoPanel.activeSelf;
            infoPanel.SetActive(!isActive); // Cambia el estado activo del panel
        }
        else
        {
            Debug.LogWarning("El panel de informaci�n no est� asignado en el Inspector.");
        }
    }

}

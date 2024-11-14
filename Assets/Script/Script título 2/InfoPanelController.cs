using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanelControl : MonoBehaviour
{
    public GameObject infoPanel; // El panel de información

    // Función para alternar la visibilidad del panel de información
    public void ToggleInfoPanel()
    {
        if (infoPanel != null)
        {
            bool isActive = infoPanel.activeSelf;
            infoPanel.SetActive(!isActive); // Cambia el estado activo del panel
        }
        else
        {
            Debug.LogWarning("El panel de información no está asignado en el Inspector.");
        }
    }

}

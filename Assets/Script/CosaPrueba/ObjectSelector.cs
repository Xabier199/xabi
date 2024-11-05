using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class ObjectSelector : MonoBehaviourPunCallbacks
{
    // SerializeField permite que la variable sea visible en el Inspector
    [SerializeField] private GameObject selectedObject;

    // Referencia al botón que usaremos para seleccionar
    [SerializeField] public Button pinBut;
    [SerializeField] public GameObject pingu;

    private void Start()
    {
        // Asignar el listener al botón
        pinBut.onClick.AddListener(SelectObject);
    }

    private void SelectObject()
    {
        selectedObject = pingu;
        Debug.Log($"Objeto seleccionado: {selectedObject.name}");
    }
}
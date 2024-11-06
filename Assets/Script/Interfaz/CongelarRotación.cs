using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongelarRotación : MonoBehaviour
{
    private Quaternion initialRotation;

    void Start()
    {
        // Guardamos la rotación inicial del objeto hijo
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // Reseteamos la rotación del objeto hijo a su rotación inicial en cada frame
        transform.rotation = initialRotation;
    }
}

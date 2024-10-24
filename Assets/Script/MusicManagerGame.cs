using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightManager : MonoBehaviour
{
    public AudioClip bossFightMusic; // Asigna la música para la pelea del jefe desde el editor

    void Start()
    {
        // Encontrar el MusicManager y cambiar la música
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        if (musicManager != null)
        {
            musicManager.ChangeMusic(bossFightMusic);
        }
    }
}
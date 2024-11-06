/*using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;

public class PersonajeRandom : MonoBehaviourPunCallbacks
{
    public List<GameObject> characterPrefabs; // Lista de prefabs de personajes
    private bool characterAssigned = false;   // Para verificar si el personaje ya ha sido asignado
    public Transform spawnPoint;              // Punto de spawn para el personaje

    void Start()
    {
        // Aseguramos que cada cliente asigne un personaje solo una vez
        if (!characterAssigned && PhotonNetwork.IsConnectedAndReady)
        {
            AssignCharacter();
        }
    }

    void AssignCharacter()
    {
        // Solo asignamos un personaje si aún no ha sido asignado
        if (characterAssigned) return;

        // Generamos un índice aleatorio para el personaje
        int characterIndex = Random.Range(0, characterPrefabs.Count);

        // Usamos PhotonNetwork.Instantiate para que todos vean el personaje
        Vector3 spawnPosition = spawnPoint != null ? spawnPoint.position : transform.position;
        Quaternion spawnRotation = spawnPoint != null ? spawnPoint.rotation : transform.rotation;
        GameObject characterInstance = PhotonNetwork.Instantiate(characterPrefabs[characterIndex].name, spawnPosition, spawnRotation);

        Debug.Log("Personaje asignado: " + characterInstance.name);
        characterAssigned = true; // Marcamos como asignado para evitar instancias duplicadas
    }
}
*/

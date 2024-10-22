using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;


public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Se llama al iniciar el script
    private void Start()
    {
        // Conectarse a Photon utilizando los ajustes configurados
        PhotonNetwork.ConnectUsingSettings();
    }

    // Se llama cuando se conecta correctamente al Master
    public override void OnConnectedToMaster()
    {
        // Unirse al lobby de Photon
        PhotonNetwork.JoinLobby();
    }

    // Se llama cuando se une correctamente al lobby
    public override void OnJoinedLobby()
    {
        // Iniciar la corutina para esperar 5 segundos antes de cargar la escena
        StartCoroutine(EsperarYCargarEscena());
    }

    // Corutina para esperar 5 segundos antes de cargar la escena "Lobby"
    IEnumerator EsperarYCargarEscena()
    {
        // Verificar que la conexión a Photon sigue activa antes de proceder
        if (PhotonNetwork.IsConnected)
        {
            // Esperar 5 segundos
            yield return new WaitForSeconds(3);

            // Cargar la escena del lobby después de la espera
            SceneManager.LoadScene("Lobby");
        }
        else
        {
            Debug.LogError("Error: Desconectado de Photon antes de cargar la escena.");
        }
    }

    // Este método se ejecuta en cada frame, pero no se utiliza en este caso
    void Update()
    {
    }
}









/*public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        
        SceneManager.LoadScene("Lobby");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}*/
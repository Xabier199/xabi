using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class SeleccionPersonajes : MonoBehaviour
{
    // Definir los personajes
    private Personaje guerrera;
    private Personaje maga;
    private Personaje ojo;
    private Personaje pingu;

    // Estructura para los personajes
    public struct Personaje
    {
        public string nombre;
        public string clase;
        public int nivel;
        public string descripcion;

        public Personaje(string nombre, string clase, int nivel, string descripcion)
        {
            this.nombre = nombre;
            this.clase = clase;
            this.nivel = nivel;
            this.descripcion = descripcion;
        }

        public string ObtenerDetalles()
        {
            return $"Nombre: {nombre}\nClase: {clase}\nNivel: {nivel}\nDescripción: {descripcion}";
        }
    }

    void Start()
    {
        // Inicializar los personajes
        guerrera = new Personaje("Guerrera", "Tanque", 15, "Un guerrero fuerte y resistente.");
        maga = new Personaje("Maga", "Hechicero", 12, "Un mago que domina los elementos.");
        ojo = new Personaje("Ojo", "Distancia", 10, "Un arquero ágil y preciso.");
        pingu = new Personaje("Pingu", "Corto", 20, "Un animal ágil y preciso.");

        // Mostrar instrucciones iniciales
        Debug.Log("=== Menú de Selección de Personajes ===");
        Debug.Log("Presiona 1 para elegir al Guerrero, 2 para elegir al Mago, 3 para elegir al Arquero.");
    }

    void Update()
    {
        // Detectar la entrada del teclado
        if (Input.GetKeyDown(KeyCode.X))
        {
            SeleccionarPersonaje(guerrera);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            SeleccionarPersonaje(maga);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            SeleccionarPersonaje(ojo);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            SeleccionarPersonaje(pingu);
        }
    }

    // Método para seleccionar un personaje y mostrar sus detalles
    public void SeleccionarPersonaje(Personaje personajeSeleccionado)
    {
        Debug.Log($"Has seleccionado a: {personajeSeleccionado.nombre}");
        Debug.Log(personajeSeleccionado.ObtenerDetalles());
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class Salud : MonoBehaviourPunCallbacks
{
    public SpriteRenderer healthRenderer;     // Referencia al SpriteRenderer que muestra la vida
    public Sprite[] healthSprites;            // Array de sprites que representan los diferentes niveles de vida
    public int maxHealth = 13;                // Vida máxima del personaje
    private int currentHealth;                // Vida actual del personaje
    private Animator animator;
    public PLayerColisionBarbara ScriptMovimiento;
    public Image youlose;

    void Start()
    {
        // Inicializamos la vida en su máximo valor
        currentHealth = maxHealth;
        // Actualizamos el sprite de vida inicial
        UpdateHealthSprite();                 
        animator = GetComponent<Animator>();
        ScriptMovimiento = GetComponent<PLayerColisionBarbara>();
        youlose = GameObject.Find("LoseImage").GetComponent<Image>(); //Pedirle que cuando se ejecute el código busque en la escena una la imágen con el nombre especificado--------------
        youlose.gameObject.SetActive(false);
    }


    private void Update()
    {
        //Mirar si la vida es inferior o igual a 0 y hacer la animación de muerte.
        if (currentHealth <= 0)
        {
            animator.SetBool("IsDead", true);
            ScriptMovimiento.enabled = false;
            
            //Activar la imágen de derrota si has perdido.
            if (photonView.IsMine)
            {
                youlose.gameObject.SetActive(true);
            }
        }

        
    }

    //Llamar a esta función cuando el personaje reciba un golpe
    public void TakeHit()
    {
        if (currentHealth > 0)                //Verificamos que aún quede vida
        {
            currentHealth--;                  //Reducimos la vida en 1 por golpe
            Debug.Log("Current Health: " + currentHealth);
            UpdateHealthSprite();             //Actualizamos el sprite de vida
        }
    }

    //Actualiza el sprite de la barra de vida basado en la vida actual
    void UpdateHealthSprite()
    {
        if (currentHealth >= 0 && currentHealth < healthSprites.Length)
        {
            healthRenderer.sprite = healthSprites[currentHealth];   //Cambiamos el sprite en el renderer
        }
    }

    //Detectar si te has caído del mapa
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Caida")
        {
            currentHealth = 0;
            Destroy(gameObject);
            youlose.gameObject.SetActive(true);

        }
    }




}

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
    public int maxHealth = 13;                 // Vida máxima del personaje
    private int currentHealth;                // Vida actual del personaje
    private Animator animator;
    public PLayerColisionBarbara ScriptMovimiento;
    public Image youlose;

    void Start()
    {
        currentHealth = maxHealth;            // Inicializamos la vida en su máximo valor
        UpdateHealthSprite();                 // Actualizamos el sprite de vida inicial
        animator = GetComponent<Animator>();
        ScriptMovimiento = GetComponent<PLayerColisionBarbara>();
        youlose = GameObject.Find("LoseImage").GetComponent<Image>();
        youlose.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (currentHealth <= 0)
        {
            animator.SetBool("IsDead", true);
            ScriptMovimiento.enabled = false;
            
            if (photonView.IsMine)
            {
                youlose.gameObject.SetActive(true);
            }
        }

        
    }

    // Llamar a esta función cuando el personaje reciba un golpe
    public void TakeHit()
    {
        if (currentHealth > 0)                // Verificamos que aún quede vida
        {
            currentHealth--;                  // Reducimos la vida en 1 por golpe
            Debug.Log("Current Health: " + currentHealth);
            UpdateHealthSprite();             // Actualizamos el sprite de vida
        }
    }

    // Actualiza el sprite de la barra de vida basado en la vida actual
    void UpdateHealthSprite()
    {
        if (currentHealth >= 0 && currentHealth < healthSprites.Length)
        {
            healthRenderer.sprite = healthSprites[currentHealth];   // Cambiamos el sprite en el renderer
        }
    }

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

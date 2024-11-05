using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class Salud : MonoBehaviour
{
    public SpriteRenderer healthRenderer;     // Referencia al SpriteRenderer que muestra la vida
    public Sprite[] healthSprites;            // Array de sprites que representan los diferentes niveles de vida
    public int maxHealth = 5;                 // Vida máxima del personaje
    private int currentHealth;                // Vida actual del personaje
    private Animator animator;
    public PLayerColisionBarbara ScriptMovimiento;

    void Start()
    {
        currentHealth = maxHealth;            // Inicializamos la vida en su máximo valor
        UpdateHealthSprite();                 // Actualizamos el sprite de vida inicial
        animator = GetComponent<Animator>();
        ScriptMovimiento = GetComponent<PLayerColisionBarbara>();
    }


    private void Update()
    {
        if (currentHealth <= 0)
        {
            animator.SetBool("IsDead", true);
            ScriptMovimiento.enabled = false;
            
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

    


}

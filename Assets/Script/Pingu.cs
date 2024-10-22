using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Pingu : MonoBehaviourPunCallbacks
{

    // Variables configurables en el Inspector de Unity
    public float speed = 5f; // Velocidad de movimiento
    public float jumpForce = 7f; // Fuerza de salto
    public bool isGrounded; // Verificar si está en el suelo
    public Transform groundCheck; // Transform para verificar si está tocando el suelo
    public LayerMask groundLayer; // Capa del suelo para la detección

    // Variables de ataque
    public Transform attackPoint; // Punto desde donde se realizará el ataque
    public float attackRange = 0.5f; // Alcance del ataque
    public int attackDamage = 10; // Daño que hace el ataque
    public LayerMask enemyLayers; // Capa de los enemigos para detectar con el ataque
    public float attackRate = 2f; // Velocidad de ataque
    private float nextAttackTime = 0f; // Tiempo entre ataques

    // Variables de salud
    public int maxHealth = 100;
    private int currentHealth;

    private Rigidbody2D rb;
    private Animator anim;
    private bool facingRight = true;

    // Método llamado al iniciar el juego
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth; // Inicia con la salud máxima
    }

    // Método llamado una vez por frame
    void Update()
    {
        Move();
        Jump();

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1")) // El botón de ataque es "Fire1" (normalmente clic izquierdo o Ctrl)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Move()
    {
        // Obtener entrada del jugador en el eje horizontal (flechas o WASD)
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Control de animación de caminar
        anim.SetFloat("Speed", Mathf.Abs(moveInput));

        // Cambiar dirección del personaje si es necesario
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    void Jump()
    {
        // Verificar si el personaje está tocando el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Si presiona espacio y está en el suelo, el personaje salta
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetTrigger("Jump");
        }

        // Configuración de animaciones según si está en el suelo o no
        anim.SetBool("isGrounded", isGrounded);
    }

    // Método para atacar
    void Attack()
    {
        // Reproducir animación de ataque
        anim.SetTrigger("Attack");

        // Detectar enemigos en el rango del ataque
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        /*Aplicar daño a cada enemigo detectado
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<enemy>().TakeDamage(attackDamage); // Suponiendo que el enemigo tiene un script con TakeDamage()
        }*/
    }

    // Método para recibir daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método para morir
    void Die()
    {
        anim.SetBool("isDead", true);
        // Desactivar el jugador o reiniciar la escena
        this.enabled = false;
        // Puedes agregar aquí lógica adicional para cuando el jugador muera.
    }

    // Método para voltear al personaje
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    // Método para mostrar el área de ataque en el Editor de Unity
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
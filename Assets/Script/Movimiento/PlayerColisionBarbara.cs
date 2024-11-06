using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PLayerColisionBarbara : MonoBehaviourPunCallbacks
{
    private Rigidbody2D rb;
    private float horizontal;
    [SerializeField]
    private float jumpforce = 1.0f;
    private bool grounded;
    public float velocidad;
    private Animator animator;
    [SerializeField]
    private int playerViewID;

    private float subiendoAnterior;
    [SerializeField]
    public float margen = 0.001f;  // Umbral para detectar el cambio significativo en Y

    private Collider2D objectCollider; // Para controlar las colisiones

    private Vector2 direction;

    private Salud Salud;

    public float cooldownTime = 0.5f; // Tiempo de cooldown en segundos
    private bool canBeHit = true; // Variable para controlar el cooldown


    private const string HIT_PARAM = "IsHit";
    private const string PLAYER_HITBOX_TAG = "BarbaraHit";

    // Definimos una variable para el combo
    private int ataqueCombo = 0;
    // Definimos el tiempo en segundos para reiniciar el combo
    private float tiempoReinicioCombo = 0.8f;
    // Variable para guardar el tiempo del último ataque
    private float tiempoUltimoAtaque;




    void Start()//Coger el RigidBody del objeto al que esté asignado el script
    {
        subiendoAnterior = transform.position.y;
        objectCollider = gameObject.GetComponent<Collider2D>();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Salud = GetComponent<Salud>();
    }

    void Update()
    {



        if (photonView.IsMine)
        {
            //Girar al jugador
            if (horizontal < 0)
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            else if (horizontal > 0)
                transform.localRotation = Quaternion.Euler(0f, 0f, 0f);


            if (horizontal < 0)
            {
                animator.SetBool("Running", true);
            }
            else if (horizontal > 0)

            {
                animator.SetBool("Running", true);
            }
            else if(horizontal == 0)
            {
                animator.SetBool("Running", false);
            }


                horizontal = Input.GetAxis("Horizontal");  // Coger el input del teclado, con valores del -1 al 1


            Debug.DrawRay(transform.position, Vector2.down * 2.5f, Color.red);
            RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, 2.5f);
            if (hitGround)
            {
                animator.SetBool("EnSuelo", true);
                grounded = true;
            }
            else
            {
                animator.SetBool("EnSuelo", false);
                grounded = false;
            }

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && grounded)//GetkeyDown quiere decir cuando presionas una tecla, en este caso con KeyCode hemos puesto el espacio
            {
                Jump();
            }

            if ((Input.GetKeyDown(KeyCode.Z)) && grounded)
            {
                Ataque();
            }

            if (Time.time - tiempoUltimoAtaque > tiempoReinicioCombo)
            {
                ataqueCombo = 0;
                Reseteo();
            }

            float subiendoActual = transform.position.y;

         


            // Comparamos las posiciones con un umbral para evitar problemas de precisión
            if (subiendoActual > subiendoAnterior + margen)
            {
                if (objectCollider.enabled) // Verifica si el collider está activo
                {
                    objectCollider.enabled = false; // Desactiva el collider (sin colisiones)
                    animator.SetBool("Subiendo", true);
                }
            }
            else if (subiendoActual < subiendoAnterior - margen)
            {
                if (!objectCollider.enabled) // Verifica si el collider está desactivado
                {
                    objectCollider.enabled = true; // Activa el collider (colisiones activas)
                    animator.SetBool("Subiendo", false);
                }
            }

            // Actualizar la posición anterior solo después de la comparación
            subiendoAnterior = subiendoActual;

            

           
        }




    }
    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            rb.velocity = new Vector2(horizontal * velocidad, rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (photonView.IsMine)
        {
            rb.AddForce(new Vector2(0, jumpforce));
        }
    }

    private void Ataque()
    {
        if (photonView.IsMine)
        {
            tiempoUltimoAtaque = Time.time;
            
            if (ataqueCombo == 0)
            {
                animator.SetBool("Ataque1", true);
                animator.SetBool("Ataque2", false);
                animator.SetBool("Ataque3", false);
            }
            else if (ataqueCombo == 1)
            {
                animator.SetBool("Ataque1", false);
                animator.SetBool("Ataque2", true);
                animator.SetBool("Ataque3", false);
            }
            else if (ataqueCombo == 2)
            {
                animator.SetBool("Ataque1", false);
                animator.SetBool("Ataque2", false);
                animator.SetBool("Ataque3", true);
            }

            ataqueCombo = (ataqueCombo + 1) % 3;
        }
    }

    private void Reseteo()
    {
        animator.SetBool("Ataque1", false);
        animator.SetBool("Ataque2", false);
        animator.SetBool("Ataque3", false);
    }





    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == PLAYER_HITBOX_TAG && canBeHit)
        {
            animator.SetTrigger(HIT_PARAM);
            Salud.TakeHit();
            StartCoroutine(StartCooldown());
        }
    }

    private IEnumerator StartCooldown()
    {
        canBeHit = false;
        yield return new WaitForSeconds(cooldownTime); // Espera el tiempo de cooldown
        canBeHit = true;
    }
}

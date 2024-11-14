using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PLayerColisionBarbara : MonoBehaviourPunCallbacks
{
    //Movimiento lateral----------------------------------------------------------------------------------------------------------------------
    private Rigidbody2D rb;
    private float horizontal;
    public float velocidad;
    //Estado corriendo
    private bool isMoving = false;
    //Salto-----------------------------------------------------------------------------------------------------------------------------------
    [SerializeField]
    private float jumpforce = 1.0f;
    private bool grounded;
    
    //Coger el animator-----------------------------------------------------------------------------------------------------------------------
    
    private Animator animator;

    //Detectar si el personaje está subiendo o bajando para activar/desactivar las colisiones-------------------------------------------------
    private float subiendoAnterior;
    [SerializeField]
    public float margen = 0.1f;  // Umbral para detectar el cambio significativo en Y (Para que detecte bien si baja o sube en Y)

    private Collider2D objectCollider; // Para controlar las colisiones


    //Coger el script "Salud" asignado al GameObject en el que esté este script
    private Salud Salud;

    //Ataque-----------------------------------------------------------------------------------------------------------------------------------
    public float cooldownTime = 0.5f; // Cooldown entre golpes
    private bool canBeHit = true; // Variable para controlar el cooldown para poder volver a ser golpeado
    private bool puedemoverse = true; //Restricciones para el movimiento
    public float cooldownTime2 = 1.5f; //Cooldown después del tercer ataque
    public float cooldownTime3 = 1.2f; //Cooldown para moverse después de ser golpeado

    //Detectar que has sido golpeado por una hitbox de ataque
    private const string HIT_PARAM = "IsHit";
    private const string PLAYER_HITBOX_TAG = "BarbaraHit";
    //Variable para el combo
    private int ataqueCombo = 0;
    //Tiempo en segundos para reiniciar el combo
    public float tiempoReinicioCombo = 0.8f;
    //Variable para guardar el tiempo del último ataque
    private float tiempoUltimoAtaque;

    //Cooldown pulsación Z
    public float cdduracion = 1.0f;
    private bool puedeatacar = true;
    private bool puedeatacar2 = true; //Puede atacar después del tercer ataque
    private float cdtemporizador = 0.0f;

    //Estado de ataque
    private bool isAnimating = false;
    
    

    //límite salto
    public float maxJumpSpeed = 15000f;




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

            //Movimiento lateral----------------------------------------------------------------------------------------------------------
            //Girar al jugador
            if (horizontal < 0 && !isAnimating && puedemoverse)
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            else if (horizontal > 0 && !isAnimating && puedemoverse)
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

            //Detectar que esté tocando el suelo o no para que no pueda saltar en el aire--------------------------------------------------------
            Debug.DrawRay(transform.position, Vector2.down * 2.1f, Color.red);
            RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, 2.1f);
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


            //Usar la función de salto cuando se pulsan las teclas correspondientes y se cumplan las condiciones--------------------------------------------------------------------------
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && grounded && puedeatacar && puedemoverse)//GetkeyDown quiere decir cuando presionas una tecla, en este caso con KeyCode hemos puesto el espacio
            {
                Jump();
            }

            //Empezar el cooldown para la pulsación de Z
            if (!puedeatacar) 
            { 
                cdtemporizador -= Time.deltaTime; if (cdtemporizador <= 0) { puedeatacar = true; } 
            }

            //Usar la función de ataque cuando se pulsa la tecla correspondiente y se cumplan las condiciones-----------------------------------------------------------------------------
            if (puedeatacar && puedeatacar2 && Input.GetKeyDown(KeyCode.Z) && grounded)
            {
                Ataque(); 
                puedeatacar = false; 
                cdtemporizador = cdduracion; // Reinicia el cooldown timer
            }

            //Usar la función de reiniciar el combo
            if (Time.time - tiempoUltimoAtaque > tiempoReinicioCombo)
            {
                ataqueCombo = 0;
                Reseteo();
            }

            
            
            
            //Activar/desactivar la colisión del personaje para que atraviese las plataformas cuando sube, y se pose sobre ellas al bajar--------------------------------------
            
            float subiendoActual = transform.position.y;

            // Comparamos las posiciones con un umbral para evitar problemas de precisión
            if (subiendoActual > subiendoAnterior + margen) //Si está subiendo
            {
                if (objectCollider.enabled && !grounded) //Verifica si el collider está activo
                {
                    objectCollider.enabled = false; //Desactiva el collider (sin colisiones)
                    animator.SetBool("Subiendo", true);
                }
            }
            else if (subiendoActual < subiendoAnterior - margen && grounded) //Si está bajando
            {
                if (!objectCollider.enabled) //Verifica si el collider está desactivado
                {
                    objectCollider.enabled = true; //Activa el collider (colisiones activas)
                    animator.SetBool("Subiendo", false);
                }
            }

            // Actualizar la posición anterior solo después de la comparación
            subiendoAnterior = subiendoActual;

            

           
        }

     


    }
    
    //Movimiento lateral----------------------------------------------------------------------------------------------------
    private void FixedUpdate()
    {
        if (photonView.IsMine && !isAnimating && puedemoverse)
        {
            
            rb.velocity = new Vector2(horizontal * velocidad, rb.velocity.y);
        }
    }
    //Definimos la función de salto------------------------------------------------------------------------------------------------------------------
    private void Jump()
    {
        if (photonView.IsMine)
        {
            rb.AddForce(new Vector2(0, jumpforce));

            if (rb.velocity.y > maxJumpSpeed) 
            {    
                rb.velocity = new Vector2(rb.velocity.x, maxJumpSpeed);
            }
        }
    }
    //Definimos la función de ataque--------------------------------------------------------------------------------------------------------------------
    private void Ataque()
    {
        if (photonView.IsMine)
        {
            isAnimating = true;
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
                StartCoroutine(StartCooldown2());
            }
            
            ataqueCombo = (ataqueCombo + 1) % 3;

        }
    }
    //Reinicio del combo de atques
    private void Reseteo()
    {
        animator.SetBool("Ataque1", false);
        animator.SetBool("Ataque2", false);
        animator.SetBool("Ataque3", false);
        isAnimating = false;
    }


    //Función para detectar la colisión de golpe----------------------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == PLAYER_HITBOX_TAG && canBeHit)
        {
            animator.SetTrigger(HIT_PARAM);
            Salud.TakeHit();
            Reseteo();
            StartCoroutine(StartCooldown());
            StartCoroutine(StartCooldown3());

        }
    }
    //Función de Cooldown para poder ser golpeado otra vez------------------------------------------------------------------------
    private IEnumerator StartCooldown()
    {

        canBeHit = false;
        yield return new WaitForSeconds(cooldownTime); // Espera el tiempo de cooldown
        canBeHit = true;
    }
    //Cooldown después del tercer ataque----------------------------------------------------------------------------------------
    private IEnumerator StartCooldown2() 
    {
        puedeatacar2 = false;
        yield return new WaitForSeconds(cooldownTime2); // Espera el tiempo de cooldown
        puedeatacar2 = true;
    }
    //
    private IEnumerator StartCooldown3() //Cooldown después del tercer ataque
    {
        puedemoverse = false;
        yield return new WaitForSeconds(cooldownTime3); // Espera el tiempo de cooldown
        puedemoverse = true;
    }
}

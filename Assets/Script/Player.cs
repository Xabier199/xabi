using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PLayerMovement : MonoBehaviourPunCallbacks
{
    private Rigidbody2D rb;
    private float horizontal;
    [SerializeField]
    private float jumpforce = 1.0f;
    private bool grounded;
    public float velocidad;
    private Animator animator;


    void Start()//Coger el RigidBody del objeto al que esté asignado el script
    {

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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


            horizontal = Input.GetAxis("Horizontal");// Coger el input del teclado, con valores del -1 al 1


            Debug.DrawRay(transform.position, Vector2.down * 2.5f, Color.red);
            RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, 2.5f);
            if (hitGround) grounded = true;
            else grounded = false;

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && grounded)//GetkeyDown quiere decir cuando presionas una tecla, en este caso con KeyCode hemos puesto el espacio
            {
                Jump();
            }

        }




    }
    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            rb.velocity = new Vector2(horizontal*velocidad, rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (photonView.IsMine)
        {
            rb.AddForce(new Vector2(0, jumpforce));
        }
    }
}

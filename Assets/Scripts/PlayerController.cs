using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Propiedad que nos ayuda como singleton para modificar las animaciones del conejito
    public static PlayerController sharedInstance;

    public float jumpForce = 25.0f;
    public float runningSpeed = 4.0f;
    private new Rigidbody2D rigidbody;
    public LayerMask groundLayerMask;
    public Animator animator;

    private Vector3 startPosition;//variable donde alamacenamos la posicion inicial del conejito
    private string highScoreKey = "highScore";

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        sharedInstance = this;
        startPosition = this.transform.position;
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);
    }
    //
    public void StartGame()
    {
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);
        this.transform.position = startPosition;
        rigidbody.velocity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inTheGame)//Preguntamos el estado del juego si no esta en menu
        {
            if (Input.GetMouseButtonDown(0))
                Jump();

            animator.SetBool("isGrounded", IsOnTheFloor()); //Aquí activamos la animacion de correr del conejito
        }
    }
    private void FixedUpdate() //Tiene un tiempo fijo. Un bueno lugar para colocar fuerzas constantes a la física. Aquí hacemos que el conejito obtenga una velocidad constante
    {
        /*
         * Recordando que este metodo se activa por un tiempo determinado.
         * Le preguntamos si esta en el estado de 'InTheGame' si no, entonces no obtendra su velocidad para que el conejito se mueva
         */
        if(GameManager.sharedInstance.currentGameState == GameState.inTheGame)
            if (rigidbody.velocity.x < runningSpeed)
                rigidbody.velocity = new Vector2(runningSpeed, rigidbody.velocity.y);
    }

    void Jump()
    {
        if (IsOnTheFloor())
            rigidbody.AddForce(Vector2.up * jumpForce , ForceMode2D.Impulse);
    }

    bool IsOnTheFloor()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 1.0f, groundLayerMask.value))
            return true;
        else
            return false;
    }
    //Metodo para matar al conejito
    public void KillPlayer()
    {
        GameManager.sharedInstance.GameOver();
        animator.SetBool("isAlive", false);

        if (PlayerPrefs.GetFloat(highScoreKey, 0) < this.GetDistance())
            PlayerPrefs.SetFloat(highScoreKey, this.GetDistance());
    }

    public float GetDistance()
    {
        float distanceTravelled = Vector2.Distance(new Vector2(startPosition.x, 0), new Vector2(this.transform.position.x, 0));
        return distanceTravelled;
    }
}

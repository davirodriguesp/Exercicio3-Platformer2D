using UnityEngine;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Start  ris called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D rb;
    private float xDir;
    private Animator playerSpriteAnimator;
    [SerializeField] float xSpeed;
    [SerializeField] float jumpForce = 5f; 
    private int jumpCount = 0; // Quantos pulos já foram dados no ar
    private int maxJumps = 2; // Limite para o pulo duplo
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSpriteAnimator = GetComponentInChildren<Animator>();
    }
    private void FixedUpdate()
    {
        Movimentar();
    }
    
    void OnMove(InputValue inputValue)
    {
        xDir = inputValue.Get<Vector2>().x;
    }
    void OnJump(InputValue value)
    {
        // Se a tecla foi pressionada e o limite de 2 pulos não foi atingido
        if (value.isPressed && jumpCount < maxJumps)
        {
            // Aplica a força de pulo no eixo Y 
            rb.linearVelocityY = jumpForce;
            
            // Registra que um pulo foi gasto
            jumpCount++;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Zera o contador de pulos para permitir pular novamente ao tocar no cenário
        jumpCount = 0;
    }

    void Movimentar()
    {
        rb.linearVelocityX = xDir * xSpeed;
        bool IsRunning = Mathf.Abs(rb.linearVelocityX) > Mathf.Epsilon;
        playerSpriteAnimator.SetBool("IsRunning", IsRunning);
        if (IsRunning)
        {
            FlipSprite();
        }
    }

    void FlipSprite()
    {
        transform.localScale = new Vector3(Mathf.Sign(rb.linearVelocityX), 1, 1);
    }
    
}



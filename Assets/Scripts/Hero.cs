using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f; // Скорость движения
    [SerializeField] private int health = 5; // Текущее здоровье
    [SerializeField] private float jumpForce = 10f; // Сила прыжка

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite activeHeart;
    [SerializeField] private Sprite emptyHeart;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private CapsuleCollider2D collider;
    private bool isGrounded = false;
    private float heroHeight;
    private float heroWidth;
    private int lives;
    public Joystick joystick;

    private States State
    {
        get { return (States)animator.GetInteger("state"); }
        set { animator.SetInteger("state", (int) value); }
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        collider = GetComponentInChildren<CapsuleCollider2D>();
        heroWidth = collider.size.x;
        heroHeight = collider.size.y;
        lives = health;
    }

    private void FixedUpdate() {
        CheckGround();
    }

    // Update is called once per frame
    private  void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            lives = health;
        }
        if(lives <= 0) {
            State = States.death;
            return;
        }
        if (isGrounded) {
            State = States.idle;
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            lives--;
        }
        if (joystick.Horizontal != 0) { // Бег
            Run();
        }
        if (isGrounded && joystick.Vertical > 0.5f) {
            Jump();
        }
        
        for (int i = 0; i < hearts.Length; i++) {
            if(i < lives) {
                hearts[i].sprite = activeHeart;
                hearts[i].enabled = true;
            } else {
                hearts[i].sprite = emptyHeart;
            }
        } 
    }

    private void Run()
    {
        if (isGrounded) {
            State = States.run;
        }
        
        Vector3 dir = transform.right * joystick.Horizontal;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump() {
        rb.velocity = Vector2.up * jumpForce;
    }

    private void CheckGround() {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        isGrounded = collider.Length > 1;

        if (!isGrounded) {
            State = States.jump;
        }
    }

    private void Squat(bool down) {
        if (down) {
            collider.size = new Vector2(heroWidth, heroHeight / 2);
        } else {
            collider.size = new Vector2(heroWidth, heroHeight);
        }
    }
}

public enum States {
    idle,
    jump,
    run,
    death
}


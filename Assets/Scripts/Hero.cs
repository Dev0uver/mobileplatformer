using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 3;
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private CapsuleCollider2D collider;
    private bool isGrounded = false;
    private float heroHeight;
    private float heroWidth;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        collider = GetComponentInChildren<CapsuleCollider2D>();
        heroWidth = collider.size.x;
        heroHeight = collider.size.y;
    }

    private void FixedUpdate() {
        CheckGround();
    }

    // Update is called once per frame
    private  void Update()
    {
        // Бег
        if (Input.GetButton("Horizontal")) {
            Run();
        }
        // Приседания
        if (Input.GetButton("Vertical")) {
            Squat(true);
        } else {
            Squat(false);
        }
        // Прыжки
        if (isGrounded && Input.GetButtonDown("Jump")) {
            Jump();
        }
    }

    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump() {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround() {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
    }

    private void Squat(bool down) {
        if (down) {
            collider.size = new Vector2(heroWidth, heroHeight / 2);
        } else {
            collider.size = new Vector2(heroWidth, heroHeight);
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;

    private SpriteRenderer spriteRenderer;
    private int fruitId;
    private bool disappear = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        System.Random rnd = new System.Random();
        fruitId  = rnd.Next(1, 9);
        animator.SetInteger("state", fruitId);
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetInteger("state") == 0) {
            return;
        }
        animator.SetInteger("state", fruitId);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player") && !disappear) {
            disappear = true;
            Score.score += 1;
            animator.SetInteger("state", 0);
            spriteRenderer.sprite = null;
        }
    }
}

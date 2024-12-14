using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [SerializeField] private bool isMoving;
    [SerializeField] private float MovementDistanceUp;
    [SerializeField] private float MovementDistanceDown;
    [SerializeField] private float MovementSpeed;
    private bool movingUp;
    private float upEdge;
    private float downEdge;

    private void Awake()
    {
        upEdge = transform.position.y + MovementDistanceUp;
        downEdge = transform.position.y - MovementDistanceDown;
    }

    private void Update()
    {
        if (isMoving)
        {
            if (movingUp)
            {
                if (transform.position.y < upEdge)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + MovementSpeed * Time.deltaTime, transform.position.z);
                }
                else
                {
                    movingUp = false;
                }
            } 
            else 
            {
                if (transform.position.y > downEdge)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - MovementSpeed * Time.deltaTime, transform.position.z);
                }
                else
                {
                    movingUp = true;
                }
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Hero>().TakeDamage();
        }
    }
}

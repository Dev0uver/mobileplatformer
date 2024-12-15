using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public static bool isClosed = true;

    private UIManager uiManager;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void OpenFinish()
    {
        isClosed = false;
        State = States.open;
    }

    public void SetOpened()
    {
        State = States.opened;
    }


    private States State
    {
        get { return (States)animator.GetInteger("state"); }
        set { animator.SetInteger("state", (int)value); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isClosed)
        {
            uiManager.LevelComplete();
        }
    }

    public enum States
    {
        closed,
        open,
        opened
    }
}

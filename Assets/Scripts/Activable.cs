using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Activable : MonoBehaviour
{
    private Animator animator;

    public bool isUsable;
    public bool powered;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        isUsable = false;
        powered = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Action") && isUsable)
        {
            powered = !powered;
            if (animator != null)
            {
                if (powered)
                    animator.SetTrigger("enable");
                else
                    animator.SetTrigger("disable");
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            isUsable = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            isUsable = false;
    }

}

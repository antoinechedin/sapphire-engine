using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Activable : MonoBehaviour
{
    public Animator animator;

    public Trapdoor[] trapdoors;

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
                {
                    animator.SetTrigger("enable");
                    if (trapdoors != null)
                        foreach (Trapdoor trapdoor in trapdoors)
                            trapdoor.Closed();
                }
                else
                {
                    animator.SetTrigger("disable");
                    if (trapdoors != null)
                        foreach (Trapdoor trapdoor in trapdoors)
                            trapdoor.Open();
                }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Collider2D))]
public class Trapdoor : MonoBehaviour
{
    private Animator animator;
    private Collider2D c2D;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        c2D = GetComponent<Collider2D>();
    }

    public void Open()
    {
        animator.SetTrigger("open");
    }

    public void Closed()
    {
        animator.SetTrigger("close");
    }

    private void EnableCollider()
    {
        c2D.enabled = true;
    }
    private void DisableCollider()
    {
        c2D.enabled = false;
    }
}

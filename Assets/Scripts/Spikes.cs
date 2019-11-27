using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            ActorPlayer player = other.GetComponent<ActorPlayer>();
            player.Die();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeObject : MonoBehaviour
{
    public bool isRewinding = false;

    private List<Instant> history;

    private void Awake()
    {
        history = new List<Instant>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRewinding = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            isRewinding = false;
    }

    private void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    private void Record()
    {
        history.Insert(0, new Instant(transform.position, GetComponent<ActorPlayer>().velocity));
    }

    private void Rewind()
    {
        transform.position = history[0].position;
        GetComponent<ActorPlayer>().velocity = history[0].velocity;
        history.RemoveAt(0);
    }

    public struct Instant
    {
        public Vector3 position;
        public Vector3 velocity;

        public Instant(Vector3 position, Vector3 velocity)
        {
            this.position = position;
            this.velocity = velocity;
        }
    }
}

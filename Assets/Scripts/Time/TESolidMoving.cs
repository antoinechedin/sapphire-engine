using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SolidMoving))]
public class TESolidMoving : TimeEntity
{
    private SolidMoving solidMoving;
    private Stack<Instant> history;

    private void Awake()
    {
        solidMoving = GetComponent<SolidMoving>();
        history = new Stack<Instant>();
    }

    public override void Record()
    {
        Instant instant = new Instant(solidMoving.transform.position);
        history.Push(instant);
    }

    public override void Rewind()
    {
        Instant instant;
        if (history.Count > 1)
        {
            instant = history.Pop();
        }
        else
        {
            instant = history.Peek();
        }
        solidMoving.transform.position = instant.position;
    }

    private struct Instant
    {
        public Vector2 position;

        public Instant(Vector2 position)
        {
            this.position = position;
        }
    }
}

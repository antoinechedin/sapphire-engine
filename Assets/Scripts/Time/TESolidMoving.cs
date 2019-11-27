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
        Instant instant = new Instant(solidMoving.timer);
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
        solidMoving.timer = instant.timer;
        solidMoving.transform.position = solidMoving.GetTargetPosition(instant.timer);
    }

    private struct Instant
    {
        public float timer;

        public Instant(float timer)
        {
            this.timer = timer;
        }
    }
}

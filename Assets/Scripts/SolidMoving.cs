﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidMoving : Solid
{
    public float duration;
    private Vector3 start;
    private Vector3 end;
    public float timer;
    public bool checkCollision;

    protected override void Awake()
    {
        start = transform.GetChild(0).position;
        end = transform.GetChild(1).position;
        checkCollision = true;
        base.Awake();
    }

    public Vector3 GetTargetPosition(float timer)
    {
        if (timer < duration / 2f)
            return Vector3.Lerp(start, end, timer * 2f / duration);
        else
            return Vector3.Lerp(end, start, timer * 2f / duration - 1);
    }

    private void Update()
    {
        if (!TimeManager.Instance.rewind)
        {            
            Move(GetTargetPosition(timer) - transform.position);  

            timer += Time.deltaTime;
            timer %= duration;
        }
    }




}

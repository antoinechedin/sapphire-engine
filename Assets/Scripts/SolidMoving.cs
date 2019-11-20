using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidMoving : Solid
{
    public float duration;
    private Vector3 start;
    private Vector3 end;
    private float timer;

    protected override void Awake()
    {
        start = transform.GetChild(0).position;
        end = transform.GetChild(1).position;
        base.Awake();
    }

    private void Update()
    {
        if (timer < duration / 2f)
        {
            Vector3 targetPos = Vector3.Lerp(start, end, timer * 2f / duration);
            Move(targetPos - transform.position);
        }
        else
        {
            Vector3 targetPos = Vector3.Lerp(end, start, timer * 2f / duration - 1);
            Move(targetPos - transform.position);
        }


        timer += Time.deltaTime;
        timer %= duration;
    }




}

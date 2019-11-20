using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Solid))]
public class SolidMoving : MonoBehaviour
{
    private Solid solid;

    public float duration;
    private Vector3 start;
    private Vector3 end;
    private float timer;

    private void Awake()
    {
        solid = GetComponent<Solid>();
        start = transform.GetChild(0).position;
        end = transform.GetChild(1).position;
    }


    private void Update()
    {
        if (timer < duration / 2f)
        {
            Vector3 targetPos = Vector3.Lerp(start, end, timer * 2f / duration);
            solid.Move(targetPos - transform.position);
        }
        else
        {
            Vector3 targetPos = Vector3.Lerp(end, start, timer * 2f / duration - 1);
            solid.Move(targetPos - transform.position);
        }


        timer += Time.deltaTime;
        timer %= duration;
    }




}

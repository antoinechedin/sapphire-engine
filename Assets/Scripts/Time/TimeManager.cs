using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    public bool rewind;
    public TimeEntity[] timeEntities;

    private void Awake()
    {
        rewind = false;
    }

    private void Start()
    {
        timeEntities = GameObject.FindObjectsOfType<TimeEntity>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            rewind = true;

        if (Input.GetKeyUp(KeyCode.LeftShift))
            rewind = false;
    }

    private void FixedUpdate()
    {
        if (rewind)
            foreach (TimeEntity timeEntity in timeEntities)
                timeEntity.Rewind();
        else
            foreach (TimeEntity timeEntity in timeEntities)
                timeEntity.Record();
    }
}

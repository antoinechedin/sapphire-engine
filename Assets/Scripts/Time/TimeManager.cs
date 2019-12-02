using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    public bool rewind;

    public TimeEntity[] timeEntities;

    private void Start() {
        timeEntities = GameObject.FindObjectsOfType<TimeEntity>();
    }

    private void Update()
    {
        rewind = Input.GetButton("Rewind");
    }

    private void FixedUpdate()
    {
        if (rewind)
            foreach (TimeEntity entity in timeEntities)
            {
                entity.Rewind();
            }
        else
            foreach (TimeEntity entity in timeEntities)
            {
                entity.Record();
            }
    }
}

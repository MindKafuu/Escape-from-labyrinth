using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerText : MonoBehaviour {

    public bool isStart;
    public float CountingTime { get; private set; }

    private void Start()
    {
        CountingTime = 4;
    }

    void Update()
    {
        if (isStart)
            CountingTime -= Time.deltaTime;
    }

    public void StartTimer()
    {
        isStart = true;
    }

    public void StopTimer()
    {
        isStart = false;
        CountingTime = 4;
    }
}

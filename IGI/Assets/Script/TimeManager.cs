using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public float duration;
    public TextMeshProUGUI timer;
    private float minute = 0;
    public Spawner[] spawner;
    public TowerBuilder[] builder;
    public GameObject Barrier;
    public TextMeshProUGUI nextTimer;

    // Update is called once per frame
    void FixedUpdate()
    {
        duration += Time.deltaTime;
    }

    private void Update()
    {
        if(duration > 59)
        {
            duration = 0;
            minute++;
        }
        timer.text = minute.ToString() + ":" + duration.ToString("00");
        if(minute >= 15)
        {
            for(int i = 0; i < 5; i++)
            {
                spawner[i].spawnInterval = 1;
            }
        }
        else if (minute >= 12)
        {
            spawner[4].ActivateSpawner();
            nextTimer.text = "Overtime at 15:00";
        }
        else if (minute >= 9)
        {
            spawner[3].ActivateSpawner();
            builder[4].gameObject.SetActive(true);
            nextTimer.text = "Next phase at 12:00";
        }
        else if(minute >= 6)
        {
            for (int i = 0; i < 5; i++)
            {
                spawner[i].spawnInterval = 3;
            }
            spawner[2].ActivateSpawner();
            builder[3].gameObject.SetActive(true);
            nextTimer.text = "Next phase at 9:00";
        }
        else if (minute >= 3)
        {
            spawner[1].ActivateSpawner();
            builder[2].gameObject.SetActive(true);
            Barrier.SetActive(false);
            nextTimer.text = "Next phase at 6:00";
        }
        else if (minute >= 1)
        {
            spawner[0].ActivateSpawner();
            builder[1].gameObject.SetActive(true);
            nextTimer.text = "Next phase at 3:00";
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                spawner[i].spawnInterval = 5;
            }
        }
    }
}

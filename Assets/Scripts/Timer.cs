using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private int sec = 0;
    private int min = 0;
    private Text timerText;

    void Start()
    {
        timerText = GameObject.Find("Timer").GetComponent<Text>();
        StartCoroutine(TimerWork());
    }

    IEnumerator TimerWork()
    {
        while(true)
        {
            sec++;
            if(sec == 60)
            {
                min++;
                sec = 0;
            }
            timerText.text = min.ToString("D2") + " : " + sec.ToString("D2");
            yield return new WaitForSeconds(1);
        }
    }
}

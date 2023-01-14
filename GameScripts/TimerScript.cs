using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerScript : MonoBehaviour
{




    [SerializeField] public TMP_Text _timerText;
    private int min;
    private int sec= -1;
    private int milsec = 0;
    private int milsec2;
    private int prom;

    private void Start()
    {

        StartCoroutine("TimeFlow");
    }
    IEnumerator TimeFlow()
    {

        while (true)
        {
            sec++;
            milsec = 0;
            prom = 0;
            if(sec == 60)
            {
                sec = 0;
                min += 1;
            }
            yield return new WaitForSeconds(1);
        }
        
    }

    private void FixedUpdate()
    {
        milsec2 = Random.Range(0, 9); 
        
        prom++;

        if (prom == 5)
        {
            prom = 0;
            if(milsec!=9)
                milsec++;
        }
        if (sec < 10)
        {
            _timerText.text = "0" + min + ":0" + sec + ":"+ milsec+milsec2;
        }
        else
            _timerText.text = "0" + min + ":" + sec + ":"+ milsec + milsec2;


    }
}

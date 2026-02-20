using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField] TMPro.TMP_Text text;
    private int timeElapsed;

    private void Start()
    {
        timeElapsed = 0;
        StartCoroutine(PlayTimer());
    }
    IEnumerator PlayTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            timeElapsed++;
            UpdateTimeDisplay();
        }
    }
    void UpdateTimeDisplay()
    {
      
        int minutes = timeElapsed / 60;
        int seconds = timeElapsed % 60;

     
        text.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
}

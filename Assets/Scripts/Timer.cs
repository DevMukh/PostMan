using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component
    float elapsedTime;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime; // Increment elapsed time
        UpdateTimerText(); // Update the displayed timer text
    }

    void UpdateTimerText()
    {
        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        //can also write name or email instaed of this  NameText.text="AmeerMukhtar";
        //EmailText.text="ameermukhtar998@gmail.com";
        // Format the time as mm:ss
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

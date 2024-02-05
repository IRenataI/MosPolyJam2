using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeLabel;

    private string ToTimeFormat(int seconds)
    {
        int minutes = seconds / 60;
        string timeString = (minutes < 10 ? "0" + minutes.ToString() : minutes.ToString()) + ":";
        int onlySeconds = seconds % 60;
        
        timeString += onlySeconds < 10 ? "0" + onlySeconds : onlySeconds;

        return timeString;
    }

    public void SetTime(float seconds)
    {
        timeLabel.text = ToTimeFormat(Mathf.CeilToInt(seconds));
    }
}
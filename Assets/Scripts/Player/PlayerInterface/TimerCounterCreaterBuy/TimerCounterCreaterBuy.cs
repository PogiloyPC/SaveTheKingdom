using UnityEngine;
using UnityEngine.UI;

public class TimerCounterCreaterBuy : MonoBehaviour
{
    [SerializeField] private Image _image;    
    
    public void DisplayTimerCounterCreaterBuy(float currentTime, float finishTime, bool on)
    {
        _image.enabled = on;

        _image.fillAmount = currentTime / finishTime;
    }
}

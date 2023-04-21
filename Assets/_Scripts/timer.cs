
using UnityEngine;
using UnityEngine.UI;
public class timer : MonoBehaviour
{
    public float timeLeft = 120.0f;
    public Text time;
    [SerializeField]GameObject losepanel;
    void Update()
    {
        timeLeft -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeLeft / 60F);
        int seconds = Mathf.FloorToInt(timeLeft - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
        time.text= niceTime;
        if(timeLeft<=0f)
        {
            Time.timeScale = 0f;
            FindObjectOfType<AudioManager>().Play("Losing");
            losepanel.SetActive(true);
        }
    }

}

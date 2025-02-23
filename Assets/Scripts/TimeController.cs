using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public Slider timeSlider; // ʱ���Ử��
    public Button pauseButton; // ��ͣ��ť
    private bool isPaused = false;
    private float previousTimeScale = 1f;
    // Start is called before the first frame update
    void Start()
    {
        // ���û���ĳ�ʼֵ
        if(timeSlider != null)
        {
            timeSlider.value = 1f;
            // ��ӻ���ı�ʱ�ļ���
            timeSlider.onValueChanged.AddListener(OnTimeScaleChanged);
        }
        // �������ͣ��ť����ӵ���¼�
        if(pauseButton != null)
        {
            pauseButton.onClick.AddListener(TogglePause);
        }
    }

    void OnTimeScaleChanged(float value)
    {
        if (!isPaused)
        {
            Time.timeScale = value;
            previousTimeScale = value;
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = previousTimeScale;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public Slider timeSlider; // 时间轴滑块
    public Button pauseButton; // 暂停按钮
    private bool isPaused = false;
    private float previousTimeScale = 1f;
    // Start is called before the first frame update
    void Start()
    {
        // 设置滑块的初始值
        if(timeSlider != null)
        {
            timeSlider.value = 1f;
            // 添加滑块改变时的监听
            timeSlider.onValueChanged.AddListener(OnTimeScaleChanged);
        }
        // 如果有暂停按钮，添加点击事件
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

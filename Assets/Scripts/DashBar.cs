using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    public void SetMax(float value)
    {
        slider.maxValue = value;
    }
    public void SetValue(float value)
    {
        value = Mathf.Clamp(value, 0f, 3f);
        slider.value = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;



public class CanBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public void UpdateCanBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
}

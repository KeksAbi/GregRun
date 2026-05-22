using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorBar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxArmor(float armor)
    {
        slider.maxValue = armor;
        slider.value = armor;
    }

    public void SetArmor(float armor)
    {
        slider.value = armor;
    }

}

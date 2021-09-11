using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Slider slider;
    public Gradient gradient;
    public Image barFill;

    public void SetMaxHealth(float health) {
        slider.maxValue = health;
        slider.value = health;

        barFill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health) {
        slider.value = health;
        barFill.color = gradient.Evaluate(slider.normalizedValue);
    }
}

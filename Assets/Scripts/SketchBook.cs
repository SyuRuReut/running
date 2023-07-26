using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SketchBook : Player
{
    void Start()
    {
        slider.maxValue += 30f;
        slider.value = slider.maxValue;
        Hp = slider.value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Element", menuName = "ScriptableObjects/ElementBar", order = 1)]
public class ElementBar : ScriptableObject
{
    public int maxValue, minBorder, maxBorder;
    public FloatVariable monitoredTotal;

    //[HideInInspector] public int currentTotal;


    public void ResetValues()
    {
        monitoredTotal.total = 0.0f;
    }
}

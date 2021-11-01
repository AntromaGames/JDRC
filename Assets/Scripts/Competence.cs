using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Competence  
{
    public string title;
    public string description;
    public string iconPath;
    public int niveau;
    public int value;
    public int maxValue;
    public Sprite icon;

    public Competence (string _title,string _description, int _niveau, int _value, int _maxValue)
    {
        title = _title;
        description = _description;
        niveau = _niveau;
        value = _value;
        maxValue = _maxValue;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Power 
{
    public string title;
    public string description;
    public int level;
    public int niveau;
    public bool isUsed;

    public Power(string _title, string _description, int _level, int _niveau, bool _isUsed)
    {
        title = _title;
        description = _description;
        level = _level;
        niveau = _niveau;
        isUsed = _isUsed;

    }
 
}

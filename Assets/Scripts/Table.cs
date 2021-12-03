using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Table 
{
    public int index;
    public float xPos;
    public float yPos;
    public float rotation;
    public bool issimple;

    public Table(int _index,float _xPos,float _yPos, float _rotation, bool _isSimple)
    {
        index = _index;
        xPos = _xPos;
        yPos = _yPos;
        rotation = _rotation;
        issimple = _isSimple;
    }

}

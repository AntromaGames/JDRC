using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassroomManager : MonoBehaviour
{
    public static ClassroomManager instance;

    public Toggle placementToggle;

    TableToFillManager[] tables;

    private void Awake()
    {
        MakeSingleton();

    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }



    public void TogglePlacement()
    {
        tables = FindObjectsOfType<TableToFillManager>();
        for (int i = 0; i< tables.Length; i++)
        {
            tables[i].ToggleButtonVisibility(SetPlacement());
        }

    }


    public bool SetPlacement()
    {
        if (placementToggle.isOn)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}

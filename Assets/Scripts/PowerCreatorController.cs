using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerCreatorController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI p_niveau;
    [SerializeField] TMP_InputField p_Level;
    [SerializeField] TMP_InputField p_Name;
    [SerializeField] TMP_InputField p_Description;



    // Start is called before the first frame update
    void Start()
    {
        ClearAllInputs();

    }
    private void ClearAllInputs()
    {
        p_Level.text = "1";
        p_Name.text = "";
    }

    public void SetPowers()
    {
        int level = 0;
        switch (p_niveau.text)
        {
            case "3eme":
                level = 3;
                break;
            case "4eme":
                level = 4;
                break;
            case "5eme":
                level = 5;
                break;
            case "6eme":
                level = 6;
                break;
        }

        Power power = new Power(p_Name.text, p_Description.text,int.Parse(p_Level.text) , level,false );

      
        Debug.Log("Power :" + p_Name + " ajouté ");

        if (GameManager.instance.powers != null)
        {
            GameManager.instance.powers.Add(power);
        }
        else
        {
            GameManager.instance.powers = new List<Power>();
            GameManager.instance.powers.Add(power);
        }

        GameManager.instance.GivePowersToEleves();
        LoadAndSave.instance.SavePower(power);

    }
}

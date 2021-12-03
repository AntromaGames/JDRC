using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PowerController : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI tooltipText;
    public Power power;
    public Image icon;

    [SerializeField] Color available, used, unavailable;


    public void SetUI()
    {
        tooltipText.text = power.description;
        if (power.isUsed)
        {
            GetComponent<Button>().interactable = false;
            icon.color = used;
        }else if(GameManager.instance.currentEleve.level >= power.level)
        {
            GetComponent<Button>().interactable = true;
            icon.color = available;
        }
        else
        {
            GetComponent<Button>().interactable = false;
            icon.color = unavailable;
        }
    }

    public void UsePower()
    {
        GameManager.instance.currentEleve.powers.Find(p => p.title == power.title).isUsed = true;
        GetComponent<Button>().interactable = false;
        icon.color = used;
        LoadAndSaveWithJSON.instance.SaveList();
    }

}

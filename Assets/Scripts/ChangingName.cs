using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.ModernUIPack;

public class ChangingName : MonoBehaviour
{

    //Create a List of new Dropdown options
    List<string> m_DropOptions ;
    //This is the Dropdown
    public TMP_Dropdown m_Dropdown;
    public string photoPath;


    Transform dropDownTrans;
    public Image eleveSprite;

    public void OnClick()
    {
        m_Dropdown.gameObject.SetActive(true);
    }

    public void SetName()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = m_Dropdown.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        FindObjectOfType<PhotoImporter>().elevesNames.Remove(GetComponentInChildren<TextMeshProUGUI>().text);
    }


    void Start()
    {
        m_DropOptions = FindObjectOfType<PhotoImporter>().elevesNames;
        //Clear the old options of the Dropdown menu
        m_Dropdown.ClearOptions();
        //Add the options created in the List above
        m_Dropdown.AddOptions(m_DropOptions);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Michsky.UI.ModernUIPack;

public class ChangingName : MonoBehaviour
{

    public GameObject changingNameDropdwn;

    Transform dropDownTrans;
    public Image eleveSprite;

    public void OnClick()
    {

        dropDownTrans = GameObject.Find("PhotoImporterPanel").transform;
        GameObject dropDown =Instantiate(changingNameDropdwn, dropDownTrans);
        dropDown.GetComponent<CustomDropdown>().SetSpriteFromButton(eleveSprite.sprite);

    }

    public void SetName()
    {
        GetComponentInChildren<TextMeshProUGUI>().text = FindObjectOfType<PhotoImporter>().currentName;

    }

}

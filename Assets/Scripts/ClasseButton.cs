using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClasseButton : MonoBehaviour
{

    public void SetEleveClasse()
    {

        string classeName = GetComponentInChildren<TMPro.TextMeshProUGUI>().text;
        Debug.Log(" classe " + classeName + "ajouté");
        UIController.instance.DisplayEleves(classeName);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCSVDisplayClasses : MonoBehaviour
{

    // Objects reference for the classes panel
    [SerializeField] private GameObject classeButtonPrefab;
    [SerializeField] private Transform panelTransform;



    public void DisplayClasses()
    {
        List<string> classes = new List<string>();
        foreach (Eleve e in GameManager.instance.eleves)
        {
            if (!classes.Contains(e.classe))
            {
                classes.Add(e.classe);
            }
        }

        foreach (string s in classes)
        {
            GameObject classeButton = Instantiate(classeButtonPrefab, panelTransform);
            classeButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = s;
            classeButton.GetComponentInChildren<MakeCsvFileFromList>().classe = s;
        }
    }

    private void OnEnable()
    {
        DisplayClasses();
    }
}

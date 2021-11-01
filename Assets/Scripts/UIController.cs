using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // singleton pattern for uicontroller
    public static UIController instance;

    // Objects reference for the classes panel
    [SerializeField] private GameObject classeButtonPrefab;
    [SerializeField] private Transform panelTransform;

    // objects ref for Eleves Panel
    [SerializeField] private GameObject elevesSelectionPanel;
    [SerializeField] private GameObject eleveButtonPrefab;
    [SerializeField] private Transform eleveButtonPanelTransform;


    [SerializeField] private GameObject eleveCardPanel;

    // Start is called before the first frame update
    void Start()
    {
        MakeSingleton();
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
        }
    }


    public void DisplayEleves(string classe)
    {


        foreach(Eleve e in GameManager.instance.eleves)
        {
            if (e.classe==classe)
            {
                elevesSelectionPanel.gameObject.SetActive(true);
                GameObject eleveButton = Instantiate(eleveButtonPrefab, eleveButtonPanelTransform);
                eleveButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = e.prenom + " " + e.nom;
                if(e.photo != null)
                {
                    eleveButton.GetComponent<Image>().sprite = e.photo;
                }
            }
        }
    }

    public void DisplayEleveCard()
    {
        eleveCardPanel.SetActive(true);
    }

}

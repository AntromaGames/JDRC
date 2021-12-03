using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // singleton pattern for uicontroller
    public static UIController instance;

    // Objects reference for the classes panel
    [SerializeField] private GameObject classeButtonPrefab;
    [SerializeField] private Transform classePanelTransform;
    [SerializeField] private Transform classRoomTransform;

    // objects ref for Eleves Panel
    [SerializeField] private GameObject elevesSelectionPanel;
    [SerializeField] private GameObject eleveButtonPrefab;
     public Transform eleveButtonPanelTransform;


    [SerializeField] private GameObject eleveCardPanel;
    [SerializeField] private Transform JDRCPanel;


    List<GameObject> elevesDansLeScroll = new List<GameObject>();

    [SerializeField] TMPro.TextMeshProUGUI classeNameText;


    [SerializeField] Transform planTransfrom;
    [SerializeField] GameObject simpleTablePrefab, doubleTableprefab;
    public Transform dropTrans;

    // Start is called before the first frame update
    void Start()
    {
        MakeSingleton();
        if (GameManager.instance.plan != null)
        {        
            Debug.Log("displayTables");
            DisplayTables();
        }
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


    public void DisplayTables()
    {
        foreach (Table t in GameManager.instance.plan)
        {

            if (t.issimple)
            {
                GameObject newTable = Instantiate(simpleTablePrefab, planTransfrom);
                RectTransform trans = newTable.GetComponent<RectTransform>();
                trans.anchoredPosition = new Vector2(t.xPos, t.yPos);


                trans.Rotate(new Vector3(0, 0, t.rotation));
                //trans.rotation.ToAngleAxis(out t.rotation, out angle);
                newTable.GetComponent<TableToFillManager>().table = t;
            }
            else
            {
                GameObject newTable = Instantiate(doubleTableprefab, planTransfrom);
                RectTransform trans = newTable.GetComponent<RectTransform>();
                trans.anchoredPosition = new Vector2(t.xPos, t.yPos);

                trans.Rotate(new Vector3(0, 0, t.rotation), Space.Self);
                // Vector3 angle = new Vector3(0, 0, t.rotation);
                //trans.Rotate(angle);
                //trans.rotation.ToAngleAxis(out t.rotation, out angle);
                newTable.GetComponent<TableToFillManager>().table = t;
            }
        }
    }

    public void ClearUI()
    {
        foreach (Transform child in classePanelTransform.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public void DisplayClasses()
    {

        ClearClassesPanel();

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
            GameObject classeButton = Instantiate(classeButtonPrefab, classePanelTransform);
            classeButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = s;
        }
    }

    private void ClearClassesPanel()
    {
       foreach(Transform t in classePanelTransform)
        {
            Destroy(t.gameObject);
        }
    }

    public void DisplayEleves(string classe)
    {
        elevesSelectionPanel.gameObject.SetActive(true);

        ClearTables();
        DisplayTables();

        List<Eleve> elevesToPlace = GameManager.instance.eleves.FindAll(Eleve => Eleve.classe == classe);
        TableToFillManager[] tables = FindObjectsOfType<TableToFillManager>();
        List<TableToFillManager> tablesToFill = new List<TableToFillManager>();
        DisplayClasseName();
        for (int i = 0; i < tables.Length; i++)
        {
            tablesToFill.Add(tables[i]);
        }
        ClearPreviousNamesOnLeft();

        // Place les eleves dans la partie de gauche  ( scroll view )
        foreach (Eleve e in GameManager.instance.eleves)
        {
            if (e.classe == classe)
            {
                GameObject eleveButton = Instantiate(eleveButtonPrefab, eleveButtonPanelTransform);
                eleveButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = e.prenom + " " + e.nom;
                if (!string.IsNullOrEmpty(e.photoPath))
                {
                    byte[] b = File.ReadAllBytes(e.photoPath);
                    Texture2D tex = new Texture2D(2, 2);
                    tex.LoadImage(b);
                    byte[] pngByte = tex.EncodeToPNG();
                    eleveButton.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                }
                elevesDansLeScroll.Add(eleveButton);

            }
        }



        //Place les eleves dans la partie centrale s'ils ont une place déjà attitrée
        foreach (Eleve e in elevesToPlace)
        {

            if (e.table != 0)
            {

                TableToFillManager tableToFill = tablesToFill.Find(TableToFillManager => TableToFillManager.table.index == e.table);
                GameObject eleveButton = Instantiate(eleveButtonPrefab, classRoomTransform);


                DeleteeleveInScroill(e.prenom + " " + e.nom);

                if (e.direction == ChaiseDirection.left)
                {
                    eleveButton.transform.SetParent(tableToFill.leftTransform);
                    eleveButton.transform.localPosition = new Vector3(0, 0, 0);
                    // SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                    //eleveButton.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                    //eleveButton.transform.SetPositionAndRotation(tableToFill.leftTransform.position, Quaternion.identity);
                }
                else if (e.direction == ChaiseDirection.right)
                {
                    eleveButton.transform.SetParent(tableToFill.rightTransform);
                    eleveButton.transform.localPosition = new Vector3(0, 0, 0);
                    // eleveButton.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                    //eleveButton.transform.SetPositionAndRotation(tableToFill.rightTransform.position,Quaternion.identity);
                }
                else
                {
                    eleveButton.transform.SetParent(tableToFill.leftTransform);
                }

                // GameObject eleveButton = Instantiate(eleveButtonPrefab, tableToFill.currentTransform);

                Debug.Log(e.nom + " va s assoir sur la table  " + tableToFill.table.index);
                eleveButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = e.prenom + " " + e.nom;
                if (!string.IsNullOrEmpty(e.photoPath))
                {
                    byte[] b = File.ReadAllBytes(e.photoPath);
                    Texture2D tex = new Texture2D(2, 2);
                    tex.LoadImage(b);
                    byte[] pngByte = tex.EncodeToPNG();
                    eleveButton.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

                }
            }
        }


    }

    private void ClearTables()
    {
        // Destroy all previous tables

        foreach (Transform go in planTransfrom)
        {
            Destroy(go.gameObject);
        }
    }

    private void ClearPreviousNamesOnLeft()
    {
        // Destroy all previous names in left panel

        foreach (Transform go in eleveButtonPanelTransform)
        {
            Destroy(go.gameObject);
        }
    }

    internal void DeleteFromLeftSide(string v)
    {
        foreach(Transform go in eleveButtonPanelTransform)
        {
            if (go.GetComponentInChildren<TMPro.TextMeshProUGUI>().text == v)
            {
                Destroy(go.gameObject);
            }
        }
    }

    private void DeleteeleveInScroill(string v)
    {
        GameObject eleveGo = elevesDansLeScroll.Find(GameObject => GameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text == v);

        if(eleveGo != null)
        {
            Debug.Log(" on retire " + eleveGo.GetComponentInChildren<TMPro.TextMeshProUGUI>().text + " du scroll");
            Destroy(eleveGo);
            elevesDansLeScroll.Remove(eleveGo);
        }

    }

    public void DisplayEleveCard()
    {
        Instantiate(eleveCardPanel, JDRCPanel);
    }


    public void DisplayClasseName()
    {
        classeNameText.text = GameManager.instance.currentClasse;
    }
}

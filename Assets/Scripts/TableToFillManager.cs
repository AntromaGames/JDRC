using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TableToFillManager : MonoBehaviour
{

    public Table table;
    public Transform rightTransform, leftTransform;
    public Transform currentTransform;
    [SerializeField] TMPro.TMP_Dropdown rightDrop, leftDrop;
    TMP_Dropdown m_Dropdown;
    [SerializeField] Transform dropTrans;
    public GameObject eleveButtonPrefab;
    [SerializeField] Button rightBtn, leftBtn;
    Transform eleveButtonPanelTransform;
    List<string> m_DropOptions;


    [SerializeField] bool isSimple;
    private void Start()
    {
        eleveButtonPanelTransform = UIController.instance.eleveButtonPanelTransform;
        dropTrans = UIController.instance.dropTrans;
    }

    public void ToggleButtonVisibility(bool b)
    {
        Debug.Log("changing visibility of table btn");
        if(!isSimple)
        rightBtn.gameObject.SetActive(b);
        leftBtn.gameObject.SetActive(b);
    }

    public void ActivateRightDrop()
    {
        CloseAllDrops();
        if (!isSimple)
        {
            rightDrop.gameObject.SetActive(true);
            rightDrop.transform.localPosition = new Vector3 (+10 ,0,0);
            rightDrop.transform.Rotate(0, 0, -table.rotation,Space.Self);
            rightDrop.gameObject.transform.SetParent(dropTrans);
            Populate(rightDrop);
            m_Dropdown = rightDrop;
            currentTransform = rightTransform;
        }
    }
    public void ActivateLeftDrop()
    {
        CloseAllDrops();
        leftDrop.gameObject.SetActive(true);
        leftDrop.transform.localPosition = new Vector3(-10, 0, 0);
        leftDrop.transform.Rotate(0, 0, -table.rotation,Space.Self);
        leftDrop.gameObject.transform.SetParent(dropTrans);
        Populate(leftDrop);
        m_Dropdown = leftDrop;
        currentTransform = leftTransform;

    }

    private void CloseAllDrops()
    {
        TMP_Dropdown[] drops = FindObjectsOfType<TMP_Dropdown>();
        for(int i =0; i < drops.Length; i++)
        {
            drops[i].gameObject.SetActive(false);
        }
    }

    private void Populate(TMP_Dropdown drop)
    {

        Debug.Log("populate Dropdown With Eleves");
        List<Eleve> eleves = GameManager.instance.eleves.FindAll(Eleve => Eleve.classe == GameManager.instance.currentClasse);

        List<string> elevesNames = new List<string>();

        foreach (Eleve e in eleves)
        {
            elevesNames.Add(e.prenom + " " + e.nom);
        }
        m_DropOptions = new List<string>();
        m_DropOptions.Add("nom de l'élève...");
        m_DropOptions.AddRange( elevesNames);

        foreach(string s in ClassroomManager.instance.elevesDejaPlaces)
        {
            if (m_DropOptions.Contains(s))
            {
                m_DropOptions.Remove(s);
            }
        }

        //Clear the old options of the Dropdown menu
        drop.ClearOptions();
        //Add the options created in the List above
        drop.AddOptions(m_DropOptions);


        Debug.Log("il y a " + drop.options.Count + " dans le drop options");
        //for (int i = 0; i < eleves.Count; i++)
        //{
        //    Debug.Log("setting Picture of " + eleves[i].nom);
        //    eleves[i].SetPicture();
        //    drop.options[i].image = ConvertStringToSprite( eleves[i].photoPath);

        //}

    }
    public void SetElevePlace()
    {
        ClearTable();
        string choosenEleve = m_Dropdown.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;

        Eleve currentEleve = GameManager.instance.eleves.Find(Eleve => Eleve.prenom + " " + Eleve.nom == choosenEleve);

        if (currentEleve != null)
        {
            currentEleve.table = table.index;

            if (table.issimple)
            {
                currentEleve.direction = ChaiseDirection.alone;
                currentTransform = leftTransform;
            }
            else
            {
                if (currentTransform == leftTransform)
                {
                    currentEleve.direction = ChaiseDirection.left;
                }
                else if (currentTransform == rightTransform)
                {
                    currentEleve.direction = ChaiseDirection.right;
                }
            }


            GameObject eleveButton = Instantiate(eleveButtonPrefab, currentTransform);
            UIController.instance.DeleteFromLeftSide(currentEleve.prenom + " " + currentEleve.nom);
            ClassroomManager.instance.elevesDejaPlaces.Add(currentEleve.prenom + " " + currentEleve.nom);
            eleveButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = currentEleve.prenom + " " + currentEleve.nom;

            if (!string.IsNullOrEmpty(currentEleve.photoPath))
            {
                if(File.Exists(currentEleve.photoPath))
                {
                    byte[] b = File.ReadAllBytes(currentEleve.photoPath);
                    Texture2D tex = new Texture2D(2, 2);
                    tex.LoadImage(b);
                    byte[] pngByte = tex.EncodeToPNG();
                    eleveButton.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                }

            }
        }
        LoadAndSaveWithJSON.instance.SaveList();
    }

    public Sprite ConvertStringToSprite(string s)
    {
        if (!string.IsNullOrEmpty(s))
        {
            byte[] b = File.ReadAllBytes(s);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(b);
            byte[] pngByte = tex.EncodeToPNG();
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }else
        {
            return null;
        }

    }

    public void ClearTable()
    {

        Debug.Log("Clearing table !");

        if (!isSimple)
        {
            foreach (Transform t in rightTransform)
            {
                Eleve currentEleve = GameManager.instance.eleves.Find(Eleve => Eleve.prenom + " " + Eleve.nom == t.GetComponentInChildren<TMPro.TextMeshProUGUI>().text);
                if (currentEleve != null)
                {
                    currentEleve.table = 0;
                }
                Destroy(t.gameObject);

            }
        }

        foreach (Transform t in leftTransform)
        {
            Eleve currentEleve = GameManager.instance.eleves.Find(Eleve => Eleve.prenom + " " + Eleve.nom == t.GetComponentInChildren<TMPro.TextMeshProUGUI>().text);
            if (currentEleve != null)
            {
                currentEleve.table = 0;
            }
            Destroy(t.gameObject);

        }
    }
}

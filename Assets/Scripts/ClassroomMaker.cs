using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomMaker : MonoBehaviour
{
    public static ClassroomMaker instance;

    [SerializeField] GameObject simpleTablePrefab, doubleTablePrefab;

    [SerializeField] public Transform simpleTableTrans, doubleTableTrans,classRoomTrans;


    // Start is called before the first frame update
    void Start()
    {
        MakeSingleton();
        CreateNewSimpleTable();
        CreateNewDoubleTable();

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


    public void SavePlanDeClasse()
    {
        TableController[] tables = FindObjectsOfType<TableController>();
        for(int i =0;i < tables.Length; i++)
        {
            if(tables[i].gameObject.GetComponent<RectTransform>().localPosition.x >-300 && tables[i].gameObject.GetComponent<RectTransform>().localPosition.x < 280)
            {
                Debug.Log("on sauvegarde la table ici ");
                tables[i].SaveTable();
            }
            else
            {
                Debug.Log("on ne peut pas sauvegarder de table a cet endroit");
            }

        }
    }
    public void CreateNewSimpleTable()
    {
        int numberOfSimpleTable = 0;
        foreach (Transform child in simpleTableTrans)
        {
            if(child.GetComponent<TableController>() != null && child.GetComponent<TableController>().isSimple)
            {
                Debug.Log("il y a deja une table ici");
                numberOfSimpleTable++;
            }
        }


        if (numberOfSimpleTable == 0)
        {
            GameObject newTable = Instantiate(simpleTablePrefab, simpleTableTrans);
            newTable.transform.position = new Vector2(simpleTableTrans.position.x, simpleTableTrans.position.y);
        }


    }

    public void CreateNewDoubleTable()
    {

        int numberOfDoubleTable = 0;
        foreach (Transform child in doubleTableTrans)
        {
            if (child.GetComponent<TableController>() != null && !child.GetComponent<TableController>().isSimple)
            {
                Debug.Log("il y a deja une double table ici");
                numberOfDoubleTable++;
            }
        }
        if (numberOfDoubleTable == 0)
        {
            GameObject newTable = Instantiate(doubleTablePrefab, doubleTableTrans);
            newTable.transform.position = new Vector2(doubleTableTrans.position.x, doubleTableTrans.position.y);
        }


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleFileBrowser;
using System;
using System.Text;
using File = System.IO.File;

public class MakeCsvFileFromList : MonoBehaviour
{
    public static MakeCsvFileFromList instance;
    List<Eleve> eleveList;
    public bool autosave = true;
    string data;
    public string classe;
    List<string> temporaryEleveList;

    Encoding ae = Encoding.GetEncoding(
              "utf-8",
              new EncoderExceptionFallback(),
              new DecoderExceptionFallback());

    private List<string[]> rowData = new List<string[]>();

    [SerializeField] private int maxNumberOfFiles = 100;

    private void Awake()
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
    private void Start()
    {
        data = "";
    }

    public byte[] MakeByteDataFromEleveList(List<Eleve> playerList)
    {

        foreach (Eleve eleve in playerList)
        {
            string powerString = "";
            string competenceString = "";
            foreach (Competence c in eleve.competences)
            {
                competenceString += c.title + " : " + c.value + ",";
            }
            foreach (Power p in eleve.powers)
            {
                powerString += p.title + " : " + p.isUsed + ",";
            }


            data += eleve.niveau + "," + eleve.nom + "," + eleve.prenom + "," + eleve.classe + "," + eleve.id + "," +
                eleve.level + "," +  eleve.xp + "," // données de l'élève
                + powerString + competenceString  + '\n';
        }

        byte[] encodedBytes = new byte[ae.GetMaxByteCount(data.Length)];
        int numberOfEncodedBytes = 0;
        try
        {
            numberOfEncodedBytes = ae.GetBytes(data, 0, data.Length,
                                               encodedBytes, 0);
        }
        catch (EncoderFallbackException e)
        {
            Debug.Log("bad conversion" + e);
        }
        return encodedBytes;
    }


    public void CreateListFileAtEnd()
    {
        List<Eleve> liste = GameManager.instance.eleves.FindAll(Eleve => Eleve.classe == classe);

        Debug.Log("il y a " + liste.Count + "  d 'élèves de "+ classe + "  à sauvegarder");

        string elevesFullName = "";
        foreach ( Eleve e in liste)
        {
            elevesFullName += e.nom + " " + e.prenom + " , ";
        }
        Debug.Log("les eleves a sauvagarder sont " + elevesFullName);

        string utf8Sb = MakeStringFromPLayerList(liste);

        string filePath = Application.dataPath + "/" +classe + System.DateTime.Now.ToShortDateString()+ "_Saved_data";

        StreamWriter outStream = System.IO.File.CreateText(filePath + ".csv");

        outStream.WriteLine(utf8Sb);

        FileBrowser.ShowSaveDialog((paths) => {

            Debug.Log("Selected: " + paths[0]);
            FileBrowserHelpers.CreateFileInDirectory(paths[0], filePath + ".csv");
            FileBrowserHelpers.AppendTextToFile(filePath + ".csv", utf8Sb);
        },
                          () => { Debug.Log("Canceled"); },
                          FileBrowser.PickMode.Folders, false, null, null, "Select Folder", "Select");
        outStream.Close();
        //MessageDisplayer.instance.ShowMessage("liste sauvegardée dans" + filePath);
    }
    public string MakeStringFromPLayerList(List<Eleve> listToSave)
    {

        List<Competence> compList = listToSave[2].competences;
        int compindex = compList.Count;

        Debug.Log(" nombre de comp pour cette classe : " + compindex);
        // Creating First row of titles manually..1ere ligne
        string[] rowDataTemp = new string[8];
        //string[] rowDataTemp = new string[8 + compindex];
        rowDataTemp[0] = "Niveau";
        rowDataTemp[1] = "Nom";
        rowDataTemp[2] = "Prénom";
        rowDataTemp[3] = "Classe";
        rowDataTemp[4] = "identifiant";
        rowDataTemp[5] = "level";
        rowDataTemp[6] = "points XP";
        rowDataTemp[7] = "pouvoirs utilisés";
        //for (int i = 7; i< 7 + compindex;i++)
        //{
        //    rowDataTemp[i] = compList[i-7].title;
        //}
        //rowDataTemp[7+ compindex] = "pouvoirs utilisés";
        //rowData[0] = rowDataTemp;

        string entete = "";
        for(int i=0; i< rowDataTemp.Length; i++)
        {
            entete += rowDataTemp[i] + " ";
        }
        Debug.Log(" header : " + entete);
        // rowData.Add(rowDataTemp);
        // Pour chaque player de la liste créer une ligne

        foreach (Eleve eleve in listToSave)
        {
                AddPlayerToNewRow(eleve);
        }
        int length = rowData.Count;
        Debug.Log("rowData Count: " + length);
        string delimiter = ";";
        StringBuilder sb = new StringBuilder();
        for (int index = 0; index < length - 1; index++)
        {
            Debug.Log("rowData Adds: " + rowData[index]);
            sb.AppendLine(string.Join(delimiter, rowData[index]));
        }
        byte[] utf8byte = Encoding.UTF8.GetBytes(sb.ToString());
        char[] sbchar = Encoding.UTF8.GetChars(utf8byte);
        string utf8Sb = new string(sbchar);
        Debug.Log(utf8Sb);
        PlayerPrefs.SetString("UserList", utf8Sb);
        return utf8Sb;

    }

    public void AddPlayerToNewRow(Eleve eleve)
    {
        List<Competence> compList = eleve.competences;
        int compindex = compList.Count;

        var rowDataTemp = new string[7 + compindex];

            rowDataTemp[0] = eleve.niveau.ToString();
            rowDataTemp[1] = eleve.nom;
            rowDataTemp[2] = eleve.prenom;
            rowDataTemp[3] = eleve.classe;
            rowDataTemp[4] = eleve.id.ToString();
            rowDataTemp[5] = eleve.level.ToString();
            rowDataTemp[6] = eleve.xp.ToString();
        //for (int i = 7; i < 7 + compindex; i++)
        //{
        //    rowDataTemp[i] = compList[i].title;
        //}
        List<Power> powList = eleve.powers;

        string usedPowers = "";
        foreach (Power p in powList)
        {
            if(p.isUsed)
            usedPowers += p.title;
        }
        rowDataTemp[7 ] = usedPowers;

        rowData.Add(rowDataTemp);

        Debug.Log(rowDataTemp[0] + rowDataTemp[1] + " a été ajouté a une ligfne du fichier");
        
    }

}

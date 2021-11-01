using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleFileBrowser;
using System;

public class FileBrowserGetFile : MonoBehaviour
{
    // Warning: paths returned by FileBrowser dialogs do not contain a trailing '\' character
    // Warning: FileBrowser can only show 1 dialog at a time
    string classeName;
    int niveau;

    public void GetFile()
    {
        // Set filters (optional)
        // It is sufficient to set the filters just once (instead of each time before showing the file browser dialog), 
        // if all the dialogs will be using the same filters
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"), new FileBrowser.Filter("Text Files", ".txt",".csv", ".pdf"));

        // Set default filter that is selected when the dialog is shown (optional)
        // Returns true if the default filter is set successfully
        // In this case, set Images filter as the default filter
        FileBrowser.SetDefaultFilter(".csv");

        // Set excluded file extensions (optional) (by default, .lnk and .tmp extensions are excluded)
        // Note that when you use this function, .lnk and .tmp extensions will no longer be
        // excluded unless you explicitly add them as parameters to the function
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

        // Add a new quick link to the browser (optional) (returns true if quick link is added successfully)
        // It is sufficient to add a quick link just once
        // Name: Users
        // Path: C:\Users
        // Icon: default (folder icon)
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);

        // Show a save file dialog 
        // onSuccess event: not registered (which means this dialog is pretty useless)
        // onCancel event: not registered
        // Save file/folder: file, Allow multiple selection: false
        // Initial path: "C:\", Initial filename: "Screenshot.png"
        // Title: "Save As", Submit button text: "Save"
        // FileBrowser.ShowSaveDialog( null, null, FileBrowser.PickMode.Files, false, "C:\\", "Screenshot.png", "Save As", "Save" );

        // Show a select folder dialog 
        // onSuccess event: print the selected folder's path
        // onCancel event: print "Canceled"
        // Load file/folder: folder, Allow multiple selection: false
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Select Folder", Submit button text: "Select"
         FileBrowser.ShowLoadDialog( ( paths ) => { Debug.Log( "Selected: " + paths[0] ); },
        						   () => { Debug.Log( "Canceled" ); },
        						   FileBrowser.PickMode.Folders, false, null, null, "Select Folder", "Select" );

        // Coroutine example
        StartCoroutine(ShowLoadDialogCoroutine());
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: both, Allow multiple selection: true
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Load File", Submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");

        // Dialog is closed
        // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {

            // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
            for (int i = 0; i < FileBrowser.Result.Length; i++)
                Debug.Log(FileBrowser.Result[i]);
            classeName = FileBrowserHelpers.GetFilename(FileBrowser.Result[0]);
            classeName = classeName.Remove(2);//   PadRight(4);
            Debug.Log("classe en cours " + classeName);

            char firstLetterClasse = classeName[0];

            bool isGettingNiveau = int.TryParse(firstLetterClasse.ToString(), out niveau);
            if (!isGettingNiveau)
            {
                    ///TODO Meassage Warning Displayer
            }

            // Read the bytes of the first file via FileBrowserHelpers
            // Contrary to File.ReadAllBytes, this function works on Android 10+, as well
            byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);
      
            ConvertFileToEleves(bytes);
            // Or, copy the first file to persistentDataPath
            string destinationPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
            FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);
        }
    }

    private void ConvertFileToEleves(byte[] bytes)
    {

        char[] separator = new char[] { '\n' };

        string liste = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        Debug.Log(liste);

        string[] strArray = liste.Split(separator);

        List<Eleve> elevesList = new List<Eleve>();
        int id = 0;
        if (GameManager.instance.eleves != null)
        {
             id = GameManager.instance.eleves.Count+1;
        }
        else
        {
            id = 0;
        }

        for (int i = 1; i < strArray.Length-1 ; i++)
        {
            char[] separator2 = new char[] { ';' };
            string[] elevesInfos = strArray[i].Split(separator2);
            string fullName = elevesInfos[0];
            string[] prenomNom = fullName.Split(' ');
            string prenom = prenomNom[prenomNom.Length -1];
            string nom = prenomNom[0];
            prenom = prenom.Replace("\"", "");
            nom = nom.Replace("\"", "");
            Eleve currentEleve = new Eleve(prenom,nom,classeName,id,0,0,niveau);
            Debug.Log("eleve ajouté en " + currentEleve.classe + " : " + currentEleve.nom);
            elevesList.Add(currentEleve);
            id++;
        }
       // GameManager.instance.AddEleves(elevesList);
        GameManager.instance.eleves.AddRange(elevesList);
        GameManager.instance.GiveCompToEleves();
        LoadAndSave.instance.SaveList();
        PlayerPrefs.SetInt("numberOfEleves", id);
    }
}








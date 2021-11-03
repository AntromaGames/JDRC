using SimpleFileBrowser;
using Spire.Pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PhotoImporter : MonoBehaviour
{

    Texture mainTex;
    // Warning: paths returned by FileBrowser dialogs do not contain a trailing '\' character
    // Warning: FileBrowser can only show 1 dialog at a time

    public GameObject newimage;
    public Transform receiver;


    public GameObject changingNameDropDown;

    string classeName;
    string path;
    public List<string> elevesNames;

    public string currentName;

   [SerializeField] private float timeBetweenEleves=0.2f;

    public void GetFile()
    {
        // Set filters (optional)
        // It is sufficient to set the filters just once (instead of each time before showing the file browser dialog), 
        // if all the dialogs will be using the same filter
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"), new FileBrowser.Filter("Text Files", ".txt", ".csv", ".pdf"));

        // Set default filter that is selected when the dialog is shown (optional)
        // Returns true if the default filter is set successfully
        // In this case, set Images filter as the default filter
        FileBrowser.SetDefaultFilter(".pdf");

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
        FileBrowser.ShowLoadDialog((paths) => { Debug.Log("Selected: " + paths[0]); },
                                  () => { Debug.Log("Canceled"); },
                                  FileBrowser.PickMode.Folders, false, null, null, "Select Folder", "Select");

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

            SetElevesNames(classeName);
            // Read the bytes of the first file via FileBrowserHelpers
            // Contrary to File.ReadAllBytes, this function works on Android 10+, as well
            byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);
        
            ConvertFileToImages(bytes);

            // Or, copy the first file to persistentDataPath
            string destinationPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
            FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);
            path = destinationPath;
        }
    }

    private void SetElevesNames(string classeName)
    {

        elevesNames = new List<string>();
        foreach (Eleve e in GameManager.instance.eleves)
        {
            if(e.classe == classeName)
            {
                string fullName = e.prenom + " " + e.nom;
                elevesNames.Add(fullName);
            }
        }

    }

    private void ConvertFileToImages(byte[] bytes)
    {
        PdfDocument doc = new PdfDocument();
        doc.LoadFromBytes(bytes);
        doc.SaveAsImage(0);

        PdfPageBase page = doc.Pages[0];

        Stream[] images = page.ExtractImages();
        List<Sprite> imageList = new List<Sprite>();

        for (int i = 0; i < images.Length; i++)
        {
            BinaryReader streamrd = new BinaryReader(images[i]);

            byte[] b = null;

            using (StreamReader reader = new StreamReader(images[i]))
            {
                using (var memstream = new MemoryStream())
                {
                    var buffer = new byte[512];
                    var bytesRead = default(int);
                    while ((bytesRead = reader.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
                        memstream.Write(buffer, 0, bytesRead);
                    b = memstream.ToArray();
                }
            }

            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(b);
            byte[] pngByte = tex.EncodeToPNG();
            File.WriteAllBytes("D:\\UnityProjects\\Jeu de role en classe\\DOCS" + "\\3A_Eleve_" + i + ".png", pngByte);
            Sprite img = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

            imageList.Add(img);
        }
        StartCoroutine(LoadImages(imageList));
    }
    IEnumerator LoadImages(List<Sprite> list)
    {
        int nameIndex = 0;
        foreach (Sprite s in list)
        {        
            GameObject newImg = Instantiate(newimage, receiver);
            newImg.GetComponentInChildren<Image>().sprite = s;
            newImg.GetComponentInChildren<TextMeshProUGUI>().text = elevesNames[nameIndex];
            nameIndex++;
            yield return new WaitForSeconds(timeBetweenEleves);
        }
    }
}

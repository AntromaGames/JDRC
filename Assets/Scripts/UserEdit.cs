using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class UserEdit : MonoBehaviour
{
    Genre u_genre;
    string u_name, u_matiere, u_etablissement, u_firstName;
    public User user;


    public static UserEdit instance;


    [SerializeField] private GameObject firstTimePanel;
    [SerializeField] TMPro.TMP_InputField nom_Input;
    [SerializeField] TMPro.TMP_InputField prenom_Input;
    [SerializeField] TMPro.TMP_InputField etablissement_Input;
    [SerializeField] TMPro.TextMeshProUGUI matiere_Input;

    [SerializeField] TMPro.TextMeshProUGUI bienvenueName;

    [SerializeField] Animator notificationAnim;
    [SerializeField] TMPro.TextMeshProUGUI notifText;
    [SerializeField] private float timeBetweenScenes = 3f;

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
 
        //if (PlayerPrefs.GetInt("firstTimeOpen") == 0) // Si l utilisateur ouvre pour la premiere fois  firsttimeopen = 0
        //{
        //    OpenFirstTimePanel();
        //}
        //else // l'utilisateur a déjà ouvert l appli et a un compte user !
        //{
        //    user = LoadAndSave.instance.GetUser();
        //    GameManager.instance.currentUser = user;
        //    bienvenueName.text = user.firstName + " " + user.name;
        //    StartCoroutine(nextScene());
        //}
        if (!File.Exists(Application.dataPath + @"\Save_User.csv")) // Si l utilisateur ouvre pour la premiere fois  firsttimeopen = 0
        {
            OpenFirstTimePanel();
        }
        else // l'utilisateur a déjà ouvert l appli et a un compte user !
        {
            user = LoadAndSaveWithJSON.instance.GetUser();
            GameManager.instance.currentUser = user;
            if (user.genre == Genre.homme)
            {
                bienvenueName.text = "Monsieur" + " " + user.name;
            }
            else
            {
                bienvenueName.text = "Madame" + " " + user.name;
            }

            StartCoroutine(nextScene());
        }      
    }
    private void OpenFirstTimePanel()
    {
        //PlayerPrefs.SetInt("firstTimeOpen", 1);
        firstTimePanel.SetActive(true);
    }

    #region Inputs
    public void SetName( )
    {
        u_name = nom_Input.text;
    }
    public void SetFirstName( )
    {
        u_firstName = prenom_Input.text;
    }
    public void SetMatiere( )
    {
        u_matiere = matiere_Input.text; 
    }
    public void SetEtablissement( )
    {
        u_etablissement = etablissement_Input.text;
    }
    public void SetGenre(string genre)
    {
        if (genre == "male")
        {
            u_genre = Genre.homme;
        }
        else
        {
            u_genre = Genre.femme;
        }
    }

    #endregion

    public void SetNotif(string message)
    {
        notifText.text = message;
        notificationAnim.SetTrigger("Notif");
    }
    public void ValidateUser()
    {
        SetMatiere();
        if (nom_Input.text == "")
        {
            SetNotif("Veuillez remplir le nom avant !");
        }
        else if (prenom_Input.text == "")
        {
            SetNotif("Veuillez remplir le prénom avant !");
        }
        else if (etablissement_Input.text == "")
        {
            SetNotif("Veuillez renseigner l'établissement avant !");
        }
        else 
        {
            user = new User(u_genre, u_name, u_firstName, u_matiere, u_etablissement);

            SaveUserInPlayerPrefs(user);
            GameManager.instance.currentUser = user;
            if (u_genre == Genre.femme)
            {
                bienvenueName.text = "Madame " + u_name;
            }
            else
            {
                bienvenueName.text = "Monsieur " + u_name;
            }
            StartCoroutine(nextScene());
        }
    }
    private void SaveUserInPlayerPrefs(User user)
    {
        LoadAndSaveWithJSON.instance.SaveUser_JSON(user);
    }


    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(timeBetweenScenes);
        SceneManager.LoadScene("MainMenuScene");

    }
}

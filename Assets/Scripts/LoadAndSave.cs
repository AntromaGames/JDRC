using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public  class LoadAndSave :MonoBehaviour
{

    public static LoadAndSave instance;
    public int numberOfComp;

    private void Awake()
    {
        MakeSingleton();
        numberOfComp = PlayerPrefs.GetInt("numberOfCompetences");
    }
    private void Start()
    {

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
    #region User
    public  void SaveUser(User user)
    {
        PlayerPrefs.SetString("user_name", user.name);
        PlayerPrefs.SetString("user_firstname", user.firstName);
        PlayerPrefs.SetString("user_etablissement", user.etablissement);
        PlayerPrefs.SetString("user_genre", user.genre.ToString());
        PlayerPrefs.SetString("user_matiere", user.matiere);
    }
    public User GetUser()
    {
        Genre userGenre = new Genre();
        if (PlayerPrefs.GetString("user_genre") == "femme")
        {
            userGenre = Genre.femme;
        }
        else
        {
            userGenre = Genre.homme;
        }
        User user = new User(
            userGenre,
            PlayerPrefs.GetString("user_name"),
            PlayerPrefs.GetString("user_firstname"),
            PlayerPrefs.GetString("user_matiere"),
            PlayerPrefs.GetString("user_etablissement")
            );
        return user;
    }


    #endregion


    #region liste d élèves


    public  void SaveList()
    {
        foreach (Eleve e in GameManager.instance.eleves)
        {
            PlayerPrefs.SetString("eleve_name"+e.id,e.nom);
            PlayerPrefs.SetString("eleve_firstname" + e.id, e.prenom);
            PlayerPrefs.SetString("eleve_classe" + e.id, e.classe);
            PlayerPrefs.SetInt("eleve_id" + e.id, e.id);
            PlayerPrefs.SetInt("eleve_xp" + e.id, e.xp);
            PlayerPrefs.SetInt("eleve_niveau" + e.id, e.niveau);
            PlayerPrefs.SetInt("eleve_level" + e.id, e.level);
            if(e.competences!=null)
            {
                foreach (Competence c in e.competences)
                {
                    if (!string.IsNullOrEmpty(c.title))
                    {
                        PlayerPrefs.SetString("eleve_"+ e.id +"_competence"  + c.title, c.title);
                        PlayerPrefs.SetInt("eleve_"+e.id+ "_competence_value" + c.title, c.value);
                    }
                }
            }

        }
    }
    //public Eleve(string _prenom, string _nom, string _classe, int _id, int _xp, int _level, int _niveau)
    public  List<Eleve> GetElevesList()
    {
        List<Eleve> liste = new List<Eleve>();

        if (PlayerPrefs.HasKey("numberOfEleves"))
        {
            int amountOfEleves = PlayerPrefs.GetInt("numberOfEleves");

            if (amountOfEleves != 0)
            {

                for(int i =0; i < amountOfEleves; i++)
                {
                    Eleve eleve = new Eleve(

                    PlayerPrefs.GetString("eleve_firstname" + i),
                    PlayerPrefs.GetString("eleve_name" + i),
                    PlayerPrefs.GetString("eleve_classe" + i),
                    PlayerPrefs.GetInt("eleve_id" + i),
                    PlayerPrefs.GetInt("eleve_xp" + i),
                    PlayerPrefs.GetInt("eleve_level" + i),
                    PlayerPrefs.GetInt("eleve_niveau" + i));


                    if (GameManager.instance.competences != null)
                    {
                        foreach (Competence c in GameManager.instance.competences)
                        {
                            if (c.niveau == eleve.niveau)
                            {

                                Debug.Log("compétence " + c.title + "ajouté à l'élève " + eleve.prenom);
                                if (eleve.competences == null)
                                {
                                    eleve.competences = new List<Competence>();
                                    eleve.competences.Add(c);

                                    eleve.competences.Find(Competence => Competence.title == c.title).value = PlayerPrefs.GetInt("eleve_" + eleve.id + "_competence_value" + c.title);
                                    //PlayerPrefs.SetString("eleve_" + e.id + "_competence" + c.title, c.title);
                                    //PlayerPrefs.SetInt("eleve_" + e.id + "_competence_value" + c.title, c.value);


                                }
                                else if (!eleve.competences.Contains(c))
                                {
                                    eleve.competences.Add(c);
                                    eleve.competences.Find(Competence => Competence.title == c.title).value = PlayerPrefs.GetInt("eleve_" + eleve.id + "_competence_value" + c.title);

                                }
                                else
                                {
                                    eleve.competences.Find(Competence => Competence.title == c.title).value = PlayerPrefs.GetInt("eleve_" + eleve.id + "_competence_value" + c.title);

                                }

                            }

                        }
                    }
                    liste.Add(eleve);

                }

            }
        }
        return liste;
    }

    #endregion


    #region Competences 
    public void SaveCompetence(Competence comp)
    {

        PlayerPrefs.SetString("competence_" + GameManager.instance.competences.Count + "_title", comp.title);
        PlayerPrefs.SetString("competence_" + GameManager.instance.competences.Count + "_description", comp.description);
        PlayerPrefs.SetInt("competence_" + GameManager.instance.competences.Count + "_niveau", comp.niveau);
        PlayerPrefs.SetInt("competence_" + GameManager.instance.competences.Count + "_maxValue", comp.maxValue);
        PlayerPrefs.SetString("competence_" + GameManager.instance.competences.Count + "_iconPath", comp.iconPath);
        numberOfComp++;
        PlayerPrefs.SetInt("numberOfCompetences", numberOfComp);
    }
    public List<Competence> GetCompetences()
    {
        if (numberOfComp != 0)
        {
            List<Competence> newList = new List<Competence>();

            for (int i = 0; i < numberOfComp; i++)
            {
                Competence comp = new Competence( /// Competence (string _title,string _description, int _niveau, int _value, int _maxValue)
                PlayerPrefs.GetString("competence_" + i + "_title"),
                PlayerPrefs.GetString("competence_" + i + "_description"),
                PlayerPrefs.GetInt("competence_" + i + "_niveau"),
                0,
                PlayerPrefs.GetInt("competence_" + i + "_maxValue"));


                comp.iconPath = PlayerPrefs.GetString("competence_" + i + "_iconPath");


                Sprite icon = LoadNewSprite(comp.iconPath, 100);

                comp.icon = icon;
                newList.Add(comp);
            }
            Debug.Log("il y a " + numberOfComp + " a ajouter au game manager");
            return newList;
        }
        else
        {
            Debug.Log("aucune competence trouvée ");
            return null;
        }

    }

    public Sprite LoadNewSprite(string FilePath, float PixelsPerUnit = 100.0f, SpriteMeshType spriteType = SpriteMeshType.Tight)
    {

        // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference

        Texture2D SpriteTexture = LoadTexture(FilePath);
        if(SpriteTexture != null)
        {
            Sprite NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0, 0), PixelsPerUnit, 0, spriteType);

            return NewSprite;
        }
        else
        {
            return null;
        }

    }

    public Sprite ConvertTextureToSprite(Texture2D texture, float PixelsPerUnit = 100.0f, SpriteMeshType spriteType = SpriteMeshType.Tight)
    {
        // Converts a Texture2D to a sprite, assign this texture to a new sprite and return its reference

        Sprite NewSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0, 0), PixelsPerUnit, 0, spriteType);

        return NewSprite;
    }

    public Texture2D LoadTexture(string FilePath)
    {

        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                return Tex2D;                 // If data = readable -> return texture
        }
        return null;                     // Return null if load failed
    }
    #endregion
}


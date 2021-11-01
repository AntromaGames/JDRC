using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public User currentUser;

    public List<Eleve> eleves;

    public Eleve currentEleve { get; internal set; }

    public List<Competence> competences;

    private void Awake()
    {
        MakeSingleton();
    }
    // Start is called before the first frame update
    void Start()
    {
        competences = LoadAndSave.instance.GetCompetences();

        eleves = LoadAndSave.instance.GetElevesList();

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


    public void AddEleves(List<Eleve>  eleves)
    {
        if(competences != null)
        {
            foreach( Eleve e in eleves)
            {
                foreach(Competence c in competences)
                {
                    if(c.niveau == e.niveau && !e.competences.Contains(c))
                    {
                        Debug.Log("compétence " + c.title + "ajouté à l'élève " + e.prenom);
                        e.competences.Add(c);
                    }

                }
            }
        }
        eleves.AddRange(eleves);
    }

    public void GiveCompToEleves()
    {
        if (competences != null)
        {
            foreach (Eleve e in eleves)
            {
                foreach (Competence c in competences)
                {
                    if (c.niveau == e.niveau)
                    {

                        Debug.Log("compétence " + c.title + "ajouté à l'élève " + e.prenom);
                        if(e.competences == null)
                        {
                            e.competences = new List<Competence>();
                            e.competences.Add(c);
                        }
                        else if(!e.competences.Contains(c))
                        {
                            e.competences.Add(c);
                        }
                        else
                        {
                            return;
                        }

                    }

                }
            }
        }
    }

}

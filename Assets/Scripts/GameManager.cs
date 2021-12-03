using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<Table> plan;

    public User currentUser;

    public List<Eleve> eleves;

    public Eleve currentEleve;


    public List<Competence> competences;
    public List<Power> powers;

    public string currentClasse;

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
    // Start is called before the first frame update
    void Start()
    {
        competences = LoadAndSaveWithJSON.instance.GetCompetences();
        powers = LoadAndSaveWithJSON.instance.GetPowers();
        eleves = LoadAndSaveWithJSON.instance.GetEleveList();
        plan = LoadAndSaveWithJSON.instance.GetTables();
        currentUser = UserEdit.instance.user;
    }

    public void AddTableToPlan(Table table)
    {

        Debug.Log("Ajout d'une table au plan");
        if(plan == null)
        {
            plan = new List<Table>();
        }

        table.index = plan.Count;
        plan.Add(table);
        LoadAndSaveWithJSON.instance.SaveTables(table);
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
        if (powers != null)
        {
            foreach (Eleve e in eleves)
            {
                foreach (Power c in powers)
                {
                    if (c.niveau == e.niveau && !e.powers.Contains(c))
                    {
                        Debug.Log("power " + c.title + "ajouté à l'élève " + e.prenom);
                        e.powers.Add(c);
                    }
                }
            }
        }
        eleves.AddRange(eleves);
    }
    public void GivePowersToEleves()
    {
        if (powers != null)
        {
            foreach (Eleve e in eleves)
            {
                foreach (Power p in powers)
                {
                    if (p.niveau == e.niveau)
                    {

                        Debug.Log("powers " + p.title + "ajouté à l'élève " + e.prenom);
                        if (e.powers == null)
                        {
                            e.powers = new List<Power>();

                            Power po = new Power(p.title, p.description, p.level, p.niveau, p.isUsed);
                            e.powers.Add(po);

                        }
                        else if (!e.powers.Contains(p))
                        {

                            Power po = new Power(p.title, p.description, p.level, p.niveau, p.isUsed);
                            e.powers.Add(po);
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
                            Competence newComp = new Competence(c.title, c.description, c.niveau, c.value, c.maxValue);

                            e.competences.Add(newComp);
                        }
                        else if(!e.competences.Contains(c))
                        {
                            Competence newComp = new Competence(c.title, c.description, c.niveau, c.value, c.maxValue);

                            e.competences.Add(newComp);
                        }
                    }

                }
            }
        }
    }

}

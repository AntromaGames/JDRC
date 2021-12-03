using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class LoadAndSaveWithJSON : MonoBehaviour
{

    public static LoadAndSaveWithJSON instance;

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
    #region User
    public void SaveUser_JSON(User user)
    {
        if (File.Exists(Application.dataPath + @"\Save_User.csv"))
        {
            File.Delete(Application.dataPath + @"\Save_User.csv");
        }

        object[] objArray2 = new object[10];
        objArray2[0] = user.name;
        objArray2[1] = ";";
        objArray2[2] = user.firstName;
        objArray2[3] = ";";
        objArray2[4] = user.etablissement;
        objArray2[5] = ";";
        objArray2[6] = user.genre.ToString();
        objArray2[7] = ";";
        objArray2[8] = user.matiere;
        objArray2[9] = "\n";
        File.AppendAllText(Application.dataPath + @"\Save_User.csv", string.Concat((object[])objArray2));

    }
    public User GetUser()
    {
        if (!File.Exists(Application.dataPath + @"\Save_User.csv"))
        {
            return null;
        }
        else
        {
            User user = new User(Genre.femme, "name", "firstName", "matiere", "etablissement");
            string message = Application.dataPath + @"\Save_User.csv";
            IEnumerable<string> enumerable1 = File.ReadLines(message, Encoding.UTF8);
            if (enumerable1 == null)
            {
                Debug.Log("Le fichier \"Save_User\" n' est pas  à l'emplacement : " + message);
            }
            string[] strArray = Enumerable.ToList<string>(enumerable1).ToArray();
            Debug.Log(message);
            for (int i = 0; i < strArray.Length; i++)
            {
                Debug.Log(strArray[i]);
                char[] separator = new char[] { ';' };
                string[] strArray2 = strArray[i].Split(separator);
                if (strArray2[1] != "")
                {
                    user = new User(
                    strArray2[3] == Genre.femme.ToString() ? Genre.femme : Genre.homme,
                    strArray2[1],
                    strArray2[0],
                    strArray2[4],
                    strArray2[2]);
                    Debug.Log("user " + user.name + "chargé");
                }
                else
                {

                    Debug.Log("pas d'user enregistré");
                }
            }
            return user;
        }
    }


    #endregion

    #region liste d élèves

    public void SaveList()
    {
        if (File.Exists(Application.dataPath + @"\eleves.csv"))
        {
            File.Delete(Application.dataPath + @"\eleves.csv");

        }
        foreach (Eleve e in GameManager.instance.eleves)
        {
            //Debug.Log("il y a " + e.powers.Count + " pouvoir par élève et " + e.competences.Count + "competences");
            int powerAmount = 0;
            if (e.powers != null)
            {
                powerAmount = e.powers.Count;
            }

            int compAmount = 0;
            if (e.competences != null)
            {
                compAmount = e.competences.Count;
            }

            Debug.Log("il y a " + powerAmount + " powerAmount " + compAmount + "compamount");

            object[] objArray2 = new object[22 +  powerAmount +  compAmount];
            objArray2[0] = e.nom;
            objArray2[1] = ";";
            objArray2[2] = e.prenom;
            objArray2[3] = ";";
            objArray2[4] = e.classe;
            objArray2[5] = ";";
            objArray2[6] = (int)e.id;
            objArray2[7] = ";";
            objArray2[8] = (int)e.xp;
            objArray2[9] = ";";
            objArray2[10] = (int)e.niveau;
            objArray2[11] = ";";
            objArray2[12] = (int)e.level;
            objArray2[13] = ";";
            objArray2[14] = (int)e.table;
            objArray2[15] = ";";
            objArray2[16] = e.photoPath;
            objArray2[17] = ";";
            objArray2[18] = e.direction.ToString();
            objArray2[19] = ";";
            if (powerAmount != 0)
            {
                for (int i = 0; i < powerAmount; i++)
                {
                    objArray2[20 + i] = e.powers[i].title.ToString() + " power : " + e.powers[i].isUsed.ToString() + ";";
                }
            }
            if (compAmount != 0)
            {
                for (int i = 0; i < compAmount; i++)
                {
                    objArray2[21 + powerAmount + i] = "competence " + e.competences[i].title + " " + e.competences[i].value.ToString() + ";" ;
                }
            }
            objArray2[21 + powerAmount + compAmount] = "\n";

            File.AppendAllText(Application.dataPath + @"\eleves.csv", string.Concat((object[])objArray2));
        }
    }

    //public Eleve(string _prenom, string _nom, string _classe, int _id, int _xp, int _level, int _niveau)

    public List<Eleve> GetEleveList()
    {
        List<Eleve> liste = new List<Eleve>();
        if (!File.Exists(Application.dataPath + @"\eleves.csv"))
        {
            return new List<Eleve>();
        }
        else
        {
            string message = Application.dataPath + @"\eleves.csv";
            IEnumerable<string> enumerable1 = File.ReadLines(message, Encoding.UTF8);
            if (enumerable1 == null)
            {
                Debug.Log("Le fichier \"eleves\" est pas à  l'emplacement : " + message);
            }
            string[] strArray = Enumerable.ToList<string>(enumerable1).ToArray();
            Debug.Log(message);
            for (int i = 0; i < strArray.Length; i++)
            {
                Debug.Log(strArray[i]);
                char[] separator = new char[] { ';' };
                string[] strArray2 = strArray[i].Split(separator);
                List<string> compString = new List<string>();
                List<string> powerString = new List<string>();

                for (int j = 0; j < strArray2.Length; j++)
                {
                    if (strArray2[j].Contains("power"))
                    {
                        powerString.Add(strArray2[j]);

                    }
                }
                for (int j = 0; j < strArray2.Length; j++)
                {
                    if (strArray2[j].Contains("competence"))
                    {
                        compString.Add(strArray2[j]);
                    }
                }
                if (strArray2[1] != "")
                {
                    Eleve eleve = new Eleve(
                        strArray2[1],
                        strArray2[0],
                        strArray2[2],
                       int.Parse(strArray2[3]),
                       int.Parse(strArray2[4]),
                       int.Parse(strArray2[6]),
                       int.Parse(strArray2[5])
                       );
                    eleve.table = int.Parse(strArray2[7]);
                    eleve.photoPath = strArray2[8];

                    if (strArray2[9] == ChaiseDirection.alone.ToString())
                    {
                        eleve.direction = ChaiseDirection.alone;
                    }
                    else if (strArray2[9] == ChaiseDirection.left.ToString())
                    {
                        eleve.direction = ChaiseDirection.left;
                    }
                    else
                    {
                        eleve.direction = ChaiseDirection.right;
                    }


                    /*if (GameManager.instance.competences != null)
                    {
                        foreach (Competence c in GameManager.instance.competences)
                        {
                            if (c.niveau == eleve.niveau)
                            {

                                Debug.Log("compétence " + c.title + "ajouté à l'élève " + eleve.prenom);
                                if (eleve.competences == null)
                                {
                                    eleve.competences = new List<Competence>();
                                    Competence nexComp = new Competence(c.title, c.description, c.niveau, 0, c.maxValue);
                                    nexComp.value = PlayerPrefs.GetInt("eleve_" + eleve.id + "_competence_value" + c.title);
                                    eleve.competences.Add(nexComp);

                                    PlayerPrefs.SetString("eleve_" + e.id + "_competence" + c.title, c.title);
                                    PlayerPrefs.SetInt("eleve_" + e.id + "_competence_value" + c.title, c.value);
                                }
                                else if (!eleve.competences.Contains(c))
                                {
                                    Competence nexComp = new Competence(c.title, c.description, c.niveau, 0, c.maxValue);
                                    nexComp.value = PlayerPrefs.GetInt("eleve_" + eleve.id + "_competence_value" + c.title);
                                    eleve.competences.Add(nexComp);

                                }
                                else
                                {
                                    Competence nexComp = new Competence(c.title, c.description, c.niveau, 0, c.maxValue);
                                    nexComp.value = PlayerPrefs.GetInt("eleve_" + eleve.id + "_competence_value" + c.title);
                                    eleve.competences.Add(nexComp);
                                }

                            }

                        }
                    }
                    if (GameManager.instance.powers != null)
                    {
                        foreach (Power p in GameManager.instance.powers)
                        {
                            if (p.niveau == eleve.niveau)
                            {
                                Debug.Log("pouvoir " + p.title + "ajouté à l'élève " + eleve.prenom);
                                if (eleve.powers == null)
                                {
                                    eleve.powers = new List<Power>();

                                    Power newPow = new Power(p.title, p.description, p.level, p.niveau, bool.Parse(PlayerPrefs.GetString("eleve_" + eleve.id + "_power_isUsed" + p.title)));
                                    eleve.powers.Add(newPow);
                                    //eleve.powers.Find(Power => Power.title == p.title).isUsed = bool.Parse(PlayerPrefs.GetString("eleve_" + eleve.id + "_power_isUsed" + p.title));
                                    //PlayerPrefs.SetString("eleve_" + e.id + "_competence" + c.title, c.title);
                                    //PlayerPrefs.SetInt("eleve_" + e.id + "_competence_value" + c.title, c.value);
                                }
                                else if (!eleve.powers.Contains(p))
                                {
                                    Power newPow = new Power(p.title, p.description, p.level, p.niveau, bool.Parse(PlayerPrefs.GetString("eleve_" + eleve.id + "_power_isUsed" + p.title)));
                                    eleve.powers.Add(newPow);
                                }
                                else
                                {
                                    Power newPow = new Power(p.title, p.description, p.level, p.niveau, bool.Parse(PlayerPrefs.GetString("eleve_" + eleve.id + "_power_isUsed" + p.title)));
                                    eleve.powers.Add(newPow);
                                }

                            }

                        }*/


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

                                    Competence nexComp = new Competence(c.title, c.description, c.niveau, 0, c.maxValue);
                                    string goodComp = compString.Find(goodString => goodString.Contains(c.title));
                                    Debug.Log(goodComp + "est le mot de la competence avant operation");
                                    goodComp = goodComp.Remove(0,11+c.title.Count());
                                    Debug.Log(goodComp + "est le mot de la competence ");
                                    nexComp.value = int.Parse(goodComp);
                                    eleve.competences.Add(nexComp);

                                }
                                else if (!eleve.competences.Contains(c))
                                {
                                    Competence nexComp = new Competence(c.title, c.description, c.niveau, 0, c.maxValue);
                                    string goodComp = compString.Find(goodString => goodString.Contains(c.title));
                                    Debug.Log(goodComp + "est le mot de la competence avant operation");
                                    goodComp = goodComp.Remove(0, 11 + c.title.Count());
                                    Debug.Log(goodComp + "est le mot de la competence ");
                                    nexComp.value = int.Parse(goodComp);
                                    eleve.competences.Add(nexComp);

                                }
                                else
                                {
                                    Competence nexComp = new Competence(c.title, c.description, c.niveau, 0, c.maxValue);
                                    string goodComp = compString.Find(goodString => goodString.Contains(c.title));
                                    Debug.Log(goodComp + "est le mot de la competence avant operation");
                                    goodComp = goodComp.Remove(0, 11 + c.title.Count());
                                    Debug.Log(goodComp + "est le mot de la competence ");
                                    nexComp.value = int.Parse(goodComp);
                                    eleve.competences.Add(nexComp);
                                }

                            }

                        }
                    }


                    //List<Competence> comps = GameManager.instance.competences.FindAll(Competence => Competence.niveau == eleve.niveau);
                    //for (int k = 0; k < compString.Count; k++)
                    //{

                    //    comps[k].value = int.Parse(compString[k].Remove(0, 10));
                    //}

                    //if (eleve.competences != null)
                    //{
                    //    eleve.competences.AddRange(comps);
                    //}
                    //else
                    //{
                    //    eleve.competences = new List<Competence>();
                    //    eleve.competences.AddRange(comps);
                    //}

                    //List<Power> pows = GameManager.instance.powers.FindAll(Power => Power.niveau == eleve.niveau);


                    if (GameManager.instance.powers != null)
                    {
                        foreach (Power p in GameManager.instance.powers)
                        {
                            if (p.niveau == eleve.niveau)
                            {
                                Debug.Log("pouvoir " + p.title + "ajouté à l'élève " + eleve.prenom);
                                if (eleve.powers == null)
                                {
                                    eleve.powers = new List<Power>();

                                    string goodBool = powerString.Find(goodString => goodString.StartsWith(p.title));


                                    var lastWord = goodBool.Split(' ').Last();
                                    Debug.Log("goodBool string is " + goodBool + " and his boolean is " + lastWord);

                                    Power newPow = new Power(p.title, p.description, p.level, p.niveau, bool.Parse(lastWord));
                                    eleve.powers.Add(newPow);
                                    //eleve.powers.Find(Power => Power.title == p.title).isUsed = bool.Parse(PlayerPrefs.GetString("eleve_" + eleve.id + "_power_isUsed" + p.title));
                                    //PlayerPrefs.SetString("eleve_" + e.id + "_competence" + c.title, c.title);
                                    //PlayerPrefs.SetInt("eleve_" + e.id + "_competence_value" + c.title, c.value);
                                }
                                else if (!eleve.powers.Contains(p))
                                {
                                    string goodBool = powerString.Find(goodString => goodString.StartsWith(p.title));


                                    var lastWord = goodBool.Split(' ').Last();
                                    Debug.Log("goodBool string is " + goodBool + " and his boolean is " + lastWord);
                                    Power newPow = new Power(p.title, p.description, p.level, p.niveau, bool.Parse(lastWord));
                                    eleve.powers.Add(newPow);
                                }
                                else
                                {
                                    string goodBool = powerString.Find(goodString => goodString.StartsWith(p.title));


                                    var lastWord = goodBool.Split(' ').Last();
                                    Debug.Log("goodBool string is " + goodBool + " and his boolean is " + lastWord);
                                    Power newPow = new Power(p.title, p.description, p.level, p.niveau, bool.Parse(lastWord));
                                    eleve.powers.Add(newPow);
                                }

                            }

                        }



                        //for (int k = 0; k < powerString.Count; k++)
                        //{
                        //    Debug.Log(" power boolean is " + powerString[k].Remove(0, 7));
                        //    pows[k].isUsed = bool.Parse(powerString[k].Remove(0, 7));
                        //}

                        //if (eleve.powers != null)
                        //{
                        //    eleve.powers.AddRange(pows);
                        //}
                        //else
                        //{
                        //    eleve.powers = new List<Power>();
                        //    eleve.powers.AddRange(pows);
                        //}

                        liste.Add(eleve);
                    }
                    else
                    {
                        Debug.Log("le fichier commence par un blanc ");
                    }
                }
            }
                return liste;
            
        } }


        #endregion


        #region Competences 

        public void SaveCompetence(Competence comp)
        {
            if (File.Exists(Application.dataPath + @"\competences.csv"))
            {
                File.Delete(Application.dataPath + @"\competences.csv");
            }
            foreach (Competence c in GameManager.instance.competences)
            {
                object[] objArray2 = new object[10];
                objArray2[0] = c.title;
                objArray2[1] = ";";
                objArray2[2] = c.description;
                objArray2[3] = ";";
                objArray2[4] = (int)c.niveau;
                objArray2[5] = ";";
                objArray2[6] = (int)c.value;
                objArray2[7] = ";";
                objArray2[8] = (int)c.maxValue;
                objArray2[9] = "\n";
                File.AppendAllText(Application.dataPath + @"\competences.csv", string.Concat((object[])objArray2));
            }
        }
        public List<Competence> GetCompetences()
        {
            List<Competence> liste = new List<Competence>();

            if (!File.Exists(Application.dataPath + @"\competences.csv"))
            {
                return new List<Competence>();
            }
            else
            {
                string message = Application.dataPath + @"\competences.csv";

                IEnumerable<string> enumerable1 = File.ReadLines(message, Encoding.UTF8);
                if (enumerable1 == null)
                {
                    Debug.Log("Le fichier \"competences\" n' est pas  à l'emplacement : " + message);
                }
                string[] strArray = Enumerable.ToList<string>(enumerable1).ToArray();
                Debug.Log(message);
                for (int i = 0; i < strArray.Length; i++)
                {
                    Debug.Log(strArray[i]);
                    char[] separator = new char[] { ';' };
                    string[] strArray2 = strArray[i].Split(separator);

                    Competence comp = new Competence("title", "descri", 0, 0, 0);
                    if (strArray2[0] != "")
                    {
                        comp = new Competence(
                        strArray2[0],
                        strArray2[1],
                        int.Parse(strArray2[2]),
                        int.Parse(strArray2[3]),
                        int.Parse(strArray2[4]));
                        Debug.Log("competence " + comp.title + "chargé");
                    }
                    else
                    {
                        Debug.Log("pas de competences enregistré");
                    }

                    liste.Add(comp);
                }

                return liste;
            }
        }

        #endregion


        #region Powers 
        public void SavePower(Power po)
        {
            if (File.Exists(Application.dataPath + @"\pouvoirs.csv"))
            {
                File.Delete(Application.dataPath + @"\pouvoirs.csv");
            }
            foreach (Power p in GameManager.instance.powers)
            {
                object[] objArray2 = new object[10];
                objArray2[0] = p.title;
                objArray2[1] = ";";
                objArray2[2] = p.description;
                objArray2[3] = ";";
                objArray2[4] = (int)p.niveau;
                objArray2[5] = ";";
                objArray2[6] = (int)p.level;
                objArray2[7] = ";";
                objArray2[8] = (bool)p.isUsed;
                objArray2[9] = "\n";
                File.AppendAllText(Application.dataPath + @"\pouvoirs.csv", string.Concat((object[])objArray2));
            }
        }
        public List<Power> GetPowers()
        {
            if (!File.Exists(Application.dataPath + @"\pouvoirs.csv"))
            {
                return new List<Power>();
            }
            else
            {

                List<Power> liste = new List<Power>();
                string message = Application.dataPath + @"\pouvoirs.csv";

                IEnumerable<string> enumerable1 = File.ReadLines(message, Encoding.UTF8);
                if (enumerable1 == null)
                {
                    Debug.Log("Le fichier \"pouvoirs\" n' est pas  à l'emplacement : " + message);
                }
                string[] strArray = Enumerable.ToList<string>(enumerable1).ToArray();
                Debug.Log(message);
                for (int i = 0; i < strArray.Length; i++)
                {
                    Debug.Log(strArray[i]);
                    char[] separator = new char[] { ';' };
                    string[] strArray2 = strArray[i].Split(separator);

                    Power pow = new Power("title", "descri", 0, 0, false);
                    if (strArray2[1] != "")
                    {
                        pow = new Power(
                        strArray2[0],
                        strArray2[1],
                        int.Parse(strArray2[3]),
                        int.Parse(strArray2[2]),
                        bool.Parse(strArray2[4]));
                        Debug.Log("pouvoir " + pow.title + "chargé");
                    }
                    else
                    {
                        Debug.Log("pas de pouvoir enregistré");
                    }

                    liste.Add(pow);
                }

                return liste;
            }
        }
        #endregion

        #region Tables 

        public void SaveTables(Table table)
        {
            if (File.Exists(Application.dataPath + @"\tables.csv"))
            {
                File.Delete(Application.dataPath + @"\tables.csv");
            }
            foreach (Table t in GameManager.instance.plan)
            {
                object[] objArray2 = new object[10];
                objArray2[0] = t.index;
                objArray2[1] = ";";
                objArray2[2] = t.issimple.ToString();
                objArray2[3] = ";";
                objArray2[4] = t.rotation;
                objArray2[5] = ";";
                objArray2[6] = t.xPos;
                objArray2[7] = ";";
                objArray2[8] = t.yPos;
                objArray2[9] = "\n";
                File.AppendAllText(Application.dataPath + @"\tables.csv", string.Concat((object[])objArray2));
            }
        }

        public List<Table> GetTables()
        {
            List<Table> liste = new List<Table>();
            if (!File.Exists(Application.dataPath + @"\tables.csv"))
            {
                return new List<Table>();
            }
            else
            {
                string message = Application.dataPath + @"\tables.csv";

                IEnumerable<string> enumerable1 = File.ReadLines(message, Encoding.UTF8);
                if (enumerable1 == null)
                {
                    Debug.Log("Le fichier \"tables\" n' est pas  à l'emplacement : " + message);
                }
                string[] strArray = Enumerable.ToList<string>(enumerable1).ToArray();
                Debug.Log(message);
                for (int i = 0; i < strArray.Length; i++)
                {
                    Debug.Log(strArray[i]);
                    char[] separator = new char[] { ';' };
                    string[] strArray2 = strArray[i].Split(separator);

                    Table tab = new Table(0, 0, 0, 0, false);
                    if (strArray2[1] != "")
                    {
                        tab = new Table(
                        int.Parse(strArray2[0]),
                        float.Parse(strArray2[3]),
                        float.Parse(strArray2[4]),
                        float.Parse(strArray2[2]),
                        bool.Parse(strArray2[1]));
                        Debug.Log("table " + tab.index + "chargé");
                    }
                    else
                    {
                        Debug.Log("pas de pouvoir enregistré");
                    }

                    liste.Add(tab);
                }

                return liste;

            }

        }
        #endregion
    
}

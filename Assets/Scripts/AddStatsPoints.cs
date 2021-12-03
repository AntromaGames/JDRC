using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AddStatsPoints : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI competenceText;
    public void AddstatPoint(int amount)
    {
        Eleve thisEleve = GameManager.instance.eleves.Find(Eleve => Eleve.id == GameManager.instance.currentEleve.id);
        Debug.Log(thisEleve.nom + " " + thisEleve.prenom + "gagne" + amount + " pt de competence  :" + competenceText.text);
        // GameManager.instance.currentEleve.competences.Find(Competence => Competence.title == competenceText.text).value += amount;
        thisEleve.competences.Find(Competence => Competence.title == competenceText.text).value += amount;
        GetComponentInChildren<Slider>().value += amount;
        LoadAndSaveWithJSON.instance.SaveList();

    }
}

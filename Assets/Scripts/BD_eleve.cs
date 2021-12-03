using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BD_eleve : MonoBehaviour
{

    [SerializeField] Image photo;
    [SerializeField] TextMeshProUGUI _prenom;
    [SerializeField] TextMeshProUGUI _nom;
    [SerializeField] TextMeshProUGUI classe;
    [SerializeField] Image bgnd;

    public void PopulateDatas(Eleve e,Color c)
    {
        photo.sprite = e.photo;
        _prenom.text = e.prenom;
        _nom.text = e.nom;
        bgnd.color = c;
    }

    public void Delete()
    {
        string eleveName = _prenom.text + " " + _nom.text;
        Eleve oneToremove = GameManager.instance.eleves.Find(Eleve => Eleve.prenom + " " + Eleve.nom == eleveName);

        if (oneToremove != null)
        {
            Debug.Log("Removing " + eleveName + " from gameManagerList");
            GameManager.instance.eleves.Remove(oneToremove);
            LoadAndSaveWithJSON.instance.SaveList();
            Destroy(gameObject);
        }

    }

}

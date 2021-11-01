using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompetenceCreatorController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI c_level;
    [SerializeField] TMP_InputField c_max;
    [SerializeField] TMP_InputField c_name;
    [SerializeField] TMP_InputField c_description;
    public Sprite c_sprite;
    public string iconPath;

    // Start is called before the first frame update
    void Start()
    {
        ClearAllInputs();

    }
    private void ClearAllInputs()
    {
        c_max.text = "100";
        c_name.text = "";
    }

    public void SetCompetence()
    {
        int level = 0;
        switch (c_level.text)
        {
            case "3eme":
                level = 3;
                break;
            case "4eme":
                level = 4;
                break;
            case "5eme":
                level = 5;
                break;
            case "6eme":
                level = 6;
                break;
        }

        Competence comp = new Competence(c_name.text, c_description.text, level, 0, int.Parse(c_max.text));
        if(c_sprite != null)
        {
            comp.icon = c_sprite;
        }
        if (iconPath != null)
        {
            comp.iconPath = iconPath;
        }
        Debug.Log("Compétence :" + c_name + " ajouté ");

        if(GameManager.instance.competences != null)
        {
            GameManager.instance.competences.Add(comp);
        }
        else
        {
            GameManager.instance.competences = new List<Competence>();
            GameManager.instance.competences.Add(comp);
        }

        GameManager.instance.GiveCompToEleves();
        LoadAndSave.instance.SaveCompetence(comp);

    }
}

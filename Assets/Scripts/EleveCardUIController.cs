using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class EleveCardUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI classeText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI xpText;

    [SerializeField] Slider levelindicatorSlider;

    [SerializeField] TextMeshProUGUI LevelprogressionText;

    public Transform statPanelTransform;



    public GameObject statPrefab;


    public Color[] colors;
    int colorIndex = 0;

    private void OnEnable()
    {
        Eleve eleve = GameManager.instance.currentEleve;
        nameText.text = eleve.nom + " " + eleve.prenom;
        classeText.text = eleve.classe;
        levelText.text = eleve.level.ToString();

        RefreshUi();

    }

    private void Display(Competence c)
    {
        GameObject stat = Instantiate(statPrefab, statPanelTransform);
        stat.GetComponentInChildren<TextMeshProUGUI>().text = c.title;
        Transform fillBAr = stat.transform.Find("Slider_Green").transform.Find("Fill");

        fillBAr.gameObject.GetComponent<Image>().color = colors[colorIndex];
        colorIndex++;
        stat.GetComponentInChildren<Slider>().maxValue = c.maxValue;
        stat.GetComponentInChildren<Slider>().value = c.value;
        Transform icon = stat.transform.Find("StatIcon");
        if(c.icon != null)
        icon.GetComponent<Image>().sprite = c.icon;
        icon.GetComponent<Image>().color = colors[colorIndex];
    }

    public void AddXP(int amount)
    {
        GameManager.instance.currentEleve.xp += amount;

        RefreshUi();
    }

    public void RefreshUi()
    {

        Eleve eleve = GameManager.instance.currentEleve;
        xpText.text = eleve.xp.ToString();
        colorIndex = 0;
        if (eleve.competences != null)
        {
            foreach (Competence c in eleve.competences)
            {
                Display(c);
            }
        }else
        {
            Debug.Log("vous n avez pas encore établi de compétences pour cette classe ");
        }


    }

}

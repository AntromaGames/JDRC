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
    [SerializeField] Image playerImage;

    [SerializeField] Slider levelindicatorSlider;

    [SerializeField] TextMeshProUGUI LevelprogressionText;

    [SerializeField] Transform statPanelTransform;
    [SerializeField] Transform powerTransform;



    public GameObject powerPrefab;


    public GameObject statPrefab;

    Eleve eleve;
    public Color[] colors;
    int colorIndex = 0;

    private void OnEnable()
    {
        eleve = GameManager.instance.currentEleve;
        nameText.text = eleve.nom + " " + eleve.prenom;
        classeText.text = eleve.classe;
        levelText.text = eleve.level.ToString();
        if(eleve.photo != null)
        {
            playerImage.sprite = eleve.photo;
        }
        SetPowers(eleve);
        if (eleve.competences != null)
        {
            colorIndex = 0;
            foreach (Competence c in eleve.competences)
            {
                Display(c);
                if(colorIndex == colors.Length-1)
                {
                    colorIndex = 0;
                }
                else
                {
                    colorIndex++;
                }

            }
        }
        else
        {
            Debug.Log("vous n avez pas encore établi de compétences pour cette classe ");
        }

        RefreshUi();
    }

    public void ClearUI()
    {
        Destroy(gameObject);
    }

    private void SetPowers(Eleve eleve)
    {
        if(eleve.powers != null && eleve.powers.Count > 0)
        {
            foreach (Power p in eleve.powers)
            {
                GameObject powerGo = Instantiate(powerPrefab, powerTransform);
                PowerController controller = powerGo.GetComponent<PowerController>();
                controller.power = p;
                controller.SetUI();
            }
        }

    }
    private void CheckForPowerUpgrade()
    {
        PowerController[] powers = FindObjectsOfType<PowerController>();
        for(int i =0; i< powers.Length; i++)
        {
            powers[i].SetUI();
        }
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
        CheckForLevelUpgrade(GameManager.instance.currentEleve.xp);
        CheckForPowerUpgrade();
        RefreshUi();
        LoadAndSaveWithJSON.instance.SaveList();

    }



    private void CheckForLevelUpgrade(int xp)
    {
     
        if( 0<=xp && xp < 500)
        {
            GameManager.instance.currentEleve.level = 1;
        }else if (500 <= xp && xp < 1000)
        {
            GameManager.instance.currentEleve.level = 2;
        }
        else if (1000 <= xp && xp < 1500)
        {
            GameManager.instance.currentEleve.level = 3;
        }
        else if (1500 <= xp && xp < 2000)
        {
            GameManager.instance.currentEleve.level = 4;
        }
        else if (2000 <= xp && xp < 2500)
        {
            GameManager.instance.currentEleve.level = 5;
        }
        else if (2500 <= xp && xp < 3000)
        {
            GameManager.instance.currentEleve.level = 6;
        }
        else if (3000 <= xp && xp < 3500)
        {
            GameManager.instance.currentEleve.level = 7;
        }
        else if (3500 <= xp && xp < 4000)
        {
            GameManager.instance.currentEleve.level = 8;
        }
        else if (4000 <= xp && xp < 4500)
        {
            GameManager.instance.currentEleve.level = 9;
        }
        else if (4500 <= xp && xp < 5000)
        {
            GameManager.instance.currentEleve.level = 10;
        }
        else if (5000 <= xp && xp < 5500)
        {
            GameManager.instance.currentEleve.level = 11;
        }
        else if (5500 <= xp && xp < 6000)
        {
            GameManager.instance.currentEleve.level = 12;
        }
        else if (6000 <= xp && xp < 6500)
        {
            GameManager.instance.currentEleve.level = 13;
        }
        else if (6500 <= xp && xp < 7000)
        {
            GameManager.instance.currentEleve.level = 14;
        }
        else if (7000 <= xp && xp < 7500)
        {
            GameManager.instance.currentEleve.level = 15;
        }
        else if (7500 <= xp && xp < 8000)
        {
            GameManager.instance.currentEleve.level = 16;
        }
        else if (8000 <= xp && xp < 8500)
        {
            GameManager.instance.currentEleve.level = 17;
        }
        else if (8500 <= xp && xp < 9000)
        {
            GameManager.instance.currentEleve.level = 18;
        }
        else if (9000 <= xp && xp < 10000)
        {
            GameManager.instance.currentEleve.level = 19;
        }
        else if (10000 <= xp && xp < 11000)
        {
            GameManager.instance.currentEleve.level = 20;
        }
    }

    public void RefreshUi()
    {


        xpText.text = eleve.xp.ToString();
        levelText.text = eleve.level.ToString();
        int reste = (eleve.xp % 500);
        LevelprogressionText.text = (reste / 5).ToString();
        levelindicatorSlider.value = (reste/5);


        //if (eleve.competences != null)
        //{
        //    foreach (Competence c in eleve.competences)
        //    {

        //    }
        //}else
        //{
        //    Debug.Log("vous n avez pas encore établi de compétences pour cette classe ");
        //}
    }


}

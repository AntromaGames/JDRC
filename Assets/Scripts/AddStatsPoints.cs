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
        foreach(Competence c in GameManager.instance.currentEleve.competences)
        {
            if(c.title == competenceText.text)
            {
                c.value += amount;
            }
        }
        FindObjectOfType<EleveCardUIController>().RefreshUi();
    }
}

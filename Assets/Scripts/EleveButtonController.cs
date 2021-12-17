using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EleveButtonController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;

    public void SelectThisEleve()
    {
        string identite = nameText.text;

        GameManager.instance.currentEleve = GameManager.instance.eleves.Find(delegate (Eleve e) {
            Debug.Log("eleve trouvé  :" + identite);
            return e.prenom + " " + e.nom == identite;
        });

        FindObjectOfType<UIController>().DisplayEleveCard();
    }


    public void ToggleInteractibility(bool b)
    {
        if (b)
        {
            GetComponent<Button>().interactable = false;
            GetComponent<Image>().raycastTarget = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
            GetComponent<Image>().raycastTarget = true;
        }

    }

}

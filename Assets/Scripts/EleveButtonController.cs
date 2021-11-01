using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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



}

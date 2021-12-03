using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeEleve : MonoBehaviour
{
   public void ChooseRandom()
    {

        List<Eleve> elevesToPick = GameManager.instance.eleves.FindAll(Eleve => Eleve.classe == GameManager.instance.currentClasse);

        int maxAmount = elevesToPick.Count;

        int index = Random.Range(0, maxAmount);
        GameManager.instance.currentEleve = elevesToPick[index];
        UIController.instance.DisplayEleveCard();
    }
}

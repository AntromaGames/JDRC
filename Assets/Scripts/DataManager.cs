using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] Transform dataTransform;

    public GameObject data;
    [SerializeField] Color color1, color2;
    Color color;

    private void OnEnable()
    {
        color = color1;
        foreach(Eleve e in GameManager.instance.eleves)
        {
            GameObject eleveObject = Instantiate(data, dataTransform);
            eleveObject.GetComponent<BD_eleve>().PopulateDatas(e, color);
            color = color == color1 ? color2 : color1;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI teacher;
    [SerializeField] TextMeshProUGUI etablissement;


    private void Start()
    {
        teacher.text = GameManager.instance.currentUser.name;
        etablissement.text = GameManager.instance.currentUser.etablissement;
    }

    // Update is called once per frame
    void Update()
    {
        time.text = System.DateTime.Now.ToString();
    }
}

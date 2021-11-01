using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WarningDiplayer : MonoBehaviour
{
    public static WarningDiplayer instance;

    [SerializeField] TextMeshProUGUI warningText;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        MakeSingleton();
        anim = GetComponent<Animator>();
    }

    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void DisplayText(string text)
    {
        anim.SetBool("show", true);
        warningText.text = text;
    }
    public void ClosePanel()
    {
        anim.SetBool("show", false);
        warningText.text = " ";
    }
}

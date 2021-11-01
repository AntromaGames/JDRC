using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderMenuAnim : MonoBehaviour
{
    public GameObject PanelEleve, PanelMenu, PanelParams,PanelJDRC,PanelInfos;

    public GameObject currentPanel;

    private void Start()
    {
        currentPanel = null;

    }
    public void ShowPanel(string panel)
    {
        switch (panel)
        {
            case "eleve":
                ShowPanel(PanelEleve);
                break;
            case "param":
                ShowPanel(PanelParams);
                break;
            case "JDRC":
                ShowPanel(PanelJDRC);
                break;
            case "Infos":
                ShowPanel(PanelInfos);
                break;
        }
    }
    public void ShowPanel(GameObject panel)
    {
        if(currentPanel)
        currentPanel.GetComponent<Animator>().SetTrigger("slidout");
        if (panel != null)
        {
            currentPanel = panel;
            Animator anim = panel.GetComponent<Animator>();
            anim.SetTrigger("slidin");
        }
    }
    public void ShowHideMenu()
    {
        if(PanelMenu != null)
        {
            Animator animator = PanelMenu.GetComponent<Animator>();
            if(animator != null)
            {
                bool isOpen = animator.GetBool("show");
                animator.SetBool("show", !isOpen);
            }
        }
    }
}

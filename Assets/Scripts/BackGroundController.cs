using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundController : MonoBehaviour
{
    [SerializeField] Image[] backGrounds;

    [SerializeField] Sprite mathsSprite;
    [SerializeField] Sprite FrancaisSprite;
    [SerializeField] Sprite HGSprite;
    [SerializeField] Sprite AnglaisSprite;
    [SerializeField] Sprite musiqueSprite;
    [SerializeField] Sprite TechnoSprite;
    [SerializeField] Sprite SVTSprite;
    [SerializeField] Sprite PhysiqueChimieSprite;
    [SerializeField] Sprite ArtsPlaSprite;

    // Start is called before the first frame update
    void Start()
    {
        SetBackGrounds();
    }

    private void SetBackGrounds()
    {
        switch (GameManager.instance.currentUser.matiere)
        {
            case "Mathématiques":
                SetImages(mathsSprite);
                break;
            case "Français":
                SetImages(FrancaisSprite);
                break;
            case "Histoire-Géographie":
                SetImages(HGSprite);
                break;
            case "Anglais":
                SetImages(AnglaisSprite);
                break;
            case "Education Musicale":
                SetImages(musiqueSprite);
                break;
            case "Technologie":
                SetImages(TechnoSprite);
                break;
            case "S.V.T.":
                SetImages(SVTSprite);
                break;
            case "Physique-Chimie":
                SetImages(PhysiqueChimieSprite);
                break;
            case "Arts Plastiques":
                SetImages(ArtsPlaSprite);
                break;
        }
    }

    private void SetImages(Sprite sprite)
    {
       for(int i= 0; i< backGrounds.Length; i++)
        {
            backGrounds[i].sprite = sprite;
        }
    }
}

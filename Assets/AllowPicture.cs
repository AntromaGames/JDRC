using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;



public class AllowPicture : MonoBehaviour
{
    [SerializeField] Image playerImage;
    [SerializeField] GameObject crossImage;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance.currentEleve.alloPhoto)
        {
            crossImage.SetActive(false);
        }
        else
        {
            crossImage.SetActive(true);
        }

    }

    public void TogglePhotoPermission()
    {
        if (GameManager.instance.currentEleve.alloPhoto)
        {
            GameManager.instance.currentEleve.alloPhoto = false;
            playerImage.sprite = null;
            crossImage.SetActive(false);

        }
        else
        {
            GameManager.instance.currentEleve.alloPhoto = true;
            crossImage.SetActive(true);
            if (File.Exists(GameManager.instance.currentEleve.photoPath))
            {
                byte[] b = File.ReadAllBytes(GameManager.instance.currentEleve.photoPath);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(b);
                byte[] pngByte = tex.EncodeToPNG();
                playerImage.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            }
        }


    }
}

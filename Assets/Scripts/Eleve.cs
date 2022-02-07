using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public enum ChaiseDirection { alone, right, left }

[Serializable]
public class Eleve 
{
    public string prenom,nom,classe;
    public int id, xp, level,niveau,table;
    public bool isSelected, alreadyDid;
    public Sprite photo;
    public string photoPath;
    public bool alloPhoto;
    public  List<Competence> competences;
    public  List<Power> powers;
    public ChaiseDirection direction = ChaiseDirection.alone;

    public Eleve(string _prenom, string _nom, string _classe, int _id, int _xp, int _level, int _niveau)
    {
        prenom = _prenom;
        nom = _nom;
        classe = _classe;
        id = _id;
        xp = _xp;
        level = _level;
        niveau = _niveau;
    }


    public void SetPicture()
    {
        if (!string.IsNullOrEmpty(photoPath))
        {
            byte[] b = File.ReadAllBytes(photoPath); 
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(b);
            byte[] pngByte = tex.EncodeToPNG();
            photo = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
        }
    }
}

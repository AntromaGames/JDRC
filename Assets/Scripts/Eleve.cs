using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Eleve 
{
    public string prenom,nom,classe;
    public int id, xp, level,niveau;
    public bool isSelected, alreadyDid;
    public Sprite photo;
    public List<Competence> competences;
    public List<Power> powers;
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
}

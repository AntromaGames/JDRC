using System;

public enum Genre { homme, femme }
[Serializable]
public class User
{
    public Genre genre;
    public string name,firstName, matiere, etablissement;

    public User(Genre _Genre, string _name,string _firstName, string _matiere, string _etablissement)
    {
        name = _name;
        firstName = _firstName;
        matiere = _matiere;
        etablissement = _etablissement;
        genre = _Genre;

    }
}

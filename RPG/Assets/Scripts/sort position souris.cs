using System;
using System.Collections.Generic;
using UnityEngine;


public class sortpositionsouris : MonoBehaviour
{
    public int width;
    public int heigth;
    public int tailleCarreauX;
    public int tailleCarreauY;
    public int positionXPrécédente;
    public int positionYPrécédente;
    public int longueurListe;
    class variations
    {
        int x;
        int y;
        public variations(int a, int b)
        {
            this.x = a;
            this.y = b;
        }
        public bool equals(variations v)
        {
            if (this.x==v.x && this.y == v.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
    List<variations> formeDessinée = new List<variations>();
    List<variations> boule = new List<variations>();
    List<variations> mur = new List<variations>();
    List<variations> attaque = new List<variations>();
    List<List<variations>> formesPossibles = new List<List<variations>>();
    List<String> noms = new List<string>();
    List<variations> droite = new List<variations>();
    List<variations> gauche = new List<variations>();
    List<variations> haut = new List<variations>();
    List<variations> bas = new List<variations>();

    // Start is called before the first frame update
    void Start()
    {
        formesPossibles.Add(boule); formesPossibles.Add(mur); formesPossibles.Add(attaque); //on crée la liste de toutes les formes
        noms.Add("boule"); noms.Add("mur");noms.Add("attaque"); // on leur associe un nom via la liste noms
        Vector3 sourisInitiale = Input.mousePosition;
        width = (int)Mathf.Round(Screen.width);
        heigth = (int)Mathf.Round(Screen.height);
        tailleCarreauX = width / 3;
        tailleCarreauY = heigth / 3;
        positionXPrécédente = (int)Math.Round(sourisInitiale[0])/tailleCarreauX;
        positionYPrécédente = (int)Math.Round(sourisInitiale[1])/tailleCarreauY; //on récupère le cadran initial de la souris
    }

    // Update is called once per frame
    string formeRenvoyee()
    {
        foreach (List<variations> forme in formesPossibles)
        {
            for (int i = 0; i < longueurListe; i++)
            {
                if (!formeDessinée[i].equals(forme[i]))
                {
                    break;
                }
            }
            return noms[formesPossibles.IndexOf(forme)];
        }
        return "Mauvaise forme"; //on renvoie une erreur si la forme saisie n'est pas la bonne.
    }
    void Update()
    {
        Vector3 mouseInScreen = Input.mousePosition;
        int positionX = (int)Math.Round(mouseInScreen[0]); int positionY = (int)Math.Round(mouseInScreen[1]); // on récupère la position exacte de la souris sur l'écran
        longueurListe = formeDessinée.Count;
        if (longueurListe == 3)
        {
            formeRenvoyee();
        }
        int positionXnormee = positionX / tailleCarreauX; // on récupère le cadran dans lequel se trouve la souris
        int positionYnormee = positionY / tailleCarreauY; // on récupère le cadran dans lequel se trouve la souris

        if (positionXPrécédente != positionX || positionYPrécédente != positionY){
            variations changt = new variations(positionX - positionXPrécédente, positionY - positionYPrécédente);
            formeDessinée.Add(changt);
            positionYPrécédente = positionY;
            positionXPrécédente = positionX;
        }
    }

}

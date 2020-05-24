using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;


public class Mouvement : MonoBehaviour
{
    public float tailleCarreauX;
    public float tailleCarreauY;
    public int positionXPrecedentNormee;
    public int positionYPrecedentNormee;
    public int positionXnormee;
    public int positionYnormee;


    

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
            if (this.x == v.x && this.y == v.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void display()
        {
            print((x, y));
        }
    }
    
    // Les formes dessinées sont identifiées par des variations "haut" "bas" "gauche" "droite".
    variations haut = new variations(0, 1);
    variations bas = new variations(0, -1);
    variations gauche = new variations(-1, 0);
    variations droite = new variations(1, 0);
    Collection<variations> formeDessinee = new Collection<variations>();
    Collection<Collection<variations>> formesIdentifiées = new Collection<Collection<variations>>();
    Collection<string> formesDispo = new Collection<string>();
    Collection<variations> boule = new Collection<variations>();
    Collection<variations> mur1 = new Collection<variations>();
    Collection<variations> mur2 = new Collection<variations>();
    Collection<variations> mur3 = new Collection<variations>();
    Collection<variations> mur4 = new Collection<variations>();
    Collection<variations> defense = new Collection<variations>();
    public string dessinEffectué;
    public string dessinRenvoyé;
    public string dessin(){
        return (dessinEffectué);
        }

// Start is called before the first frame update
void Start()
    {
        dessinEffectué = "Mauvaise forme";
        dessinRenvoyé = "Mauvaise forme";
        //on initialise les différentes formes avec les conditions de lancement
        boule.Add(droite);boule.Add(bas);boule.Add(gauche);boule.Add(haut); 
        defense.Add(droite); defense.Add(bas); defense.Add(droite); defense.Add(bas);
        mur1.Add(droite); mur1.Add(droite); mur1.Add(droite); mur1.Add(droite);
        mur2.Add(gauche); mur2.Add(gauche); mur2.Add(gauche); mur2.Add(gauche);
        mur3.Add(haut); mur3.Add(haut); mur3.Add(haut); mur3.Add(haut);
        mur4.Add(bas); mur4.Add(bas); mur4.Add(bas); mur4.Add(bas);

        //on initialise la collection qui permettra de renvoyer la forme du sort lancé
        formesDispo.Add("boule"); formesDispo.Add("defense"); formesDispo.Add("mur");
        formesDispo.Add("mur"); formesDispo.Add("mur"); formesDispo.Add("mur");

        //on initialise la collection qui permet de reconnaitre les formes
        formesIdentifiées.Add(boule); formesIdentifiées.Add(defense); formesIdentifiées.Add(mur1);
        formesIdentifiées.Add(mur2); formesIdentifiées.Add(mur3); formesIdentifiées.Add(mur4);

        print("ça lance");
        tailleCarreauX=Screen.width/3;
        tailleCarreauY=Screen.height/3;
        Vector3 mouseInScreen = Input.mousePosition;
        positionXPrecedentNormee = (int)Mathf.Floor(mouseInScreen.x / tailleCarreauX);
        positionYPrecedentNormee = (int)Mathf.Floor(mouseInScreen.y / tailleCarreauY);
    }

    
    // Update is called once per frame
    void Update()
    {
        string dessin2 = "Mauvaise forme";
        //print(dessinEffectué);
        if (Input.GetMouseButtonDown(0))    // Le tracé commence dès que le click Gauche est enfoncé et s'arrête quand le boutton est relaché.
        {
            Vector3 mouseInScreen = Input.mousePosition;
            positionXPrecedentNormee = (int)Mathf.Floor(mouseInScreen.x / tailleCarreauX);
            positionYPrecedentNormee = (int)Mathf.Floor(mouseInScreen.y / tailleCarreauY);
        }
        if (Input.GetMouseButton(0))
        {

            tailleCarreauX =Screen.width/5;
            tailleCarreauY=Screen.height/5;
            Vector3 mouseInScreen = Input.mousePosition;
            positionXnormee = (int)Mathf.Floor(mouseInScreen.x / tailleCarreauX);
            positionYnormee = (int)Mathf.Floor(mouseInScreen.y / tailleCarreauY);
            if (positionXPrecedentNormee != positionXnormee || positionYPrecedentNormee != positionYnormee)
            {
                variations changement = new variations(positionXnormee-positionXPrecedentNormee,positionYnormee-positionYPrecedentNormee);
                
                /*formeDessinee.Add(changement);
                changement.display(); if (changement.equals(haut))
                */
                if (changement.equals(haut)){
                    print("haut");
                    formeDessinee.Add(haut);
                }
                else if (changement.equals(bas))
                {
                    print("bas");
                    formeDessinee.Add(bas);
                }
                else if (changement.equals(gauche))
                {
                    print("gauche");
                    formeDessinee.Add(gauche);
                }
                else if (changement.equals(droite))
                {
                    print("droite");
                    formeDessinee.Add(droite);
                }


                if (formeDessinee.Count>4)
                {
                    formeDessinee = new Collection<variations>();
                }
                if ( formeDessinee.Count>3)
                {
                    //print("assez long");
                    foreach (Collection<variations> formes in formesIdentifiées)
                    {
                        if (dessin2!="Mauvaise forme")
                        {
                            //print("forme reconnue");
                            break;
                        }
                        for (int i=0; i <= 3; i++)
                        {
                            /*print("formes[i]: "); formes[i].display();
                            print("formeDessinee[i]: "); formeDessinee[i].display();*/
                            if (!(formes[i].equals(formeDessinee[i])))
                            {
                                break;
                            }
                            if (i == 3)
                            {
                                dessinEffectué = formesDispo[formesIdentifiées.IndexOf(formes)];
                                dessin2 = dessinEffectué;
                            }

                        }
                    }
                    //print(dessinEffectué);
                    formeDessinee = new Collection<variations>();
                }

                positionXPrecedentNormee=positionXnormee;
                positionYPrecedentNormee=positionYnormee;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            formeDessinee = new Collection<variations>();
            dessinEffectué = "Mauvaise forme";
        }
    }
}


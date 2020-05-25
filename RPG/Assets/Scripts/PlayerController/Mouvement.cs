using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;


public class Mouvement : MonoBehaviour
{
    //pour permettre une marge d'erreur, la position de la souris est contenue dans des carreaux. L'écran est découpé selon un quadrillage.
    // Ainsi les variations correspondent au changement de carreau.
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
        public bool equals(variations v)    // On vérifie si la position actuelle est identique ou non à la position précédente.
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
    Collection<variations> formeDessinee = new Collection<variations>();    // collection de variations, on vérifiera si cette suite de variations correspond ou non aux suites de variations des sorts.
    Collection<Collection<variations>> formesIdentifiées = new Collection<Collection<variations>>();
    Collection<string> formesDispo = new Collection<string>(); // Collection de sorts
    
    // Un sort est identifié par une suite de variation haut bas gauche droite.
    Collection<variations> boule = new Collection<variations>();
    Collection<variations> bouclier1 = new Collection<variations>();
    Collection<variations> bouclier2 = new Collection<variations>();
    Collection<variations> bouclier3 = new Collection<variations>();
    Collection<variations> bouclier4 = new Collection<variations>();
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
        bouclier1.Add(droite); bouclier1.Add(droite); bouclier1.Add(droite); bouclier1.Add(droite);
        bouclier2.Add(gauche); bouclier2.Add(gauche); bouclier2.Add(gauche); bouclier2.Add(gauche);
        bouclier3.Add(haut); bouclier3.Add(haut); bouclier3.Add(haut); bouclier3.Add(haut);
        bouclier4.Add(bas); bouclier4.Add(bas); bouclier4.Add(bas); bouclier4.Add(bas);

        //on initialise la collection qui permettra de renvoyer la forme du sort lancé
        formesDispo.Add("boule"); formesDispo.Add("defense"); formesDispo.Add("bouclier");
        formesDispo.Add("bouclier"); formesDispo.Add("bouclier"); formesDispo.Add("bouclier");

        //on initialise la collection qui permet de reconnaitre les formes
        formesIdentifiées.Add(boule); formesIdentifiées.Add(defense); formesIdentifiées.Add(bouclier1);
        formesIdentifiées.Add(bouclier2); formesIdentifiées.Add(bouclier3); formesIdentifiées.Add(bouclier4);

        print("ça lance");
        tailleCarreauX=Screen.width/5;
        tailleCarreauY=Screen.height/5;
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
            Vector3 mouseInScreen = Input.mousePosition;    // Récupération de la position de la souris sous forme d'un vecteur à 3 dimensions.
            //on récupère le cadran initial dans lequel se trouve dans la souris, repéré grâce à son ordre dans les cadrans selon les x et les y.
            positionXPrecedentNormee = (int)Mathf.Floor(mouseInScreen.x / tailleCarreauX); 
            positionYPrecedentNormee = (int)Mathf.Floor(mouseInScreen.y / tailleCarreauY);
        }
        if (Input.GetMouseButton(0))
        {
            tailleCarreauX =Screen.width/5;
            tailleCarreauY=Screen.height/5;
            Vector3 mouseInScreen = Input.mousePosition; 
            //on met à jour la position de la souris dans les cadrans à chaque image comme précédemment :
            positionXnormee = (int)Mathf.Floor(mouseInScreen.x / tailleCarreauX); 
            positionYnormee = (int)Mathf.Floor(mouseInScreen.y / tailleCarreauY);
            if (positionXPrecedentNormee != positionXnormee || positionYPrecedentNormee != positionYnormee) //lorsque l'on remarque que la souris a changé de cadran,
            {
                //le changement de cadran par la souris est défini ici:
                variations changement = new variations(positionXnormee-positionXPrecedentNormee,positionYnormee-positionYPrecedentNormee); 
                
                // On ajoute ce changement à la collection de variations définissant la forme dessinée
                
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

                //on vérifie ici si la forme dessinée correspond à un sort lançable :
                if (formeDessinee.Count>4)  // Forme incorrect car nos formes ont au plus 4 variations.
                {
                    formeDessinee = new Collection<variations>();
                }
                if ( formeDessinee.Count>3)
                {

                    foreach (Collection<variations> formes in formesIdentifiées)    // On teste si la collection dessinée correspond à une collection de sorts.
                    {
                        if (dessin2!="Mauvaise forme") //si jamais un sort a été reconnu mais qu'il reste d'autre sorts à vérifier...
                        {

                            break; //...on sort de la boucle
                        }
                        for (int i=0; i <= 3; i++)  // On compare la forme dessinée à un sort en comparant les variations une par une.
                        {

                            if (!(formes[i].equals(formeDessinee[i])))
                            {
                                break;// si une des variations ne correspond pas au sort regardé, on passe directement au sort suivant.
                            }
                            if (i == 3)
                            {
                                dessinEffectué = formesDispo[formesIdentifiées.IndexOf(formes)]; //si la forme dessinée correspond à une forme prédéfinie, on l'enregistre pour le renvoyer avec la fonction dessin()
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
        if (Input.GetMouseButtonUp(0)) //lorsque le bouton gauche de la souris est relaché :
        {
            formeDessinee = new Collection<variations>();   // On rénitialise pour pouvoir lancer un nouveau sort.
            dessinEffectué = "Mauvaise forme"; //on réinitialise la valeur de la variable traduisant la forme dessinée.
        }
    }
}


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
            if (this.x==v.x && this.y == v.y)
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
            print((x,y));
        }
    }

    variations haut = new variations (0,1);
    variations bas = new variations (0,-1);
    variations gauche = new variations (-1,0);
    variations droite = new variations (1,0);
    Collection<variations> formeDessinee = new Collection<variations>();

   

    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetMouseButton(0))
        {
            tailleCarreauX=Screen.width/3;
            tailleCarreauY=Screen.height/3;
            Vector3 mouseInScreen = Input.mousePosition;
            positionXnormee = (int)Mathf.Floor(mouseInScreen.x / tailleCarreauX);
            positionYnormee = (int)Mathf.Floor(mouseInScreen.y / tailleCarreauY);
            if (positionXPrecedentNormee != positionXnormee || positionYPrecedentNormee != positionYnormee)
            {
                variations changement = new variations(positionXnormee-positionXPrecedentNormee,positionYnormee-positionYPrecedentNormee);
                if (changement.equals(haut))
                {
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
                if (formeDessinee.Count>2)
                {
                    formeDessinee = new Collection<variations>();
                }
                if ( formeDessinee.Count>1 && formeDessinee[0].equals(formeDessinee[1]))
                {
                    print("C'est un trait!!!");
                    formeDessinee = new Collection<variations>();
                }

                positionXPrecedentNormee=positionXnormee;
                positionYPrecedentNormee=positionYnormee;
            }
        }
    }
}


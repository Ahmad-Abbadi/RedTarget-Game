using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class rightButtoninthecarshopscript : MonoBehaviour
{
    [SerializeField] private int index = 0;
    [SerializeField] GameObject G;
    [SerializeField] GameObject G1;
    [SerializeField] GameObject G2;
    [SerializeField] GameObject G3;
    [SerializeField] GameObject G4;
    public void if_click()
    {
        index++;
        index %= 5;
        switch(index)
        {
            case 0:
                G.SetActive(false);
                G1.SetActive(true);
                break;
            case 1:
                G1.SetActive(false);
                G2.SetActive(true);
                break;
            case 2:
                G2.SetActive(false);
                G3.SetActive(true);
                break;
            case 3:
                G3.SetActive(false);
                G4.SetActive(true);
                break;
            case 4:
                G4.SetActive(false);
                G.SetActive(true);
                break;
        }
    }
}

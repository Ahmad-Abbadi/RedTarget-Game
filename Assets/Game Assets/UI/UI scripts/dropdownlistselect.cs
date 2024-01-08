using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class dropdownlistselect : MonoBehaviour
{
    [SerializeField]private GameObject G;
    [SerializeField]private GameObject G1;
    [SerializeField] private GameObject G2;
    [SerializeField] private GameObject G3;
    public void select(int index)
    {
        switch (index)
        {
            case 0:
                G.SetActive(false);
                G1.SetActive(false);
                G3.SetActive(false);
                break;
            case 1:
                G.SetActive(false);
                G1.SetActive(false);
                G2.SetActive(false);
                G3.SetActive(true);
                break;
            case 2:
                G.SetActive(false);
                G1.SetActive(true);
                G2.SetActive(false);
                G3.SetActive(false);
                break;
            default:
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class dropdownlistselect : MonoBehaviour
{
    [SerializeField]private GameObject G;
    [SerializeField]private GameObject G1;

    public void select(int index)
    {
        switch (index)
        {
            case 0:
                G.SetActive(false);
                G1.SetActive(false);
                break;
            case 1:
                G.SetActive(true);
                G1.SetActive(false);
                break;
            case 2:
                G.SetActive(false);
                G1.SetActive(true);
                break;
            case 3:
                G.SetActive(false);
                G1.SetActive(false);
                break;
            default:
                break;
        }
    }
}

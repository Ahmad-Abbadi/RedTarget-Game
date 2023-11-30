using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class dropdownlistselect : MonoBehaviour
{
    [SerializeField]private GameObject G;
    public GameObject indexzerofromthedropdownlist;

    public void select(int index)
    {
        switch (index)
        {
            case 0:
                G.SetActive(false);
                break;
            case 1:
                G.SetActive(true);
                break;
            case 2:
                G.SetActive(false);
                break;
            case 3:
                G.SetActive(false);
                break;
            default:
                break;
        }
    }
}

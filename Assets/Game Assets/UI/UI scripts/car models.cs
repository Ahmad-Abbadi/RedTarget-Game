using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carmodels : MonoBehaviour
{
    public int currentindex;
    public GameObject[] carmodel;

    // Start is called before the first frame update
    void Start()
    {
        currentindex = PlayerPrefs.GetInt("selectdcar",0);
       foreach(GameObject car in carmodel)
        {
            car.SetActive(false);
        }
        carmodel[currentindex].SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

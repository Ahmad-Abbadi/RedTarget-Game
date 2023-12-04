using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class back_ground_lock : MonoBehaviour
{
    [SerializeField]
    private GameObject G;
    //[SerializeField] private Animator G;
    void stop()
    {
        //G.GetComponent<Animator>().enabled = false;
        //G.StopPlayback();
        // error // G.GetComponent<Animator>().Stop();
        //G.GetComponent<Animator>().StopPlayback();
        G.GetComponent<Animator>().enabled = false;
    }

    
}

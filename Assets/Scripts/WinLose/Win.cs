using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] SwitchScenes swithcScene;
    [SerializeField] GameObject particle;
    [SerializeField] int currentScene;

    private void Start()
    {
        swithcScene = FindAnyObjectByType<SwitchScenes>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            StartCoroutine(WaitAndSwitchLevel());
            Instantiate(particle, transform.position, transform.rotation);

            CarController carController = other.GetComponent<CarController>();
            if (carController != null)
            {
                if (carController.player1)
                {
                   float t = carController.GetComponent<Timer>().GetTime();
                   FileReadWriteDataSaver data =  FindAnyObjectByType<FileReadWriteDataSaver>();
                data.player1.time = t;
                if(data.player2.time == 0) 
                data.player2.time = t;

                    data.Save();  
                
                }
                else
                {
                    float t = carController.GetComponent<Timer>().GetTime();
                    FileReadWriteDataSaver data = FindAnyObjectByType<FileReadWriteDataSaver>();
                    data.player2.time = t;
                    if (data.player1.time == 0)
                        data.player1.time = t;

                    data.Save();
                }   
            }
        }
    }

    private void Update()
    {


    }

    private IEnumerator WaitAndSwitchLevel()
    {

        yield return new WaitForSeconds(7f);

        if (currentScene == 1)
        {
            swithcScene.ActivateLevelTwo();
        }
        else if (currentScene == 2)
        {
            swithcScene.ActivateLevelThree();
        }
    }
}

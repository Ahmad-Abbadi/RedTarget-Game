using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    
    [SerializeField] GameObject particle;
    float nextLevelTime = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            nextLevelTime = Time.time;
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
        if(Time.time - nextLevelTime > 7f)
        {
            //TODO go to next Level
        }
    }
}

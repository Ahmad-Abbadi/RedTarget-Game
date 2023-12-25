using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void ActivateLevelOne()
    {
      SceneManager.LoadScene(2);
    }

    public void ActivateLevelTwo()
    {
        SceneManager.LoadScene(3);
    }

    public void ActivateLevelThree()
    {
        SceneManager.LoadScene(4);
    }

    public void ActivateCarShop()
    {
        SceneManager.LoadScene(1);
    }

    public void ActivateUiScene() 
    {
        SceneManager.LoadScene(0);
    }

    public void ActivateSelectionScene() 
    {
        SceneManager.LoadScene(5);
    }
}


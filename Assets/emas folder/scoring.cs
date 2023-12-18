using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class scoring : MonoBehaviour
{
    public GameObject ScoreText;
    public int theScore;
    public AudioSource collectedSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        collectedSound.Play();
        theScore += 5;
        ScoreText.GetComponent<Text>().text = "Score : " + theScore;
        Destroy(gameObject);
    }
}

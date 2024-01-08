using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obsticalscode : MonoBehaviour
{
    public playerhealth player;
    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 6)
        {
            player.player1takeAdamage(damage);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

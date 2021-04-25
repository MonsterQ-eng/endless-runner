using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorBooster : MonoBehaviour
{

    private Booster booster;
     
    private void Start()
    {
        booster = GameObject.Find("GameHandler").GetComponent<Booster>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spike"))
        {
            //Destroy(this.gameObject);
            transform.position = new Vector3(transform.position.x + 3f, transform.position.y+10f);
            Debug.Log("Spike! Move");
        }

        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("DeathZone"))
        {
            booster.OneBoosterSpawn();
            Debug.Log("Fall Down! Spawn!");
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public Player player;
    Rigidbody2D rb;
    Vector2 playerDirector;
    float timeStamp;
    bool flyToPlayer;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (flyToPlayer)
        {
            playerDirector = -(transform.position - player.transform.position).normalized;
            rb.velocity = new Vector2(playerDirector.x, playerDirector.y) * 400f * (Time.time / timeStamp);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (player != null)
            {
                player.Money(1);
            }
            
            Destroy(this.gameObject);
        }

        if(other.gameObject.tag == "CoinMagnetCollider")
        {
            timeStamp = Time.time;
            flyToPlayer = true;
        }
    }


}


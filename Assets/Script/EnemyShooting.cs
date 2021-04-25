using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{

    private Player player;
    private PlayerTut playerTut;
    public float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject shoot;
    public GameObject destroyParticle;

    float lives;

    private void Start()
    {
        
        player = GameObject.Find("Player").GetComponent<Player>();

        timeBtwShots = startTimeBtwShots;
    }

    private void Update()
    {
        if (player != null)
        {
            lives = player.Live();
            if (Vector2.Distance(transform.position, player.transform.position) <= 50 && Vector2.Distance(transform.position, player.transform.position) >= -50 && lives > 0)
            {
                if (timeBtwShots <= 0)
                {
                    Instantiate(shoot, transform.position, Quaternion.identity);
                    timeBtwShots = startTimeBtwShots;
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (player != null)
        {
            if (other.CompareTag("Player"))
            {
                player.MoneyEnemy();
                Instantiate(destroyParticle, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }


}

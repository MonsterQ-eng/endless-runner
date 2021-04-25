using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingTut : MonoBehaviour
{
    private PlayerTut playerTut;
    public float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject shoot;
    public GameObject destroyParticle;


    private void Start()
    {

        playerTut = GameObject.Find("Player").GetComponent<PlayerTut>();

        timeBtwShots = startTimeBtwShots;
    }

    private void Update()
    {
        if (playerTut != null)
        {
            if (Vector2.Distance(transform.position, playerTut.transform.position) <= 50 && Vector2.Distance(transform.position, playerTut.transform.position) >= -50)
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

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (playerTut != null)
    //    {
    //        if (other.CompareTag("Player"))
    //        {
    //           // player.MoneyEnemy();
    //           // Instantiate(destroyParticle, transform.position, Quaternion.identity);
    //            Destroy(this.gameObject);
    //        }
    //    }
    //}
}

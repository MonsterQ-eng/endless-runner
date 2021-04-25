using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehavior : MonoBehaviour
{
    public float speed;

    private Player player;
    private Vector2 triger;
    [SerializeField]
    private GameObject particle;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        triger = new Vector2(player.transform.position.x, player.transform.position.y);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, triger, speed * Time.deltaTime);


        if (transform.position.x == triger.x && transform.position.y == triger.y)
        {
            DestroyShoot();
        }

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.Damage(3);
            DestroyShoot();
        }
    }


    private void DestroyShoot()
    {
        Destroy(this.gameObject);
        Instantiate(particle, transform.position, Quaternion.identity);
    }

}

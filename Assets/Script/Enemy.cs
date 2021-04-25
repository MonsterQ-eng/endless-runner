using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask platformlayerMask;
    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxCollider;
    [SerializeField]private float _speed = 3f;
    Vector3 raycast;
    Player player;
    
    void Start()
    {
        rigidbody2d = transform.GetComponent<Rigidbody2D>();
        boxCollider = transform.GetComponent<BoxCollider2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }



    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, platformlayerMask);
        
        return raycastHit2D.collider != null;
        

    }


    public void Move()
    {
        if (IsGrounded())
        {
            transform.Translate(Vector3.left * _speed * Time.deltaTime);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other) //Odbija od ścian i zabija enemy
    {
        if (other.gameObject.CompareTag("Inv_Wall"))
        {
            _speed = -3f;
        }
        if (other.gameObject.CompareTag("Inv_Wall_Right"))
        {

            //Debug.Log("Speed 3f");
            _speed = 3f;
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hitted destroy");
            if(player != null)
            {
                player.MoneyEnemy();
            }
            
            Destroy(this.gameObject);
        }


    }
    private void OnCollisionEnter2D(Collision2D other)  // Rani gracza
    {
        if (other.gameObject.CompareTag("Player"))
        {
                Debug.Log("Hitted life taken");
                Destroy(this.gameObject);
            if(player != null)
            {
                player.Damage(0);
            }
                
        }
    }
}

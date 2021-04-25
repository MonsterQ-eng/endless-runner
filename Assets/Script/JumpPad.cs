using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Rigidbody2D>
                    ().AddForce(Vector2.up * 2500);
            other.gameObject.GetComponent<Animator>().SetTrigger("Jump");
        }
    }
}

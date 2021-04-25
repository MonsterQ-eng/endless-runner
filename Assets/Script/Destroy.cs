using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    [SerializeField] private GameObject deathCamera;
    Vector3 deathcameraposition;


    // Start is called before the first frame update
    void Start()
    {
        deathCamera= GameObject.Find("Destroy_Point");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(deathCamera.transform.position.x > transform.position.x)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingstrokes : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector2.down * 6 * Time.deltaTime);
        Destroy(this.gameObject, 0.3f);
    }
}

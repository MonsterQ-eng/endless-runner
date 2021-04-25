using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), 0.6f).setEase(LeanTweenType.easeOutBack);
    }


}

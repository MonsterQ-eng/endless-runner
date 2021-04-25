using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public Vector2 offset = new Vector2(-3, -3);

    private SpriteRenderer sprRendCaster;
    private SpriteRenderer sprRendShadow;

    private Transform transCaster;
    private Transform transShadow;

    public Material shadowMaterial;
    public Color shadowColor;

    private void Start()
    {
        transCaster = transform;
        transShadow = new GameObject().transform;
        transShadow.parent = transCaster;
        transShadow.gameObject.name = "shadow";
        transShadow.localRotation = Quaternion.identity;

        sprRendCaster = GetComponent<SpriteRenderer>();
        sprRendShadow = transShadow.gameObject.AddComponent<SpriteRenderer>();

        sprRendShadow.material = shadowMaterial;
        sprRendShadow.color = shadowColor;
        sprRendShadow.transform.localScale.Set(1.7f, 1.7f, 1.7f);
        sprRendShadow.sortingLayerName = sprRendCaster.sortingLayerName;
        sprRendShadow.sortingOrder = sprRendCaster.sortingOrder - 1;
    }


    private void LateUpdate()
    {
        transShadow.position = new Vector2(transCaster.position.x + offset.x, transCaster.position.y + offset.y);

        sprRendShadow.sprite = sprRendCaster.sprite;

    }

}

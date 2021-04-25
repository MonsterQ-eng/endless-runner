using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUPSet : MonoBehaviour
{

    private float disapperTime = 1f;
    private float DISAPPERA_TIME_MAX;

    private TextMesh textMesh;
    private Color textColor;
    public Player player;
    public bool coinD;
    private Vector3 moveVector;


    private void Awake()
    {
        textMesh = transform.GetComponent<TextMesh>();
        textColor = textMesh.color;
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player != null)
        {
            coinD = player.doubleCoin;
        }
        if (player != null)
        {
            if (coinD)
            {
                textMesh.text = 20.ToString();
            }
            else
            {
                textMesh.text = 10.ToString();
            }
        }
        disapperTime = DISAPPERA_TIME_MAX;
        moveVector = new Vector3(0.7f, 1f) * 20f;
    }


    // Update is called once per frame
    void Update()
    {

        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if(disapperTime > DISAPPERA_TIME_MAX * .5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }



        disapperTime -= Time.deltaTime;
        if(disapperTime < 0)
        {
            float disappearSpeed = 2f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
        }
        if (textColor.a < 0)
        {
            Destroy(gameObject);
        }

    }
}

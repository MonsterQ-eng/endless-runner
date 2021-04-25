using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuantumTek.EncryptedSave;

public class Booster : MonoBehaviour
{
    private Player player;
    private Transform playerGameObject;
    public GameObject[] boosters;
    private float time;
    private float spawnTimeDown;
    private float spawnTimeUp;
    private bool DoOnce;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        playerGameObject = GameObject.Find("Player").GetComponent<Transform>();
        DoOnce = true;
        spawnTimeDown = ES_Save.Load<float>("allBoosterFasterTimeDown");
        spawnTimeUp = ES_Save.Load<float>("allBoosterFasterTimeUp");
    }

   

    // SPAWN BOOSTERS

    IEnumerator SpawnBooster()
    {
        while (true)
        {
            Debug.Log("Spawn Booster WHILE!");
            int boostersInt = Random.Range(0, boosters.Length);
            Instantiate(boosters[boostersInt], new Vector2(playerGameObject.position.x + 200, playerGameObject.position.y + 100), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(spawnTimeDown, spawnTimeUp));
        }
        
        
    }

    // ENS SPAWN BOOSTERS

    private void Update()
    {
        if(player.Speed() != 0 && DoOnce)
        {
            StartCoroutine(SpawnBooster());
            DoOnce = false;
        }
    }


    public void OneBoosterSpawn()
    {
        Debug.Log("Spawn Booster One!");
        int boostersInt = Random.Range(0, boosters.Length);
        Instantiate(boosters[boostersInt], new Vector2(playerGameObject.position.x + 200, playerGameObject.position.y + 100), Quaternion.identity);
    }





}

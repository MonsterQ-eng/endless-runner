using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuantumTek.EncryptedSave;

public class PlatformGenerate : MonoBehaviour
{

    private const float PLAYER_DISTANCE_SPAWN_LEVEL = 200f;
    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> thePlatformsList;
    [SerializeField] private List<Transform> thePlatformsList500;
    [SerializeField] private List<Transform> thePlatformsList2k;
    [SerializeField] private Player player;

    private Vector2 lasEndPosition;

    private int playerLevel;

    private int startPoints;

    private int choose;
    private int kNumber;

    // Start is called before the first frame update
    void Start()
    {

        lasEndPosition = levelPart_Start.Find("EndPosition").position;
        SpawnLevelPart();
        playerLevel = ES_Save.Load<int>("playerLevel");
        kNumber = 1000;
        startPoints = kNumber * playerLevel;
        choose = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.GetComponent<Transform>().position, lasEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL)
        {
            //if (player.Distance() > startPoints)
            //{
            //    SpawnLevelPart500();
            //}
            //else
            //{
            //    SpawnLevelPart();
            //}

            if (player.Distance() > startPoints)
            {
                choose++;
                if (choose > 2)
                {
                    choose = 1;
                }
                SwitchSpawn(choose);
                kNumber += 1000;
                startPoints = kNumber * playerLevel;
            }
            else
            {
                SwitchSpawn(choose);
            }

        }
    }

    private void SwitchSpawn(int i)
    {
        switch (i)
        {
            case 1:
                SpawnLevelPart();
                break;
            case 2:
                SpawnLevelPart500();
                break;
        }
    }


    private void SpawnLevelPart2k()
    {
        Transform choosenLevelPart = thePlatformsList2k[Random.Range(0, thePlatformsList2k.Count)];
        Transform levelPartTransform = SpawnLevelPart(choosenLevelPart, lasEndPosition);
        lasEndPosition = levelPartTransform.Find("EndPosition").position;
    }


    private void SpawnLevelPart500()
    {
        Transform choosenLevelPart = thePlatformsList500[Random.Range(0, thePlatformsList500.Count)];
        Transform levelPartTransform = SpawnLevelPart(choosenLevelPart, lasEndPosition);
        lasEndPosition = levelPartTransform.Find("EndPosition").position;
    }




    private void SpawnLevelPart()
    {
        Transform choosenLevelPart = thePlatformsList[Random.Range(0, thePlatformsList.Count)];
        Transform levelPartTransform = SpawnLevelPart(choosenLevelPart,lasEndPosition);
        lasEndPosition = levelPartTransform.Find("EndPosition").position;
    }


    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {

        //string spriteName = "";
        //SpriteRenderer spriteRenderer =  levelPart.Find("Platform").GetComponent<SpriteRenderer>();
        //Sprite sprite = Resources.LoadAll<Sprite>(spriteName);

        Transform levelPartTransform = Instantiate(levelPart, spawnPosition , Quaternion.identity);
        return levelPartTransform;

    }

}

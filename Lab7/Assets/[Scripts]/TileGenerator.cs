using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public Transform spawnPoint;

    [Header("Prefab Parents")] 
    public Transform robotParent;
    public Transform hazardParent;
    public Transform coinParent;

    private GameObject robotPrefab;
    private GameObject hazardPrefab;
    private GameObject coinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GetComponentInChildren<SpawnPoint>().transform;
        robotPrefab = Resources.Load<GameObject>("Prefabs/Robot");
        hazardPrefab = Resources.Load<GameObject>("Prefabs/Hazard");
        coinPrefab = Resources.Load<GameObject>("Prefabs/Coin");
        robotParent = GameObject.Find("Robots").transform;
        hazardParent = GameObject.Find("Hazards").transform;
        coinParent = GameObject.Find("Coins").transform;
        GameObject randomObject = null;

        // 30% for a Robot - 30% for a Hazard - 30% for a coin - 10% nothing
        var randomRoll = Random.Range(1, 11);
        if (randomRoll > 0 && randomRoll < 4)
        {
            // spawn a Robot
            randomObject = Instantiate(robotPrefab, spawnPoint.position + new Vector3(0.0f, 0.2f, 0.0f), Quaternion.identity);
            randomObject.transform.SetParent(robotParent);

        }
        else if (randomRoll > 3 && randomRoll < 7)
        {
            // spawn a Hazard
            randomObject = Instantiate(hazardPrefab, spawnPoint.position, Quaternion.identity);
            randomObject.transform.SetParent(hazardParent);
        }
        else if(randomRoll > 6 && randomRoll < 10)
        {
            // spawn a coin
            randomObject = Instantiate(coinPrefab, spawnPoint.position + new Vector3(0.0f, 1.5f, 0.0f), Quaternion.identity);
            randomObject.transform.SetParent(coinParent);
        }
    }
}

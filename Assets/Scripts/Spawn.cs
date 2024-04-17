using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    //public GameObject spawnObject;

    //public Transform[] spawnLocations;
    
    // Start is called before the first frame update
    //void Start()
    //{
      //  GameObject newObject = GameObject.Instatiate(spawnObject, spawnLocations[Random.Range(0, spawnLocations.Length)]) as GameObject;
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}

	
	public GameObject scoringRegionPrefab; // Prefab of the scoring region
    public int numberOfRegions = 5; // Number of scoring regions to spawn
    public Vector2 boardSize = new Vector2(2.054976f, 2.1717f); // Size of the board
	//public Vector2 boardSize = new Vector2(GetComponent<Collider>().bounds.size);


    private void Start()
    {
        SpawnScoringRegions();
    }

    private void SpawnScoringRegions()
    {
        for (int i = 0; i < numberOfRegions; i++)
        {
            Vector2 randomPosition = GenerateRandomPosition();
            Instantiate(scoringRegionPrefab, randomPosition, Quaternion.identity);
        }
    }

    private Vector2 GenerateRandomPosition()
    {
        float randomX = Random.Range(-boardSize.x / 2f, boardSize.x / 2f);
        float randomY = Random.Range(-boardSize.y / 2f, boardSize.y / 2f);
        return new Vector2(randomX, randomY);
    }
}

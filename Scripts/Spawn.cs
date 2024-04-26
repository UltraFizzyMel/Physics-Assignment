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

	
	//public GameObject scoringRegionPrefab; // Prefab of the scoring region
    //public int numberOfRegions = 5; // Number of scoring regions to spawn
    //public Vector2 boardSize = new Vector2(2.054976f, 2.1717f); // Size of the board
	//public Vector2 boardSize = new Vector2(GetComponent<Collider>().bounds.size);


    //private void Start()
    //{
      //  SpawnScoringRegions();
    //}

    //private void SpawnScoringRegions()
    //{
      //  for (int i = 0; i < numberOfRegions; i++)
        //{
          //  Vector2 randomPosition = GenerateRandomPosition();
            //Instantiate(scoringRegionPrefab, randomPosition, Quaternion.identity);
        //}
    //}

    //private Vector2 GenerateRandomPosition()
    //{
      //  float randomX = Random.Range(-boardSize.x / 2f, boardSize.x / 2f);
        //float randomY = Random.Range(-boardSize.y / 2f, boardSize.y / 2f);
        //return new Vector2(randomX, randomY);
    //}

	public float respawnInterval = 10f;

    private bool isPowerUpActive = true;

	//public Vector2 boardSize = new Vector2(GetComponent<Collider>().bounds.size.x, GetComponent<Collider>().bounds.size.x);
	//Bounds bounds = spawnAreaCollider.bounds;
	
	
	//Collider2D spawnAreaCollider = GetComponent<Collider2D>();
	//Bounds bounds = spawnAreaCollider.bounds;
    //float width = spawnAreaCollider.size.x;
    //float height = spawnAreaCollider.size.y;

	public Score IncScore;

	public PuckScript PuckCheck;

    private void Start()
    {
        // Start the respawn coroutine
        StartCoroutine(RespawnPowerUp());
    }

    // Method to handle collisions with the puck
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is the puck and the power-up is active
        if (other.CompareTag("Puck") && isPowerUpActive)
        {
			if (PuckCheck.lastCollidedWithTag == "Player")
				IncScore.IncreasePlayerScore(Score.eScore.PlayerScore);
			if (PuckCheck.lastCollidedWithTag == "AI")
				IncScore.IncreasePlayerScore(Score.eScore.AIScore);
            // Determine the last player who hit the puck
            //string lastPlayerTag = other.GetComponent<PuckCollision>().GetLastCollidedWithTag();

            // Increase the score of the last player who hit the puck
            //if (lastPlayerTag == "Player")
            //{
              //  playerScoreScript.IncreaseScore();
                //Debug.Log("Player scored using power-up!");
            //}
            //else if (lastPlayerTag == "AI")
            //{
              //  aiScoreScript.IncreaseScore();
                //Debug.Log("AI scored using power-up!");
            //}

            // Deactivate the power-up
            isPowerUpActive = false;

            // Destroy the power-up region GameObject
            Destroy(gameObject);
        }
    }

    // Coroutine to respawn the power-up region after a delay
    private IEnumerator RespawnPowerUp()
    {
        // Wait for the specified respawn interval
        yield return new WaitForSeconds(respawnInterval);
		
		if(gameObject != null)
			Destroy(gameObject);

        // Instantiate a new power-up region GameObject at a random position within spawn areas
       // Instantiate(gameObject, GetRandomSpawnPosition(), Quaternion.identity);

        // Set the power-up to be active again
        isPowerUpActive = true;
    }

    // Method to get a random spawn position for the power-up region
    //private Vector2 GetRandomSpawnPosition()
    //{
        // Replace this with your logic to determine the spawn position within predefined spawn areas
        //return new Vector2(Random.Range(-boardSize.x / 2f, boardSize.x / 2f), Random.Range(-boardSize.y / 2f, boardSize.y / 2f));
    //}
}

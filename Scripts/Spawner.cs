using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform SpawnBoundsHolder;
    private Bounds SpawnBounds;
    private bool isPowerUpActive = true;
    public PuckScript PuckCheck;
    public Score IncScore;
    public GameObject BonusPoints;

    private GameObject BonusPointsInstance;
    public GameObject BonusPointsPrefab;
    private bool isSpawnPending = false;
    private bool firstObj = true;
    
    private Collider2D myCollider;
    private Renderer myRenderer;
    
    private float respawnDelay = 5f;

    private void Start()
    {
        myCollider = BonusPoints.GetComponent<Collider2D>();
        myRenderer = BonusPoints.GetComponent<Renderer>();
        SpawnBounds = new Bounds(SpawnBoundsHolder.GetChild(0).position.y,
                            SpawnBoundsHolder.GetChild(1).position.y,
                            SpawnBoundsHolder.GetChild(2).position.x,
                            SpawnBoundsHolder.GetChild(3).position.x);

        //StartCoroutine(SpawnBonusPoints());
        
        myCollider.enabled = false;
        myRenderer.enabled = false;
        
        //BonusPointsInstance = Instantiate(BonusPointsPrefab, GetRandomSpawnPosition(), Quaternion.identity);
        
        //BonusPointsPrefab.SetActive(false);
        
        BonusPoints.SetActive(true);
        Spawn();
    }
    
    private IEnumerator SpawnBonusPoints()
    {
        //if (isSpawnPending)
        //{
          //  yield break;
        //}

        // Set the spawn pending flag to true
        //isSpawnPending = true;

        // If requested after a collision, wait for 10 seconds before spawning
        //if (afterCollision)
        //{
            //yield return new WaitForSeconds(10f);
        //}

        // If there's an existing BonusPoints instance, destroy it
        //if (BonusPointsInstance != null)
        //{
        //    Destroy(BonusPointsInstance);
        //}

        // Spawn BonusPoints object
        //Vector2 spawnPosition = GetRandomSpawnPosition();
        //BonusPointsInstance = Instantiate(BonusPointsPrefab, spawnPosition, Quaternion.identity);
        
        //Spawn();

        // Reset the spawn pending flag after spawning
        //isSpawnPending = false;
        
        
        if (BonusPointsInstance != null)
        {
            Destroy(BonusPointsInstance);
            isPowerUpActive = false;
        }
        
        yield return new WaitForSeconds(10f);

        if (isPowerUpActive == false)
        {
            isPowerUpActive = true;
            Spawn();
        }
    }
    
    private void Spawn()
    {
        //StartCoroutine(SpawnBonusPoints());
        
        //if (firstObj)
        //{
         //   Destroy(BonusPointsInstance);
           // firstObj = false;
            //isPowerUpActive = false;
        //}
        
        //Vector2 spawnPosition = GetRandomSpawnPosition();
        //BonusPointsInstance = Instantiate(BonusPointsPrefab, spawnPosition, Quaternion.identity);
        
        transform.position = GetRandomSpawnPosition();
        isPowerUpActive = true;
        BonusPoints.SetActive(true);
        myCollider.enabled = true;
        myRenderer.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is the puck and the power-up is active
        if (other.gameObject.CompareTag("Puck") && BonusPoints.activeSelf && PuckCheck.lastCollidedWithTag != null) // && !isSpawnPending)
        {
            if (PuckCheck.lastCollidedWithTag == "Player")
            {
                IncScore.IncreasePlayerScore(Score.eScore.PlayerScore);
                //IncScore.Increment(Score.eScore.PlayerScore);
            }
            else if (PuckCheck.lastCollidedWithTag == "AITag")
            {
                IncScore.IncreasePlayerScore(Score.eScore.AIScore);
                //IncScore.Increment(Score.eScore.AIScore);
            }
            
            isPowerUpActive = false;
            //Spawn();
            //StartCoroutine(SpawnBonusPoints());
            
            StartCoroutine(RespawnAfterDelay(other.gameObject));
        }
    }

    private IEnumerator RespawnAfterDelay(GameObject pointsObject)
    {
        myRenderer.enabled = false;
        myCollider.enabled = false;
        
        // Wait for the specified respawn interval
        yield return new WaitForSeconds(respawnDelay);
        
        BonusPoints.SetActive(false);
        if (isPowerUpActive == false)
        {
            gameObject.SetActive(true);
            Spawn();
        }
    }
    

    // Method to get a random spawn position for the power-up region
    private Vector2 GetRandomSpawnPosition()
    {
        return new Vector2(Random.Range(SpawnBounds.Left / 2f, SpawnBounds.Right / 2f), 
                            Random.Range(SpawnBounds.Down / 2f, SpawnBounds.Up / 2f));
    }
}
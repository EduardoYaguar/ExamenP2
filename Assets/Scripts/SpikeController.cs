using System.Collections;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public GameObject spikePrefab; 
    public Transform[] spawnPoints; 
    public float minSpawnTime = 3f; 
    public float maxSpawnTime = 7f; 

    void Start()
    {
        StartCoroutine(SpawnSpikesRandomly());
    }

    IEnumerator SpawnSpikesRandomly()
    {
        while (true) // Infinite loop to keep spawning spikes
        {
            float waitTime = Random.Range(minSpawnTime, maxSpawnTime); 
            yield return new WaitForSeconds(waitTime); 

            
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(spikePrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
        }
    }
}

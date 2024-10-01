using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VirusSpawner : MonoBehaviour {
    public GameObject virusPrefab;
    public List<Transform> spawnPoints;
    public float minSpawnInterval = 20f;
    public float maxSpawnInterval = 30f;

    private bool spawning = false;

    void Start() {
        if(HospitalManager.Instance.State == GameState.DestroyAllViruses) {
            spawning = true;
            StartCoroutine(SpawnVirus());
        }
    }

    IEnumerator SpawnVirus() {
        while (HospitalManager.Instance.State == GameState.DestroyAllViruses) {
            float interval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(interval);

            // Spawn a virus at a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            Instantiate(virusPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}

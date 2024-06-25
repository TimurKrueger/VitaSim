using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSpawner : MonoBehaviour
{
    public GameObject virusPrefab;
    public float spawnRate = 1f;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        InvokeRepeating("SpawnVirus", 1f, spawnRate);
    }

    void SpawnVirus()
    {
        if (virusPrefab != null)
        {
            /*float screenAspect = (float)Screen.width / (float)Screen.height;
            float cameraHeight = mainCamera.orthographicSize * 2;
            float cameraWidth = cameraHeight * screenAspect;

            float spawnX = Random.Range(-cameraWidth / 2, cameraWidth / 2);
            Vector2 spawnPosition = new Vector2(spawnX, mainCamera.orthographicSize + 1);
            Instantiate(virusPrefab, spawnPosition, Quaternion.identity);*/

            float screenAspect = (float)Screen.width / (float)Screen.height;
            float cameraHeight = mainCamera.orthographicSize * 2;
            float cameraWidth = cameraHeight * screenAspect;

            float spawnX = Random.Range(-cameraWidth / 2, cameraWidth / 2);
            Vector3 spawnPosition = new Vector3(spawnX, mainCamera.orthographicSize + 1, 0);
            Instantiate(virusPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
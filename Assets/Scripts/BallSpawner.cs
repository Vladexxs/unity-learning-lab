
using System.Collections;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float minSpawnDelay = 0.5f; // saniyede en fazla 2 top
    [SerializeField] private float maxSpawnDelay = 1.0f; // saniyede en az 1 top
    [SerializeField] private Vector3 spawnAreaCenter;
    [SerializeField] private Vector3 spawnAreaSize;
    #endregion

    #region Unity Methods
    private void Start()
    {
        StartCoroutine(SpawnBallRoutine());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(spawnAreaCenter, spawnAreaSize);
    }
    #endregion

    #region Coroutines
    private IEnumerator SpawnBallRoutine()
    {
        while (true)
        {
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);
            SpawnBall();
        }
    }
    #endregion

    #region Private Methods
    private void SpawnBall()
    {
        if (ballPrefab == null)
        {
            Debug.LogError("Ball prefab is not assigned in the BallSpawner!");
            return;
        }

        float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float randomZ = Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2);

        Vector3 spawnPosition = new Vector3(randomX, spawnAreaCenter.y, randomZ);

        Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
    }
    #endregion
}

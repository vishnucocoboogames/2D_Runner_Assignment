using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class ObstacleSpawnManager : MonoBehaviourSingleton<ObstacleSpawnManager>
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObstacles[] obstaclePrefabs;
    [SerializeField] private List<GameObstacles> spawnedObstacles;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnDistance = 10f;
    [SerializeField] private float minSpawnInterval = 1.5f;
    [SerializeField] private float maxSpawnInterval = 3.5f;
    [SerializeField] private float obstacleYOffset = 0f;

    [Header("Debug")]
    [SerializeField] private bool showGizmo = true;

    private bool canSpawn = true;
    private CancellationTokenSource spawnCancelToken;

    void OnEnable()
    {
        GameManager.OnGameStart += OnGameStart;
        GameManager.OnGameFail += OnGameFail;
        GameManager.OnPauseGame += OnPauseGame;
        GameManager.OnRevive += OnRevive;
    }



    void OnDisable()
    {
        GameManager.OnGameStart -= OnGameStart;
        GameManager.OnGameFail -= OnGameFail;
        GameManager.OnPauseGame -= OnPauseGame;
        GameManager.OnRevive -= OnRevive;

    }
    private void OnRevive()
    {
        canSpawn = true;
        spawnCancelToken = new CancellationTokenSource();
        StartSpawnLoopAsync(spawnCancelToken.Token).Forget();

    }

    private void OnPauseGame(bool obj)
    {
        if (obj)
        {
            canSpawn = false;
        }
        else
        {
            canSpawn = true;
            spawnCancelToken = new CancellationTokenSource();
            StartSpawnLoopAsync(spawnCancelToken.Token).Forget();
        }
    }

    private void OnGameFail()
    {
        canSpawn = false;


    }

    private void OnGameStart()
    {
        StartGame();
    }
    private void StartGame()
    {
        if (player == null)
        {
            Debug.LogError("🚨 Player reference not assigned in ObstacleSpawnManager!");
            enabled = false;
            return;
        }

        spawnCancelToken = new CancellationTokenSource();
        StartSpawnLoopAsync(spawnCancelToken.Token).Forget();
    }

    private async UniTaskVoid StartSpawnLoopAsync(CancellationToken token)
    {
        while (canSpawn && !token.IsCancellationRequested)
        {
            float delay = Random.Range(minSpawnInterval, maxSpawnInterval);
            await UniTask.Delay(System.TimeSpan.FromSeconds(delay), cancellationToken: token);

            if (!canSpawn || token.IsCancellationRequested)
                break;

            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        if (obstaclePrefabs.Length == 0 || player == null) return;

        int index = Random.Range(0, obstaclePrefabs.Length);
        GameObstacles prefab = obstaclePrefabs[index];

        Vector3 spawnPos = new Vector3(player.position.x + spawnDistance, obstacleYOffset, 0);

        GameObstacles obs = Instantiate(prefab, spawnPos, Quaternion.identity);
        spawnedObstacles.Add(obs);
    }

    public void StopSpawning()
    {
        canSpawn = false;
        spawnCancelToken?.Cancel();
    }

    private void OnDestroy()
    {
        spawnCancelToken?.Cancel();
        spawnCancelToken?.Dispose();
    }

    public void RemveAllObstacles()
    {
        spawnedObstacles.Clear();
    }
    public void RemoveObstcle(GameObstacles obstacles)
    {
        spawnedObstacles.Remove(obstacles);
    }

    private void OnDrawGizmos()
    {
        if (!showGizmo || player == null) return;

        Gizmos.color = Color.red;
        Vector3 pos = player.position + Vector3.right * spawnDistance;
        Gizmos.DrawLine(player.position, pos);
        Gizmos.DrawWireCube(pos + Vector3.up * obstacleYOffset, new Vector3(1f, 1f, 0f));
    }
}

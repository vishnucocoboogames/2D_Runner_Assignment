using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ParallaxBackground_0 : MonoBehaviour
{
    [Header("Camera Follow Settings")]
    [SerializeField] private bool followPlayer = true;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private float horizontalOffset;

    [Header("Layer Settings")]
    public float[] Layer_Speed = new float[7];
    public GameObject[] Layer_Objects = new GameObject[7];

    private Transform _camera;
    private Player player;
    private float[] startPos = new float[7];
    private float boundSizeX;
    private float sizeX;
    private bool isBoosterActive;
    private float boosterEndTime;

    private void OnEnable()
    {
        GameManager.OnGameStart += OnGameStart;
        GameManager.OnGameFail += OnGameFail;
        GameManager.OnPowerUpUse += OnBoosterUse;
        GameManager.OnRevive += OnRevive;
        GameManager.OnPauseGame += OnPauseGame;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= OnGameStart;
        GameManager.OnGameFail -= OnGameFail;
        GameManager.OnPowerUpUse -= OnBoosterUse;
        GameManager.OnRevive -= OnRevive;
        GameManager.OnPauseGame -= OnPauseGame;
    }

    private void Start()
    {
        _camera = Camera.main.transform;
        sizeX = Layer_Objects[0].transform.localScale.x;
        boundSizeX = Layer_Objects[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        for (int i = 0; i < Layer_Objects.Length; i++)
        {
            if (Layer_Objects[i] != null)
                startPos[i] = _camera.position.x;
        }
    }

    private void OnGameStart()
    {
        player = GameManager.Instance.player;
    }

    private void OnGameFail()
    {
        followPlayer = false;
    }

    private void OnRevive()
    {
        followPlayer = true;
    }

    private void OnPauseGame(bool isPaused)
    {
        followPlayer = !isPaused;
    }

    private void OnBoosterUse(PowerUpType type)
    {
        if (type == PowerUpType.Boost)
        {
            isBoosterActive = true;
            boosterEndTime = Time.time + 5f;

            UniTask.Void(async () =>
            {
                await UniTask.WaitForSeconds(5, cancellationToken: destroyCancellationToken);
                isBoosterActive = false;
            });
        }
    }

    private void Update()
    {
        if (followPlayer && player != null)
        {
            // Player stays left side of screen by shifting camera’s target position
            Vector3 targetPos = new Vector3(player.transform.position.x + horizontalOffset, _camera.position.y, _camera.position.z);
            _camera.position = Vector3.Lerp(_camera.position, targetPos, Time.deltaTime * smoothSpeed);
        }

        UpdateParallaxLayers();
    }

    private void UpdateParallaxLayers()
    {
        for (int i = 0; i < Layer_Objects.Length; i++)
        {
            if (Layer_Objects[i] == null)
                continue;

            float temp = (_camera.position.x * (1 - Layer_Speed[i]));
            float distance = _camera.position.x * Layer_Speed[i];

            Layer_Objects[i].transform.position = new Vector2(startPos[i] + distance, _camera.position.y);

            if (temp > startPos[i] + boundSizeX * sizeX)
                startPos[i] += boundSizeX * sizeX;
            else if (temp < startPos[i] - boundSizeX * sizeX)
                startPos[i] -= boundSizeX * sizeX;
        }
    }
}

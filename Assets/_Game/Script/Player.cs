using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: SerializeField] public bool IsSheildActive { get; private set; }
    [field: SerializeField] public bool IsBoosterActive { get; private set; }

    [SerializeField] PlayerController playerController;
    [SerializeField] Animator animator;
    [SerializeField] GameObject sheild;
    [SerializeField] GameObject booster;
    [SerializeField] GameObject[] visuals;
    [SerializeField] Animator[] animators;
    [SerializeField] bool gameFailed;


    void Start()
    {
        SetCharecter();
    }

    void OnEnable()
    {
        GameManager.OnGameStart += OnGameStart;
        GameManager.OnPowerUpUse += OnPowerUpUse;
        GameManager.OnRevive += OnRevive;
        GameManager.OnPauseGame += OnPauseGame;
        GameManager.OnCharecterChange += OnCharecterChange;
    }



    void OnDisable()
    {
        GameManager.OnGameStart -= OnGameStart;
        GameManager.OnPowerUpUse -= OnPowerUpUse;
        GameManager.OnRevive -= OnRevive;
        GameManager.OnPauseGame -= OnPauseGame;
        GameManager.OnCharecterChange -= OnCharecterChange;

    }


    private void OnRevive()
    {
        playerController.SetCanMove(true);
        ActivateSheild();
        Revieve();
        gameFailed = false;
    }

    private void OnGameStart()
    {
        SetCharecter();
        playerController.SetSpeed(GameManager.Instance.MoveSpeed);
        playerController.SetCanMove(true);
        animator.SetBool("run", true);
    }
    private void OnGameFial()
    {
        playerController.SetCanMove(false);
        GameManager.Instance.OnGameOver();
    }

    public void jump()
    {
        animator.SetBool("run", false);
        animator.SetBool("jump", true);
    }
    public void Run()
    {
        animator.SetBool("run", true);
        animator.SetBool("jump", false);
        animator.SetBool("idle", false);

    }
    public void Revieve()
    {
        animator.SetBool("revive", true);
        animator.SetBool("fail", false);
    }
    private void OnPauseGame(bool obj)
    {
        animator.SetBool("run", !obj);
        // animator.SetBool("jump", !obj);
        animator.SetBool("idle", obj);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IIntractable>(out IIntractable intractable))
        {
            if (IsSheildActive)
            {
                intractable.Onintraction();
            }
            else
            {
                if (gameFailed)
                    return;
                OnGameFial();
                animator.SetBool("fail", true);
                AudioManager.Instance.PlaySoundOfType(SoundEffectType.Loose);
            }
        }
        else if (collision.gameObject.TryGetComponent<ICollectable>(out ICollectable collectable))
        {
            collectable.OnCollecetion();
        }
    }

    public void ActivateSheild()
    {
        UniTask.Void(async () =>
        {
            if (IsSheildActive)
                return;
            IsSheildActive = true;
            sheild.SetActive(true);
            await UniTask.WaitForSeconds(5, cancellationToken: destroyCancellationToken);
            IsSheildActive = false;
            sheild.SetActive(false);
        });
    }

    public void AvtivateBoost()
    {
        UniTask.Void(async () =>
       {
           if (IsBoosterActive)
               return;
           IsBoosterActive = true;
           playerController.SetSpeed(GameManager.Instance.MoveSpeed + 3);
           booster.SetActive(true);
           await UniTask.WaitForSeconds(5, cancellationToken: destroyCancellationToken);
           booster.SetActive(false);
           playerController.SetSpeed(GameManager.Instance.MoveSpeed);
           IsBoosterActive = false;

       });
    }
    private void OnPowerUpUse(PowerUpType type)
    {
        switch (type)
        {
            case PowerUpType.Sheild:
                ActivateSheild();
                break;
            case PowerUpType.Boost:
                AvtivateBoost();
                break;
            default:
                break;
        }

    }
    private void OnCharecterChange()
    {
        int inxt = GameManager.Instance.CharecterIndex;
        foreach (var item in visuals)
        {
            item.SetActive(false);
        }
        visuals[inxt].SetActive(true);
        animator = animators[inxt];
    }

    private void SetCharecter()
    {
        int inxt = PlayerPrefs.GetInt(GameKeys.CHARECTERINDEX);
        foreach (var item in visuals)
        {
            item.SetActive(false);
        }
        visuals[inxt].SetActive(true);
        animator = animators[inxt];
    }


}

using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameObstacles : MonoBehaviour
{

    void Start()
    {
        UniTask.Void(async () =>
        {
            await UniTask.WaitForSeconds(15, cancellationToken: destroyCancellationToken);
            ObstacleSpawnManager.Instance.RemoveObstcle(this);
            Destroy(gameObject);
        });
    }

}
public interface IIntractable
{
    public abstract void Onintraction();

}
public interface ICollectable
{
    public abstract void OnCollecetion();
}

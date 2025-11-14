using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsManager : MonoBehaviourSingletonPersistent<FpsManager>
{
    void Start()
    {
        SetFps();
    }
    private void SetFps()
    {
#if UNITY_EDITOR
        Application.targetFrameRate = 60;
#endif
#if UNITY_ANDROID || UNITY_IOS
        Application.targetFrameRate = 60;
#endif

    }
}

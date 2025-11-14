using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiToggles : MonoBehaviour
{
    private Toggle toggle;
    [SerializeField] Image bg;
    void Start()
    {
        toggle = gameObject.GetComponent<Toggle>();
        bg.enabled = !toggle.isOn;
        toggle.onValueChanged.AddListener((isOn) =>
        {
            bg.enabled = !isOn;
        });


    }


}

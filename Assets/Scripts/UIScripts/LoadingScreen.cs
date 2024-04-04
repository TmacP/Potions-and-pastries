using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] Slider LoadingBar;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateBar(float progress)
    {
        LoadingBar.value = Mathf.Clamp01(progress / 1.0f);
    }
}

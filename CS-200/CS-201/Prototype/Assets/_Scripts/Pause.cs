using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] KeyCode pauseKey;
    [SerializeField] GameObject gameLogic;
    [SerializeField] WindowManager windowManager;
    public bool paused;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            paused = !paused;
        }

        if(paused)
        {
            gameLogic.SetActive(false);
            windowManager.ShowWindow(1);
        }
        else
        {
            gameLogic.SetActive(true);
            windowManager.ShowWindow(0);
        }
    }

    public void SwitchPause()
    {
        paused = !paused;
    }
}

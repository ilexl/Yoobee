using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRatioSetup : MonoBehaviour
{
    public bool fullScreen = true;  // Whether the game should be in fullscreen mode
    private void Start()
    {
        if (PlayerPrefs.GetInt("LEGNER-STUDIO-GAME*TD*" + "Fullscreen", 1) == 1)
        {
            fullScreen = true;
        }
        else
        {
            fullScreen = false;
        }

#if UNITY_EDITOR
    }
#else

        // Calculate the target height based on the screen width and 16:9 aspect ratio
        int targetHeight = Screen.width * 9 / 16;

        // Set the game's resolution to match the target width and height
        Screen.SetResolution(Screen.width, targetHeight, fullScreen);
    }
#endif
}

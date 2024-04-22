using UnityEngine;

public class ForceAspectRatio : MonoBehaviour
{
    // Start is called before the first frame
    void Start()
    {
        // this essentially sets the resolutiont to 16:9
        // if your in the editor it messages a warning to remember to do this
#if UNITY_EDITOR
        Debug.Log("UNITY EDITOR ENABLED - PLEASE ENSURE RESOLUTION IS 16:9");
#else
        // Calculate the target height based on the screen width and 16:9 aspect ratio
        int targetHeight = Screen.width * 9 / 16;

        // Set the game's resolution to match the target width and height
        Screen.SetResolution(Screen.width, targetHeight, false);
#endif
    }
}

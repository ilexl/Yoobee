using UnityEngine;

public class DestroySoundComplete : MonoBehaviour
{
    [SerializeField] AudioSource sound;

    // Update is called once per frame
    private void Update()
    {
        // destroys the current gameObject if
        // the sound has finished playing
        if (sound.isPlaying) { return; }
        Destroy(gameObject);
    }
}

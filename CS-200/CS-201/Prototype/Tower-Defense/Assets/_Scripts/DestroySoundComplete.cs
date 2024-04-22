using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySoundComplete : MonoBehaviour
{
    [SerializeField] AudioSource sound;
    private void Update()
    {
        if (sound.isPlaying) { return; }
        Destroy(gameObject);
    }
}

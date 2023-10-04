using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]GameObject[] soundPrefabs;
    [SerializeField] GameObject backgroundMusic;
    public void PlaySound(int soundIndex)
    {
        Instantiate(soundPrefabs[soundIndex], transform);
    }

    public void PlaySoundInWorldSpace(int soundIndex, Vector3 position)
    {
        Transform s = Instantiate(soundPrefabs[soundIndex], transform).transform;
        s.position = position;
    }

    private void Awake()
    {
        if(backgroundMusic == null) { return; }
        Instantiate(backgroundMusic, transform);
    }
}

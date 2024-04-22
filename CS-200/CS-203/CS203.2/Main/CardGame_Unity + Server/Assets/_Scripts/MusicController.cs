using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    #region public variables
    public List<AudioClip> musicList;
    public AudioSource _audio;
    #endregion
    #region local variables
    private int currentMusicIndex = 0;
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        _audio = GetComponent<AudioSource>();

        if (musicList.Count > 0)
        {
            _audio.clip = musicList[currentMusicIndex];
            _audio.Play();
        }
    }

    /// <summary>
    /// toggles the music on or off using a toggle
    /// </summary>
    /// <param name="toggle">the toggle calling this</param>
    public void ToggleMusic(Toggle toggle)
    {
        bool play = toggle.isOn;
        if (play)
        {
            _audio.Pause();
            _audio.volume = 0.0f;
        }
        else
        {
            _audio.Play();
            _audio.volume = 1.0f;
        }
    }
}


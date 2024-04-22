using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] GameObject[] soundPrefabs; // list of sounds

    /// <summary>
    /// creates a sound which auto plays and deletes
    /// </summary>
    /// <param name="soundIndex"></param>
    public void PlaySound(int soundIndex)
    {
        Instantiate(soundPrefabs[soundIndex], transform);
    }

    // Start is called before the first frame
    public void Start()
    {
        var names = FindObjectsOfType<Button>(includeInactive: true).ToList();
        foreach(Button b in names)
        {
            b.onClick.AddListener(() => PlaySound(0));
        }
    }
}

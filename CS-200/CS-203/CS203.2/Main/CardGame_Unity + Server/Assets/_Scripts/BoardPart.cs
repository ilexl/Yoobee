using UnityEngine;

public class BoardPart : MonoBehaviour
{
    // this class holds data only
    public Vector3 realPos;
    public DropHolder dropHolder;

    // Awake is called when the script is loaded
    private void Awake()
    {
        dropHolder = GetComponent<DropHolder>(); // gets drop holder when loading
    }
}

using UnityEngine;
using TMPro;

public class TileLetter : MonoBehaviour
{
    #region public variables
    public Vector3? currentPos = null;
    public char currentLetter;
    #endregion
    #region local variables
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] DragDrop dragDrop;
    [SerializeField] bool playable;
    #endregion

    // Update is called once per frame
    void Update()
    {
        currentLetter = text.text[0];
        if(dragDrop.dropHolder == null) { currentPos = null; }
        else { currentPos = dragDrop.dropHolder.transform.GetComponent<BoardPart>().realPos; }
    }

    /// <summary>
    /// removes anything in the dropholder
    /// </summary>
    public void ResetDropHolder()
    {
        dragDrop.dropHolder = null;
    }
    
    /// <summary>
    /// sets whether or not a client can move this item
    /// </summary>
    /// <param name="p"></param>
    public void SetPlayable(bool p)
    {
        playable = p;
        dragDrop.playable = playable;
    }

    /// <summary>
    /// can this object be dragged/dropped
    /// </summary>
    /// <returns></returns>
    public bool GetPlayable() { return playable; }

    /// <summary>
    /// gets the current dragdrop for this tile
    /// </summary>
    /// <returns></returns>
    public DragDrop GetDragDrop() { return dragDrop; }
}

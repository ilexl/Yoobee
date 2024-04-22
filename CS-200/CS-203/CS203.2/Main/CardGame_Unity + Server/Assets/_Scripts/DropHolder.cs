using UnityEngine;
using UnityEngine.EventSystems;

public class DropHolder : MonoBehaviour, IDropHandler
{
    [SerializeField] TileLetterManager tl;
    [SerializeField] Game game;
    
    /// <summary>
    /// does checks to make sure dragdrop object can be dropped on me
    /// </summary>
    /// <param name="eventData">data including dragdrop</param>
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (!game.localTurn)
            {
                // if not local clients turn then no drop
                eventData.pointerDrag.GetComponent<DragDrop>().ReturnToPrevPos();
                game.popUpManager.ShowPopUp(6); // pop up to inform the user
                return;
            }
            if (tl.DropHolderHasLetter(this))
            {
                // if dropholder has a letter then no drop
                if (eventData.pointerDrag.GetComponent<DragDrop>().playable)
                {
                    eventData.pointerDrag.GetComponent<DragDrop>().ReturnToPrevPos();
                }
                // do not set if this one already has a letter on it
            }
            else
            {
                // check if drop here
                if (eventData.pointerDrag.GetComponent<DragDrop>().playable)
                {
                    eventData.pointerDrag.GetComponent<DragDrop>().Drop(this); // this triggers a drop
                }
            }
        }
    }
}

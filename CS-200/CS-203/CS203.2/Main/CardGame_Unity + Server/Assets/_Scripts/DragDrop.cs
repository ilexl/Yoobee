using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    #region public variables
    public bool playable = true;
    public float movementResponsiveness = 15.0f;
    public Vector2 actualPosition = Vector2.zero;
    public DropHolder dropHolder = null;
    #endregion
    #region local variables
    private Vector2 startPos = Vector2.zero;
    private DropHolder lastDropHolder = null;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] Canvas canvas;
    [SerializeField] TileLetterManager tileLettersMAIN;
    #endregion

    // Awake is called when the script is loaded
    private void Awake()
    {
        playable = true;
        #region pre checks
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvas == null)
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        }
        if (canvas == null) { Debug.LogError("No Canvas Found..."); }
        tileLettersMAIN = FindFirstObjectByType<TileLetterManager>();
        if(tileLettersMAIN == null) { Debug.LogError("No MAIN TileLetters Found..."); }
        #endregion
    }

    /// <summary>
    /// moves the item being dragged with the mouse
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (playable)
        {
            // canvas scale factor and event delta allow for accurate at any resolution and aspect ratio
            actualPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    /// <summary>
    /// sets variables to allow dragging
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (playable)
        {
            startPos = rectTransform.anchoredPosition;
            transform.SetAsLastSibling(); //ensure it renders above all other tiles
            lastDropHolder = dropHolder;
            dropHolder = null;
            canvasGroup.blocksRaycasts = false; 
            tileLettersMAIN.RayCastSetAllLetters(false);
            canvasGroup.alpha = 0.75f;
        }
    }

    /// <summary>
    /// sets variables to end drag <br/>
    /// DOES NOT TRIGGER A DROP
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (playable)
        {
            canvasGroup.blocksRaycasts = true;
            tileLettersMAIN.RayCastSetAllLetters(true);
            canvasGroup.alpha = 1f;
        }
    }

    /// <summary>
    /// allows for all of these to be set at once <br/>
    /// this is for a bug fix dont remove
    /// </summary>
    /// <param name="set">on or off</param>
    public void RayCastSet(bool set)
    {
        canvasGroup.blocksRaycasts = set;
    }

    #region empty functions to forfill drag drop
    public void OnPointerDown(PointerEventData eventData) { }
    public void OnPointerUp(PointerEventData eventData) { }
    #endregion

    /// <summary>
    /// drops this object onto a holder if able
    /// </summary>
    /// <param name="DH">drop holder to drop this object onto</param>
    public void Drop(DropHolder DH)
    {
        dropHolder = DH;
        actualPosition = DH.transform.GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // smooth movement
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, actualPosition, movementResponsiveness * Time.deltaTime);
    }

    /// <summary>
    /// ability for this object to go back to its last pos if the drop was invalid
    /// </summary>
    public void ReturnToPrevPos()
    {
        actualPosition = startPos;
        if(lastDropHolder != null) { Drop(lastDropHolder); }
    }
}

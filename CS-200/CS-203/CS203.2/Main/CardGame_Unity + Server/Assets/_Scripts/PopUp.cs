using UnityEngine;
using TMPro;

public class PopUp : MonoBehaviour
{
    #region local variables
    [SerializeField] float timeOnScreen;
    [SerializeField] bool autoHide = true;
    [SerializeField] TextMeshProUGUI textPro;
    float internalTimer = 0f;
    #endregion

    #region overflow NewPopUp()
    /// <summary>
    /// displays a pop up with specified text
    /// </summary>
    /// <param name="text">specified text</param>
    public void NewPopUp(string text)
    {
        textPro.text = text;
        internalTimer = timeOnScreen;
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// displays a pop up with its current settings applied
    /// </summary>
    public void NewPopUp()
    {
        internalTimer = timeOnScreen;
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// displays a pop up and sets autohide
    /// </summary>
    /// <param name="autoHide"></param>
    public void NewPopUp(bool autoHide)
    {
        this.autoHide = autoHide;
        internalTimer = timeOnScreen;
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// displays a pop up and edits duration
    /// </summary>
    /// <param name="duration"></param>
    public void SetDuration(float duration)
    {
        internalTimer = duration;
    }

    /// <summary>
    /// displays a pop up and sets autohide and duration
    /// </summary>
    /// <param name="text"></param>
    /// <param name="autoHide"></param>
    public void NewPopUp(string text, bool autoHide)
    {
        this.autoHide = autoHide;
        textPro.text = text;
        internalTimer = timeOnScreen;
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        // autohide if active using interal timer
        internalTimer -= Time.deltaTime;
        if (internalTimer <= 0f)
        {
            if (autoHide)
            {
                gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// hides a pop up
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

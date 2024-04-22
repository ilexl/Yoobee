using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] PopUp[] allPopUps;

    /// <summary>
    /// hides all pop ups this script controls
    /// </summary>
    public void HideAllPopUps()
    {
        foreach(PopUp popUp in allPopUps)
        {
            popUp.Hide();
        }
    }

    #region overflows ShowPopUp

    /// <summary>
    /// shows a pop up
    /// </summary>
    public void ShowPopUp(PopUp popUp, string message)
    {
        popUp.NewPopUp(message);
    }

    /// <summary>
    /// shows a pop up
    /// </summary>
    public void ShowPopUp(PopUp popUp, string message, bool autoHide)
    {
        popUp.NewPopUp(message, autoHide);
    }

    /// <summary>
    /// shows a pop up
    /// </summary>
    public void ShowPopUp(int popUp, string message)
    {
        allPopUps[popUp].NewPopUp(message);
    }

    /// <summary>
    /// shows a pop up
    /// </summary>
    public void ShowPopUp(int popUp, string message, bool autoHide)
    {
        allPopUps[popUp].NewPopUp(message, autoHide);
    }

    /// <summary>
    /// shows a pop up
    /// </summary>
    public void ShowPopUp(PopUp popUp, string message, float duration)
    {
        popUp.NewPopUp(message);
        popUp.SetDuration(duration);
    }

    /// <summary>
    /// shows a pop up
    /// </summary>
    public void ShowPopUp(PopUp popUp, string message, float duration, bool autoHide)
    {
        popUp.NewPopUp(message, autoHide);
        popUp.SetDuration(duration);
    }

    /// <summary>
    /// shows a pop up
    /// </summary>
    public void ShowPopUp(int popUp, string message, float duration)
    {
        allPopUps[popUp].NewPopUp(message);
        allPopUps[popUp].SetDuration(duration);
    }

    /// <summary>
    /// shows a pop up
    /// </summary>
    public void ShowPopUp(int popUp, string message, float duration, bool autoHide)
    {
        allPopUps[popUp].NewPopUp(message, autoHide);
        allPopUps[popUp].SetDuration(duration);
    }

    /// <summary>
    /// shows a pop up
    /// </summary>
    public void ShowPopUp(PopUp popUp, bool autoHide)
    {
        popUp.NewPopUp(autoHide);
    }

    /// <summary>
    /// shows a pop up
    /// </summary>
    public void ShowPopUp(PopUp popUp, float duration, bool autoHide)
    {
        popUp.NewPopUp(autoHide);
        popUp.SetDuration(duration);
    }

    /// <summary>
    /// shows a pop up
    /// </summary>
    public void ShowPopUp(PopUp popUp, float duration)
    {
        popUp.NewPopUp();
        popUp.SetDuration(duration);
    }

    /// <summary>
    /// shows a pop up
    /// </summary>
    public void ShowPopUp(PopUp popUp)
    {
        popUp.NewPopUp();
    }

    /// <summary>
    /// shows a pop up
    /// </summary>
    public void ShowPopUp(int popUp, bool autoHide)
    {
        allPopUps[popUp].NewPopUp(autoHide);
    }

    /// <summary>
    /// shows a pop up
    /// </summary>
    public void ShowPopUp(int popUp, float duration, bool autoHide)
    {
        allPopUps[popUp].NewPopUp(autoHide);
        allPopUps[popUp].SetDuration(duration);
    }

    /// <summary>
    /// shows a pop up
    /// </summary>
    public void ShowPopUp(int popUp, float duration)
    {
        allPopUps[popUp].NewPopUp();
        allPopUps[popUp].SetDuration(duration);
    }

    /// <summary>
    /// shows a pop up
    /// </summary>
    public void ShowPopUp(int popUp)
    {
        allPopUps[popUp].NewPopUp();
    }
    #endregion
}

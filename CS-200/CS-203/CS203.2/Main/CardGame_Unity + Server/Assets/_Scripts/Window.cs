using UnityEngine;

public class Window : MonoBehaviour
{
    public bool ShowOnStart = false;

    /// <summary>
    /// shows the window
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// hides the window
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// shows or Hides window
    /// </summary>
    /// <param sendName="active">determines if window shown</param>
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    /// <summary>
    /// gets the transforms sendName from UNITY_EDITOR
    /// </summary>
    /// <returns>(string) transform sendName of window</returns>
    public string GetName()
    {
        return transform.name;
    }
}

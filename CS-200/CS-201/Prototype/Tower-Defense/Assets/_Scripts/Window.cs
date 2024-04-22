using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public bool ShowOnStart = false;

    /// <summary>
    /// Shows the window
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the window
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Shows or Hides window
    /// </summary>
    /// <param name="active">determines if window shown</param>
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    /// <summary>
    /// Gets the transforms name from UNITY_EDITOR
    /// </summary>
    /// <returns>(string) transform name of window</returns>
    public string GetName()
    {
        return transform.name;
    }
}

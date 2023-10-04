using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class Tutorial : MonoBehaviour
{
    [SerializeField] bool triggerOnAwake = false;
    [SerializeField] Pause pause;
    [SerializeField] GameObject mainGame;
    [SerializeField] GameObject tutorial;
    [SerializeField] GameObject[] otherToHide;
    [SerializeField] CameraController camController;

    private void Awake()
    {
        if (triggerOnAwake) { Invoke(nameof(ShowTutorial), 0.01f); }
    }

    public void ShowTutorial()
    {
        pause.enabled = (false);
        mainGame.SetActive(false);
        tutorial.SetActive(true);
        camController.enabled = false;
        if (otherToHide.Length < 1) { return; }
        foreach (GameObject go in otherToHide)
        {
            go.SetActive(false);
        }
    }

    public void CloseTutorial()
    {
        mainGame.SetActive(true);
        pause.enabled = (true);
        pause.paused = false; // ensure game is unpaused at the end
        tutorial.SetActive(false);
        camController.enabled = true;
        if(otherToHide.Length < 1) { return; }
        foreach (GameObject go in otherToHide)
        {
            go.SetActive(true);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Tutorial))]
public class EDITOR_Tutorial : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Show Tutorial"))
        {
            ((Tutorial)target).ShowTutorial();
        }
        if(GUILayout.Button("Close Tutorial"))
        {
            ((Tutorial)target).CloseTutorial();
        }
    }
}
#endif
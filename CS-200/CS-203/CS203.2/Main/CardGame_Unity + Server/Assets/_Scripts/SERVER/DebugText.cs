using UnityEngine;

namespace DebugStuff
{
    public class DebugText : MonoBehaviour
    {
        private Vector2 scrollPosition = Vector2.zero;

        private int maxLength = 30000;
        //#if !UNITY_EDITOR
        static string myLog = "";
        private string output;
        private string stack;

        void OnEnable()
        {
            Application.logMessageReceived += Log;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= Log;
        }

        public void Log(string logString, string stackTrace, LogType type)
        {
            output = logString;
            stack = stackTrace;
            myLog = myLog + "\n";

            switch (type)
            {
                case LogType.Error:
                    myLog += "<color=#FF0000>" + output + "</color>";
                    break;
                case LogType.Warning:
                    myLog += "<color=#FFFF00>" + output + "</color>";
                    break;
                default:
                    myLog += output;
                    break;
            }

            if (myLog.Length > maxLength)
            {
                // Calculate how many characters to trim from the start
                int charactersToTrim = myLog.Length - maxLength;

                // Trim characters from the start
                myLog = myLog.Substring(charactersToTrim, myLog.Length);
            }
        }

        void OnGUI()
        {
            // if (!Application.isEditor) //Do not display in editor ( or you can use the UNITY_EDITOR macro to also disable the rest)
            {
                // Set up the scroll view
                Rect scrollViewRect = new Rect(0, 0, Screen.width, Screen.height);
                Rect scrollContentRect = new Rect(0, 0, Screen.width, Mathf.Max(0, myLog.Length));

                // Create the scroll view
                scrollPosition = GUI.BeginScrollView(scrollViewRect, scrollPosition, scrollContentRect);

                // Background for the entire log area
                GUI.Box(new Rect(0, 0, Screen.width, Mathf.Max(Screen.height, scrollContentRect.height)), "");

                // Split the log into lines
                string[] logLines = myLog.Split('\n');

                // Display each line with its appropriate color
                for (int i = 0; i < logLines.Length; i++)
                {
                    string line = logLines[i];
                    Color lineColor = Color.white; // Default color

                    if (line.Contains("<color=#FF0000>"))
                    {
                        lineColor = Color.red;
                        line = line.Replace("<color=#FF0000>", "").Replace("</color>", "");
                    }
                    else if (line.Contains("<color=#FFFF00>"))
                    {
                        lineColor = Color.yellow;
                        line = line.Replace("<color=#FFFF00>", "").Replace("</color>", "");
                    }

                    GUIStyle style = new GUIStyle(GUI.skin.label);
                    style.normal.textColor = lineColor;
                    GUILayout.Label(line, style);
                }

                // End the scroll view
                GUI.EndScrollView();
            }
        }
    }
}
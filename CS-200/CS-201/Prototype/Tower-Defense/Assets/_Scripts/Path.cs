using UnityEngine;
using System.Linq;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class Path : MonoBehaviour
{
    #region variables
    [Header("Main Points")]
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject endPoint;
    [SerializeField] GameObject middlePointsHolder;
    [SerializeField] GameObject travelPointsHolder;
    [SerializeField] GameObject[] middlePoints;
    [SerializeField][Range(0.1f, 10f)] float travelMin;
    [Header("Prefabs")]
    [SerializeField] GameObject prefabStartPoint;
    [SerializeField] GameObject prefabEndPoint;
    [SerializeField] GameObject prefabMiddlePoint;
    [SerializeField] GameObject prefabMiddlePointHolder;
    [SerializeField] GameObject prefabTravelPoint;
    #endregion

    /// <summary>
    /// adds a point before the end point
    /// </summary>
    public void AddPoint()
    {
        middlePoints = middlePoints.Concat(new GameObject[] { GameObject.Instantiate(prefabMiddlePoint, middlePointsHolder.transform) }).ToArray();
        middlePoints[^1].name = middlePoints.Length.ToString() + "/" + middlePoints.Length.ToString();
        middlePoints[^1].transform.position = endPoint.transform.position;
        int index = 1;
        foreach(GameObject point in  middlePoints)
        {
            point.name = index++.ToString() + "/" + middlePoints.Length.ToString();
        }
        RedoTravel();
    }

    /// <summary>
    /// removes a point before the end point
    /// </summary>
    public void RemovePoint()
    {
        OnValidate();

        GameObject[] temp = middlePoints;
        middlePoints = new GameObject[temp.Length - 1];
        int index = 0;
        foreach (GameObject t in temp)
        {
            if (t != temp[^1])
            {
                middlePoints[index++] = t;
                if (t != null)
                {
                    t.name = index.ToString() + "/" + middlePoints.Length.ToString();
                }
            }
        }
        DestroyImmediate(temp[^1]);

        RedoTravel();
        return;
    }


    /// <summary>
    /// validates data in the script
    /// </summary>
    public void OnValidate()
    {
        if(startPoint == null)
        {
            startPoint = GameObject.Instantiate(prefabStartPoint, this.transform);
            startPoint.name = "Start";
        }
        if (endPoint == null)
        {
            endPoint = GameObject.Instantiate(prefabEndPoint, this.transform);
            endPoint.name = "End";
        }
        if (middlePointsHolder == null)
        {
            middlePointsHolder = Instantiate(prefabMiddlePointHolder, this.transform);
            middlePointsHolder.name = "MiddlePoints";
        }
        if (travelPointsHolder == null)
        {
            travelPointsHolder = Instantiate(prefabMiddlePointHolder, this.transform);
            travelPointsHolder.name = "TravelPoints";
        }
        foreach (GameObject point in middlePoints)
        {
            if(point == null)
            {
                GameObject[] temp = middlePoints;
                middlePoints = new GameObject[temp.Length - 1];
                int index = 0;
                foreach (GameObject t in temp)
                {
                    if (t != point)
                    {
                        middlePoints[index++] = t;
                        if(t != null)
                        {
                            t.name = index.ToString() + "/" + middlePoints.Length.ToString();
                        }
                    }
                }
                OnValidate();
                return;
            }
        }
#if UNITY_EDITOR
        UnityEditor.EditorApplication.delayCall += RedoTravel;
#else
    RedoTravel();
#endif
    }

    /// <summary>
    /// deletes and creates new travel point to indicate travel
    /// </summary>
    public void RedoTravel()
    {
        List<GameObject> temp = new();
        if(travelPointsHolder == null) { return; }
        foreach (Transform travelPoint in travelPointsHolder.transform)
        {
            temp.Add(travelPoint.gameObject);
        }
        foreach(GameObject t in temp) 
        {
            DestroyImmediate(t);
        }

        GameObject current = startPoint;
        GameObject nextPoint = (middlePoints.Length > 0) ? middlePoints[0] : endPoint;
        ComputeTravelBetween(current, nextPoint);

        for (int i = 0; i < middlePoints.Length; i++)
        {
            current = middlePoints[i];
            nextPoint = (current == middlePoints[^1]) ? endPoint : middlePoints[i + 1];
            ComputeTravelBetween(current, nextPoint);
        } 
    }

    public void ComputeTravelBetween(GameObject start, GameObject end)  
    {
        // Debug.Log("Calculating between " + start.name + " to " + end.name);
        float distanceBetween = Vector3.Distance(start.transform.position, end.transform.position);
        int count = (int)(distanceBetween / travelMin);

        for(int i = 1; i < count; i++)
        {
            Vector3 difference = (start.transform.position - end.transform.position);
            Vector3 posForTravel = start.transform.position - ((difference / count) * (i));
            GameObject travel = Instantiate(prefabTravelPoint, travelPointsHolder.transform);
            travel.transform.position = posForTravel;
            travel.transform.parent = travelPointsHolder.transform; 
        }
    }

    public GameObject[] GetAllPoints()
    {
        GameObject[] g = new GameObject[middlePoints.Length + 2];
        int i = 0;
        g[i++] = startPoint;
        foreach(GameObject point in middlePoints)
        {
            g[i++] = point;
        }
        g[i] = endPoint;

        return g;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Path))]
public class EDITOR_Path : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(30);

        Path p = (Path)target;

        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Add Point", GUILayout.Height(30)))
        {
            p.AddPoint();
        }
        if (GUILayout.Button("Remove Point", GUILayout.Height(30)))
        {
            p.RemovePoint();
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
        if (GUILayout.Button("Validate", GUILayout.Height(30)))
        {
            p.OnValidate();
        }
    }
}
#endif
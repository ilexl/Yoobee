using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [SerializeField] Path path;
    [SerializeField] GameObject[] allPoints;
    [SerializeField] Enemy stats;
    float speed = 1f; // default is no speed found is 1f
    [SerializeField]int followInternal;
    //[SerializeField] float believedDistance;
    public void SetPath(Path _path)
    {
        path = _path;
        allPoints = path.GetAllPoints();
        followInternal = 0;
        transform.position = allPoints[0].transform.position; // set position to start

        bool success = TryGetComponent<Enemy>(out stats);
        if(success)
        {
            speed = stats.GetSpeed();
        }
        else
        {
            Debug.LogWarning("No enemy script attached to this entity");
        }
    }

    public float RemainingDistance()
    {
        if (path == null) return 0f;

        float distance = 0f;
        for (int i = followInternal ; i < allPoints.Length -1; i++)
        {
            distance += Vector3.Distance(transform.position, allPoints[i].transform.position);
        }
        return distance;
    }

    // Update is called once per frame
    void Update()
    {
        if(path == null) return;

        // set rotation
        transform.LookAt(allPoints[followInternal].transform);
        //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0); // only need y rotation

        transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        float distance = Vector3.Distance(transform.position, allPoints[followInternal].transform.position);
        //believedDistance = distance;
        if (distance < 0.05f)
        {
            if(followInternal == allPoints.Length - 1)
            {
                // lose a life
                FindAnyObjectByType<Lives>().ChangeLives(-1);
                gameObject.SetActive(false); // destroys this gameobject
            }
            else
            {
                followInternal++; // goto next point
            }
        }
            

    }
}

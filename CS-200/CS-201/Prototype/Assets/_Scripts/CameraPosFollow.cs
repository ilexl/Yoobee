using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosFollow : MonoBehaviour
{
    [SerializeField] Transform follow;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = follow.position;
        transform.eulerAngles = new Vector3(0, follow.eulerAngles.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = follow.position;
        transform.eulerAngles = new Vector3(0, follow.eulerAngles.y, 0);
    }
}

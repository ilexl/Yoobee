using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] float speed;
    // Update is called once per frame
    void Update()
    {
        // rotates... thats all it does
        transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
    }
}

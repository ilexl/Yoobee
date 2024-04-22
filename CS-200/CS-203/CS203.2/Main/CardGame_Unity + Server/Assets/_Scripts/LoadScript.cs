using UnityEngine;

public class LoadScript : MonoBehaviour
{
    [SerializeField] GameObject[] gameObjects;

    // Awake is called when the script is 
    void Awake()
    {
        // wakes up each sleeping object and sends it back to sleep to load it in memory
        foreach (GameObject obj in gameObjects)
        {
            if(obj.activeSelf == false)
            {
                obj.SetActive(true);
                obj.SetActive(false);
            }
        }
    }
}

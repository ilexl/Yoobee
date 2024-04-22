using UnityEngine;
using TMPro;

public class MainMenuLetters : MonoBehaviour
{
    #region local variables
    [SerializeField] GameObject letterPrefab;
    [SerializeField] int amountOfLetters = 0;
    [SerializeField] Transform parent;
    [SerializeField] RectTransform canvas;
    [SerializeField] [Range(0, 1)] float padding;
    [SerializeField] float maxOffsetx;
    [SerializeField] float maxOffsety;
    [SerializeField] bool animateLetters = true;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region error checking
        // Error handling
        if (letterPrefab == null)
        {
            Debug.LogError("No Letter Prefab Found");
            return;
        }
        if(parent == null)
        {
            Debug.LogError("No parent found");
            return;
        }
        if(amountOfLetters == 0)
        {
            Debug.LogWarning("Amount of letters was ZERO");
            return;
        }
        if(amountOfLetters < 0)
        {
            Debug.LogError("Amount can NOT be less than ZERO");
            return;
        }
        if (canvas == null)
        {
            Debug.LogError("No Canvas Found");
            return;
        }

        #endregion

        // spawn letters
        for (int i = 0; i < amountOfLetters; i++)
        {
            GameObject letter = Instantiate(letterPrefab, parent);
            letter.transform.localPosition = RandomPosition();
            letter.GetComponent<TextMeshProUGUI>().text = RandomLetter();
        }
    }

    /// <summary>
    /// creates a psuedo random position on the canvas
    /// </summary>
    /// <returns>Vector3 newPosition</returns>
    Vector3 RandomPosition()
    {
        if(canvas == null)
        {
            Debug.LogError("No Canvas Found");
            return Vector3.zero;

        }

        float xRandomValue = Random.Range(padding * 10, canvas.rect.width - (maxOffsetx - padding)) - canvas.transform.position.x / canvas.localScale.x;
        float yRandomValue = Random.Range((padding * 10) + maxOffsety, canvas.rect.height - padding) - canvas.transform.position.y / canvas.localScale.y;

        //Debug.Log(new Vector3(xRandomValue, yRandomValue, 0));

        return new Vector3(xRandomValue, yRandomValue, 0);

    }

    /// <summary>
    /// selects a random letter
    /// </summary>
    /// <returns>char random</returns>
    string RandomLetter()
    { 
        char random = (char)Random.Range(65, 91);
        return random.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // animates the letters if needed [depricated]
        if (animateLetters)
        {
            foreach(Transform l in parent)
            {
                l.Rotate(300 * Time.deltaTime * new Vector3(0, 0, Random.value - 0.5f));
            }
        }
    }
}

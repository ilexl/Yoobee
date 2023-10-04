using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float distanceToEnd = 0;
    [SerializeField] float speed;
    [SerializeField] private int health;
    private int maxHealth;
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject healthBar;
    [SerializeField] private int deathMoney;
    [SerializeField] private int scoreAmount;

    public float GetSpeed() { return speed; }
    public int GetHealth() { return health; }

    private void Awake()
    {
        maxHealth = health;
        Canvas.GetComponent<Canvas>().worldCamera = Camera.main;
        healthBar.GetComponent<UnityEngine.UI.Slider>().minValue = 0;
        healthBar.GetComponent<UnityEngine.UI.Slider>().maxValue = maxHealth;    
    }

    private void Update()
    {
        distanceToEnd = GetComponent<FollowPath>().RemainingDistance();
        Canvas.transform.LookAt(Camera.main.transform.position);
        Canvas.transform.Rotate(new Vector3(0, 180, 0));
        healthBar.GetComponent<Slider>().value = health;

        if(health <= 0)
        {
            FindAnyObjectByType<Money>().ChangeBalance(deathMoney);
            FindAnyObjectByType<Score>().ChangeScore(scoreAmount);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}

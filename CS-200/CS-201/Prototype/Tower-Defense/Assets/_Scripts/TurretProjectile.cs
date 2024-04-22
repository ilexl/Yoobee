using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    public List<GameObject> targetPositions = new();
    public int currentTarget = 0;
    public float projectileSpeed = 1;
    public int damage;
    public GameObject explosionPrefab;
    public SoundManager soundManager;

    // OnEnable is called when the script is enabled
    void OnEnable()
    {
        currentTarget = 0;




    }

    // Update is called once per frame
    void Update()
    {
        if (targetPositions[currentTarget] == null)
        {
            Explode();
            return;
        }

        if(targetPositions.Count <= 0) { return; }
        transform.LookAt(targetPositions[currentTarget].transform);
        transform.Translate(new Vector3(0, 0, projectileSpeed * Time.deltaTime));
        if(Vector3.Distance(transform.position, targetPositions[currentTarget].transform.position) < 0.1f)
        {
            // move to next target if able
            if(currentTarget < targetPositions.Count - 1)
            {
                //currentTarget++;
                Destroy(targetPositions[currentTarget++]);
            }
            else
            {
                // reached final target - explode and damage etc
                Explode();
            }
        }
    }

    void Explode()
    {
        soundManager.PlaySoundInWorldSpace(0, transform.position);
        //Debug.Log("Explosion needs implementing!");
        if (targetPositions[currentTarget] != null)
        {
            bool isEnemy = targetPositions[currentTarget].TryGetComponent<Enemy>(out Enemy enemy);
            if(isEnemy)
            {
                enemy.TakeDamage(damage);
            }

            
        }
        GameObject explosion = Instantiate(explosionPrefab, transform.parent);
        explosion.transform.position = transform.position;
        Destroy(gameObject);
    }
}

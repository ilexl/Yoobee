using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTargeting : MonoBehaviour
{
    [SerializeField] TurretManager manager;
    [SerializeField] float idleRotateSpeed = 0;
    [SerializeField] GameObject currentTarget;
    [SerializeField] Transform EnemiesParent;
    [Space]
    [SerializeField] float currentReloadTime;
    [SerializeField] GameObject projectilesParent;
    [SerializeField] float midHeight;
    [SerializeField] GameObject emptyPrefab;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] SoundManager soundManager;
    [SerializeField] int damageMultiplier;

    private void Awake()
    {
        if (EnemiesParent == null) { EnemiesParent = GameObject.Find("Enemies").transform; }
        if(projectilesParent == null) { projectilesParent = GameObject.Find("Projectiles"); }
        if(soundManager == null) { soundManager = GameObject.Find("SoundGame").GetComponent<SoundManager>(); }
    }

    // Update is called once per frame
    void Update()
    {
        if(manager == null) { return; }
        Turret turret = manager.GetCurrent();
        if(turret == null) { return; }
        currentTarget = null;

        foreach(Transform e in EnemiesParent)
        {
            if (Vector3.Distance(transform.position, e.position) > turret.range * manager.rangeMultiplier)
            {
                continue; // not in range
            }
            if (!e.TryGetComponent<Enemy>(out Enemy enemy)) { continue; }
            if(currentTarget == null)
            {
                currentTarget = e.gameObject;
                continue;
            }
            if (currentTarget.GetComponent<Enemy>().distanceToEnd > enemy.distanceToEnd)
            {
                // if the distance to the end is shorter then we will target that enemy instead
                currentTarget = e.gameObject;
                continue;
            }
        }

        if(currentTarget != null)
        {
            manager.GetTurretModel().transform.LookAt(currentTarget.transform);
            manager.GetTurretModel().transform.eulerAngles = new Vector3(0, manager.GetTurretModel().transform.eulerAngles.y, 0);
        }
        else
        {
            manager.GetTurretModel().transform.Rotate(new Vector3(0, Time.deltaTime * idleRotateSpeed, 0));
        }

        if(manager.GetState() != TurretManager.State.Active) { return; }

        if(currentReloadTime <= 0f)
        {
            if(currentTarget == null) {  return; }
            currentReloadTime = turret.reloadTime;
            SpawnProjectile(currentTarget, turret);
        }
        else
        {
            currentReloadTime -= Time.deltaTime;
        }
    }

    void SpawnProjectile(GameObject target, Turret turret)
    {
        // calculate mid height target
        Vector3 diff = (target.transform.position - transform.position) / 2;
        Vector3 midPos = transform.position + diff + new Vector3(0, midHeight, 0); // ------------------------------------------------------
        GameObject midPoint = Instantiate(emptyPrefab, projectilesParent.transform);
        midPoint.transform.position = midPos;

        // create prefab
        GameObject projectile = Instantiate(turret.projectilePrefab, projectilesParent.transform);
        projectile.AddComponent(typeof(TurretProjectile));
        TurretProjectile tp = projectile.GetComponent<TurretProjectile>();

        // set position and variables
        tp.transform.position = transform.position;
        tp.projectileSpeed = turret.projectileSpeed;
        tp.damage = turret.damage * damageMultiplier;
        tp.targetPositions = new List<GameObject> { midPoint, target };
        tp.explosionPrefab = explosionPrefab;
        tp.soundManager = soundManager;
    }


}

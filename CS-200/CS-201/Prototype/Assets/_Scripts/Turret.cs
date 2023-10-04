using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Game/Turret")]
public class Turret : ScriptableObject
{
    public GameObject modelPrefab;
    public GameObject projectilePrefab;
    [Range(0, 5f)] public int damage;
    public Sprite icon;
    [Range(0, 5f)] public float range;
    [Range(0, 2.5f)] public float reloadTime;
    public float projectileSpeed;
    public int cost;
    public string title;


    public Turret(GameObject _modelPrefab, GameObject _projectilePrefab, int _damage, Sprite _icon, float _range, float _reloadTime, float _projectileSpeed, int _cost, string _title)
    {
        modelPrefab = _modelPrefab;
        projectilePrefab = _projectilePrefab;
        damage = _damage;
        icon = _icon;
        range = _range;
        reloadTime = _reloadTime;
        projectileSpeed = _projectileSpeed;
        cost = _cost;
        title = _title;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Wave")]
public class PathSpawn : ScriptableObject
{
    public GameObject[] enemy;
    public float[] timeBetweenEnemy;

    public PathSpawn(GameObject[] _enemy, float[] _timeBetweenEnemy)
    {
        enemy = _enemy;
        timeBetweenEnemy = _timeBetweenEnemy;
    }
}
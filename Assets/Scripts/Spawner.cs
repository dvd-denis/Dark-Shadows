using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public GameObject[] SpawnerObject;

    public void Summon()
    {
        GameObject frag = Instantiate(SpawnerObject[Random.Range(0, SpawnerObject.Length)], transform.position, transform.rotation, transform);
        Room.countEnemyinRoom++;
    }
}

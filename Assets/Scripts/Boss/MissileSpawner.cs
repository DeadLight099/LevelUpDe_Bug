using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [SerializeField] private float spawnCooldown;
    [SerializeField] private GameObject missile;
    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }
    private IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(spawnCooldown);
        Instantiate(missile, transform.position, Quaternion.identity);
        StartCoroutine(SpawnCoroutine());
    }
}

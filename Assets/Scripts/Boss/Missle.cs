using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    private Transform target;
    public float speed = 5f;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        transform.LookAt(target);
        gameObject.transform.position += (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerDeath>().Death();
        }
        Destroy(gameObject);
    }
}

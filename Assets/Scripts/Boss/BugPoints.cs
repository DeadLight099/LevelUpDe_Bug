using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugPoints : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            FindObjectOfType<Boss>().GetDamage();

        Destroy(gameObject);
    }
}

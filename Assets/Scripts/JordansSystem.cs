using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JordansSystem : MonoBehaviour
{
    public int speedRotation;
    public void FixedUpdate()
    {
        Quaternion rotationZ = Quaternion.AngleAxis(1, new Vector3(0, 1, 0) * speedRotation);
        transform.rotation *= rotationZ;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerDash>().shouldDash = true;
            Destroy(gameObject);
        }
    }
}

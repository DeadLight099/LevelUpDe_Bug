using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private Transform target;
    public int health = 4;

    private void Update()
    {
        transform.LookAt(target);
    }
    private void Death()
    {
        if(health <= 0)
        {
            FindObjectOfType<SceneController>().LoadLevel("FinalComic");
        }
    }
    public void GetDamage()
    {
        health -= 1;
        SoundManager.instance.Play("EnemyDeath");
        Death();
    }
}

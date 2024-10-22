using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject objectToRotate;
    [SerializeField] private GameObject particlePrefab;
    [SerializeField] private bool shouldLook;
    private PlayerMovement player;
    private Transform playerPos;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        playerPos = player.transform; 
    }
    private void Update()
    {
        if(shouldLook)
            objectToRotate.transform.LookAt(playerPos);
    }
    public bool Interact()
    {
        Death();
        return true;
    }
    private void Death()
    {
        player.GetJump();
        SoundManager.instance.Play("EnemyDeath");
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerDeath>().Death();
        }
    }
}

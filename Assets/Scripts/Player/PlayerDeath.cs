using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;

    public void Death()
    {
        deathScreen.SetActive(true);
        SoundManager.instance.Play("Death");
        GetComponent<PlayerDash>().enabled = false;
        GetComponent<PlayerMovement>().enabled = false;
    }
}

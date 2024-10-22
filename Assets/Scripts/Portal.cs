using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;
    [SerializeField] private string nextLevel;
    private SceneController sceneController;

    private void Start()
    {
        sceneController = FindObjectOfType<SceneController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.instance.Play("Win");
            winScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            other.GetComponent<PlayerMovement>().enabled = false;
            other.GetComponent<PlayerDash>().enabled = false;
        }
    }
    public void LoadButton()
    {
        sceneController.LoadLevel(nextLevel);
    }
}

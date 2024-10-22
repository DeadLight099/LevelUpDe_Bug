using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private DialogController dialog;
    [SerializeField] private float timeForText;
    [SerializeField, TextArea] private string text;
    private void OnTriggerEnter(Collider other)
    {   
        if (other.CompareTag("Player"))
        {
            StartCoroutine(dialog.SetText(text, timeForText));
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}

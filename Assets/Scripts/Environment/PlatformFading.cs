using System.Collections;
using UnityEngine;

public class PlatformFading : MonoBehaviour
{
    [SerializeField] private int timeFading;
    [SerializeField] private int timeFadingOut;

    public Animator anim;
    public GameObject triggerObject;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadingPlat(timeFading, timeFadingOut));
        }
    }

    public IEnumerator FadingPlat(int timeforfade, int timefadeout)
    {
        anim.SetBool("Fading", true);
        yield return new WaitForSeconds(timeforfade);
        triggerObject.GetComponent<BoxCollider>().enabled = false; triggerObject.GetComponent<MeshRenderer>().enabled = false; gameObject.GetComponent<BoxCollider>().enabled = false;
        anim.SetBool("Fading", false);
        yield return new WaitForSeconds(timefadeout);
        triggerObject.GetComponent<BoxCollider>().enabled = true; triggerObject.GetComponent<MeshRenderer>().enabled = true; gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}

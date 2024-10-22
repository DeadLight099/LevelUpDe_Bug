using System.Collections;
using TMPro;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public GameObject dialogBar;
    public TextMeshProUGUI textDialog;

    public IEnumerator SetText(string fortext, float time)
    {
        dialogBar.SetActive(true);
        textDialog.text = fortext;
        yield return new WaitForSeconds(time);
        dialogBar.SetActive(false);
        textDialog.text = "";
    }
}

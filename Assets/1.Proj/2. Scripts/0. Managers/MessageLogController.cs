using System.Collections;
using TMPro;
using UnityEngine;

public class MessageLogController : MonoBehaviour
{
    public TMP_Text messageText;
    public float displayDuration = 2f;
    
    public void Start()
    {
        messageText.text = "";
    }

    public void LogMessage(string message)
    {
        messageText.text = message;
        messageText.enabled = true;
        StartCoroutine(HideMessage());
    }

    private IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(displayDuration);
        messageText.enabled = false;
    }
}

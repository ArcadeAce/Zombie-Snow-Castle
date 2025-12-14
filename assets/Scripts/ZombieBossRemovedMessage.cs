using UnityEngine;
using TMPro;

public class ZombieBossRemovedMessage : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public string customMessage = "Zombie bosses removed 3";
    private bool messageActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !messageActive)
        {
            // Start a coroutine to display and move the message
            StartCoroutine(DisplayAndMoveMessage());
        }
    }

    private System.Collections.IEnumerator DisplayAndMoveMessage()
    {
        // Display the custom message in the TextMeshPro text component
        messageText.text = customMessage;
        messageActive = true;

        // Move the message down from the top of the screen
        float startY = 250f; // Adjust this value as needed
        float targetY = 200f; // The Y position where the message should stay
        float moveSpeed = 1.30f; // The speed at which the message moves

        float elapsedTime = 0f;
        while (elapsedTime < 1f / moveSpeed)
        {
            float newY = Mathf.Lerp(startY, targetY, elapsedTime * moveSpeed);
            messageText.rectTransform.localPosition = new Vector3(0f, newY, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Wait for 5 seconds
        yield return new WaitForSeconds(12f);

        // Slowly move the message back up
        elapsedTime = 0f;
        while (elapsedTime < 1f / moveSpeed)
        {
            float newY = Mathf.Lerp(targetY, startY, elapsedTime * moveSpeed);
            messageText.rectTransform.localPosition = new Vector3(0f, newY, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Hide the message and reset the messageActive flag
        messageText.text = "";
        messageActive = false;
    }
}


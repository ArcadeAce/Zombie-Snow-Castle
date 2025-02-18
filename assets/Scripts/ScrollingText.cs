using System.Collections;
using UnityEngine;
using TMPro;

public class ScrollingText : MonoBehaviour
{
    public float scrollSpeed = 10f;

    private TMP_Text textMesh;
    private RectTransform textRectTransform;

    private void Start()
    {
        textMesh = GetComponentInChildren<TMP_Text>();
        textRectTransform = textMesh.GetComponent<RectTransform>();
        StartCoroutine(ScrollText());
    }

    private IEnumerator ScrollText()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second before starting scrolling

        float height = textMesh.preferredHeight;
        float startYPosition = textRectTransform.anchoredPosition.y;

        while (true)
        {
            float newYPosition = textRectTransform.anchoredPosition.y + scrollSpeed * Time.deltaTime;

            if (newYPosition > startYPosition + height)
            {
                newYPosition = startYPosition;
            }

            textRectTransform.anchoredPosition = new Vector2(textRectTransform.anchoredPosition.x, newYPosition);

            yield return null;
        }
    }
}











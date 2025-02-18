using UnityEngine;

public class TransparentAnimation : MonoBehaviour
{
    public float transparentDuration = 2f; // Duration for the transparency effect
    public Animation Animation; // Reference to the animation component
    public Material transparentMaterial; // Reference to the transparent material

    private Material[] originalMaterials; // To store the original materials

    private void Start()
    {
        // Store the original materials of the GameObject
        originalMaterials = GetComponent<Renderer>().materials;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Trigger the transparency animation
            StartCoroutine(AnimateTransparency());
        }
    }

    private System.Collections.IEnumerator AnimateTransparency()
    {
        // Set the transparent material to all materials
        Material[] newMaterials = new Material[originalMaterials.Length];
        for (int i = 0; i < newMaterials.Length; i++)
        {
            newMaterials[i] = transparentMaterial;
        }
        GetComponent<Renderer>().materials = newMaterials;

        // Gradually decrease alpha value to make the object transparent
        float startTime = Time.time;
        Color startColor = transparentMaterial.color;
        Color targetColor = new Color(
            startColor.r,
            startColor.g,
            startColor.b,
            0f
        );

        while (Time.time - startTime < transparentDuration)
        {
            float t = (Time.time - startTime) / transparentDuration;
            Color newColor = Color.Lerp(startColor, targetColor, t);

            foreach (Material mat in GetComponent<Renderer>().materials)
            {
                mat.color = newColor;
            }

            yield return null;
        }

        // Restore the original materials
        GetComponent<Renderer>().materials = originalMaterials;
    }
}




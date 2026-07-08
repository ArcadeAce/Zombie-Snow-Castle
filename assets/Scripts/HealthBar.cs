using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Slider _HealthBar;              // The health bar slider
    public TextMeshProUGUI _GameOverText;  // Game Over text
    public Image[] _Hearts;                // Heart icons (3 hearts)

    private void Start()
    {
        // Initialize health bar and hearts based on PlayerManager values
        InitializeHealthBar();
    }

    public void InitializeHealthBar()
    {
        UpdateHealth(PlayerManager.Instance.curHealth / PlayerManager.Instance.PlayerHealth,
                     PlayerManager.Instance.lives);
    }

    public void UpdateHealth(float normalizedValue, int lives)
    {
        // Update the health bar fill
        _HealthBar.value = normalizedValue;

        // ⭐ Reset ALL hearts first (Unity loads them enabled every scene)
        for (int i = 0; i < _Hearts.Length; i++)
        {
            _Hearts[i].enabled = true;
        }

        // ⭐ Disable hearts based on lives
        // If lives = 3 → show 
        // If lives = 2 → show 
        // If lives = 1 → show 
        // If lives = 0 → show 
        for (int i = lives; i < _Hearts.Length; i++)
        {
            _Hearts[i].enabled = false;
        }
    }

    public IEnumerator _GameOver()
    {
        _HealthBar.value = 0f;
        _GameOverText.enabled = true;

        yield return new WaitForSeconds(3f);

        PlayerManager.Instance.Die();
    }

    public void TakeDamage(float amount)
    {
        PlayerManager.Instance.TakeDamage((int)amount);

        UpdateHealth(PlayerManager.Instance.curHealth / PlayerManager.Instance.PlayerHealth,
                     PlayerManager.Instance.lives);

        if (PlayerManager.Instance.curHealth <= 0)
        {
            StartCoroutine(_GameOver());
        }
    }

    public void Heal(float amount)
    {
        PlayerManager.Instance.curHealth =
            Mathf.Min(PlayerManager.Instance.curHealth + amount,
                      PlayerManager.Instance.PlayerHealth);

        UpdateHealth(PlayerManager.Instance.curHealth / PlayerManager.Instance.PlayerHealth,
                     PlayerManager.Instance.lives);
    }
}








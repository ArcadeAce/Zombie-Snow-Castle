using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider _HealthBar; // Reference to the health bar slider UI element
    public TextMeshProUGUI _GameOverText; // Reference to the game over text UI element
    public Image[] _Hearts; // Reference to the heart UI elements

    private int _index; // To keep track of the current heart index

    private void Awake()
    {
        _index = 2; // Initialize _index variable
    }

    private void Start()
    {
        // Initialize health bar with current health
        InitializeHealthBar();
    }

    public void InitializeHealthBar()
    {
        // Ensure current health and lives are correctly initialized
        UpdateHealth(PlayerManager.Instance.curHealth / PlayerManager.Instance.PlayerHealth, PlayerManager.Instance.lives);
    }

    public void UpdateHealth(float normalizedValue, int lives)
    {
        _HealthBar.value = normalizedValue; // Set the value directly as the normalized health value

        if (lives - 1 < _index)
        {
            UpdateHearts();
        }
    }

    private void UpdateHearts()
    {
        if (_index >= 0 && _index < _Hearts.Length)
        {
            _Hearts[_index].enabled = false;
            _index--;
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
        UpdateHealth(PlayerManager.Instance.curHealth / PlayerManager.Instance.PlayerHealth, PlayerManager.Instance.lives);

        if (PlayerManager.Instance.curHealth <= 0)
        {
            StartCoroutine(_GameOver());
        }
    }

    public void Heal(float amount)
    {
        PlayerManager.Instance.curHealth = Mathf.Min(PlayerManager.Instance.curHealth + amount, PlayerManager.Instance.PlayerHealth);
        UpdateHealth(PlayerManager.Instance.curHealth / PlayerManager.Instance.PlayerHealth, PlayerManager.Instance.lives);
    }
}







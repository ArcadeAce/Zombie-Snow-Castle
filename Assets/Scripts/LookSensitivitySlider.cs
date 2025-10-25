using UnityEngine;
using UnityEngine.UI;

public class LookSensitivitySlider : MonoBehaviour
{
    [Header("Joystick Reference")]
    [SerializeField] private UIVirtualJoystick lookJoystick;

    [Header("Settings")]
    [SerializeField] private float minSensitivity = 0.5f;
    [SerializeField] private float maxSensitivity = 2.5f;

    private Slider sensitivitySlider;
    private const string saveKey = "LookSensitivity";

    void Awake()
    {
        sensitivitySlider = GetComponent<Slider>();
    }

    void Start()
    {
        float savedSensitivity = PlayerPrefs.GetFloat(saveKey, 1.0f);

        sensitivitySlider.minValue = minSensitivity;
        sensitivitySlider.maxValue = maxSensitivity;
        sensitivitySlider.value = savedSensitivity;

        lookJoystick.magnitudeMultiplier = savedSensitivity;

        sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);
    }

    void UpdateSensitivity(float value)
    {
        lookJoystick.magnitudeMultiplier = value;
        PlayerPrefs.SetFloat(saveKey, value);
    }
}


using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIVirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    // Define a serializable class for custom event with Vector2 parameter
    [System.Serializable]
    public class Event : UnityEvent<Vector2> { }

    // References to RectTransforms for the joystick's background and handle
    [Header("Rect References")]
    public RectTransform containerRect; // Background area of the joystick
    public RectTransform handleRect;    // Draggable handle of the joystick

    // Settings for the joystick control
    [Header("Settings")]
    public float joystickRange = 50f;   // Maximum distance the handle can move from the center
    public float magnitudeMultiplier = 1f; // Multiplier applied to the output vector's magnitude
    public bool invertXOutputValue;     // Flag to invert output values along the X-axis
    public bool invertYOutputValue;     // Flag to invert output values along the Y-axis

    // Event for joystick output
    [Header("Output")]
    public Event joystickOutputEvent;   // Event invoked with a Vector2 parameter representing joystick direction

    // Start is called before the first frame update
    void Start()
    {
        // Call SetupHandle method to initialize joystick handle
        SetupHandle();
    }

    // Initialize the joystick handle
    private void SetupHandle()
    {
        // Check if handleRect is assigned
        if (handleRect)
        {
            // Set initial position of the handle to zero
            UpdateHandleRectPosition(Vector2.zero);
        }
    }

    // Called when a pointer is pressed on the joystick
    public void OnPointerDown(PointerEventData eventData)
    {
        // Pass pointer down event to OnDrag method
        OnDrag(eventData);
    }

    // Called while the pointer is dragged on the joystick
    public void OnDrag(PointerEventData eventData)
    {
        // Convert screen point to local point in the containerRect
        RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRect, eventData.position, eventData.pressEventCamera, out Vector2 position);

        // Apply size delta to position
        position = ApplySizeDelta(position);

        // Clamp position values to maximum magnitude of 1
        Vector2 clampedPosition = ClampValuesToMagnitude(position);

        // Apply inversion filter to position if required
        Vector2 outputPosition = ApplyInversionFilter(position);

        // Output the joystick position through the joystickOutputEvent
        OutputPointerEventValue(outputPosition * magnitudeMultiplier);

        // Update the position of the joystick handle
        if (handleRect)
        {
            UpdateHandleRectPosition(clampedPosition * joystickRange);
        }
    }

    // Called when a pointer is released from the joystick
    public void OnPointerUp(PointerEventData eventData)
    {
        // Output zero vector to stop joystick movement
        OutputPointerEventValue(Vector2.zero);

        // Reset joystick handle position
        if (handleRect)
        {
            UpdateHandleRectPosition(Vector2.zero);
        }
    }

    // Output joystick position through the joystickOutputEvent
    private void OutputPointerEventValue(Vector2 pointerPosition)
    {
        joystickOutputEvent.Invoke(pointerPosition);
    }

    // Update the position of the joystick handle
    private void UpdateHandleRectPosition(Vector2 newPosition)
    {
        handleRect.anchoredPosition = newPosition;
    }

    // Apply size delta to position
    Vector2 ApplySizeDelta(Vector2 position)
    {
        float x = (position.x / containerRect.sizeDelta.x) * 2.5f;
        float y = (position.y / containerRect.sizeDelta.y) * 2.5f;
        return new Vector2(x, y);
    }

    // Clamp position values to maximum magnitude of 1
    Vector2 ClampValuesToMagnitude(Vector2 position)
    {
        return Vector2.ClampMagnitude(position, 1);
    }

    // Apply inversion filter to position if required
    Vector2 ApplyInversionFilter(Vector2 position)
    {
        if (invertXOutputValue)
        {
            position.x = InvertValue(position.x);
        }

        if (invertYOutputValue)
        {
            position.y = InvertValue(position.y);
        }

        return position;
    }

    // Invert the value
    float InvertValue(float value)
    {
        return -value;
    }
}

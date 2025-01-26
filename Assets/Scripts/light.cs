using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class light : MonoBehaviour
{
    public InputActionReference changeLightColor;
    private Light lightComponent;
    private bool isBlue = true;

    void Start()
    {
        // Get the Light component attached to the GameObject
        lightComponent = GetComponent<Light>();
        if (lightComponent == null)
        {
            Debug.LogError("No Light component found on this GameObject!");
        }
        else
        {
            // Initialize the light color to blue
            lightComponent.color = Color.blue;
        }
    }

    void Update()
    {
        
        // Check if the Tab key is pressed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleLightColor();
        }

        // Check if the Space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleLightState();
        }

    }

    void OnEnable()
    {
        // Enable the input action and bind the method to it
        changeLightColor.action.Enable();
        changeLightColor.action.performed += OnTriggerPressed;
    }

    void OnDisable()
    {
        // Unbind the method and disable the input action
        changeLightColor.action.performed -= OnTriggerPressed;
        changeLightColor.action.Disable();
    }

    void OnTriggerPressed(InputAction.CallbackContext context)
    {
        // Log the trigger value
        float triggerValue = context.ReadValue<float>();
        Debug.Log($"Trigger value: {triggerValue}");

        if (triggerValue > 0.5f) // Consider trigger "pressed" when value is greater than 0.5
        {
            ToggleLightColor();
        }
    }

    void ToggleLightColor()
    {
        if (lightComponent != null)
        {
            // Toggle between blue and red
            if (isBlue)
            {
                lightComponent.color = Color.red;
            }
            else
            {
                lightComponent.color = Color.blue;
            }

            // Flip the isBlue flag
            isBlue = !isBlue;
        }
    }

    void ToggleLightState()
    {
        if (lightComponent != null)
        {
            // Toggle the light on/off
            lightComponent.enabled = !lightComponent.enabled;
        }
    }

}

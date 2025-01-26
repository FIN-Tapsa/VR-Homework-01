using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// A script to manage player position switching, light state and color toggling, and application quitting.
/// Uses Unity's Input System to handle input actions.
/// </summary>
public class PlayerPositionSwitcher : MonoBehaviour
{
    // Public variables
    [Header("Input Actions")]
    public InputActionReference switchPosition; // Input action to switch positions
    public InputActionReference quitButton; // Input action to quit the application
    public InputActionReference turnOffLigt; // Input action to toggle light state
    public InputActionReference changeLightColor; // Input action to toggle light color

    [Header("Light Settings")]
    public Light Valo; // Reference to the Light component

    [Header("Positions and Rotations to Cycle Through")]
    public Vector3[] positions = new Vector3[2]
    {
        new Vector3(0, 1.8f, 0), // Default position
        new Vector3(3.78f, 1.17f, 3.16f) // Alternate position
    };

    public Vector3[] rotations = new Vector3[2]
    {
        new Vector3(0, 0, 0), // Default rotation
        new Vector3(0, 145, 0) // Alternate rotation
    };

    // Private variables
    private int currentPositionIndex = 0; // Tracks the current position index
    private bool isBlue = true; // Tracks the current light color state
    private Light lightComponent; // Cached reference to the Light component

    void Start()
    {
        // Get the Light component attached to the specified GameObject
        lightComponent = Valo.GetComponent<Light>();
        if (lightComponent == null)
        {
            Debug.LogError("No Light component found on the specified GameObject!");
        }
        else
        {
            // Initialize the light color to blue
            lightComponent.color = Color.blue;
        }
    }

    void Update()
    {
        // Optional controls instead of vr controllers
        // Handle escape key to quit application
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CyclePositionAndRotation();
        }
        // Handle tab key to switch positions
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           Quit();
        }

        // Handle space key to toggle light color
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleLightColor();
        }

        // Handle X key to toggle light state
        if (Input.GetKeyDown(KeyCode.X))
        {
            ToggleLightState();
        }
    }

    /// <summary>
    /// Toggles the light's enabled state (on/off).
    /// </summary>
    void ToggleLightState()
    {
        if (lightComponent != null)
        {
            lightComponent.enabled = !lightComponent.enabled;
        }
    }

    /// <summary>
    /// Toggles the light's color between blue and red.
    /// </summary>
    void ToggleLightColor()
    {
        if (lightComponent != null)
        {
            lightComponent.color = isBlue ? Color.red : Color.blue;
            isBlue = !isBlue;
        }
    }

    void OnEnable()
    {
        // Enable input actions and bind methods to their performed events
        switchPosition.action.Enable();
        switchPosition.action.performed += TriggerPosition;

        quitButton.action.Enable();
        quitButton.action.performed += TriggerQuit;

        turnOffLigt.action.Enable();
        turnOffLigt.action.performed += TriggerTurnOffLight;

        changeLightColor.action.Enable();
        changeLightColor.action.performed += TriggerChangeLightColor;
    }

    void OnDisable()
    {
        // Unbind methods from events and disable input actions
        switchPosition.action.performed -= TriggerPosition;
        switchPosition.action.Disable();

        quitButton.action.performed -= TriggerQuit;
        quitButton.action.Disable();

        turnOffLigt.action.performed -= TriggerTurnOffLight;
        turnOffLigt.action.Disable();

        changeLightColor.action.performed -= TriggerChangeLightColor;
        changeLightColor.action.Disable();
    }

    /// <summary>
    /// Handles the trigger to turn off the light.
    /// </summary>
    void TriggerTurnOffLight(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.5f) // Consider trigger "pressed" when value > 0.5
        {
            ToggleLightState();
        }
    }

    /// <summary>
    /// Handles the trigger to change the light color.
    /// </summary>
    void TriggerChangeLightColor(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.5f) // Consider trigger "pressed" when value > 0.5
        {
            ToggleLightColor();
        }
    }

    /// <summary>
    /// Handles the trigger to quit the application.
    /// </summary>
    void TriggerQuit(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.5f) // Consider trigger "pressed" when value > 0.5
        {
            Quit();
        }
    }

    /// <summary>
    /// Handles the trigger to switch the player's position and rotation.
    /// </summary>
    void TriggerPosition(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() > 0.5f) // Consider trigger "pressed" when value > 0.5
        {
            CyclePositionAndRotation();
        }
    }

    /// <summary>
    /// Cycles through predefined positions and rotations.
    /// </summary>
    void CyclePositionAndRotation()
    {
        currentPositionIndex = (currentPositionIndex + 1) % positions.Length;

        // Update position and rotation
        transform.position = positions[currentPositionIndex];
        transform.eulerAngles = rotations[currentPositionIndex];
    }

    /// <summary>
    /// Quits the application. In the Unity Editor, stops play mode.
    /// </summary>
    void Quit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("Speed of the planet's rotation (degrees per second).")]
    public float rotationSpeed = 10f;

    void Update()
    {
        // Rotate the planet around its Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}

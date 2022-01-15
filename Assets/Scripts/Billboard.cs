using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for Look at to camera transform
/// </summary>
public class Billboard : MonoBehaviour
{
    /// <summary>
    /// Camera for lookat target
    /// </summary>
    Camera cam;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled
    /// </summary>
    void Update()
    {
        // Try find camera in scene
        if (cam == null) cam = FindObjectOfType<Camera>();
        if (cam == null) return;

        transform.LookAt(cam.transform);
        transform.Rotate(Vector3.up * 180);
    }
}

/* Author: Emmanuel (Manny) Vaccaro
 * Date: 05/03/2020
 * Brief: Component for controlling Camera Orbit at Runtime
 */

using UnityEngine;

[AddComponentMenu("Camera Control/Mouse Orbit")] // Adds this script to the 'Add Component' Menu under 'Camera Control'
public class MouseOrbit : MonoBehaviour
{
    #region Variables
    public bool hideCursor = true;      // Should the Cursor be hidden?
    public Transform target;            // Defined Target (defaults to zero if not referencing anything)
    public float yawSpeed = 120.0f;     // Speed of Yaw (Degrees)
    public float pitchSpeed = 120.0f;   // Speed of Pitch (Degrees)
    public float pitchMinLimit = -20f;  // Min Pitch Limit (Degrees)
    public float pitchMaxLimit = 80f;   // Max Pitch Limit (Degrees)
    public float distanceMin = .5f;     // Min Distance (Units)
    public float distanceMax = 15f;     // Max Distance (Units)
    private float distance = 5.0f;      // Current Distance (Units)
    private float yaw = 0.0f;           // Current Yaw (Degrees)
    private float pitch = 0.0f;         // Current Pitch (Degrees)
    #endregion
    #region Functions/Methods
    // Use this for initialization
    private void Start()
    {
        // Store the current Yaw (Y) & Pitch (X) of the Camera. No need for Roll (Z)
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        // Hide the Cursor?
        if (hideCursor)
        {
            // Lock & Hide the Cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    // Late Update is called after Update at the end of each frame
    private void LateUpdate()
    {
        // Offsets the Yaw (Y) & Pitch (X) by Mouse X & Y using Speed
        // Mouse X and Y are Inverted due to Screen Space
        yaw += Input.GetAxis("Mouse X") * yawSpeed * distance * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * pitchSpeed * Time.deltaTime;

        // Clamps the Pitch between Min and Max Limits
        pitch = ClampAngle(pitch, pitchMinLimit, pitchMaxLimit);

        // Convert Yaw & Pitch to a Quaternion Rotation & Apply Rotation to Transform
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.rotation = rotation;

        // Calculate Distance using Scroll Wheel & Min & Max Distances
        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

        // Calculate Position of the Camera using Origin - Camera's Forward x Distance
        Vector3 origin = target != null ? target.position : Vector3.zero;
        Vector3 position = origin - transform.forward * distance;

        // Apply Position to Transform
        transform.position = position;
    }
    /// <summary>
    /// Clamps the given Angle between a given Min & Max Value
    /// Note: This script confines the angle between 0-360 degrees
    /// </summary>
    /// <param name="angle">Angle in Degrees</param>
    /// <param name="min">Min Angle in Degrees</param>
    /// <param name="max">Max Angle in Degrees</param>
    /// <returns></returns>
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
    #endregion
}
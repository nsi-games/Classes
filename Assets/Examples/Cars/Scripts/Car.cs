/* Author: Emmanuel (Manny) Vaccaro
 * Date: 05/03/2020
 * Brief: This is an example Car class that
 * outlines the variables & methods associated with every car
 */

// To use Unity's tools (API), you need this namespace
using UnityEngine;

/* Background:
 * Here are some notable keywords used in the following script:
 * 'access-specifier': public, private, protected
 * 'class-name': the name you give the class, in this case 'Car'
 * 'inherited-classes': classes that your class will derive from
 */

#region Definitions
public enum Transmission
{
    AUTOMATIC = 0,
    MANUAL = 1
};
#endregion

// Classes have the following format: <access-specifier> class <name> : <inherited-classes>

[RequireComponent(typeof(Rigidbody))] // This instruction forces Unity to attach a 'Rigidbody' along with the Car script
public class Car : MonoBehaviour
{
    #region Variables
    public int value = 4000;            // Value of the car in AUD($)
    public int windows = 6;             // Amount of windows the car has
    public int wheels = 4;              // Amount of wheels the car has
    public float moveSpeed = 10f;       // Movement Speed in km/s
    public float turnSpeed = 30f;       // Turning Speed in degrees/s
    public Transmission transmission;   // Transmission: AUTOMATIC / MANUAL
    private Rigidbody rigid;            // Reference to the rigidbody attached to the same GameObject
    #endregion
    #region Functions/Methods
    #region Unity
    // Start is called before the first frame update
    private void Start()
    {
        // Rigidbody is marked 'private' so that the script can automatically reference the object
        rigid = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    private void Update()
    {
        // If the 'W' Key is pressed
        if (Input.GetKey(KeyCode.W))
        {
            // Accelerate the car using speed
            Accelerate(moveSpeed);
        }

        // If the 'S' Key is pressed
        if (Input.GetKey(KeyCode.S))
        {
            // Perform Reverse Method
            Reverse(moveSpeed);
        }

        // If the 'SpaceBar' is pressed in one frame
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Stop the car from moving
            Break();
        }

        // Get Left / Right input as a float value
        float inputH = Input.GetAxis("Horizontal");
        // Steer the Car using horizontal input
        Steer(inputH);
    }
    #endregion
    /// <summary>
    /// Accelerates the Car in it's forward direction by a given force value
    /// </summary>
    /// <param name="force">Amount of speed to accelerate by</param>
    public void Accelerate(float force)
    {
        // Accelerates the Rigidbody's velocity
        rigid.AddForce(transform.forward * force);
    }
    /// <summary>
    /// Reverses the Car in the opposite direction of Accelerate by a given force value
    /// </summary>
    /// <param name="force">Amount of speed to reverse by</param>
    public void Reverse(float force)
    {
        // Accelerates the Rigidbody's velocity in the opposite direction
        rigid.AddForce(-transform.forward * force);
    }
    /// <summary>
    /// Stops the car from moving
    /// </summary>
    public void Break()
    {
        // Cancels out Rigidbody's velocity
        rigid.velocity = Vector3.zero;
    }
    /// <summary>
    /// Turns the Car's direction by a given angle
    /// </summary>
    /// <param name="direction"></param>
    public void Steer(float direction)
    {
        // Force = Direction x Turn Speed
        float force = direction * turnSpeed;
        // Apply Torque (Rotational Velocity) in the Up Direction by Force
        rigid.AddTorque(transform.up * force);
    }
    #endregion
}

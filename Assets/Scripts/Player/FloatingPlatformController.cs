using UnityEngine;

public class FloatingPlatformController : MonoBehaviour
{
    public float floatHeight = 3.0f;  // The desired floating height.
    public float floatSpeed = 1.0f;   // Speed of the up and down motion.

    private float originalY;
    
    private void Start()
    {
        originalY = transform.position.y;
    }

    private void Update()
    {
        // Calculate the up and down motion using a sine wave.
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        
        // Calculate the new position based on the original Y position and the oscillation.
        Vector3 newPosition = new Vector3(transform.position.x, originalY + yOffset, transform.position.z);
        
        // Set the new position of the spaceship using the Transform component.
        transform.position = newPosition;
    }
}

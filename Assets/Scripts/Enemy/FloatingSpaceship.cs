using UnityEngine;

public class FloatingSpaceship : MonoBehaviour
{

    [SerializeField]
    Transform playerPosition;
    public float floatDistance = 3.0f;  // The desired floating height.
    public float floatSpeed = 1.0f;   // Speed of the up and down motion.

    private float originalX;
    
    private void Start()
    {
        originalX = transform.position.x;
    }

    private void Update()
    {
        // Calculate the up and down motion using a sine wave.
        float xOffset = Mathf.Sin(Time.time * floatSpeed) * floatDistance;
        
        // Calculate the new position based on the original Y position and the oscillation.
        Vector3 newPosition = new Vector3(originalX + xOffset, transform.position.y, transform.position.z);
        
        // Set the new position of the spaceship using the Transform component.
        transform.position = newPosition;

        if(playerPosition != null)
        {
            transform.LookAt(playerPosition);
        }
    }
}

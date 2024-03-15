using UnityEngine;

public class SmartwatchAttachment : MonoBehaviour
{
    [SerializeField]
    private Transform wristAttachmentPoint; // The predefined wrist attachment point
    private Rigidbody rb; // The smartwatch's rigidbody

    private void Start()
    {
        AttachToWrist();
    }

    public void AttachToWrist() 
    {
        rb = GetComponent<Rigidbody>();

        // Check if a predefined wrist attachment point is assigned
        if (wristAttachmentPoint != null)
        {
            // Make the smartwatch kinematic to prevent physics interactions while attached
            if(rb != null)
            {
                rb.isKinematic = true;
            }
            

            // Attach the smartwatch to the predefined wrist attachment point
            transform.parent = wristAttachmentPoint;
        }
        else
        {
            Debug.LogError("No wrist attachment point assigned. Please assign a transform for attachment.");
        }
    }
}

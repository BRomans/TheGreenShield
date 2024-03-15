using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float rotationSpeed = 2.0f;
    public Transform playerTransform;
    public float destroyDistance = 1.0f; // The distance at which the enemy is destroyed.
    public GameObject explosionEffect;

    [SerializeField]
    [Tooltip("The explosion sound.")]
    private AudioSource explosionSound;
    private Vector3 playerPosition;

    private void Start()
    {
        explosionSound = GameObject.FindGameObjectWithTag("Explosion Sound").GetComponent<AudioSource>();
        // If the playerTransform is not set, assume player's position is (0, 0, 0).
        if (playerTransform == null)
        {
            playerPosition = new Vector3(0, 1, 0 );
        } else {
            playerPosition = playerTransform.position;
        }
    }

    private void Update()
    {
        // Move the enemy towards the player.
        Vector3 direction = playerPosition - transform.position;

        transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);

        // Rotate the enemy to face the player.
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Check the distance to the player and destroy the enemy if it's too close.
        if (direction.magnitude < destroyDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is a trigger and do something with the object that was hit.
        // Add your collision logic here, for example:
        //Debug.Log("Collided with: " + other.name);

        if (other.CompareTag("Shield"))
        {
            Debug.Log("Collided with: " + other.name);

            GameObject smoke = Instantiate(explosionEffect, gameObject.transform.position, gameObject.transform.rotation);
            smoke.transform.localScale *= 0.05f;
            ParticleSystem parts = smoke.GetComponent<ParticleSystem>();
            float totalDuration = parts.main.duration + parts.main.startLifetimeMultiplier;
            explosionSound.Play();

            ScoreManager.Instance.AddScore(1);

            Destroy(smoke, totalDuration / 2);
            Destroy(gameObject);
        }
    }
}

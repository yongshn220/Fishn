using UnityEngine;

// This is test script
public class FishAIMovement : MonoBehaviour
{
    // Speed of the fish
    public float speed = 2.0f;

    // Minimum and maximum speed of the fish
    public float minSpeed = 1.0f;
    public float maxSpeed = 3.0f;

    // Maximum duration for which the fish will swim in a single direction
    public float maxDuration = 5.0f;

    // Probability that the fish will change direction each frame
    public float changeProbability = 0.1f;

    // Damping time for smooth direction and speed change
    public float dampingTime = 0.5f;

    // Current direction and speed of the fish
    Vector3 direction;
    float currentSpeed;

    // Timer for changing direction
    float timer;

    // Update is called once per frame
    void Update()
    {
        // Check if the fish should change direction
        if (timer <= 0 || Random.Range(0.0f, 1.0f) < changeProbability)
        {
            // Choose a new direction and reset the timer
            Vector3 newDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
            timer = maxDuration;
            float newSpeed = Random.Range(minSpeed, maxSpeed);

            // Smoothly interpolate to the new direction and speed
            direction = Vector3.SmoothDamp(direction, newDirection, ref direction, dampingTime);
            currentSpeed = Mathf.SmoothDamp(currentSpeed, newSpeed, ref currentSpeed, dampingTime);
        }
        else
        {
            // Decrement the timer
            timer -= Time.deltaTime;
        }

        // Move the fish in the current direction
        transform.position += direction * currentSpeed * Time.deltaTime;

        // Rotate the fish to face the direction of movement
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}

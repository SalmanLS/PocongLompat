using UnityEngine;

public class ObjectBounce : MonoBehaviour
{
    public float bounceSpeed = 8f; // Speed of the bounce animation
    public float bounceAmplitude = 0.05f; // Amplitude of the bounce
    public float rotationSpeed = 90f; // Speed of the object's spin

    private float startHeight;
    private float timeOffset;
    private bool isOnGround = false; // Tracks whether the object is touching the ground

    void Start()
    {
        // Record the initial height of the object
        startHeight = transform.localPosition.y;
        // Randomize the bounce animation offset
        timeOffset = Random.value * Mathf.PI * 2;
    }

    void Update()
    {
        if (isOnGround)
        {
            // Bounce animation
            float finalHeight = startHeight + Mathf.Sin(Time.time * bounceSpeed + timeOffset) * bounceAmplitude;
            var position = transform.localPosition;
            position.y = finalHeight;
            transform.localPosition = position;

            // Spin animation
            Vector3 rotation = transform.localRotation.eulerAngles;
            rotation.y += rotationSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object touched the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true; // Enable bouncing
            startHeight = transform.localPosition.y + 0.5f;
        }
    }

}

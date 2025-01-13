using UnityEngine;

public class ObjectBounce : MonoBehaviour
{
    public float bounceSpeed = 8f; 
    public float bounceAmplitude = 0.05f; 
    public float rotationSpeed = 90f; 
    private float startHeight;
    private float timeOffset;
    private bool isOnGround = false;

    void Start()
    {
        startHeight = transform.localPosition.y;
        timeOffset = Random.value * Mathf.PI * 2;
    }

    void Update()
    {
        if (isOnGround)
        {
            float finalHeight = startHeight + Mathf.Sin(Time.time * bounceSpeed + timeOffset) * bounceAmplitude;
            var position = transform.localPosition;
            position.y = finalHeight;
            transform.localPosition = position;

            Vector3 rotation = transform.localRotation.eulerAngles;
            rotation.y += rotationSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            startHeight = transform.localPosition.y + 0.5f;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bodycontrollers : MonoBehaviour
{
    RaycastHit hit;
    float moveInput, steerInput, rayLength;
    public float maxSpeed = 5f, acceleration = 8f, steerStrenght;
    public Rigidbody sphereRB, Bikebody;
    [Range(1, 10)]
    public float brakingFactor;
    public LayerMask groundLayer;
    public float tiltSmoothing = 5f;
    public float leanAngle = 15f;

    void Start()
    {
        sphereRB.transform.parent = null;
        Bikebody.transform.parent = null;
        SphereCollider sphereCollider = sphereRB.GetComponent<SphereCollider>();
        if (sphereCollider != null)
        {
            rayLength = sphereCollider.radius + 0.2f;
        }
        else
        {
            Debug.LogError("SphereCollider is not attached to sphereRB!");
        }
        sphereRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        if (sphereRB != null)
        {
            Movement();
            Tilt();
            Lean();
            transform.position = sphereRB.position;
            Bikebody.MoveRotation(transform.rotation);
        }
    }

    void Movement()
    {
        Vector3 targetVelocity = maxSpeed * moveInput * transform.forward;
        sphereRB.velocity = Vector3.Lerp(
            sphereRB.velocity,
            targetVelocity,
            Time.fixedDeltaTime * acceleration
        );
        transform.Rotate(0, steerInput * moveInput * steerStrenght * Time.fixedDeltaTime, 0, Space.World);
    }

    void Brake()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            sphereRB.velocity *= brakingFactor / 10f;
        }
    }

    void Tilt()
    {
        Ray ray = new Ray(sphereRB.position, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, rayLength, groundLayer))
        {
            Vector3 surfaceNormal = hit.normal;
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, surfaceNormal) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * tiltSmoothing);
        }
    }

    void Lean()
    {
        float lean = -steerInput * leanAngle; 
        Quaternion leanRotation = Quaternion.Euler(0, 0, lean);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, leanRotation, Time.fixedDeltaTime * tiltSmoothing);
    }
}

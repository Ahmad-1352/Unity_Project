using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float followSpeed = 5f;
    public float lookSpeed = 5f;

    void FixedUpdate()
    {
        FollowTarget();
        LookAtTarget();
    }

    void FollowTarget()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.fixedDeltaTime * followSpeed);
    }

    void LookAtTarget()
    {
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * lookSpeed);
    }
}

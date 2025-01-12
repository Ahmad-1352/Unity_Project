using UnityEngine;

public class Sphere : MonoBehaviour
{
    public Rigidbody sphereRB;

    void Start()
    {
        if (sphereRB == null)
        {
            sphereRB = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        if (sphereRB != null)
        {
            sphereRB.GetComponentInChildren<Renderer>().enabled = false;
        }
    }
}

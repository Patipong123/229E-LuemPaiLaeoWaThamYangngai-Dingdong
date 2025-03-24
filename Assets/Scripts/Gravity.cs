using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravityConstant = 0.6674f;
    public float attractorMass = 50f; // mass of object (Black Hole) = m1

    private void FixedUpdate()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box"); // find object with tag

        foreach (GameObject obj in boxes)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = transform.position - obj.transform.position; // find box vector to gravity attractor
                float distance = direction.magnitude;

                if (distance > 0.1f)
                {
                    float forceMagnitude = gravityConstant * (attractorMass * rb.mass) / (distance * distance); // = G * (m1 * m2) / r2
                    Vector3 force = direction.normalized * forceMagnitude; 
                    rb.AddForce(force);
                }
            }
        }
    }
}

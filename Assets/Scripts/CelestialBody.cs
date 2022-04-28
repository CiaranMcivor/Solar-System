using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{

    [SerializeField] private float mass;
    [SerializeField] private float radius;
    [SerializeField] private Vector3 initialV;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private float rotationSpeed;
    // Start is called before the first frame update

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.mass = mass;
        velocity = initialV;
    }

    public void UpdateVelocity(CelestialBody[] celestialBodies, float timeStep)
    {
        foreach (var body in celestialBodies)
        {
            if (body != this)
            {
                float sqrDist = (body.rigidbody.position - rigidbody.position).sqrMagnitude;
                Vector3 forceDirection = (body.rigidbody.position - rigidbody.position).normalized;
                Vector3 acceleration = forceDirection * Universe.G * body.mass / sqrDist;
                velocity += acceleration * timeStep;
            }
        }
    }

    public void UpdateVelocity(Vector3 acceleration, float timeStep)
    {
        velocity += acceleration * timeStep;
    }
    public void UpdatePosition(float timeStep)
    {
        rigidbody.MovePosition(rigidbody.position + velocity * timeStep);
        transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));
    }
    // Update is called once per frame
    void OnValidate()
    {
        transform.localScale = Vector3.one * radius;
    }

    public Vector3 getPosition()
    {
        
        
        return rigidbody.position;
        
    }
    public float getMass()
    {               
       return mass;        
    }

    public Vector3 getInitialV()
    {
        return initialV;
    }
}

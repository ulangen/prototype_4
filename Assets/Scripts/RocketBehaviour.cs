using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private Transform target;
    private float speed = 15f;
    private bool homing;

    private float rocketStength = 15f;
    private float aliveTimer = 5f;

    void Update()
    {
        if (homing && target != null)
        {
            Vector3 moveDirection = (target.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (target != null)
        {
            if (other.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -other.contacts[0].normal;
                targetRigidbody.AddForce(away * rocketStength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }

    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }
}

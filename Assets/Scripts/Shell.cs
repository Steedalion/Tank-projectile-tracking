using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject explosion;
    float mass = 10;
    float force = 200;
    float acceleration;
    float gravity = -9.8f;
    float gAccel;
    float speedZ;
    float speedY;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("tank"))
        {
            GameObject exp = Instantiate(explosion, this.transform.position, Quaternion.identity);
            Destroy(exp, 0.5f);
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    public Rigidbody rb;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.forward = rb.velocity;
    }
}

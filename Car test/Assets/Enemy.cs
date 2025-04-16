using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{

    public int speed;
    public int SpeedLimit;
    public float Health;
    public Rigidbody rb;
    public Rigidbody player;
    public GameObject target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 0;
        SpeedLimit = 75;
        Health = 250;
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vel = transform.forward * speed;
        rb.linearVelocity = new Vector3(vel.x, rb.linearVelocity.y, vel.z);

        if (speed <= SpeedLimit)
        {
            speed = SpeedLimit;
        }

        transform.LookAt(target.transform);



        const float MaxDistance = 5.0f; 

        if (Vector3.Distance(transform.position, target.transform.position) > MaxDistance)
        {
            speed = speed + 1;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {

            if (Health <= 0)
            {
                Health = 250;
            }
        }

    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            Vector3 ourVel = rb.linearVelocity = transform.forward * speed;
            Vector3 enemyVel = other.gameObject.GetComponent<Car>().rb.linearVelocity;// = transform.forward * speed * GetComponent<Enemy>().speed;
            Vector3 impactVel = ourVel - enemyVel;
            float relativeSpeed = impactVel.magnitude;
            relativeSpeed = Mathf.Clamp(relativeSpeed, 0, 20);   // lowest highest
            Health = Health - relativeSpeed * 5;

        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            Health = Health - 5;
        }

        if (other.gameObject.CompareTag("Cone"))
        {
            speed = speed - 75;
        }
    }
}

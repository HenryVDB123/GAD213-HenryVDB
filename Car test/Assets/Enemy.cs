using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{

    public int speed;
    public int SpeedLimit;
    public float Health;
    public Rigidbody rb;

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
        

    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            Vector3 ourVel = rb.linearVelocity = transform.forward * speed;
            Vector3 enemyVel = other.gameObject.GetComponent<Movement>().rb.linearVelocity;// = transform.forward * speed * GetComponent<Enemy>().speed;
            Vector3 impactVel = ourVel - enemyVel;
            float relativeSpeed = impactVel.magnitude;
            relativeSpeed = Mathf.Clamp(relativeSpeed, 0, 20);   // lowest highest
            Health = Health - relativeSpeed * 5;

        }
    }
}

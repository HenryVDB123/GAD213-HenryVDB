using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.Events;
public class Car : MonoBehaviour
{

    public int speed;
    public int Rotatespeed;
    public int Reversespeed;
    public Vector3 rotations;
    [SerializeField] private float timer;
    [SerializeField] private float deadclock;
    public TextMeshProUGUI TimeIndicator;
    public int SpeedLimit;
    public bool InTime;
    public int PowerUpVersion;
    public Rigidbody Bullet;
    public Rigidbody Cones;
    public int Bullets;
    public int ConeCount;
    public float Health;
    public Rigidbody rb;


    private Powers[] PowerTypes
            = { new Gun(), new Cone()};

    // Start is called before the first frame update
    void Start()
    {
        speed = 0; 
        SpeedLimit = 75;
        Rotatespeed = 100;
        Reversespeed = 25;
        InTime = true;
        PowerUpVersion = 0;
        Bullets = 6;
        ConeCount = 3;
        Health = 250;
        rb=GetComponent<Rigidbody>();

        
    }

 void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Powerup"))
            {
               var PowerType = PowerTypes[Random.Range(0, PowerTypes.Length)]; 
               PowerUpVersion = PowerType.PowerUpVersion;
            }

        if (other.gameObject.CompareTag("EnemyCar"))
        {
                Vector3 ourVel = rb.linearVelocity = transform.forward * speed;
            Vector3 enemyVel = other.gameObject.GetComponent<Enemy>().rb.linearVelocity;// = transform.forward * speed * GetComponent<Enemy>().speed;
                Vector3 impactVel = ourVel - enemyVel;
                float relativeSpeed = impactVel.magnitude;
                relativeSpeed=Mathf.Clamp(relativeSpeed,0,20);   // lowest highest
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

    void Update()
    {
        if (Health <= 0)
        {
            speed = 0;
            Reversespeed = 0;
            deadclock += Time.deltaTime;

            if (deadclock >= 2)
            {
                transform.position = new Vector3(-43, 40, 22);
                transform.rotation = new Quaternion(0, 0, 0, 1);
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                Health = 250;
                deadclock = 0;
            }
        }


        if (PowerUpVersion == 1)
        {
            if (Bullets == 0)
            {
                PowerUpVersion = 0;
                Bullets = Bullets + 6;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (Bullets != 0)
                {
                    Rigidbody instantiatedProjectile = Instantiate(Bullet, transform.position, transform.rotation) as Rigidbody;
                instantiatedProjectile.linearVelocity = transform.TransformDirection(new Vector3(0, 0, 150));
                Bullets = Bullets - 1;
                }
                
            }
        }

        if (PowerUpVersion == 2)
        {
            if (ConeCount == 0)
            {
                PowerUpVersion = 0;
                ConeCount = ConeCount + 3;
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                if (ConeCount !=0)
                {
                    Rigidbody instantiatedProjectile = Instantiate(Cones, transform.position, transform.rotation) as Rigidbody;
                instantiatedProjectile.linearVelocity = transform.TransformDirection(new Vector3(0, 0, 0));
                ConeCount = ConeCount - 1;
                }
            }
        }

        if (speed > SpeedLimit)
        {
            speed = 75;
        }
        Vector3 vel = transform.forward * speed;
        rb.linearVelocity = new Vector3(vel.x, rb.linearVelocity.y, vel.z);
        if (Input.GetKey(KeyCode.W))
        {
            if (speed != SpeedLimit)
            {
                speed = speed + 1;
            }
        }
        else if (!Input.GetKey(KeyCode.W))
        {
            if (speed != 0)
            {
                speed = speed - 1;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            vel = transform.forward * -1 * Reversespeed;
            rb.linearVelocity = new Vector3(vel.x, rb.linearVelocity.y, vel.z);
            
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Rotatespeed = 175;
            if (speed != 0)
            {
                speed = 25;
            }
        }
        else
        {
            Rotatespeed = 100;
        }

        {
            TimeIndicator.text = timer.ToString();

            timer += Time.deltaTime;
            

            if (timer > 0.4)
            {
                if (timer < 1)
                {
                    if (InTime == true)
                    {
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            SpeedLimit = 150;
                            speed = 150;
                        }
                    }
                    
                }

                else if (timer > 1)
                {
                    timer = 0;
                    SpeedLimit = 75;
                    InTime = true;
                }
            }

            if (timer !< 0.4)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        InTime = false;
                    }
                }

        }

       
    }

    

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rotations = Vector3.up;
        }

        else if (Input.GetKey(KeyCode.A))
        {
            rotations = Vector3.down;
        }
       
        else
        {
            rotations = Vector3.zero;
        }

        transform.Rotate(rotations * Rotatespeed * Time.deltaTime);
    }

    public class Powers
    {
        public int PowerUpVersion;
    }

public class Gun : Powers
    {
        public Gun()
        {
            PowerUpVersion = 1;
        }
    }

    public class Cone : Powers
    {
        public Cone()

        {
            PowerUpVersion = 2;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.Events;
public class Movement : MonoBehaviour
{

    public int speed;
    public int Rotatespeed;
    public int Reversespeed;
    public Vector3 rotations;
    [SerializeField] private float timer;
    public TextMeshProUGUI TimeIndicator;
    public int SpeedLimit;
    public bool InTime;
    public int PowerUpVersion;
    public Rigidbody Bullet;
    public Rigidbody Cones;
    public int Bullets;
    public int ConeCount;

    
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
        
    }

 void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Powerup"))
            {
               var PowerType = PowerTypes[Random.Range(0, PowerTypes.Length)]; 
               PowerUpVersion = PowerType.PowerUpVersion;
            }

        } 

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = new Vector3(-12,3,-12);
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
                instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, 150));
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
                instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, 0));
                ConeCount = ConeCount - 1;
                }
            }
        }

        if (speed > SpeedLimit)
        {
            speed = 75;
        }

        transform.position += transform.forward * speed * Time.deltaTime;
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
            transform.position += transform.forward * -1 * Reversespeed * Time.deltaTime;
            
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
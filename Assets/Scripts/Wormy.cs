using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormy : MonoBehaviour
{
    public Rigidbody2D bulletPrefab;
    public Transform currentGun;
    public Transform currentGun1;
    public Rigidbody2D bulletPrefab1;
    public Rigidbody2D Grenade;
   
    public float wormySpeed = 1;
    public float maxRelativeVelocity;
    public float misileForce = 5; 

    public bool IsTurn { get { return WormyManager.singleton.IsMyTurn(wormId); } }

    public int wormId;
    WormyHealth wormyHealth;
    SpriteRenderer ren;
    private RopeSystem RP;
    private LineRenderer LN;
    private DistanceJoint2D DJ;




    private void Start()
    {
        LN = GetComponent<LineRenderer>();
        RP = GetComponent<RopeSystem>();
        DJ = GetComponent<DistanceJoint2D>();
        wormyHealth = GetComponent<WormyHealth>();
        ren = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        
        if (!IsTurn)
        {
                RP.enabled = false;
                LN.enabled = false;
                DJ.enabled = false;
            return;
        }
            

        RotateGun();

        var hor = Input.GetAxis("Horizontal");
        if (hor == 0)
        {
            
            ren.flipX = currentGun.eulerAngles.z < 180;
            ren.flipX = currentGun1.eulerAngles.z < 180;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                // bulletPrefab.gameObject.SetActive(true);
                //  currentGun.gameObject.SetActive(false);
                //  currentGun1.gameObject.SetActive(true);
                 RP.enabled = !RP.enabled;
                 LN.enabled = !RP.enabled;
            }
            else 
            {
                
               
            }
                if (Input.GetKeyDown(KeyCode.Q))
            {
                currentGun.gameObject.SetActive(true);
                currentGun1.gameObject.SetActive(false);
                
               var p = Instantiate(bulletPrefab,
                                   currentGun.position - currentGun.right,
                                   currentGun.rotation);

                p.AddForce(-currentGun.right * misileForce, ForceMode2D.Impulse);

                if (IsTurn)
                    WormyManager.singleton.NextWorm();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                currentGun.gameObject.SetActive(true);
                currentGun1.gameObject.SetActive(false);
                var p = Instantiate(bulletPrefab1,
                                   currentGun.position - currentGun.right,
                                   currentGun.rotation);

                p.AddForce(-currentGun.right * misileForce, ForceMode2D.Impulse);

                if (IsTurn)
                    WormyManager.singleton.NextWorm();
            }
            if (Input.GetKeyDown(KeyCode.R))
            { 
                currentGun.gameObject.SetActive(false);
                currentGun1.gameObject.SetActive(true);
               
                var p = Instantiate(Grenade,
                                   currentGun1.position - currentGun1.right,
                                   currentGun1.rotation);

                p.AddForce(-currentGun1.right * misileForce, ForceMode2D.Impulse);

                if (IsTurn)
                    WormyManager.singleton.NextWorm();
            }
        }
        else
        {
            currentGun.gameObject.SetActive(false);
            currentGun1.gameObject.SetActive(false);
            transform.position += Vector3.right *
                                hor *
                                Time.deltaTime *
                                wormySpeed;            
             ren.flipX = Input.GetAxis("Horizontal") > 0;
        }
        if (Input.GetMouseButton(1))
        {
            if (IsTurn)
                WormyManager.singleton.NextWorm();
        }

    }

    void RotateGun()
    {
        var diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        currentGun.rotation = Quaternion.Euler(0f, 0f, rot_z + 180);
        currentGun1.rotation = Quaternion.Euler(0f, 0f, rot_z + 180);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > maxRelativeVelocity)
        {
            wormyHealth.ChangeHealth(-3);
           // if (IsTurn)
              //  WormyManager.singleton.NextWorm();
        }  
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            wormyHealth.ChangeHealth(-10);
           // if (IsTurn)
             //   WormyManager.singleton.NextWorm();
        }
        if (collision.CompareTag("Explosion1"))
        {
            wormyHealth.ChangeHealth(-20);
            // if (IsTurn)
            //   WormyManager.singleton.NextWorm();
        }
        if (collision.CompareTag("Grenade"))
        {
            wormyHealth.ChangeHealth(-40);
            // if (IsTurn)
            //   WormyManager.singleton.NextWorm();
        }
    }
   
}

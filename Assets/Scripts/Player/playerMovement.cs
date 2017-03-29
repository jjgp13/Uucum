using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}
public class playerMovement : MonoBehaviour {

    public Boundary boundary;


    public float speed;
    public float tilt;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float shipSensitive;

    private float nextFire;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        float xAce = Input.acceleration.x;
        float yAce = Input.acceleration.y;
        float zAce = Input.acceleration.z;
        Vector3 movement;


        //movement = new Vector3(-Input.GetAxis("Horizontal"), -Input.GetAxis("Vertical"), 1f);
        //Debug.Log(" X: " + Input.GetAxis("Horizontal") + " Y: " + Input.GetAxis("Vertical"));

        movement = new Vector3(-xAce, -yAce, 1f);
        rb.velocity = movement * speed;

        rb.position = new Vector3
            (
               Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
               Mathf.Clamp(rb.position.y, boundary.yMin, boundary.yMax),
               rb.position.z
            );

        if(Mathf.Abs(xAce) > shipSensitive || Mathf.Abs(yAce) > shipSensitive)
            rb.rotation = Quaternion.Euler(rb.velocity.y * -tilt, rb.velocity.x * tilt, 0.0f);
        else
        {
            rb.rotation = Quaternion.Euler(0f, 0f, 0.0f);
        }
    }
}

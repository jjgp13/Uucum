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

	//Velocidad de la nave.
	public float rotationSpeed;
	public float translationSpeed;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float shipSensitive;

    Vector3 InitialDevicePosition;
    Vector3 movement;
    public float smooth = 0.5f;

    private float nextFire;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        InitialDevicePosition = Input.acceleration;
    }


    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
		//Mover la nave hacia adelante
		rb.velocity = transform.forward * translationSpeed;

		//Rotar la nave segun el input del acelerometro.
		movement = Vector3.Lerp(movement, Input.acceleration - InitialDevicePosition, smooth);

		transform.Rotate (movement.y * rotationSpeed, -movement.x * rotationSpeed, 0.0f);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour {
	//Velocidad de la nave.
	public float rotationSpeed;
	public float translationSpeed;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    Vector3 InitialDevicePosition;
    Vector3 movement;
	public float smooth = 0.5f;

    private float nextFire;

    Rigidbody rb;

	public bool isShootBtnPress;
	public bool isIncSpeedBtnPrees;

	public GameObject pSlow;
	public GameObject pFast;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        InitialDevicePosition = Input.acceleration;
    }


    void Update()
    {
		if (isShootBtnPress) {
			if (Time.time > nextFire){
				nextFire = Time.time + fireRate;
				Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			}
		}

		if (isIncSpeedBtnPrees)
			translationSpeed = 100f;
		else
			translationSpeed = 50f;
		
    }

	// Update is called once per frame
	void FixedUpdate () {
		//Mover la nave hacia adelante
		rb.velocity = transform.forward * translationSpeed;

		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, -2500, 2500), 
			Mathf.Clamp (rb.position.y, 0, 400), 
			Mathf.Clamp (rb.position.z, -2500, 2500)
		);

		//Rotar la nave segun el input del acelerometro.
		movement = Vector3.Lerp (movement, Input.acceleration - InitialDevicePosition, smooth);
		transform.Rotate(movement.y * rotationSpeed,  -movement.x * rotationSpeed, 0f);
		/*if(Input.GetKey(KeyCode.LeftArrow))
			transform.Rotate(Vector3.up, -rotationSpeed);

		if(Input.GetKey(KeyCode.RightArrow))
			transform.Rotate(Vector3.up, rotationSpeed);

		if(Input.GetKey(KeyCode.UpArrow))
			transform.Rotate(Vector3.left, -rotationSpeed);

		if(Input.GetKey(KeyCode.DownArrow))
			transform.Rotate(Vector3.left, rotationSpeed);*/
		/*float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		transform.Rotate(moveHorizontal * rotationSpeed,  -moveVertical * rotationSpeed, 0f);*/
	}

	public void shoot(){
		isShootBtnPress = true;
	}

	public void noShoot(){
		isShootBtnPress = false;
	}

	public void increaseSpeed(){
		isIncSpeedBtnPrees = true;
		pFast.SetActive (true);
		pSlow.SetActive (false);
	}

	public void noIncreaseSpeed(){
		isIncSpeedBtnPrees = false;
		pFast.SetActive (false);
		pSlow.SetActive (true);
	}
}
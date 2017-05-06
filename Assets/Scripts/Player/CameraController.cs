using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	// The target we are following
	[SerializeField]
	private Transform target;
	// The distance in the x-z plane to the target
	[SerializeField]
	private float distance = 10.0f;

	[SerializeField]
	private float rotationDamping;

	// Use this for initialization
	void Start() { }

	// Update is called once per frame
	void LateUpdate()
	{
		// Early out if we don't have a target
		if (!target)
			return;

		// Calculate the current rotation angles X
		var wantedRotationAngleX = target.eulerAngles.x;
		var currentRotationAngleX = transform.eulerAngles.x;
		// Y
		var wantedRotationAngleY = target.eulerAngles.y;
		var currentRotationAngleY = transform.eulerAngles.y;

		// Damp the rotation around the y-axis
		currentRotationAngleX = Mathf.LerpAngle(currentRotationAngleX, wantedRotationAngleX, rotationDamping * Time.deltaTime);
		currentRotationAngleY = Mathf.LerpAngle(currentRotationAngleY, wantedRotationAngleY, rotationDamping * Time.deltaTime);

		// Convert the angle into a rotation
		var currentRotation = Quaternion.Euler(currentRotationAngleX, currentRotationAngleY, 0);

		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;

		// Always look at the target
		transform.LookAt(target);
	}

}

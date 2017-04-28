using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;       //Public variable to store a reference to the player game object
    private Vector3 offset;         //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
		gameObject.transform.position = player.transform.position - new Vector3(0,0,10f);
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
	{
		//Para tunel
		/*transform.position = new Vector3(
            Mathf.Clamp(player.transform.position.x + offset.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(player.transform.position.y + offset.y, boundary.yMin, boundary.yMax),
            player.transform.position.z + offset.z);*/

		//Movimiento libre;
		transform.position = new Vector3(
			player	.transform.position.x + offset.x,
			player.transform.position.y + offset.y,
			player.transform.position.z + offset.z);
    }
}

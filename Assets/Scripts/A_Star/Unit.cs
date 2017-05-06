using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	//Referencia a la torre
	public Transform target;
	//Velocidad del enemigo
	public float speed = 20;
	//Camino a seguir
	Vector3[] path;
	int targetIndex;
	EnemyBehavior behavior;

	void Start() {
		target = GameObject.Find ("PiramideCentral").transform;
		behavior = GetComponent<EnemyBehavior> ();
	}

	void Update(){
		
	}

	//Cuando se haya encontrado el camino. Asignamos el path
	public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
		if (pathSuccessful) {
			path = newPath;
			targetIndex = 0;
			//Parar en caso de que ya haya iniciado la corrutina
			StopCoroutine ("FollowPath");
			StartCoroutine ("FollowPath");
		}
	}

	//Seguir el camino
	IEnumerator FollowPath() {
		//Almacaner los puntos a seguir
		Vector3 currentWaypoint = path[0];
		//mientras no llegue a la torre seguir los puntos
		while (true) {
			if (transform.position == currentWaypoint) {
				targetIndex ++;
				if (targetIndex >= path.Length) {
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}
			//mover hacia adelante
			transform.position = Vector3.MoveTowards(transform.position,currentWaypoint,speed * Time.deltaTime);
			transform.LookAt (currentWaypoint);
			yield return null;
		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);
				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}

	//Una vez en el escenario pedir una ruta al pathRequestManager
	void OnTriggerEnter(Collider other){
		if (other.gameObject.name == "GameController") {
			PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);
		}

		if (other.gameObject.name == "player") {
			StopCoroutine("FollowPath");
			GetComponent<Unit> ().enabled = false;

			behavior.target = other.gameObject;
			behavior.followPlayerState = true;
			StartCoroutine(behavior.shoot());

			behavior.inTurretRangeState = false;
			behavior.inTowerRangeState = false;

		}

		if (other.gameObject.tag == "TorretaAliada") {
			StopCoroutine ("FollowPath");
			GetComponent<Unit> ().enabled = false;

			behavior.target = other.gameObject;
			behavior.inTurretRangeState = true;
			StartCoroutine(behavior.shoot());

			behavior.followPlayerState = false;
			behavior.inTowerRangeState = false;
		}

		if (other.gameObject.tag == "tower") {
			StopCoroutine ("FollowPath");
			GetComponent<Unit> ().enabled = false;

			behavior.inTowerRangeState = true;
			behavior.target = other.gameObject;

			behavior.followPlayerState = false;
			behavior.inTurretRangeState = false;
		}
	}

	void OnTriggerExit(Collider other){
		//Si sale del rango del jugador, pedir otro paht
		if (other.gameObject.name == "player") {
			StopCoroutine (behavior.shoot ());
			PathRequestManager.RequestPath (transform.position, target.position, OnPathFound);
		}
		//Si sale de la torreta ir contra la principal
		if (other.gameObject.tag == "TorretaAliada") {
			behavior.target = GameObject.FindGameObjectWithTag ("tower");
			behavior.inTowerRangeState = true;
		}
	}
}
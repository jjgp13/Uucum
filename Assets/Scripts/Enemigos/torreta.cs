using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torreta : MonoBehaviour {

    public GameObject target;
    public GameObject bullet;
    public Transform spawnPos;

	public bool isSearchingForEnemy;
	public bool isShooting;

	public GameObject Explosion;

    // Use this for initialization
    void Start() {
		isSearchingForEnemy = true;
		isShooting = false;
	}

	// Update is called once per frame
	void Update () {
		if (isSearchingForEnemy) {
			StopCoroutine (shoot ());
			transform.Rotate (Vector3.up, 10 * Time.deltaTime);
		}

		if (target != null) {
			if (isShooting)
				transform.LookAt (target.transform);
		} else {
			isSearchingForEnemy = true;
			isShooting = false;
		}
			
	}

    private void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Enemy") {
			isShooting = true;
			isSearchingForEnemy = false;
			target = other.gameObject;
			StartCoroutine(shoot());
		}
    }

	private void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Enemy") {
			isShooting = false;
			isSearchingForEnemy = true;
		}
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Bullet") {
			Instantiate (Explosion, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}

    public IEnumerator shoot(){
        while (true){
            yield return new WaitForSeconds(1f);
            Instantiate(bullet, spawnPos.position, spawnPos.rotation);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullets : MonoBehaviour {

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bullet") Destroy(other.gameObject);
    }
}

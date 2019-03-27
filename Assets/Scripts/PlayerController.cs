using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 0;
    public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(transform.forward * speed);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("fire");
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = this.gameObject.transform.position;
            bullet.transform.localScale = Vector3.one;
            bullet.transform.rotation = transform.rotation;

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            Vector3 direction = Camera.main.ViewportToWorldPoint(Input.mousePosition);
            rb.AddForce(direction * 10);
            Debug.Log("direcction: " + direction);

        }
    }
}

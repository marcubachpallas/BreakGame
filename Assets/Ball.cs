using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        /* collision.collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
         //collision.gameObject.GetComponentInParent<BoronoidController>().timer = 0;
         Joint[] joints = collision.gameObject.GetComponents<Joint>();

         foreach (Joint joint in joints)
             Destroy(joint);

         collision.gameObject.GetComponent<BoxCollider>().enabled = false;*/

        collision.collider.gameObject.GetComponent<Piece_Info>().Hitted();

    }
}

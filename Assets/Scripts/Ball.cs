using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private void Start()
    {
        Destroy(this.gameObject, 3);
    }

    private void OnCollisionEnter(Collision collision)
    {
        /* collision.collider.gameObject.GetComponent<Rigidbody>().isKinematic = false;
         //collision.gameObject.GetComponentInParent<BoronoidController>().timer = 0;
         Joint[] joints = collision.gameObject.GetComponents<Joint>();

         foreach (Joint joint in joints)
             Destroy(joint);

         collision.gameObject.GetComponent<BoxCollider>().enabled = false;*/

        if (collision.collider.gameObject.GetComponent<Piece_Info>())
        {
            collision.collider.gameObject.GetComponent<Piece_Info>().Hitted();
            if(collision.collider.transform.parent.tag == "Piramid")
            {
                Camera.main.GetComponent<PlayerController>().UpdateBullets(5);
                collision.collider.transform.parent.tag = "Untagged";
            }
        }

    }


}

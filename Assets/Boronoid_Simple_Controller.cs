using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boronoid_Simple_Controller : MonoBehaviour {

    public GameObject[] anchors;
    public bool[] piecesCheked;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < transform.childCount; i++)
        {
            for (int j = 0; j < transform.childCount; j++)
            {
                if (i != j) { 

                    if (transform.GetChild(i).GetComponent<Collider>().bounds.Intersects(transform.GetChild(j).GetComponent<Collider>().bounds))
                    {
                        transform.GetChild(i).GetComponent<Piece_Info>().nerbyPieces.Add(transform.GetChild(j).gameObject);
                        FixedJoint joint = transform.GetChild(i).gameObject.AddComponent<FixedJoint>();
                        //joint.breakForce = 100;
                        joint.connectedBody = transform.GetChild(j).GetComponent<Rigidbody>();
                    }
                }
            }
        }
    }
}

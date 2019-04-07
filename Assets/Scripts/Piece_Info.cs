using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece_Info : MonoBehaviour {

    public List<GameObject> nerbyPieces = new List<GameObject>();
    public bool hitted = false;

    public void Hitted()
    {
        hitted = true;

        Joint[] joints = GetComponents<Joint>();

        foreach (Joint joint in joints)
            Destroy(joint);

        for(int i = 0; i < nerbyPieces.Count; i++)
        {
            Joint[] newjoints = nerbyPieces[i].GetComponents<Joint>();
            foreach (Joint joint in newjoints)
            {
                if(joint.connectedBody.gameObject == this.gameObject)
                {
                    joint.gameObject.GetComponent<Piece_Info>().hitted = true;
                    Destroy(joint);
                }
            }
        }
    }

}

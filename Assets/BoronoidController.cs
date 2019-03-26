using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoronoidController : MonoBehaviour {

    bool intersects = false;
    public float timer = 10;
    public GameObject[] anchors;
    public bool[] tested;
    // Use this for initialization


    private void Start()
    {
        tested = new bool[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            tested[i] = false;
        }
    }




    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;

        if (timer < 2)
        {
            intersects = false;
            for (int i = 0; i < transform.childCount; i++)
            {
                for (int j = 0; j < transform.childCount; j++)
                {
                    if (transform.GetChild(i).GetComponent<Collider>().bounds.Intersects(transform.GetChild(j).GetComponent<Collider>().bounds))
                    {
                        if (transform.GetChild(j).GetComponent<Rigidbody>().isKinematic)
                        {
                            intersects = true;
                            break;
                        }
                    }
                }
                if (!intersects)
                {
                    transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
                }
                intersects = false;
            }

            intersects = false;
            for (int i = 0; i < anchors.Length; i++)
            {
                if (anchors[i].GetComponent<Rigidbody>().isKinematic)
                {
                    intersects = true;
                }
            }

            if (!intersects)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
                }
            }
        }
    }

    //hacer recursiva que revise si alguno de los trozos que esta tocando tiene anchor, en caso contrario desactivarlo
    public bool FindAnchors()
    {
        return false;
    }
}

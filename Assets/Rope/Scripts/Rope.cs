using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Be careful when using ExecuteInEditMode
//Changes made to objects in the scene are saved to the scene file,
//generating nasty merge conflicts when two people modify the same scene,
//even if they don’t touch the generated objects.
//[ExecuteInEditMode]

/// <summary>
/// automatically connects rope segments
/// </summary>
public class Rope : MonoBehaviour
{
    public Rigidbody2D hookBegin;
    //public HingeJoint2D hookEnd;
    public GameObject[] prefabRopeSegs;
    public int numLinks = 5;

    void Start()
    {
        GenerateRope();
    }

    void GenerateRope()
    {
        Rigidbody2D prevBod = hookBegin;
        int prefabSegRange = (prefabRopeSegs.Length > 1) ? prefabRopeSegs.Length - 1 : prefabRopeSegs.Length;
        for (int i = 0; i < numLinks - 1; i++)
        {
            int index = Random.Range(0, prefabSegRange);
            GameObject newSeg = Instantiate(prefabRopeSegs[index]);
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBod;
            prevBod = newSeg.GetComponent<Rigidbody2D>();

            //if (i == numLinks-1)
            //{
            //    hookEnd.connectedBody = hj.GetComponent<Rigidbody2D>();
            //}
        }

        GameObject lastSeg = Instantiate(prefabRopeSegs[prefabRopeSegs.Length - 1]);
        lastSeg.transform.parent = transform;
        lastSeg.transform.position = transform.position;
        HingeJoint2D lasthj = lastSeg.GetComponent<HingeJoint2D>();
        lasthj.connectedBody = prevBod;

    }
}
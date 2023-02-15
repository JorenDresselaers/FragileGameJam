using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// automatically positions the rope segments 
/// </summary>
public class RopeSegment : MonoBehaviour
{
    public GameObject connectedAbove, ConnectedBelow;

    // Start is called before the first frame update
    void Start()
    {
        connectedAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
        RopeSegment aboveSegment = connectedAbove.GetComponent<RopeSegment>();
        if (aboveSegment != null)
        {
            aboveSegment.ConnectedBelow = gameObject;
            float spriteRight = connectedAbove.GetComponent<SpriteRenderer>().bounds.size.x;
            const float padding = 0.2f;
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(spriteRight - padding, 0);
        }
        else
        {
            GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
        }

    }
}

//void Start()
//{
//    connectedAbove = GetComponent<HingeJoint2D>().connectedBody.gameObject;
//    RopeSegment aboveSegment = connectedAbove.GetComponent<RopeSegment>();
//    if (aboveSegment != null)
//    {
//        aboveSegment.ConnectedBelow = gameObject;
//        float spriteBottom = connectedAbove.GetComponent<SpriteRenderer>().bounds.size.y;
//        GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, spriteBottom * -1);
//    }
//    else
//    {
//        GetComponent<HingeJoint2D>().connectedAnchor = new Vector2(0, 0);
//    }

//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float offset_X = 4.5f;
    [SerializeField] private float offset_Y = 4.5f;
    Vector2 playerPrevPosition;

    void Update()
    {
        float xdistance = player.position.x - transform.position.x;
        float ydistance = player.position.y - transform.position.y;
        Vector2 delta_position = Vector2.zero;
        bool shoudlMove = false;

        if (xdistance > offset_X || xdistance < -offset_X)
        {
            //Vector2 delta_position = transform.position;
            delta_position.x = player.position.x - playerPrevPosition.x;
            shoudlMove = true;
            //transform.Translate(delta_position);
        }
        if (ydistance > offset_Y || ydistance < -offset_Y)
        {
            //Vector2 delta_position = transform.position;
            delta_position.y = player.position.y - playerPrevPosition.y;
            shoudlMove = true;
            //transform.Translate(delta_position);
        }
        //if(delta_position!=(Vector2)transform.position)
        if (shoudlMove)
        {
            transform.Translate(delta_position);
        }

        playerPrevPosition = player.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    const string HORIZONTAL = "Horizontal";
    const string VERTICAL = "Vertical";

    private Rigidbody2D _rigidBody;
    private float _distanceToGround = 1.0f;

    [SerializeField] float _movementSpeed = 1;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _distanceToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
    }

    void Update()
    {
        float horizontalMovement = Input.GetAxis(HORIZONTAL);
        _rigidBody.velocity = new Vector3(horizontalMovement * _movementSpeed, 0);
        if (Input.GetAxis(VERTICAL) > 0)
        {
            Jump(20);
        }
    }

    void Jump(float jumpForce)
    {
        if (!_rigidBody) return;
        _rigidBody.AddForce(Vector3.up * jumpForce, ForceMode2D.Force);
    }


    //check if currently on ground, not working yet
    private bool IsOnGround()
    {
        if (_distanceToGround != 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.down/2, Vector3.down, _distanceToGround + .1f);
            
            if (!hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }
}

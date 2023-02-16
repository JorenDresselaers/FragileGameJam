using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
	const string HORIZONTAL = "Horizontal";
	const string VERTICAL = "Vertical";

	const string LAYER_PLATFORM = "Platform";

	private Rigidbody2D _rigidBody;
	private BoxCollider2D _collider;
	private SpriteRenderer _renderer;

	private bool _isOnGround = false;
	[SerializeField] private bool _canMove = false;

	[SerializeField] public float _movementSpeed = 1;

	public Animator _animator;

	[SerializeField] private AudioSource _skateAudioSource;
	//[SerializeField] private AudioSource _jumpAudioSource;
	//[SerializeField] private AudioSource _landAudioSource;

	void Awake()
	{
		_rigidBody = GetComponent<Rigidbody2D>();
		_collider = GetComponent<BoxCollider2D>();
		_renderer = GetComponentInChildren<SpriteRenderer>();
	}

	void Update()
	{
		if (_animator)
			_animator.SetBool("IsGrounded", _isOnGround);

		if (_renderer) _renderer.flipX = _rigidBody.velocity.x < -1;

		if (_canMove)
		{
			float horizontalMovement = Input.GetAxis(HORIZONTAL);
			if (horizontalMovement != 0)
			{

				//_rigidBody.velocity = new Vector3(
				//    Mathf.Clamp(_rigidBody.velocity.x + horizontalMovement * _movementSpeed, -10, 10),
				//    _rigidBody.velocity.y);

				_rigidBody.AddForce(Vector2.right * horizontalMovement * _movementSpeed);
			}

			if (Input.GetAxis(VERTICAL) > 0)
			{
				if (_isOnGround) Jump(20);
			}
		}
		else
		{
			float rotationDirection = Input.GetAxis(HORIZONTAL);
			_rigidBody.rotation -= rotationDirection * _movementSpeed;

		}

	}
		void Jump(float jumpForce)
		{
			if (!_rigidBody) return;
			_rigidBody.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
		}


	private const string PlatformTag = "Platform";
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.CompareTag(PlatformTag))
		{
			_isOnGround = true;
			//if(_landAudioSource)
			//	_landAudioSource.Play();
			if(_skateAudioSource)
				_skateAudioSource.UnPause();
		}
	}

	void OnCollisionExit2D(Collision2D col)
	{
		if (col.collider.CompareTag(PlatformTag))
		{
			_isOnGround = false;
            //if(_jumpAudioSource) 
            //	_jumpAudioSource.Play();
            if (_skateAudioSource)
                _skateAudioSource.Pause();
        }
	}
}
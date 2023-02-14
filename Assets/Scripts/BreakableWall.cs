using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{

	[Range(0, 90)] private float _impactAngle = 45.0f;
	[SerializeField] private float _impactThreshold = 0.5f;
	[SerializeField] private GameObject _wall;
	[SerializeField] private BoxCollider2D _wallCollider2D;
	[SerializeField] private ParticleSystem _particleSystem;

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Vector3 playerVel = other.gameObject.GetComponent<Rigidbody>().velocity;
			if (Vector3.Angle(playerVel, Vector3.left) > _impactAngle) // Wall shattered!
			{
				_particleSystem.Play();
				Destroy(_wall);
				Invoke("DestroyObject", 5.0f);
			}
		}
	}

	void DestroyObject()
	{
		Destroy(this);
	}
}

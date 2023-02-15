using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{

	[SerializeField] [Min(0)] private float _impactAngle = 45.0f;
	[SerializeField] private float _impactThreshold = 5.0f;
	[SerializeField] private GameObject _wall;
	[SerializeField] private BoxCollider2D _wallCollider2D;
	[SerializeField] private ParticleSystem _particleSystem;
	[SerializeField] private GameObject _brokenWall;
	[SerializeField] private GameObject _parentGameObject;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Vector3 playerVel = other.gameObject.GetComponent<Rigidbody2D>().velocity;
			float playerSpeed = playerVel.magnitude;
			if (CheckImpactAngle(playerVel, _impactAngle, _impactThreshold)) // Wall shattered!
			{
				Transform tr = _parentGameObject.GetComponentInParent<Transform>();
				Debug.Log(tr.position);
				var wallInst = Instantiate(_brokenWall, tr.position, tr.rotation);
				wallInst.transform.localScale = tr.localScale;
				wallInst.GetComponent<BrokenWall>().SetCollisionInvalid(other, playerVel.normalized);
				_particleSystem.Play();
				_wall.SetActive(false);
				Invoke("DestroyObject", 1.0f);
				//Destroy(_wall);
			}
		}
	}

	bool CheckImpactAngle(Vector3 impactVector, float impactAngle, float impactThreshold)
	{
		float playerSpeed = impactVector.magnitude;
		if ((Vector3.Angle(impactVector, Vector3.left) > impactAngle) || (Vector3.Angle(impactVector, Vector3.right) > impactAngle) && playerSpeed > impactThreshold)
		{
			return true;
		}
		return false;
	}

	void DestroyObject()
	{
		Destroy(this);
	}
}

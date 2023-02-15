using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenWall : MonoBehaviour
{
	private Collider2D _other;
	[SerializeField] private float _force = 1000.0f;

	//void OnCollisionEnter2D(Collision2D collision)
	//{
	//	if (!collision.gameObject.CompareTag("Player"))
	//	{
	//		return;
	//	}
	//	Vector2 collisionPos = collision.transform.position;
	//	var parent = transform.parent.gameObject;
	//	foreach (var collider2D in parent.GetComponentsInChildren<Collider2D>())
	//	{
	//		Vector2 collisionDir = collisionPos - new Vector2(collider2D.transform.position.x, collider2D.transform.position.y);
	//		Vector2 forceDir = new Vector2((1 / -collisionDir.x) * _force , 0.0f);
	//		collider2D.attachedRigidbody.AddForce(forceDir * 2.0f);
	//		collider2D.attachedRigidbody.AddTorque(_force * 0.5f);
	//	}
	//	Invoke("Invalidate", 0.5f);
	//}

	public void SetCollisionInvalid(Collider2D other, Vector2 direction)
	{
		_other = other;
		Vector2 collisionPos = other.transform.position;
		//var parent = transform.parent.gameObject;
		foreach (var collider2D in GetComponentsInChildren<Collider2D>())
		{
			Vector2 collisionDir = collisionPos - new Vector2(collider2D.transform.position.x, collider2D.transform.position.y);
			Vector2 forceDir = new Vector2((1 / collisionDir.magnitude) * _force, 0.0f);
			if (Mathf.Abs(forceDir.x) < 20.0f)
			{
				forceDir.x = 20.0f * Mathf.Sign(collisionDir.magnitude);
			}
			Debug.DrawLine(new Vector3(collisionPos.x, collisionPos.y, 0.0f), collider2D.transform.position, Color.red);
			collider2D.attachedRigidbody.AddForce(forceDir);
			collider2D.attachedRigidbody.AddTorque(_force);
			//Physics2D.IgnoreCollision(collider2D, _other);
			Physics2D.IgnoreLayerCollision(5, 5);
		}
		Invoke("InvalidateCollision", 0.5f);
		//Debug.Break();
	}

	void InvalidateCollision()
	{
		foreach (var collider2D in GetComponentsInChildren<Collider2D>())
		{
			Physics2D.IgnoreCollision(collider2D, _other);
		}
	}

}

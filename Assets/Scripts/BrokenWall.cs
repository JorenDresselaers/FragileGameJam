using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenWall : MonoBehaviour
{
	private Collider2D _other;

	public void SetCollisionInvalid(Collider2D other)
	{
		_other = other;
		Invoke("Invalidate", 1.0f);
	}

	private void Invalidate()
	{
		foreach (var collider2D in GetComponentsInChildren<Collider2D>())
		{
			Physics2D.IgnoreCollision(collider2D, _other);
		}
	}

}

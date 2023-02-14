using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hook : MonoBehaviour
{
	private Rigidbody2D rb;
	private bool hooking = false;
	[SerializeField] private float hookspeed;
	[SerializeField] private Rigidbody2D player;


	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (!hooking)
		{
			rb.transform.position = player.transform.position;
		}

		if (Input.GetMouseButtonDown(0))
		{
			Vector3 slingPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			ShootSling(slingPos);
		}

		if (!Input.GetMouseButton(0))
		{
			RetractSling();
		}
	}

	void ShootSling(Vector3 slingPos)
	{
		hooking = true;
		rb.simulated = true;

		Vector3 toHook = slingPos - player.transform.position;
		rb.velocity = toHook * hookspeed;
	}

	void RetractSling()
	{
		hooking = false;
		rb.simulated = false;
	}

	// Update is called once per frame

	void OnCollisionEnter2D(Collision2D col)
	{
		rb.simulated = false;
	}
}

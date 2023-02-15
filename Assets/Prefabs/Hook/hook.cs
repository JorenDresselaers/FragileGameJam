using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hook : MonoBehaviour
{
	private Rigidbody2D rb;
	private SpriteRenderer sprite;
	private bool hooking = false;
	private bool hooked = true;
	[SerializeField] private float hookspeed;
	[SerializeField] private float retractspeed;
	[SerializeField] private Rigidbody2D player;
	[SerializeField] private ParticleSystem particle;


	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		sprite = rb.GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
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

		if (hooked)
		{
			Vector3 toHook = rb.transform.position - player.transform.position;
			player.velocity = toHook.normalized * retractspeed;

			float rotz = Mathf.Atan2(toHook.y, toHook.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0, 0, rotz);
		}
	}

	void ShootSling(Vector3 slingPos)
	{
		hooking = true;
		rb.simulated = true;
		sprite.enabled = true;

		Vector3 toHook = slingPos - player.transform.position;

		float rotz = Mathf.Atan2(toHook.y, toHook.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, rotz);

		rb.velocity = toHook * hookspeed;
	}

	void RetractSling()
	{
		hooking = false;
		hooked = false;
		rb.simulated = false;
		sprite.enabled = false;
	}

	// Update is called once per frame

	void OnCollisionEnter2D(Collision2D col)
	{
		rb.simulated = false;
		hooked = true;
		particle.Play();
	}
}

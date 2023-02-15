using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hook : MonoBehaviour
{
	private Rigidbody2D rb;
	private SpriteRenderer sprite;
	private LineRenderer line;
	private bool hooking = false;
	private bool hooked = true;
	[SerializeField] private float hookspeed;
	[SerializeField] private float retractspeed;
	[SerializeField] private Rigidbody2D player;
	[SerializeField] private ParticleSystem particle;

	public float checkRadius;
	public LayerMask whatIsPlatform;

	bool M1 = false;


	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		sprite = rb.GetComponent<SpriteRenderer>();
		line = player.GetComponent<LineRenderer>();
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
			M1 = true;
		}

		if (Input.GetMouseButtonDown(1))
		{
			Vector3 slingPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			ShootSling(slingPos);
		}

		if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
		{
			RetractSling();
			M1 = false;
		}

		if (Physics2D.OverlapCircle(rb.position, checkRadius, whatIsPlatform))
		{
			hooked = true;
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
		}

		if (hooked && M1)
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
		sprite.enabled = true;
		line.enabled = true;

		Vector3 toHook = slingPos - player.transform.position;

		float rotz = Mathf.Atan2(toHook.y, toHook.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0, 0, rotz);

		rb.velocity = toHook * hookspeed;
	}

	void RetractSling()
	{
		hooking = false;
		hooked = false;
		rb.constraints = RigidbodyConstraints2D.None;
		sprite.enabled = false;
		line.enabled = false;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		hooked = true;
		particle.Play();
	}
}

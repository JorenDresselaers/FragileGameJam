using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{

	[SerializeField] [Min(0)] private float _impactAngle = 45.0f;
	[SerializeField] private float _impactThreshold = 5.0f;
	[SerializeField] private GameObject _wall;
	[SerializeField] private BoxCollider2D _wallCollider2D;
	[SerializeField] private GameObject _brokenWall;
	[SerializeField] private GameObject _parentGameObject;
	[SerializeField] private AudioClip _breakWallClip;
	private LoadSceneOnDestroy _loadSceneOnDestroy;

    private AudioPlayer _audioPlayer;

	void Awake()
	{
		_loadSceneOnDestroy = GetComponentInParent<LoadSceneOnDestroy>();
        if (_loadSceneOnDestroy) print("Loaded!");

		_audioPlayer = FindAnyObjectByType<AudioPlayer>();
    }

	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.CompareTag("Player"))
		{
            if (_loadSceneOnDestroy)
            {
                bool canLoadLevel = true;
                foreach (var objective in _loadSceneOnDestroy._destroyToExitList)
                {
                    if (objective.GetComponentInChildren<BreakableWall>())
                    {
                        canLoadLevel = false;
                    }
                }

                if (!canLoadLevel)
                {
                    Vector2 newVelocity = Vector2.zero;
                    newVelocity.x = -1 * other.gameObject.GetComponentInParent<Rigidbody2D>().velocity.x;
                    newVelocity.y = other.gameObject.GetComponentInParent<Rigidbody2D>().velocity.y;
                    other.gameObject.GetComponentInParent<Rigidbody2D>().velocity = newVelocity;
                    return;
                }
            }

            Vector3 playerVel = other.gameObject.GetComponentInParent<Rigidbody2D>().velocity;
			float playerSpeed = playerVel.magnitude;
			if (CheckImpactAngle(playerVel, _impactAngle, _impactThreshold)) // Wall shattered!
			{
				Transform tr = _parentGameObject.GetComponentInParent<Transform>();
				var wallInst = Instantiate(_brokenWall, tr.position, tr.rotation);
				wallInst.transform.localScale = tr.localScale;
				wallInst.GetComponent<BrokenWall>().SetCollisionInvalid(other, playerVel.normalized);
				_wall.SetActive(false);
                Invoke("DestroyObject", 1.0f);
				//Destroy(_wall);
				//play sound
				if(_breakWallClip && _audioPlayer)
				{
					_audioPlayer.PlayAudio(_breakWallClip);
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.CompareTag("Player"))
		{
			Debug.Log("FUNNNNNYYYYY");

		}
	}

	bool CheckImpactAngle(Vector3 impactVector, float impactAngle, float impactThreshold)
	{
		float playerSpeed = impactVector.magnitude;
		if (((Vector3.Angle(impactVector, Vector3.left) > impactAngle) || (Vector3.Angle(impactVector, Vector3.right) > impactAngle)) && playerSpeed > impactThreshold)
		{
			return true;
		}
		return false;
	}

	void DestroyObject()
	{
		if(_loadSceneOnDestroy) _loadSceneOnDestroy.LoadLevel();

        Destroy(this);
    }
}

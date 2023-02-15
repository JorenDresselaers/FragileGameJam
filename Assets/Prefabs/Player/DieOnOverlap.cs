using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieOnOverlap : MonoBehaviour
{
    //[SerializeField] private Rigidbody2D _body;
    private SpriteRenderer _renderer;
    
    void Awake()
    {
        //if (!_body) _body = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //on overlap with a platform, make the character invisible and call Kill a few seconds later
    private const string PlatformTag = "Platform";
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.parent.transform.rotation.z < 180 && transform.parent.transform.rotation.z > 0)
        {
            if (collision.collider.CompareTag(PlatformTag))
            {
                Invoke(KILL_METHODNAME, 3);
                //_renderer.enabled = false;
                //Already destroy the hook so we cant hook while dead
                hook _temp = FindFirstObjectByType<hook>();
                if(_temp)
                _temp.gameObject.SetActive(false);
                //if(_temp)
                //    Destroy(_temp);
            }
        }
    }

    const string KILL_METHODNAME = "Kill";
    void Kill()
    {
        //Destroy(FindFirstObjectByType<hook>().gameObject);
        //Destroy(transform.parent.gameObject);

        //Reload the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

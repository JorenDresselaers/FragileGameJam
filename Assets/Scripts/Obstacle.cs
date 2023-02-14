using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private int ActivateDistance;
    [SerializeField] private int KillDistance;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 playerPos = player.transform.position;
        float dist = Vector3.Distance(pos, playerPos);
        //check distance to see if we need to activate our obstacle
        if (dist < ActivateDistance)
        {
            Activate();
        }
        //check distance to see if our character is dead
        if (dist < KillDistance)
        {
            KillPlayer();
        }
    }

    public virtual void Activate()
    {

    }
    private void KillPlayer()
    {
        return;
    }
}

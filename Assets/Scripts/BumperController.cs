using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, Space.World);
        
    }

    public virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ActionOnPlayer(other.gameObject.GetComponent<PlayerController>(), other.GetContact(0).normal.normalized);
        }
    }

    // POLYMORPHISM virtual
    public virtual void ActionOnPlayer(PlayerController player, Vector3 direction)
    {
        player.AddForce(- direction, 5);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRb;
    GameManager manager;
    float speed = 5;
    bool isTriggering;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        manager = GameManager.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        float verticalInput = - Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        playerRb.AddForce(new Vector3(horizontalInput, 0, verticalInput), ForceMode.Impulse);

        isTriggering = false;
        ForbidBacktrack();
    }

    void ForbidBacktrack()
    {
        if(transform.position.x < -9.5 & playerRb.velocity.x < 0)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, transform.position.z);
            playerRb.velocity = new Vector3(0, playerRb.velocity.y, playerRb.velocity.z);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bumper"))
        {
            playerRb.AddForce(other.GetContact(0).normal * (10), ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            if(!isTriggering)
            {
                isTriggering = true;
                manager.LevelComplete(); 
            }
        }
    }
}

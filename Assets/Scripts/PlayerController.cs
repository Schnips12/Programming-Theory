using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody playerRb;
    float speed = 5;
    public bool isInFinishArea;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        float verticalInput = - Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        playerRb.AddForce(new Vector3(horizontalInput, 0, verticalInput), ForceMode.Impulse);
        if(transform.position.x < -10 & playerRb.velocity.x < 0)
        {
            transform.position = new Vector3(-10, transform.position.y, transform.position.z);
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
            isInFinishArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            isInFinishArea = false;
        }
    }
}

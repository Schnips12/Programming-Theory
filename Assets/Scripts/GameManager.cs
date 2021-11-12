using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject Tile;
    public PlayerController player;
    GameObject currentLevel, nextLevel;
    bool isMoving, isWon;

    Vector3 startingPoint, newStartingPoint;
    float transitionDuration = 2.0f;
    Vector3 playerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = GameObject.Find("Tile");
        startingPoint = new Vector3(-9.0f, 0.5f, 7.5f);
        LoadNext();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isInFinishArea & !isWon)
        {
            isWon = true;
            if (!isMoving)
            {
                isWon = true;
                StartCoroutine(MoveToNext(AfterMovingToNext));
            }
        } else if (!player.isInFinishArea)
        {
            isWon = false;
        }
            
    }

    IEnumerator MoveToNext(Action action)
    {  
        isMoving = true;

        // Freeze the player.
        playerSpeed = player.GetComponent<Rigidbody>().velocity;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.enabled = false;

        // Prepare to translate.
        float timeStartMovement = Time.time;
        Vector3 translation = startingPoint - newStartingPoint;
        Vector3 step;

        while(Time.time < (timeStartMovement + transitionDuration))
        {
            step = (Time.deltaTime / transitionDuration) * translation;
            nextLevel.transform.Translate(step, Space.World);
            currentLevel.transform.Translate(step, Space.World);
            player.transform.Translate(step, Space.World);
            yield return null;
        }

        // Unfreeze the player and authorize another transition.
        action();
    }

    // To be passed as a parameter of MoveToNext.
    public void AfterMovingToNext()
    {
        isMoving = false;
        player.enabled = true;
        player.GetComponent<Rigidbody>().velocity = playerSpeed;
        
        // Disable all previous colliders.
        Collider[] currentColliders = currentLevel.GetComponentsInChildren<Collider>();
        foreach (Collider item in currentColliders)
        {
            item.enabled = false;
        }
    }

    void LoadNext()
    {
        nextLevel = Instantiate(Tile, new Vector3(20, 0, -15), Tile.transform.rotation);
        newStartingPoint = new Vector3(11.0f, 0.5f, -7.5f);
    }
}

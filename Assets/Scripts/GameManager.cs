using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public GameObject Tile;
    public PlayerController player;
    GameObject previousLevel, currentLevel, nextLevel;
    bool isMoving;
    long score;

    float transitionDuration = 2.0f;
    Vector3 playerSpeed;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;

        currentLevel = Instantiate(Tile, Vector3.zero, Tile.transform.rotation);
        currentLevel.GetComponent<TileManager>().SetBumpers(0);

        LoadNext();
    }

    // Update is called once per frame
    void Update()
    {

            
    }

    public void LevelComplete()
    {
        if(score >= 0)
        {
            SceneManager.LoadScene("Title Screen", LoadSceneMode.Single);
        }

        score++;

        previousLevel = currentLevel;
        currentLevel = nextLevel;
        LoadNext();

        previousLevel.GetComponent<TileManager>().DisableFinishTrigger();
        if (!isMoving)
        {
            StartCoroutine(MoveToNext(AfterMovingToNext));
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
        Vector3 translation = - currentLevel.GetComponent<TileManager>().travel;
        Vector3 step;

        while(Time.time <= (timeStartMovement + transitionDuration))
        {
            step = (Time.deltaTime / transitionDuration) * translation;
            nextLevel.transform.Translate(step, Space.World);
            currentLevel.transform.Translate(step, Space.World);
            previousLevel.transform.Translate(step, Space.World);
            player.transform.Translate(step, Space.World);
            yield return null;
        }

        // Actions to perform after the MoveToNext coroutine has ended.
        action();
    }

    // To be passed as a parameter of MoveToNext.
    public void AfterMovingToNext()
    {
        // Unfreeze the player and authorize another transition.
        isMoving = false;
        player.enabled = true;
        player.GetComponent<Rigidbody>().velocity = playerSpeed;

        Destroy(previousLevel);
    }

    void LoadNext()
    {
        Vector3 currentSize = currentLevel.GetComponent<TileManager>().travel;
        nextLevel = Instantiate(Tile, currentLevel.transform.position + currentSize, Tile.transform.rotation);
        nextLevel.GetComponent<TileManager>().SetBumpers(score + 1);
    }
}

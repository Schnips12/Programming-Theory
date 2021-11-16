using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject finishArea;
    public Vector3 travel;
    public GameObject[] bumpers;

    // Start is called before the first frame update
    void Awake()
    {
        //finishArea = GameObject.FindWithTag("Finish");
        travel = new Vector3(20.0f, 0.0f, -15.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Disable the finish area collider
    public void DisableFinishTrigger()
    {
        finishArea.GetComponent<Collider>().enabled = false;
    }

    public void SetBumpers(long numberToActivate)
    {
        for (int index = 0; index < bumpers.Length ; index++)
        {
            bumpers[index].SetActive(index < numberToActivate);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Loader : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("Title Screen", LoadSceneMode.Single);
    }
}

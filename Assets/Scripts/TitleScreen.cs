using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TitleScreen : MonoBehaviour
{ 
    // The title screen can display different menus and buttons. Each set is managed through a canva.
    GameObject gameCanva, profilesCanva, secretsCanva;
    TMP_Dropdown profileSelector;
    TMP_InputField newProfileName;
    public TMP_Text createProfileOrCancel;


    // Start is called before the first frame update.
    // Initialisation of the canvas then display the title/game screen.
    void Start()
    {
        // Identifies the canvas.
        gameCanva = GameObject.Find("Game Canva");
        profilesCanva = GameObject.Find("Profiles Canva");
        secretsCanva = GameObject.Find("Secrets Canva");

        // Stores items to be toggled later.
        profileSelector = GameObject.Find("Existing Profiles").GetComponent<TMP_Dropdown>();
        newProfileName = GameObject.Find("New Name").GetComponent<TMP_InputField>();

        GameScreen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameScreen()
    {
        gameCanva.SetActive(true);
        profilesCanva.SetActive(false);
        secretsCanva.SetActive(false);
    }

    public void ProfileScreen()
    {
        gameCanva.SetActive(false);
        profilesCanva.SetActive(true);
        secretsCanva.SetActive(false);
        newProfileName.gameObject.SetActive(false);
    }

    public void SecretsScreen()
    {
        gameCanva.SetActive(false);
        profilesCanva.SetActive(false);
        secretsCanva.SetActive(true);
    }

    public void SelectProfile()
    {
        if (profileSelector.IsActive())
        {
            Debug.Log(profileSelector.captionText.text);
        } else
        {
            Debug.Log(newProfileName.text);
            profileSelector.gameObject.SetActive(true);
            newProfileName.gameObject.SetActive(false);
        }
        GameScreen();
    }

    // Toggle the displays of the profile selector and the input field.
    public void CreateNewProfile()
    {
        profileSelector.gameObject.SetActive(!profileSelector.IsActive());
        newProfileName.gameObject.SetActive(!newProfileName.IsActive());
        if (createProfileOrCancel.text == "Create new")
        {
            createProfileOrCancel.text = "Cancel";
        } else
        {
            createProfileOrCancel.text = "Create new";
        }
    }


    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}

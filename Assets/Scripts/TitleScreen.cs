using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

// The title screen can display three different menus and buttons. Each set is managed through a canva.
public class TitleScreen : MonoBehaviour
{ 
    GameObject gameCanva, profilesCanva, secretsCanva;
    TMP_Dropdown profileSelector;
    TMP_InputField newProfileName;
    public TMP_Text createProfileOrCancel;


    // Start is called before the first frame update.
    void Start()
    {
        // Set the canvas defined in the editor.
        gameCanva = GameObject.Find("Game Canva");
        profilesCanva = GameObject.Find("Profiles Canva");
        secretsCanva = GameObject.Find("Secrets Canva");

        // Stores items to be toggled later (can't find them when the canva is not active).
        profileSelector = GameObject.Find("Existing Profiles").GetComponent<TMP_Dropdown>();
        newProfileName = GameObject.Find("New Name").GetComponent<TMP_InputField>();

        // Initialize a consistent profile canva. Toggling will maintain consistency.
        profileSelector.gameObject.SetActive(true);
        newProfileName.gameObject.SetActive(false);
        createProfileOrCancel.text = "Create new";

        // Display the game canva.
        DisplayCanva(gameCanva);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Activates the canva passed as a parameter in the editor, and desactivates the others.
    public void DisplayCanva(GameObject canva)
    {
        if(Profile.Instance)
            RefreshSelector();
        gameCanva.SetActive(canva == gameCanva);
        profilesCanva.SetActive(canva == profilesCanva);
        secretsCanva.SetActive(canva == secretsCanva);
    }

    // Set the player profile to the selected or new value.
    public void SelectProfile()
    {
        if (profileSelector.IsActive())
        {
            Profile.Instance.Load(profileSelector.captionText.text);
        } else
        {
            Profile.Instance.Load(newProfileName.text);
            // Toggle profile creating items to prioritize selection over creation.
            ToggleProfileCreation();
        }
        DisplayCanva(gameCanva);
    }

    // Toggle the displays of the profile selector, the input field, and the text on the toggling button.
    public void ToggleProfileCreation()
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
        RefreshSelector();
    }

    void RefreshSelector()
    {
        profileSelector.ClearOptions();
        profileSelector.AddOptions(Profile.Instance.savedProfiles);
    }

    // Exit the application.
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}

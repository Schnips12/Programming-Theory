using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>The title screen can display three different menus and buttons.
/// Each set is managed through a canva.</summary>
public class TitleScreen : MonoBehaviour
{ 
    public GameObject gameCanva, profilesCanva, secretsCanva, secretsButton, forfeitButton, startGame;
    public TMP_Dropdown profileSelector;
    public TMP_InputField newProfileName;
    public TMP_Text createProfileOrCancel, title;

    // Start is called before the first frame update.
    void Start()
    {
        // Initialize a consistent profile canva. Toggling will maintain consistency.
        profileSelector.gameObject.SetActive(true);
        newProfileName.gameObject.SetActive(false);
        createProfileOrCancel.text = "Create new";

        // Display the profile canva.
        DisplayCanva(gameCanva);
    }

    /// <summary>Activates the canva passed as a parameter in the editor, and desactivates the others.</summary>
    public void DisplayCanva(GameObject canva)
    {
        if(canva == gameCanva)
        {
            SetStartGameButton();
            secretsButton.SetActive(Profile.Instance.data != null);
        }

        if(canva == profilesCanva)
        {
            RefreshSelector();
        }
        
        gameCanva.SetActive(canva == gameCanva);
        profilesCanva.SetActive(canva == profilesCanva);
        secretsCanva.SetActive(canva == secretsCanva);
    }

    /// <summary>Set the player profile to the selected or new value.</summary>
    public void SelectProfile()
    {
        if (profileSelector.IsActive())
        {
            Profile.Instance.Load(profileSelector.captionText.text);
        } else
        {
            Profile.Instance.Load(newProfileName.text);
            // Toggle profile creating items to prioritize selection over a second creation on next load.
            ToggleProfileCreation();
        }

        title.text = Profile.Instance.data.name;
        DisplayCanva(gameCanva);
    }

    /// <summary>The "Start Game" button can have three state : disabled, start new game or resume.
    /// The behavior is based on the loaded profile data.</summary>
    public void SetStartGameButton()
    {
        if (Profile.Instance.data != null) 
        {
            if (Profile.Instance.data.hasOngoingGame)
            {
                startGame.SetActive(true);
                startGame.GetComponentInChildren<TextMeshProUGUI>().text = "Resume game";
                forfeitButton.gameObject.SetActive(true);
            } else
            {
                ForceNewGame();
            }
        } else
        {
            startGame.SetActive(false);
            forfeitButton.gameObject.SetActive(false);
        }
    }

    /// <summary>Allow the user to start a new game regardless of having a saved game.</summary>
    public void ForceNewGame()
    {
        startGame.SetActive(true);
        startGame.GetComponentInChildren<TextMeshProUGUI>().text = "Start new game";
        forfeitButton.gameObject.SetActive(false);
    }

    /// <summary>Toggle the displays of the profile selector, the input field,
    /// and the text on the toggling button.</summary>
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

    /// <summary>Refresh the list of profiles the user can pick from.
    /// To force checking the save files, invoke Profile.Instance.SearchSavedProfiles() beforehand.</summary>
    void RefreshSelector()
    {
        profileSelector.ClearOptions();
        profileSelector.AddOptions(Profile.Instance.savedProfiles);
        profileSelector.value = Profile.Instance.GetProfileIndex();
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

    /// <summary>Load the gameplay scene.</summary>
    // TODO Here or in the Game Manager, allow to resume a saved game. A parameter should be passed here.
    public void StartGame()
    {
        if (Profile.Instance.data != null)
            SceneManager.LoadScene("Playing", LoadSceneMode.Single);
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    public Button myButton;
    private bool isClicked = false;
    

    void Start()
    {
        // Subscribe to the button click event
        myButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        isClicked = true;

        // Perform actions inside the if statement
        if (isClicked)
        {
            SceneManager.LoadScene("Level");
        }
    }
}

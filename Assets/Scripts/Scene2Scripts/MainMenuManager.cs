using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    //public static MainMenuManager Instance;
    public static string nameText;
    // Start is called before the first frame update

    public TMP_InputField nameInputText;
    public TextMeshProUGUI namePlaceholder;
    public TextMeshProUGUI warningText;

    void Start()
    {
        ////start of new code
        //if (Instance != null)
        //{
        //    Destroy(gameObject);
        //    return;
        //}


       //Instance = this;
       // DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            CheckNameLength();
        }
    }

    public void CheckNameLength()
    {
        //checking for input length for name [3,7]
        if (nameInputText.text.Length > 7)
        {
            warningText.text = "Please Input A Name Less Than 7 Characters!!!";
            warningText.gameObject.SetActive(true);
        }
        else if(nameInputText.text.Length < 3)
        {
            warningText.text = "Please Input A Name Greater Than 2 Characters!!!";
            warningText.gameObject.SetActive(true);
        }
        else
        {
            warningText.gameObject.SetActive(false);
        }
    }

    public void SubmitName()
    {
        //checks to make sure name meets requirements [3,7]
        if (!warningText.IsActive())
        {
            nameText = "Name: " + nameInputText.text;
            SceneManager.LoadScene(1);
        }
    }
}

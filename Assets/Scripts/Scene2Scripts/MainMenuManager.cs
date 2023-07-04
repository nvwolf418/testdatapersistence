using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    //public static MainMenuManager Instance;
    public static string nameText;
    // Start is called before the first frame update

    public TextMeshProUGUI nameInputText;
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
        if(Input.anyKeyDown && warningText.IsActive() && nameInputText.text.Length > 3)
        {
            warningText.gameObject.SetActive(false);
        }
    }

    public void GetStartMenuName()
    {
        Debug.Log($"Name : {nameInputText.text} : length {nameInputText.text.Length}");

        if (nameInputText.text.Length > 3)
        {
            nameText = "Name: " + nameInputText.text;
            SceneManager.LoadScene(1);
        }
        else
        {
            warningText.gameObject.SetActive(true);
        }

    }
}

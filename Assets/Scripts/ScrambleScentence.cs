using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrambleScentence : MonoBehaviour
{
    [HideInInspector]
    string sentence = "This is a scentence";
    string[] words;
    public GameObject optionsPanel;
    public Button buttonPrefab;
    public Text buttonText;
    

    // Start is called before the first frame update
    void Start()
    {
        buttonText = buttonPrefab.GetComponentInChildren<Text>();
        SplitSentence(sentence);
        InstantiateButton();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SplitSentence(string sentence)
    {
        string str = sentence;
        words = sentence.Split(' ');
        //Debug.Log(words.Length);
        for (int i = 0; i < words.Length; i++)
        {
            Debug.Log(words[i]);
        }
    }

    public void InstantiateButton()
    {
        for(int i = 0; i < words.Length; i++)
        {
            Button wordButton = Instantiate(buttonPrefab, optionsPanel.transform);
            wordButton.GetComponentInChildren<Text>().text = words[i];
        }
    }

}

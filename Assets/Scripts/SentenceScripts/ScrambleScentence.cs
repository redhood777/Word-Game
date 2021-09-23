using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrambleScentence : MonoBehaviour
{
    public static ScrambleScentence instance;

    [HideInInspector]
    int score = 0;
    int question = 1;
    string sentence;
    public string answerSentence;
    string[] words;
    string[] jumbledWords;
    List<string> sentences;
    public GameObject optionsPanel;
    public GameObject answerPanel;
    public GameObject buttonPrefab;
    public GameObject gameEndScreen;
    public GameObject submitButton;
    //public GameObject loadingScreen;
    
    public Text buttonText;
    public Text scoreText;
    public Text questionNumber;

    public GameObject correctImage;
    public GameObject wrongImage;
    public GameObject warningText;


    public int maxQuestionValue;
    public int currentQuestionValue ;

    public int correctAnswerValue;
    //public GameObject correctAnswerText;

    [SerializeField]
    private SentenceDataScriptable sentenceDataScriptable;
    public GameObject finalScoreText;
    public GameObject finalCorrectAnswersText;

    [Header("Sound")]
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioSource buttonAudioSource;
    [SerializeField]
    AudioClip gamecomplete;
    [SerializeField]
    AudioClip correctanswer;
    [SerializeField]
    AudioClip wronganswer;
    [SerializeField]
    AudioClip optionSelected;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            GameObject.Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        //GameLoaded();
        scoreText.text = "0";
        questionNumber.text = "Q.1/"+ maxQuestionValue.ToString();
        sentences = sentenceDataScriptable.sentences;
        sentence = sentences[Random.Range(0, sentences.Count)];
        answerSentence = sentence + " ";
        Debug.Log(answerSentence);
        buttonText = buttonPrefab.GetComponentInChildren<Text>();
        SplitSentence(sentence);
        RandomizeArray(words);
        
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SplitSentence(string sentence)
    {
        string str = sentence;
        words = sentence.Split(' ');
        ////Debug.Log(words.Length);
        //for (int i = 0; i < words.Length; i++)
        //{
        //    Debug.Log(words[i]);
        //}
    }

    public void RandomizeArray(string[] sentenceArray)
    {
        for(int i = sentenceArray.Length - 1; i > 0; i--)
        {
            var r = Random.Range(0, i);
            var temp = sentenceArray[i];
            sentenceArray[i] = sentenceArray[r];
            sentenceArray[r] = temp;
        }

        for (int i = 0; i < sentenceArray.Length; i++)
        {
            GameObject wordButton = Instantiate(buttonPrefab, optionsPanel.transform);
            wordButton.GetComponentInChildren<Text>().text = sentenceArray[i];          
        }

        //optionsPanel.GetComponent<VerticalLayoutGroup>().childScaleHeight = false;

    }

    public void OptionSelected(GameObject option)
    {
        buttonAudioSource.PlayOneShot(optionSelected);
        GameObject copy;
        copy = Instantiate(option);
        copy.transform.SetParent(answerPanel.transform,false);
        copy.GetComponent<Button>().enabled = false;
        option.SetActive(false);

    }

    public void ResetOptions()
    {
        foreach(Transform child in answerPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach(Transform child in optionsPanel.transform)
        {
            child.gameObject.SetActive(true);
        }
        correctImage.SetActive(false);
        wrongImage.SetActive(false);
        //correctAnswerText.GetComponent<Text>().text = " ";

    }

    public void SubmitAnswer()
    {
        if (currentQuestionValue >= maxQuestionValue)
        {
            Debug.Log("GameEnd Called");          
            Invoke("GameEnd", 1f);         
        }
        

        string answer = string.Empty;
        foreach (Transform child in answerPanel.transform)
        {
            string tmp = child.gameObject.GetComponentInChildren<Text>().text + " ";
            answer = answer + tmp;
            tmp = string.Empty;
        }
        //Debug.Log(answerSentence);
        //Debug.Log(answer);
        submitButton.GetComponent<Button>().interactable = false;
        Debug.Log("CHECK");

        if (answer == answerSentence)
        {
            //correctAnswerText.GetComponent<Text>().text = "Correct Answer";
            score += 50;
            scoreText.text = score.ToString();
            
            correctAnswerValue += 1;
            correctImage.SetActive(true);
            audioSource.PlayOneShot(correctanswer);
            Invoke("NewSentence", 1f);
        }
        else
        {
            if (answerPanel.transform.childCount == 0)
            {
                warningText.SetActive(true);
                warningText.GetComponent<Text>().text = "No words selected! Please select some words before submitting";
                WarningDisplayed();
            }
            else
            {
                //correctAnswerText.GetComponent<Text>().text = "Wrong Answer";
                wrongImage.SetActive(true);
                audioSource.PlayOneShot(wronganswer);
                Invoke("NewSentence", 1f);
            }     
        }
    }

    public void NewSentence()
    {
        //correctAnswerText.GetComponent<Text>().text = " ";
        currentQuestionValue += 1;
        correctImage.SetActive(false);
        wrongImage.SetActive(false);
        submitButton.GetComponent<Button>().interactable = true;


        string newSentence = sentences[Random.Range(0, sentences.Count)];
        while(newSentence == sentence)
        {
            newSentence = sentences[Random.Range(0, sentences.Count)];
            
        }
        //string tmp = newSentence;

        //while(tmp == newSentence)
        //{
        //    newSentence = sentences[Random.Range(0, sentences.Count)];
        //}

        foreach (Transform child in answerPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in optionsPanel.transform)
        {
            Destroy(child.gameObject);
        }

        answerSentence = newSentence + " ";
        Debug.Log(answerSentence);
        SplitSentence(newSentence);
        RandomizeArray(words);

        
        question += 1;

        questionNumber.text = "Q." + question.ToString() +"/"+maxQuestionValue.ToString();

    }

    public void GameEnd()
    {
        gameEndScreen.SetActive(true);
        finalCorrectAnswersText.GetComponent<Text>().text = "Total Correct Answers : "+ correctAnswerValue.ToString() + "/" + maxQuestionValue.ToString();
        finalScoreText.GetComponent<Text>().text = "Your Score : " + score.ToString();
        audioSource.PlayOneShot(gamecomplete);
    }

    public void WarningDisplayed()
    {
        
        StartCoroutine(RemoveAfterSeconds(1, warningText));
    }

    IEnumerator RemoveAfterSeconds(int seconds, GameObject obj)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
        submitButton.GetComponent<Button>().interactable = true;
    }

    public void CheckNoRepeatedSentence()
    {

    }
}

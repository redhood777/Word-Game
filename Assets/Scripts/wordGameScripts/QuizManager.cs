using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public static QuizManager instance; //Instance to make is available in other scripts without reference

    [SerializeField] private GameObject gameComplete;
    //Scriptable data which store our questions data
    [SerializeField] private QuizDataScriptable questionDataScriptable;
    [SerializeField] private Image questionImage;           //image element to show the image
    [SerializeField] private WordData[] answerWordList;     //list of answers word in the game
    [SerializeField] private WordData[] optionsWordList;    //list of options word in the game


    private GameStatus gameStatus = GameStatus.Playing;     //to keep track of game status
    private char[] wordsArray = new char[15];               //array which store char of each options

    private List<int> selectedWordsIndex;                   //list which keep track of option word index w.r.t answer word index
    private int currentAnswerIndex = 0, currentQuestionIndex = 0;   //index to keep track of current answer and current question
    private bool correctAnswer = true;                      //bool to decide if answer is correct or not
    private string answerWord;                              //string to store answer of current question


    private int currentHintIndex = 0; // index to keep track for Hint;

    public Text Hint_txt;  // Text  to Show Hint;
    public GameObject hintButton;

    public Text scoretext;
    public Text FinalScore;
    public Text questionNumber;
    public int score;
    public int questionNo = 1;
    public int questionnumber;
    [Header("UI Image")]
    public Image greenImage;
    public Image redImage;

    public Button resetBtn;

    public GameObject correctAnsImage;
    public GameObject wrongAnsImage;


    [Header("Sound")]
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip gamecomplete;
    [SerializeField]
    AudioClip correctanswer;
    [SerializeField]
    AudioClip wronganswer;


    [Header("Animation")]
    public GameObject correctAnsAnimation;
    public GameObject WrongAnsAnimation;

    bool checkAns;
    


    int num;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);


    }

    private void Update()
    {
        FinalScore.text = score.ToString();

        if (Input.GetMouseButtonDown(0))
        {
            CloseHint();
        }
        //if (Application.runInBackground == false)
        //{
        //   // jsButton.SetActive(true);
        //}

    }


    // Start is called before the first frame update
    void Start()
    {
        //wordanimation.animate.EaseIn(); 
        Application.runInBackground = true;


        selectedWordsIndex = new List<int>();           //create a new list at start
        SetQuestion();                                  //set question
    }


    //public static int generateRandomNumber(int min, int max)
    //{

    //    int result = UnityEngine.Random.Range(min, max);

    //    if (result == lastRandomNumber)
    //    {

    //        return generateRandomNumber(min, max);

    //    }

    //    lastRandomNumber = result;
    //    return result;

    //}
    void SetQuestion()
    {

        //int go = generateRandomNumber(0, 50);


        //int num = UnityEngine.Random.Range(0, 50);

        num = RandomCheck.instance.savenumber[0];



        gameStatus = GameStatus.Playing;                //set GameStatus to playing 
        greenImage.enabled = false;

        //set the answerWord string variable
        answerWord = questionDataScriptable.questions[num].answer;
        Debug.Log(answerWord);
        //set the image of question
        questionImage.sprite = questionDataScriptable.questions[num].questionImage;

        Hint_txt.text = questionDataScriptable.questions[num].hint.ToUpper();

        //if randomness is to be removed, uncomment below three lines

        //answerWord = questionDataScriptable.questions[currentQuestionIndex].answer;
        ////set the image of question
        //questionImage.sprite = questionDataScriptable.questions[currentQuestionIndex].questionImage;

        //Hint_txt.text = questionDataScriptable.questions[currentQuestionIndex].hint;


        ResetQuestion();                               //reset the answers and options value to orignal     

        selectedWordsIndex.Clear();                     //clear the list for new question
        Array.Clear(wordsArray, 0, wordsArray.Length);  //clear the array

        //add the correct char to the wordsArray
        for (int i = 0; i < answerWord.Length; i++)
        {
            wordsArray[i] = char.ToUpper(answerWord[i]);
        }

        //add the dummy char to wordsArray
        for (int j = answerWord.Length; j < wordsArray.Length; j++)
        {
            wordsArray[j] = (char)UnityEngine.Random.Range(65, 90);
        }

        wordsArray = ShuffleList.ShuffleListItems<char>(wordsArray.ToList()).ToArray(); //Randomly Shuffle the words array

        //set the options words Text value
        for (int k = 0; k < optionsWordList.Length; k++)
        {
            optionsWordList[k].SetWord(wordsArray[k]);
        }

    }

    //Method called on Reset Button click and on new question
    public void ResetQuestion()
    {
        //activate all the answerWordList gameobject and set their word to "_"
        for (int i = 0; i < answerWordList.Length; i++)
        {
            answerWordList[i].gameObject.SetActive(true);
            answerWordList[i].SetWord('_');
        }

        //Now deactivate the unwanted answerWordList gameobject (object more than answer string length)
        for (int i = answerWord.Length; i < answerWordList.Length; i++)
        {
            answerWordList[i].gameObject.SetActive(false);
        }

        //activate all the optionsWordList objects
        for (int i = 0; i < optionsWordList.Length; i++)
        {
            optionsWordList[i].gameObject.SetActive(true);
            optionsWordList[i].gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

        }



        currentAnswerIndex = 0;
        currentHintIndex = 0;

        if (currentAnswerIndex == 0)
        {
            resetBtn.interactable = false;
        }


        if (redImage.enabled)
        {
            redImage.enabled = false;
        }
    }

    /// <summary>
    /// When we click on any options button this method is called
    /// </summary>
    /// <param name="value"></param>
    public void SelectedOption(WordData value)
    {

        //if gameStatus is next or currentAnswerIndex is more or equal to answerWord length
        if (gameStatus == GameStatus.Next || currentAnswerIndex >= answerWord.Length) return;







        selectedWordsIndex.Add(value.transform.GetSiblingIndex()); //add the child index to selectedWordsIndex list
        value.transform.LeanScale(Vector2.zero, 0.3f).setEaseInBack();
        StartCoroutine(deactivativebutton(value));

        //corutine for animation done below

        //value.gameObject.SetActive(false); //deactivate options object
        answerWordList[currentAnswerIndex].SetWord(value.wordValue); //set the answer word list
                                                                     //Debug.Log(value.wordValue);
                                                                     //  Debug.Log("S" + currentAnswerIndex.ToString());


        for (int i = 0; i <= currentAnswerIndex; i++)
        {
            //Debug.Log("QQ------"+ char.ToUpper(answerWord[i]));  // Right Answer
            //Debug.Log("WW-----"+ char.ToUpper(answerWordList[i].wordValue)); ///current answer


            if (char.ToUpper(answerWord[i]) != char.ToUpper(answerWordList[i].wordValue))
            {

                //StartCoroutine(WrongAswer());
                checkAns = false;
                Debug.Log("Wrong Answer");
            }

            if (char.ToUpper(answerWord[i]) == char.ToUpper(answerWordList[i].wordValue))
            {
                // StartCoroutine(RightAnswer());
                checkAns = true;
                Debug.Log("Right Answer");
            }
        }

        if (checkAns == false)
        {
            StartCoroutine(WrongAswer());

        }
        else
        {
            StartCoroutine(RightAnswer());

        }






        currentAnswerIndex++;   //increase currentAnswerIndex
        currentHintIndex++;    // increase Hint Index;
        if (currentAnswerIndex > 0)
        {
            resetBtn.interactable = true;
        }



        //if currentAnswerIndex is equal to answerWord length
        if (currentAnswerIndex == answerWord.Length)
        {

            correctAnswer = true;   //default value
            //loop through answerWordList
            for (int i = 0; i < answerWord.Length; i++)
            {
                //if answerWord[i] is not same as answerWordList[i].wordValue
                if (char.ToUpper(answerWord[i]) != char.ToUpper(answerWordList[i].wordValue))
                {
                    redImage.enabled = true;
                    correctAnswer = false; //set it false
                    break; //and break from the loop

                }
            }

            //if correctAnswer is true
            if (correctAnswer)
            {
                Debug.Log("Correct Answer");
                audioSource.PlayOneShot(correctanswer);
                score = score + 50;
                StartCoroutine(correctAnsImageEnable());
                scoretext.text = "" + score;

                questionNo = questionNo + 1;
                questionNumber.text = questionNo + "/15";
                greenImage.enabled = true;
                RandomCheck.instance.savenumber.Remove(num);
                gameStatus = GameStatus.Next; //set the game status
               
                currentQuestionIndex++; //increase currentQuestionIndex
                 

                int a = questionDataScriptable.questions.Count;
                int b = 35;
                int c = a - b;
                //if currentQuestionIndex is less that total available questions
                //if (currentQuestionIndex < questionDataScriptable.questions.Count) //if you want all questions from scriptable objects uncomment
                if (currentQuestionIndex < c)

                {
                    //StartCoroutine(RandomQuestion());
                    RandomCheck.instance.savenumber.Remove(num);
                    hoverText.hintscore = true;
                    hintgameobjectdisable();
                    Invoke("SetQuestion", 1f); //go to next question
                }
                else
                {
                    Debug.Log("Game Complete"); //else game is complete
                    audioSource.PlayOneShot(gamecomplete);
                    gameComplete.SetActive(true);
                }
            }

            else
            {
                StartCoroutine(WrongAnsImageEnable());
            }
        }
    }



    IEnumerator deactivativebutton(WordData value)
    {
        yield return new WaitForSeconds(0.1f);
        //value.gameObject.SetActive(false);
    }
    public void ResetLastWord()
    {
        if (selectedWordsIndex.Count > 0)
        {
            int index = selectedWordsIndex[selectedWordsIndex.Count - 1];
            optionsWordList[index].gameObject.SetActive(true);
            optionsWordList[index].gameObject.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

            transform.LeanScale(new Vector2(1f, 1f), 0.7f).setEaseInBack();
            //transform.LeanScale(Vector2.zero, 0.1f).setEaseOutBack();
            selectedWordsIndex.RemoveAt(selectedWordsIndex.Count - 1);

            currentAnswerIndex--;

            if (currentAnswerIndex == 0)
            {
                resetBtn.interactable = false;
            }


            answerWordList[currentAnswerIndex].SetWord('_');
        }

        if (redImage.enabled)
        {
            redImage.enabled = false;
        }
    }


    public void ShowHint()
    {
        if (hoverText.hintscore == true)
        {
            score = score - 10;
            scoretext.text = score.ToString();
            hoverText.hintscore = false;
        }

    }

    public void hintgameobjectdisable()
    {
        if (hintButton.activeInHierarchy == true)
        {
            hintButton.SetActive(false);
        }
    }
    IEnumerator correctAnsImageEnable()
    {
        correctAnsImage.SetActive(true);
        yield return new WaitForSeconds(1);
        correctAnsImage.SetActive(false);
    }
    IEnumerator WrongAnsImageEnable()
    {
        wrongAnsImage.SetActive(true);
        audioSource.PlayOneShot(wronganswer);
        yield return new WaitForSeconds(1);
        wrongAnsImage.SetActive(false);
        ResetQuestion();
    }

    IEnumerator RightAnswer()
    {
        correctAnsAnimation.SetActive(true);
        greenImage.enabled = true;
        yield return new WaitForSeconds(1f); //0.3f
        greenImage.enabled = false;
        correctAnsAnimation.SetActive(false);

    }

    IEnumerator WrongAswer()
    {
        WrongAnsAnimation.SetActive(true);
        redImage.enabled = true;
        yield return new WaitForSeconds(1f); //0.3f
        redImage.enabled = false;
        WrongAnsAnimation.SetActive(false);

    }


    public void CloseHint()
    {
        if (hintButton.activeInHierarchy)
            hintButton.SetActive(false);
    }

}








[System.Serializable]
public class QuestionData
{
    public Sprite questionImage;
    public string answer;
    public string hint;
}

public enum GameStatus
{
    Next,
    Playing
}

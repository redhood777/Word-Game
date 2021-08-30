using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PostData : MonoBehaviour
{
    //InputField outputArea;

    public InputField I_email;
    public InputField I_otp;




   

    // Start is called before the first frame update

    private void Awake()
    {

         

    }


    void Start()
    {
       // outputArea = GameObject.Find("OutputArea").GetComponent<InputField>();
        I_email = GameObject.Find("EMAIL").GetComponent<InputField>();
        I_otp =   GameObject.Find("OTP").GetComponent<InputField>();

        GameObject.Find("PostButton").GetComponent<Button>().onClick.AddListener(SendData);
        
    }

    void SendData() => StartCoroutine(PostData_Coroutine());

    IEnumerator PostData_Coroutine()
    {
        //outputArea.text = "Loading...";
        string uri = "https://192.168.1.81/api/verify_user_otp";
        WWWForm form = new WWWForm();
        form.AddField("I_email", "I_otp");
        using (UnityWebRequest request = UnityWebRequest.Post(uri,form))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                // outputArea.text = request.error;
                Debug.Log(request.error);
            else
                // outputArea.text = request.downloadHandler.text;
                Debug.Log("Api Send");
            // recall the api again he

        }
    }
    // Update is called once per frame
    void Update()
    {
         
    }
}

using System;
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
    public GameObject postButton;





    // Start is called before the first frame update

    private void Awake()
    {



    }


    void Start()
    {
        // outputArea = GameObject.Find("OutputArea").GetComponent<InputField>();
        I_email = GameObject.Find("EMAIL").GetComponent<InputField>();
        I_otp = GameObject.Find("OTP").GetComponent<InputField>();

        postButton.GetComponent<Button>().onClick.AddListener(SendData);

    }

    void SendData() => StartCoroutine(GetText("https://192.168.1.81/api/verify_user_otp", (result) => {
        Debug.Log(result);
                        })); // this log function is never logging a value.. Why is this?
        IEnumerator GetText(string url, Action<string> result)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            if (result != null)
                result(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.data); // this log is returning the requested data. 
            if (result != null)
                result(www.downloadHandler.text);
        }
    }

    //IEnumerator PostData_Coroutine()
    //{
    //    //outputArea.text = "Loading...";
    //    string uri = "https://192.168.1.81/api/verify_user_otp";
    //    WWWForm form = new WWWForm();
    //    UnityWebRequest www = UnityWebRequest.Post(uri, form);
    //    form.AddField("I_email", "I_otp");
    //    using (UnityWebRequest request = UnityWebRequest.Post(uri,form))
    //    {
    //        yield return request.SendWebRequest();
    //        if (request.isNetworkError)
    //            // outputArea.text = request.error;
    //            Debug.Log(request.error);
    //        else if (request.isHttpError)
    //            Debug.Log("HTTP error");
    //        else
    //            // outputArea.text = request.downloadHandler.text;
    //            Debug.Log("Api Send");
    //        // recall the api again he

    //    }
    //}
    // Update is called once per frame
    void Update()
    {
         
    }
}

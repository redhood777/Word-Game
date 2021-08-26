using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PostData : MonoBehaviour
{
    InputField outputArea;


    // Start is called before the first frame update
    void Start()
    {
        outputArea = GameObject.Find("OutputArea").GetComponent<InputField>();
        GameObject.Find("PostButton").GetComponent<Button>().onClick.AddListener(SendData);
        
    }

    void SendData() => StartCoroutine(PostData_Coroutine());

    IEnumerator PostData_Coroutine()
    {
        outputArea.text = "Loading...";
        string uri = " ";
        WWWForm form = new WWWForm();
        form.AddField("email", "otp");
        using (UnityWebRequest request = UnityWebRequest.Post(uri,form))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                outputArea.text = request.error;
            else
                outputArea.text = request.downloadHandler.text;
            // recall the api again he

        }
    }
    // Update is called once per frame
    void Update()
    {
         
    }
}

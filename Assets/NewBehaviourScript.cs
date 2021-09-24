using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void setCookie(string a, string b, int expiredays);

    [DllImport("__Internal")]
    private static extern string getCookie(string a); 
    
   
    // Start is called before the first frame update
    void Start()
    {
        setCookie("email","abc@abc.com",1);

        string result = getCookie("email");


        Debug.Log("****************--"+result + ">?>>>>>>>>>>>>>>>>>>");

        print(result + "#########################");


        // string result1 = getCookie1("email");
        //
        //string result112 = getCookie2();


        /// Debug.Log("****************--???" + result1 + ">?>>>>>>>>>>>>>>>>>>");

        // Debug.Log("****************--???" + result112 + ">?>>>>>>>>>>>>>>>>>>");

    }


    public void Shubham()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Demo_2 : MonoBehaviour
{
    string cookieName;
    [DllImport("__Internal")]
    private static extern void createCookie(string email,string value,int days);
    [DllImport("__Internal")]
    private static extern void getCookie(string c_name);

    // Start is called before the first frame update
    void Start()
    {
        //createCookie("email", "abc@abc", 1);
        getCookie("22773504");
        //Debug.LogError(cookieName +"????????");
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

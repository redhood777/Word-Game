using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    //[DllImport("__Internal")]
    //private static extern void setCookie(string a, string b, int expiredays);

    [DllImport("__Internal")]
    private static extern string getCookie(string a);

    [DllImport("__Internal")]
    private static extern string getOtp(string a);


    string email;
    string otp;


	[Space]
	[SerializeField] InputField username;
	[SerializeField] InputField password;


	[SerializeField] Text errorMessages;

	[SerializeField] Button loginButton;

	[SerializeField] string url;
	WWWForm form;

	// Start is called before the first frame update
	void Start()
    {
        //setCookie("email","abc@abc.com",1);

        email  = getCookie("email");
        otp = getOtp("otp");

        Debug.Log(">>>>>>>>>>>>>>+" + email);
        Debug.Log(">>>>>>>>>>>>>>>>" + otp);

		username.text = email;
		password.text = otp;

    }

	public void OnLoginButtonClicked()
	{
		//loginButton.interactable = false;
		//progressCircle.SetActive(true);
		StartCoroutine(Login());
	}

	IEnumerator Login()
	{
		form = new WWWForm();

		form.AddField("email", username.text);
		form.AddField("otp", password.text);

		WWW w = new WWW(url, form);
		yield return w;

		if (w.error != null)
		{
			errorMessages.text = "404 not found!";
			Debug.Log("<color=red>" + w.text + "</color>");//error
		}
		else
		{
			if (w.isDone)
			{
				if (w.text.Contains("Authenticated"))
				{
					SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
				}
				else
				{
					StartCoroutine(disbleMessage());
					Debug.Log("<color=red>" + w.text + "</color>");
				}
			}
		}

		loginButton.interactable = true;
		//progressCircle.SetActive(false);

		w.Dispose();

		IEnumerator disbleMessage()
		{
			errorMessages.text = "Invalid username or OTP!";
			yield return new WaitForSeconds(2f);
			errorMessages.text = "";
		}
	}
}
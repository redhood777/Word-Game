using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Server : MonoBehaviour
{


	//[SerializeField] GameObject welcomePanel;
	//[SerializeField] Text user;
	[Space]
	[SerializeField] InputField username;
	[SerializeField] InputField password;

	[SerializeField] Text errorMessages;
	//[SerializeField] GameObject progressCircle;


	[SerializeField] Button loginButton;


	[SerializeField] string url;

	WWWForm form;

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
					SceneManager.LoadScene(1);
				}
				else
				{
					errorMessages.text = "Invalid username or password!";
					Debug.Log("<color=red>" + w.text + "</color>");
				}
			}
		}

		loginButton.interactable = true;
		//progressCircle.SetActive(false);

		w.Dispose();
	}
}
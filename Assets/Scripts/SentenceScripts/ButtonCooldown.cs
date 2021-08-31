using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCooldown : MonoBehaviour
{

    private bool cooldown = false;

    private void OnMouseDown()
    {
        if(cooldown == false)
        {
            //do something
            this.GetComponent<Button>().interactable = false;
            Debug.Log("FALSE");
            Invoke("ResetCooldown", 5f);
            cooldown = true;
        }
    }

    void ResetCooldown()
    {
        this.GetComponent<Button>().interactable = true;
        Debug.Log("TRUE");
        cooldown = false;
    }
}

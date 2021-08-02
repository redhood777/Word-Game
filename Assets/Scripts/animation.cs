using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animation : MonoBehaviour
{
    public Button optionBtn;

    public void EaseIn()
    {
        optionBtn.transform.LeanScale(Vector2.zero, 3f).setEaseInBack();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

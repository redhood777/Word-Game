using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCheck : MonoBehaviour
{
    public static RandomCheck instance;


    public  List<int> savenumber = new List<int>();

    public  int number;

    public static int i;

     //var rand = new Random();

    public System.Random rand = new System.Random();



    private void Awake()
    {
        
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {



        for (int i = 0; i < 6; i++)
        {
            do
            {
                number = rand.Next(1, 51);
            } while (savenumber.Contains(number));
            savenumber.Add(number);
        }

      

    }


    //void MoveList()
    //{
    //    for(i =savenumber[49]; i < savenumber.Count; i++)
    //    {

    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}

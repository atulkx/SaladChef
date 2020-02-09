using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer_Order : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] menu;
    private int no,itemNo;
    public List<string> choice = new List<string>();
    void Start()
    {
       menu= new string[] {"A","B","C","D","E","F"};
       no = Random.Range (1, 3);
       for(int i=0;i<=no;i++){
           itemNo=Random.Range (0,5);
           choice.Add(menu[itemNo]);
       }
       
    }

    public List<string> place_order(){
        return choice;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

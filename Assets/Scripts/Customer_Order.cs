using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Customer_Order : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] menu;
    private int no,itemNo;
    public List<string> choice = new List<string>();
    public float timeLeft;
    public Slider slider;
    public Image[] orderPic;
    void Start()
    {
       StartCoroutine(CustomerWaitTime()); 
       menu= new string[] {"A","B","C","D","E","F"};
       
       //Customer Order creation
       no = Random.Range (1, 3);       
       for(int i=0;i<=no;i++){
           itemNo=Random.Range (0,5);
           choice.Add(menu[itemNo]);
           Sprite veggie =  Resources.Load <Sprite>(menu[itemNo]);
           orderPic[i].sprite=veggie;
       }
       
    }
     //Customer wait time
    public IEnumerator CustomerWaitTime(float countdownValue = 30)
    {
        timeLeft = countdownValue;
        while (timeLeft >= 0)
        {
           
            slider.value=timeLeft;
            yield return new WaitForSeconds(1.0f);           
            timeLeft--;
            
        }
        Destroy(gameObject);



    }


    public List<string> place_order(){
        return choice;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

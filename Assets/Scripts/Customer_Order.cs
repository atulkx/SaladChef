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
    public float timeLeft,countdownValue=30f;
    public Slider slider;

    public GameObject genCustomer;
    public Image[] orderPic;
    
    void Start()
    {
       StartCoroutine(CustomerWaitTime(countdownValue)); 
       menu= new string[] {"A","B","C","D","E","F"};
       genCustomer = GameObject.Find("Customer_genObject");
       no = Random.Range (1, 3);
       for(int i=0;i<=no;i++){
           itemNo=Random.Range (0,5);
           choice.Add(menu[itemNo]);
           Sprite veggie =  Resources.Load <Sprite>(menu[itemNo]);
           orderPic[i].sprite=veggie;
       }
       
    }

    public IEnumerator CustomerWaitTime(float countdownValue = 30f)
    {
        timeLeft = countdownValue;
        while (timeLeft >= 0)
        {
           
            slider.value=timeLeft;
            yield return new WaitForSeconds(1.0f);           
            timeLeft--;
            
        }
        Destroy(gameObject);
        int tableno = (int)gameObject.transform.position.x;
        genCustomer.GetComponent<Customer_gen>().CustomerOut(tableno);
        genCustomer.GetComponent<Customer_gen>().Instatiate();


    }


    public List<string> place_order(){
        return choice;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}

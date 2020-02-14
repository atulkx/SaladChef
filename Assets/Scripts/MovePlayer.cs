using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    CharacterController characterController;
    public string[] bag = new string[3];
    public List<string> chopper = new List<string>();
    public List<string> vessel = new List<string>();
    public List<string> veggies = new List<string> { "A", "B", "C", "D", "E", "F" };

    public Image item1, item2;
    public Image[] choppedItems, itemsinChopper;
    public float speed = 100.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    Customer_gen customer_Gen;
    public GameObject canvas, statusText,Customercontrol,choppingAnim;
    public float timeLeft;
    public Text timer, point;

    public int scorePoint = 0, chopperIndex = 0,bagindex=0,fininshIndex=0;
    private Collider detectCollision=null;
    public Animator ChefRun;
    public GameObject[] BonusPoint_prefab;
    public float ypos=555f,zpos1=9.58f,zpos2=-194.4f,xpos1=-205f,xpos2=204.8f; 
    private string scoreKey="PlayerA";
    void Start()
    {
     // Start player time countdown
        fininshIndex=0;
        StartCoroutine(StartCountdown(200));
        bag[0] = "null";
        bag[1] = "null";
        characterController = GetComponent<CharacterController>();
        customer_Gen = new Customer_gen();
        ChefRun=GetComponent<Animator>();
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;
            transform.rotation = Quaternion.LookRotation(moveDirection);
            


        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
        //Customer Interaction

        if (detectCollision!=null && Input.GetButtonUp("Take"))
            {

                if (bag[0] == "null")
                {
                    bag[0] = detectCollision.gameObject.tag;
                    Sprite veggie = Resources.Load<Sprite>(detectCollision.gameObject.tag);
                    item1.sprite = veggie;
                    Debug.Log("took");
                    bag[1] = "null";
                    // status.CreateFloatingText("took", transform);



                }
                else if (bag[1] == "null")
                {
                    Debug.Log("took");
                    // status.CreateFloatingText("took", transform);
                    bag[1] = detectCollision.gameObject.tag;
                    Sprite veggie = Resources.Load<Sprite>(detectCollision.gameObject.tag);
                    item2.sprite = veggie;
                }

                detectCollision=null;
            }

            if(Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")){
                ChefRun.SetBool("IsWalk",true);
            }
            if(Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical")){
               
                ChefRun.SetBool("IsWalk",false);
            }

    }
//Checks if order is served correctly

    private bool CheckSalad(List<string> list1, List<string> list2)
    {
        var areListsEqual = true;

        if (list1.Count != list2.Count)
            return false;

        for (var i = 0; i < list1.Count; i++)
        {
            if (list2[i] != list1[i])
            {
                areListsEqual = false;
            }
        }

        return areListsEqual;
    }

//Player count down

    public IEnumerator StartCountdown(float countdownValue = 5)
    {
        timeLeft = countdownValue;
        while (timeLeft >= 0)
        {
            timer.text = timeLeft.ToString();
            yield return new WaitForSeconds(1.0f);
            timeLeft--;
        }

        speed = 0;
        Debug.Log("Score Saved  "+scorePoint.ToString());
        PlayerPrefs.SetInt(scoreKey,scorePoint);
        PlayerPrefs.Save();
        fininshIndex=1;       

    }
    //Coroutine for speed pickup

    public IEnumerator BonusSpeed()
    {
        speed=200;
        yield return new WaitForSeconds(10f);
        speed=100;
    }
    //Coroutine for Chopping

    public IEnumerator Chopping()
    {
        speed=0;
        choppingAnim.SetActive(true);
        yield return new WaitForSeconds(5f);
        speed=100;
        choppingAnim.SetActive(false);
    }


    void OnTriggerStay(Collider collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject

        //If the GameObject has the same tag as specified, output this message in the console
        if (veggies.Contains(collision.gameObject.tag))
        {
           detectCollision=collision;
        }
        if (collision.gameObject.tag == "ChopperA")
        {
            detectCollision=null;
            if (Input.GetButtonUp("Chop"))
            {
                StartCoroutine(Chopping());
                //    status.CreateFloatingText("Chopping!!!!", transform);
                Debug.Log("Chopping!!!!!!!!!!!!!!");
                foreach (string item in bag)
                {
                    if (item != "null" && chopperIndex<3){
                        chopper.Add(item);
                        Sprite chopperSprite = Resources.Load<Sprite>(item);
                        itemsinChopper[chopperIndex].sprite = chopperSprite;
                        chopperIndex++;
                    }
                }
                foreach (string salad in chopper)
                {
                    Debug.Log(salad);
                }
                Debug.Log("Chopped!!!!!!!!!!!!!!!!");
                bag[0] = "null";
                bag[1] = "null";
                bagindex=0;
                Sprite veggie = Resources.Load<Sprite>("null");
                item1.sprite = veggie;
                item2.sprite = veggie;


            }
            if (Input.GetButtonUp("Take"))
            {
               
                // status.CreateFloatingText("took", transform);
                int i = 0;
                chopperIndex = 0;
                Sprite veggie = Resources.Load<Sprite>("null");
                itemsinChopper[0].sprite = veggie;
                itemsinChopper[1].sprite = veggie;
                itemsinChopper[2].sprite = veggie;
                foreach (string choppeditem in chopper)
                {
                    Sprite choopedItemSprite = Resources.Load<Sprite>(choppeditem);
                    choppedItems[i].sprite = choopedItemSprite;
                    i++;
                }
                Debug.Log("tAKEN oRDER");
                vessel = chopper;
                
            }
        }

        if (collision.gameObject.tag == "Customer")
        {
            if (Input.GetButtonUp("Serve"))
            {
                var order = collision.gameObject;
                var items = order.GetComponent<Customer_Order>().place_order();
                Debug.Log(vessel.Count);

                bool check = CheckSalad(vessel, items);
                if (check)
                {
                    float diff=order.GetComponent<Customer_Order>().countdownValue-order.GetComponent<Customer_Order>().timeLeft;
                    float percentage=(100-((diff/order.GetComponent<Customer_Order>().countdownValue)*100));
                    Debug.Log("Point");
                    Debug.Log("Percentage"+percentage.ToString());
                    scorePoint += 100;
                    if(percentage>=60){
                        Debug.Log("Pickup Spwaned");
                        int objectno=Random.Range(0,3);
                        float x = Random.Range (xpos1, xpos2);
                        float z = Random.Range (zpos1, zpos2);
                        Instantiate(BonusPoint_prefab[objectno],new Vector3(x,ypos,z),Quaternion.identity);
                    }
                    point.text = scorePoint.ToString();
                    //    status.CreateFloatingText("GoodJob", transform);
                    vessel = new List<string>();
                    int tableno = (int)order.transform.position.x;
                    Destroy(order);
                    Sprite veggie = Resources.Load<Sprite>("null");
                    choppedItems[0].sprite = veggie;
                    choppedItems[1].sprite = veggie;
                    choppedItems[2].sprite = veggie;
                    Customercontrol.GetComponent<Customer_gen>().CustomerOut(tableno);
                    StartCoroutine(Customercontrol.GetComponent<Customer_gen>().CustomerDrop());
               
                    chopper = new List<string>();
                }
                else
                {
                    scorePoint -= 50;
                    point.text = scorePoint.ToString();
                    Debug.Log("Wrong order");
                    // status.CreateFloatingText("Bad", transform);
                    chopper = new List<string>();
                }
            }

        }

        if (collision.gameObject.tag == "TrashCan")
        {
            if (Input.GetButtonUp("Serve"))
            {
                bag[0] = "null";
                bag[1] = "null";
                bagindex=0;
                vessel = new List<string>();
                chopper = new List<string>();
                Sprite veggie = Resources.Load<Sprite>("null");
                choppedItems[0].sprite = veggie;
                choppedItems[1].sprite = veggie;
                choppedItems[2].sprite = veggie;
                item1.sprite = veggie;
                item2.sprite = veggie;
            }

        }
        //Bonus point interactions

        if(collision.gameObject.tag=="PointBonusA")
        {
            scorePoint -= 50;
            point.text = scorePoint.ToString();
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag=="SpeedBonusA")
        {
            StartCoroutine(BonusSpeed());
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag=="TimeBonusA")
        {
            timeLeft+=40;
            timer.text = timeLeft.ToString();
            Destroy(collision.gameObject);
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Customer_gen : MonoBehaviour
{
    public GameObject Customer_prefab;
    public int ypos=538,zpos=174;
    public int[] xpos ;
    public List<int> occupency = new List<int>();
    public int customerCount,pos;
    private int lvl=3;
    public GameObject playerA,playerB;

    void Start()
    {
        xpos= new int[] {-187,-27,143};    //{-187, -107, -27, 53,143};
        StartCoroutine(CustomerDrop(5));
        playerA = GameObject.Find("Player1");
        playerB = GameObject.Find("Player2");
    }

     
//Customer generation randomly occuring function
    public IEnumerator CustomerDrop(float time=5){
        Debug.Log("Customer out");
        while(customerCount<lvl){
            yield return new WaitForSeconds(time);
            pos = Random.Range (0, lvl);
            if(occupency.Contains(pos)==false){
                Instantiate(Customer_prefab,new Vector3(xpos[pos],ypos,zpos),Quaternion.identity);
                occupency.Add(pos);     
                customerCount+=1;
            }
        }
    }

//Table no which is empty
    public void CustomerOut(int tableno){
        customerCount-=1; 
        for(int i=0;i<xpos.Length;i++){
            if(xpos[i]==tableno){
                 occupency.Remove(i);
            }
        }
       
       
    }
    
//Start Customer Spawning
    public  void Instatiate(){
         int time = Random.Range (3, 8);
         StartCoroutine(CustomerDrop(time));
         Debug.Log("CustomerOut");
    }

    void Update() {
        {
            if( playerA.GetComponent<MovePlayer>().fininshIndex==1 && playerB.GetComponent<MovePlayer_B>().fininshIndex==1){
                SceneManager.LoadScene("HighScore");
            }
        }
    }
}

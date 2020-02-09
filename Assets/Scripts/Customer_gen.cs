using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer_gen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Customer_prefab;
    public int ypos=524,zpos=243;
    public int[] xpos ;
    List<int> occupency = new List<int>();
    public int customerCount,pos;
    private int lvl=5;
    void Start()
    {
        xpos= new int[] {-187, -107, -27, 53,143};
        StartCoroutine(CustomerDrop());
        
    }

    IEnumerator CustomerDrop(){
        while(customerCount<lvl){
            pos = Random.Range (0, lvl);
            if(occupency.Contains(pos)==false){
                Instantiate(Customer_prefab,new Vector3(xpos[pos],ypos,zpos),Quaternion.identity);
                occupency.Add(pos);
                yield return new WaitForSeconds(5f);
                customerCount+=1;
            }
        }
    }
}

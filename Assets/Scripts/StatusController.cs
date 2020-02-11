using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusController : MonoBehaviour
{
    // Start is called before the first frame update
    
    public  GameObject canvas,statusText;
    void Start()
    {
         Debug.Log("Start");
        canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    public  void CreateFloatingText(string text, Transform location)
    {
       //Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x + Random.Range(-.2f, .2f), location.position.y + Random.Range(-.2f, .2f)));
        Vector3 screenRealpos = new Vector3(-27, 524, 174);
        // Vector3 screenRealpos = screenPosition;
        Instantiate(statusText, screenRealpos, Quaternion.identity);
        statusText.transform.SetParent(canvas.transform, false);
        statusText.transform.position = screenRealpos;
        statusText.GetComponent<UnityEngine.UI.Text>().text = text; 

    }
}

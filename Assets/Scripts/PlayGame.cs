using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
  
    //Change Scene According to the parameter
    public void LoadScene(string Scene){
        SceneManager.LoadScene(Scene);
    }
}

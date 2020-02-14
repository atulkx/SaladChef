using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSaver : MonoBehaviour
{
    public int highScore = 0;
    string highScoreKey = "HighScore";
    public Text[] scoreList;
    public Text ScoreA,ScoreB,winner;
    public int scoreA,scoreB;
    public GameObject[] Players; 
    // Start is called before the first frame update
    void Start()
    {
        
        scoreA=PlayerPrefs.GetInt("PlayerA",0);
        ScoreA.text=scoreA.ToString();
        scoreB=PlayerPrefs.GetInt("PlayerB",0);
        ScoreB.text=scoreB.ToString();
        Debug.Log("PLAYER A "+scoreA.ToString());
        Debug.Log("PLAYER B "+scoreB.ToString());
        if(scoreA>scoreB){
            winner.text="PLAYER A";
        }
        else if(scoreA<scoreB) {
            winner.text="PLAYER B";
        }
        else{
            winner.text="DRAW!!!";
        }
        UpdateList(scoreA);
        UpdateList(scoreB);
        ShowList();
    }

    // Update is called once per frame
    void UpdateList(int point)
    {
        for(int i=1;i<=10;i++){
            int currentScore = PlayerPrefs.GetInt(highScoreKey+i.ToString(),0); 
            if(point>currentScore){
                int temp = highScore;
                PlayerPrefs.SetInt(highScoreKey+i.ToString(),point);
                PlayerPrefs.Save();
                point=temp;
            }
        }
    }

    void ShowList(){
        for(int i =1;i<=10;i++){
            highScore=PlayerPrefs.GetInt(highScoreKey+i.ToString(),0);
            scoreList[i-1].text=highScore.ToString();
            
        }
    }
}

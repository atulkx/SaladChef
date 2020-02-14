using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSaver : MonoBehaviour
{
    public int highScore = 0;
    string highScoreKey = "HighScore";
    public Text[] scoreList;
    public Text ScoreA,ScoreB;
    public int scoreA,scoreB;
    public GameObject[] Players; 
    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.SetInt(highScoreKey+"1",0); 
        // PlayerPrefs.SetInt(highScoreKey+"2",0);   
        // PlayerPrefs.SetInt(highScoreKey+"3",0);   
        // PlayerPrefs.SetInt(highScoreKey+"4",0);   
        // PlayerPrefs.SetInt(highScoreKey+"5",0);   
        // PlayerPrefs.SetInt(highScoreKey+"6",0);   
        // PlayerPrefs.SetInt(highScoreKey+"7",0);   
        // PlayerPrefs.SetInt(highScoreKey+"8",0);   
        // PlayerPrefs.SetInt(highScoreKey+"9",0);   
        // PlayerPrefs.SetInt(highScoreKey+"10",0);   
        // PlayerPrefs.Save();
        scoreA=PlayerPrefs.GetInt("PlayerA",0);
        ScoreA.text=scoreA.ToString();
        scoreB=PlayerPrefs.GetInt("PlayerB",0);
        ScoreB.text=scoreB.ToString();
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

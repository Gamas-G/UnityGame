using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewGameOver : MonoBehaviour
{
    public static ViewGameOver sharedInstance;

    public Text coinsLabel;
    public Text scoreLabel;

    private void Awake()
    {
        sharedInstance = this;
    }

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
    //}

    public void UpdateUI()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.gameOver)
        {
            coinsLabel.text = GameManager.sharedInstance.collectedCoins.ToString();
            scoreLabel.text = PlayerController.sharedInstance.GetDistance().ToString("f0");
        }
    }
}

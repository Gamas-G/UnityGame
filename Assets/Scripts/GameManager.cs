using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum GameState
{
    menu,
    inTheGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    //Porpiedad estatica para compartir una sola instancia(un singleton)
    public static GameManager sharedInstance;
    public GameState currentGameState = GameState.menu;
    public Canvas menuCanvas;
    public Canvas gameCanvas;
    public Canvas gameOverCanvas;

    public int collectedCoins = 0;

    private void Awake()
    {
        //Metemos la clase en la propiedad sharedInstance de tipo static por lo cual no sera necerio instanciar en otras clases, con tansolo llamar la propiedad ya tomamos lo demas.
        sharedInstance = this;
    }
    private void Start()
    {
        //Nos aseguramos que cuando inicie se haga un restart pues que la variable entre en el tipo menu.(Al ser publica y los demas controllers tiene acceso a ella podria generarnos un problema)
        currentGameState = GameState.menu;
        menuCanvas.enabled = true;
        gameCanvas.enabled = false;
        gameOverCanvas.enabled = false;
    }
    private void Update()
    {
        //if (Input.GetButtonDown("s") && (currentGameState != GameState.inTheGame))
            //StartGame();
    }
    /*
     *Nota: Nuestro GamaManager solo se encarga de coordinar
     *los elementos de nuestra pantantalla.
     *Solo le interesa iniciar la partida y finalizarla.
     *
     */
    // Usado para iniciar la partida
    public void StartGame()
    {
        ChangeGameState(GameState.inTheGame);
        LevelGenerator.sharedInstance.GenerateInitialBlocks();
        PlayerController.sharedInstance.StartGame();
        ViewInGame.sharedInstance.UpdateHighScoreLabel();
    }

    //Llamado cuando el jugador muere
    public void GameOver()
    {
        LevelGenerator.sharedInstance.RemoveAllTheBlocks();
        ChangeGameState(GameState.gameOver);
        ViewGameOver.sharedInstance.UpdateUI();

    }
    //Llamado cuando el jugador quiere regresar al menu principal
    public void BackToMainMenu()
    {
        ChangeGameState(GameState.menu);

    }
    void ChangeGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.menu:
                //La escenena de Unity deberá mostrar el menú principal
                menuCanvas.enabled = true;
                gameCanvas.enabled = false;
                gameOverCanvas.enabled = false;
                break;
            case GameState.inTheGame:
                //La escena de Unity debe configurarse para mostrar el juego en si
                menuCanvas.enabled = false;
                gameCanvas.enabled = true;
                gameOverCanvas.enabled = false;
                break;
            case GameState.gameOver:
                //La escena debe mostrar la pantalla de fin de la partida
                menuCanvas.enabled = false;
                gameCanvas.enabled = false;
                gameOverCanvas.enabled = true;
                break;
            default:
                break;
        }
        currentGameState = newGameState;
    }

    public void CollectCoin()
    {
        collectedCoins++;
        ViewInGame.sharedInstance.UpdateCoinsLabel();
    }
}

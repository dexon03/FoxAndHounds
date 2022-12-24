using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Game : MonoBehaviour
{
    public GameObject Piece;

    // Positions and team for each piece
    private GameObject[,] positions = new GameObject[8, 8];

    private GameObject foxPlayer = new GameObject();

    private GameObject[] houndPlayer = new GameObject[4];

    private string currentPlayer;

    private bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        currentPlayer = "Fox";
        foxPlayer = Create("Fox", 4, 0);

        houndPlayer = new GameObject[]
        {
            Create("Hound_1", 1, 7),
            Create("Hound_2", 3, 7),
            Create("Hound_3", 5, 7),
            Create("Hound_4", 7, 7)
        };
        
        // set all piece position on the position board
        for (int i = 0; i < houndPlayer.Length; i++)
        {
            SetPosition(houndPlayer[i]);
        }

        SetPosition(foxPlayer);
    }
    

    private GameObject Create(string name, int x, int y)
    {
        var obj = Instantiate(Piece, new Vector3(0, 0, -1), Quaternion.identity);
        Checker cm = obj.GetComponent<Checker>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        Checker cm = obj.GetComponent<Checker>();
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }
    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if (currentPlayer == "Fox")
        {
            currentPlayer = "Hound";
        }
        else
        {
            currentPlayer = "Fox";
        }
    }

    public void Update()
    {
        if (gameOver && Input.GetMouseButtonDown(0))
        {
            gameOver = false;
            SceneManager.LoadScene("Game");
        }
    }

    public void Winner(string playerWinner)
    {
        gameOver = true;
        GameObject.FindGameObjectWithTag("GameOverText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("GameOverText").GetComponent<Text>().text = "Game over + \n" + playerWinner + "is a winner";
        
        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
    }
}

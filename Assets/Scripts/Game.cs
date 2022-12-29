using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TreeSearch;


public class Game : MonoBehaviour
{
    public GameObject Fox;
    public GameObject Hound;

    // Positions and team for each piece
    private GameObject[,] positions;

    private GameObject foxPlayer;

    private GameObject[] houndPlayer;

    private string currentPlayer;
    
    private bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        // Initialize objects
        positions = new GameObject[8, 8];
        foxPlayer = new GameObject();
        houndPlayer = new GameObject[4];
        gameOver = false;
        currentPlayer = "Fox";
        foxPlayer = CreateFox(4, 0);

        houndPlayer = new GameObject[]
        {
            CreateHound("Hound_1", 1, 7),
            CreateHound("Hound_2", 3, 7),
            CreateHound("Hound_3", 5, 7),
            CreateHound("Hound_4", 7, 7)
        };
        
        // set all piece position on the position board
        for (int i = 0; i < houndPlayer.Length; i++)
        {
            SetPosition(houndPlayer[i]);
        }

        SetPosition(foxPlayer);
    }
    

    private GameObject CreateFox(int x, int y)
    {
        var obj = Instantiate(Fox, new Vector3(0, 0, -1), Quaternion.identity);
        Fox fox = obj.GetComponent<Fox>();
        fox.name = "Fox";
        fox.SetXBoard(x);
        fox.SetYBoard(y);
        fox.Activate();
        return obj;
    }
    
    private GameObject CreateHound(string name, int x, int y)
    {
        var obj = Instantiate(Hound, new Vector3(0, 0, -1), Quaternion.identity);
        Hound hound = obj.GetComponent<Hound>();
        hound.name = name;
        hound.SetXBoard(x);
        hound.SetYBoard(y);
        hound.Activate();
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        if (obj.name == "Fox")
        {
            Fox fox = obj.GetComponent<Fox>();
            positions[fox.GetXBoard(), fox.GetYBoard()] = obj;
        }
        else
        {
            Hound hound = obj.GetComponent<Hound>();
            positions[hound.GetXBoard(), hound.GetYBoard()] = obj;
        }
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
        CheckGameOver();

        if (gameOver && Input.GetMouseButtonDown(0))
        {
            gameOver = false;
            SceneManager.LoadScene("Game");
        }
    }
    
    private void CheckGameOver()
    {
        if (FoxWin())
        {
            Winner("Fox");
        }
        if (HoundWin())
        {
            Winner("Hound");
        }
        
    }

    
    private bool FoxWin()
    {
        if (foxPlayer.GetComponent<Fox>().GetYBoard() == 7 || CheckOrFoxFarThanHound())
        {
            return true;
        }

        return false;
    }
    private bool CheckOrFoxFarThanHound()
    {
        int maxHoundPosition = int.MinValue;
        foreach (var hound in houndPlayer)
        {
            maxHoundPosition = Math.Max(maxHoundPosition, hound.GetComponent<Hound>().GetYBoard());
        }

        if (maxHoundPosition <= foxPlayer.GetComponent<Fox>().GetYBoard())
        {
            return true;
        }

        return false;
    }

    private bool HoundWin()
    {
        var foxX = foxPlayer.GetComponent<Fox>().GetXBoard();
        var foxY = foxPlayer.GetComponent<Fox>().GetYBoard();
        var possibleFoxMoves = FindExistingFoxMove(foxX, foxY);
        
        if (possibleFoxMoves.Count == 0)
        {
            return true;
        }

        return false;
    }

    private List<int[]> FindExistingFoxMove(int x, int y)
    {
        List<int[]> moves = new List<int[]>();
        if(PositionOnBoard(x+1, y+1) && GetPosition(x+1, y+1) == null)
        {
            moves.Add( new []{x+1,y+1});
        }
        if (PositionOnBoard(x+1,y-1) && GetPosition(x+1, y-1) == null)
        {
            moves.Add( new []{x+1,y-1});
        }
        if (PositionOnBoard(x-1,y+1) && GetPosition(x-1,y+1) == null )   
        {
            moves.Add( new []{x-1,y+1});
        }
        if(PositionOnBoard(x-1,y-1) && GetPosition(x-1,y-1) == null)
        {
            moves.Add( new []{x-1,y-1});
        }
        
        return moves;
    }

    

    public void Winner(string playerWinner)
    {
        gameOver = true;
        GameObject.FindGameObjectWithTag("GameOverText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("GameOverText").GetComponent<Text>().text = "Game over.\n" + playerWinner + " is a winner";
        
        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
    }

}

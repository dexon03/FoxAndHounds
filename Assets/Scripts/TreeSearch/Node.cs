using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TreeSearch
{
    public class Node
    {
        public int Depth { get; set; }

        public GameObject[,] Positions { get; set; } = new GameObject[8, 8];

        public List<Node> Children { get; set; } = new List<Node>();

        // 1 - Fox, -1 - Hound
        private int currentPlayer = 1;

        public Node(GameObject[,] positions, int depth, int currentPlayer)
        {
            this.Depth = depth;
            Positions = positions;
            this.currentPlayer = currentPlayer;
        }


        public void CreateChildren()
        {
            if (currentPlayer == 1)
            {
                (int x, int y) = FindFoxPosition();
                GenerateFoxChildren(x,y);
            }
            else
            {
                int[,] houndsPositions = FindHoundPositions();
                for (int i = 0; i < houndsPositions.GetLength(0); i++)
                {
                    GenerateHoundChildren(houndsPositions[i, 0], houndsPositions[i, 1]);
                }
            }
        }


        private void GenerateFoxChildren(int x, int y)
        {
            if (x > 0 && y > 0 && Positions[x - 1, y - 1] == null)
            {
                AddChildren(x,y,x-1,y-1);
            }
            if(x < 7 && y > 0 && Positions[x + 1, y-1] == null)
            {
                AddChildren(x, y, x + 1, y - 1);
            }
            if(x > 0 && y < 7 && Positions[x - 1, y+1] == null)
            {
               AddChildren(x,y,x-1,y+1);
            }
            if(x < 7 && y < 7 && Positions[x + 1, y+1] == null)
            {
                AddChildren(x,y,x+1,y+1);
            }
        }
        

        private void GenerateHoundChildren(int x, int y)
        {
            if(x>0 && y>0 && Positions[x-1,y-1] == null)
            {
                AddChildren(x,y,x-1,y-1);
            }

            if (x< 7 && y > 0 && Positions[x + 1, y - 1] == null)
            {
                AddChildren(x, y, x + 1, y - 1);
            }
        }

        private void AddChildren(int oldX,int oldY, int newX, int newY)
        {
            GameObject[,] newPositions = new GameObject[8, 8];
            Array.Copy(Positions, newPositions, Positions.Length);
            newPositions[newX, newY] = newPositions[oldX, oldY];
            newPositions[oldX, oldY] = null;
            Children.Add(new Node(newPositions, Depth + 1, currentPlayer*-1));
        }

        private (int, int) FindFoxPosition()
        {
            for (int i = 0; i < Positions.GetLength(0); i++)
            {
                for (int j = 0; j < Positions.GetLength(1); j++)
                {
                    if(Positions[i,j] != null && Positions[i,j].name == "Fox")
                    {
                        return (i, j);
                    }
                }
            }
            return (-1, -1);
        }

        private int[,] FindHoundPositions()
        {
            int[,] houndPositions = new int[4, 2];
            string[] houndNames = { "Hound_1", "Hound_2", "Hound_3", "Hound_4" };
            int houndIndex = 0;
            for (int i = 0; i < Positions.GetLength(0); i++)
            {
                for (int j = 0; j < Positions.GetLength(1); j++)
                {
                    if(Positions[i,j] != null &&  houndNames.Contains(Positions[i,j].name))
                    {
                        houndPositions[houndIndex, 0] = i;
                        houndPositions[houndIndex, 1] = j;
                        houndIndex++;
                    }
                }
            }

            return houndPositions;
        }
        
    }
}
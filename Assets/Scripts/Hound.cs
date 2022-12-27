using System;
using Unity.VisualScripting;
using UnityEngine;

public class Hound : MonoBehaviour
{
   // References
   public GameObject controller;
   public GameObject movePlate;
   
   //Positions
   private int xBoard = -1;
   private int yBoard = -1;
   

   
   //References for all the sprites that the piece can be
   public Sprite Hound_1, Hound_2, Hound_3, Hound_4;

   public void Activate()
   {
      controller = GameObject.FindGameObjectWithTag("GameController");
      
      // take the instantiated location and adjust the transform
      SetCoords();
      switch (this.name)
      {
         case "Hound_1": this.GetComponent<SpriteRenderer>().sprite = Hound_1;
            break;
         case "Hound_2" :this.GetComponent<SpriteRenderer>().sprite = Hound_2;
            break;
         case "Hound_3" : this.GetComponent<SpriteRenderer>().sprite = Hound_3;
            break;
         case "Hound_4" : this.GetComponent<SpriteRenderer>().sprite = Hound_4;
            break;
      }

      
   }

   public void SetCoords()
   {
      float x = xBoard;
      float y = yBoard;

      x *= 0.66f;
      y *= 0.66f;

      x += -2.3f;
      y += -2.3f;

      this.transform.position = new Vector3(x, y, -1.0f);
   }

   public int GetXBoard()
   {
      return xBoard;
   }
   
   public int GetYBoard()
   {
      return yBoard;
   }
   public void SetXBoard(int x)
   {
      xBoard = x;
   }
   public void SetYBoard(int y)
   {
      yBoard = y;
   }
   

   private void OnMouseUp()
   {
      DestroyMovePlates();
      InitiateMovePlates();
   }
   public void DestroyMovePlates()
   {
      GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
      for (int i = 0; i < movePlates.Length; i++)
      {
         Destroy(movePlates[i]);
      }
   }

   public void InitiateMovePlates()
   {
      LineMovePlate(1,-1);
      LineMovePlate(-1,-1);
   }

   private void LineMovePlate(int xIncrement, int yIncrement)
   {
      Game sc = controller.GetComponent<Game>();
      
      int x = xBoard + xIncrement;
      int y = yBoard + yIncrement;

      if (sc.PositionOnBoard(x,y) && sc.GetPosition(x,y) == null)
      {
         MovePlateSpawn(x, y);
      }
   }

   public void MovePlateSpawn(int matrixX, int matrixY)
   {
      float x = matrixX;
      float y = matrixY;

      x *= 0.66f;
      y *= 0.66f;

      x += -2.3f;
      y += -2.3f;
      GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

      MovePlate mpScript = mp.GetComponent<MovePlate>();
      
      mpScript.SetReference(gameObject);
      mpScript.SetCoords(matrixX,matrixY);
   }
}

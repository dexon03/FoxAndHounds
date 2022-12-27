using Unity.VisualScripting;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;
    private GameObject reference = null;
    private int matrixX;
    private int matrixY;
    

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        if (reference.name == "Fox")
        {
            controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Fox>().GetXBoard(),reference.GetComponent<Fox>().GetYBoard());
            reference.GetComponent<Fox>().SetXBoard(matrixX);
            reference.GetComponent<Fox>().SetYBoard(matrixY);
            reference.GetComponent<Fox>().SetCoords();
        
            controller.GetComponent<Game>().SetPosition(reference);
        
            controller.GetComponent<Game>().NextTurn();
            reference.GetComponent<Fox>().DestroyMovePlates();
        }
        else
        {
            controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Hound>().GetXBoard(),reference.GetComponent<Hound>().GetYBoard());
            reference.GetComponent<Hound>().SetXBoard(matrixX);
            reference.GetComponent<Hound>().SetYBoard(matrixY);
            reference.GetComponent<Hound>().SetCoords();
            
            controller.GetComponent<Game>().SetPosition(reference);
            
            controller.GetComponent<Game>().NextTurn();
            reference.GetComponent<Hound>().DestroyMovePlates();
            
        }
        
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
    
}

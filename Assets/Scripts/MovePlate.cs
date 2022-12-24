using Unity.VisualScripting;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;
    private GameObject reference = null;
    private int matrixX;
    private int matrixY;
    public bool catched = false;
    

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        
        
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Checker>().GetXBoard(),reference.GetComponent<Checker>().GetYBoard());
        reference.GetComponent<Checker>().SetXBoard(matrixX);
        reference.GetComponent<Checker>().SetYBoard(matrixY);
        reference.GetComponent<Checker>().SetCoords();
        
        controller.GetComponent<Game>().SetPosition(reference);
        
        if (reference.GetComponent<Checker>().name == "Fox" && reference.GetComponent<Checker>().GetYBoard() == 7)
        {
            controller.GetComponent<Game>().Winner("Fox");
        }
        controller.GetComponent<Game>().NextTurn();
        reference.GetComponent<Checker>().DestroyMovePlates();
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

    public bool FoxCaught()
    {
        var checker = reference.GetComponent<Checker>();
        if (checker.name == "Fox")
        {
            int xCoord = reference.GetComponent<Checker>().GetXBoard();
            int yCoord = reference.GetComponent<Checker>().GetYBoard();
            if (NotEmptyPlate(xCoord + 1, yCoord + 1) && NotEmptyPlate(xCoord - 1, yCoord - 1) &&
                NotEmptyPlate(xCoord + 1, yCoord - 1) && NotEmptyPlate(xCoord - 1, yCoord + 1))
            {
                return true;
            }
        }
        return false;

    }

    public bool NotEmptyPlate(int x, int y)
    {
        if (!controller.GetComponent<Game>().PositionOnBoard(x, y) ||
            controller.GetComponent<Game>().GetPosition(x, y) != null)
        {
            return true;
        }

        return false;
    }
}

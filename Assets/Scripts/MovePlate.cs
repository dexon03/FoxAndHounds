using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;
    private GameObject reference = null;
    private int matrixX;
    private int matrixY;
    // false:movement, true: attacking

    public void Start()
    {
        
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Checker>().GetXBoard(),reference.GetComponent<Checker>().GetYBoard());
        reference.GetComponent<Checker>().SetXBoard(matrixX);
        reference.GetComponent<Checker>().SetYBoard(matrixY);
        reference.GetComponent<Checker>().SetCoords();
        
        controller.GetComponent<Game>().SetPosition(reference);
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
}

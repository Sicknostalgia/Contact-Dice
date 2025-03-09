using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public GameObject hexPrefab;
    public int rows = 3;
    public int columns = 4;
    public float hexWidth = 100f;
    public float hexHeight = 115f;

    private void Start()
    {
        GenerateHexGrid();
    }

    void GenerateHexGrid()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int col = 0; col < columns; col++)
            {
                float xPos = col * hexWidth * 0.75f;
                float yPos = i * hexHeight;

                if (col % 2 == 1)  //if the column is odd the y position is decreased by 
                    yPos -= hexHeight * 0.5f;
            }
        }
    }
}

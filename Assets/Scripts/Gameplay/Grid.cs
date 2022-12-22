using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public enum CellType { PLAYER, WALKABLE, TRAP, REWARD, NONE}

    [SerializeField] List<Cell> cellsPoints = new List<Cell>();
    [SerializeField] List<GameObject> cellFloor = new List<GameObject>();

    public int GetCellCount()
    {
        return cellsPoints.Count;
    }

    public CellType GetCellStatus(int index)
    {
        return cellsPoints[index].cellType;
    }

    public Vector3 GetCellPosition(int index)
    {
        return cellsPoints[index].transform.position;
    }

    public void HideCellFloor(int index)
    {
        cellFloor[index].SetActive(false);
    }

    public bool IsCellAvailable(int index)
    {
        return (cellsPoints[index].cellType == CellType.NONE || cellsPoints[index].cellType == CellType.WALKABLE) && !cellsPoints[index].havePlayer;
    }

    public bool IsCellEmpty(int index)
    {
        return cellsPoints[index].cellType == CellType.NONE && !cellsPoints[index].havePlayer;
    }

    public void SetCellType(int index, CellType type)
    {
        cellsPoints[index].cellType = type;
    }

    public int GetRandomSpawnableCellIndex()
    {
        int randomIndex = Random.Range(1, cellsPoints.Count);

        while (!IsCellAvailable(randomIndex))
        {
            randomIndex += Random.Range(3, 6);
            if (randomIndex > cellsPoints.Count) randomIndex = randomIndex - cellsPoints.Count;
        }
        return randomIndex;
    }

    public int GetRandomEmptyCellIndex()
    {
        int randomIndex = Random.Range(1, cellsPoints.Count);

        while (!IsCellEmpty(randomIndex))
        {
            randomIndex += Random.Range(3, 6);
            if (randomIndex > cellsPoints.Count) randomIndex = randomIndex - cellsPoints.Count;
        }
        return randomIndex;
    }
}

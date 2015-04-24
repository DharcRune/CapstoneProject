using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour
{
	public int seedValue;
	public MazeCell[] cellPrefabs;
	public IntVector2 size;
	public MazePassage passagePrefab;
	public MazeWall wallPrefab;
	private MazeCell[,] cells;

	public MazeCell GetCell(IntVector2 coordinates)
	{
		return cells[coordinates.x, coordinates.z];
	}

	public void Generate()
	{
		cells = new MazeCell[size.x, size.z];

		List<MazeCell> activeCells = new List<MazeCell>();
		DoFirstGenerationStep(activeCells);

		while(activeCells.Count > 0)
		{
			DoNextGenerationStep(activeCells);
		}
	}

	private void DoFirstGenerationStep(List<MazeCell> activeCells)
	{
		activeCells.Add(CreateCell(RandomCoordinates));
	}

	private void DoNextGenerationStep(List<MazeCell> activeCells)
	{
		int currentIndex = activeCells.Count - 1;
		MazeCell currentCell = activeCells[currentIndex];

		if(currentCell.IsFullyInitialized)
		{
			activeCells.RemoveAt(currentIndex);
			return;
		}

		MazeDirection direction = currentCell.RandomUninitializedDirection;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();

		if(ContainsCoordinates(coordinates))
		{
			MazeCell neighbor = GetCell(coordinates);

			if(neighbor == null)
			{
				neighbor = CreateCell(coordinates);
				CreatePassage(currentCell, neighbor, direction);
				activeCells.Add(neighbor);
			}
			else
			{
				CreateWall(currentCell, neighbor, direction);
			}
		}
		else
		{
			CreateWall(currentCell, null, direction);
		}
	}

	private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
	{
		MazePassage passage = Instantiate (passagePrefab) as MazePassage;
		passage.Initialize(cell, otherCell, direction);

		passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(otherCell, cell, direction.GetOpposite());
	}

	private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
	{
		MazeWall wall = Instantiate (wallPrefab) as MazeWall;
		wall.Initialize(cell, otherCell, direction);

		if(otherCell != null)
		{
			wall = Instantiate(wallPrefab) as MazeWall;
			wall.Initialize(otherCell, cell, direction.GetOpposite());
		}
	}

	private MazeCell CreateCell(IntVector2 coordinates)
	{
		MazeCell newCell = Instantiate(cellPrefabs[Random.Range(0, cellPrefabs.Length)]) as MazeCell;
		cells [coordinates.x, coordinates.z] = newCell;

		newCell.coordinates = coordinates;
		newCell.name = "MazeCell " + coordinates.x + ", " + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3 (coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);

		return newCell;
	}

	public IntVector2 RandomCoordinates
	{
		get 
		{ 
			Random.seed = seedValue;
			int xRange = Random.Range(0, size.x);
			int zRange = Random.Range(0, size.z);
	
			return new IntVector2(xRange, zRange); 
		}
	}

	public bool ContainsCoordinates(IntVector2 coordinate)
	{
		return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
	}
}

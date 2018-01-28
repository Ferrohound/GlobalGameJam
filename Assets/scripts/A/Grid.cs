using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
	
	public Vector2 gridSize;
	public float nodeRadius;
	float nodeDiameter;
	public LayerMask unwalkableMask;
	public Node[,] grid;
	
	int resolutionX, resolutionY;
	
	public int MaxSize()
	{
		return resolutionX * resolutionY;
	}
	
	// Use this for initialization
	void Awake () {
		Debug.Log("Initializing Grid");
		nodeDiameter = nodeRadius * 2;
		resolutionX = Mathf.RoundToInt(gridSize.x/nodeDiameter);
		resolutionY = Mathf.RoundToInt(gridSize.y/nodeDiameter);
		
		CreateGrid();
	}
	
	void CreateGrid()
	{
		grid = new Node[resolutionX, resolutionY];
		Vector3 bottomLeftGrid = transform.position - Vector3.right * gridSize.x/2
			- Vector3.forward * gridSize.y/2;
		
		for(int x = 0; x < resolutionX; x++)
		{
			for(int y = 0; y < resolutionY; y++)
			{
				Vector3 worldPoint = bottomLeftGrid + Vector3.right * (x * nodeDiameter
					+ nodeRadius) + Vector3.forward * ( y * nodeDiameter + nodeRadius);
				
				bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
				grid[x,y] = new Node( walkable, worldPoint, x, y);
			}
		}
		
		Debug.Log("done creating grid.");
	}
	
	public bool InMapRange(int x, int y)
	{
		return x >= 0 && x < resolutionX && y >= 0 && y < resolutionY;
	}
	
	public Node WorldtoNode(Vector3 position)
	{
		//center is 0
		float pX = (position.x + gridSize.x/2)/gridSize.x;
		float pY = (position.z + gridSize.y/2)/gridSize.y;
		
		pX = Mathf.Clamp01(pX);
		pY = Mathf.Clamp01(pY);
		
		int x = Mathf.RoundToInt((resolutionX - 1) * pX);
		int y = Mathf.RoundToInt((resolutionY - 1) * pY);
		
		return grid[x,y];
	}
	
	public List<Node> GetNeighbors(Node n)
	{
		List<Node> neighbors = new List<Node>();
		
		for(int x = -1; x<=1; x++)
		{
			for(int y = -1; y<=1; y++)
			{
				if(x == 0 && y == 0)
					continue;
				
				int checkX = n.x + x;
				int checkY = n.y + y;
				
				if(InMapRange(checkX, checkY))
				{
					neighbors.Add(grid[checkX,checkY]);
				}
			}
		}
		
		return neighbors;
	}
	
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(gridSize.x, 1, gridSize.y));
		
		if(grid!=null)
		{
			foreach( Node n in grid)
			{
				Gizmos.color = (n.walkable) ? Color.white : Color.red;
				Gizmos.DrawCube(n.position, Vector3.one * (nodeDiameter - 0.1f));
			}
		}
		
	}
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class pathfinding : MonoBehaviour {
	
	//PathManager manager;
	
	public Grid grid;
	//public Transform seeker, target;
	
	//Node startNode, targetNode;

	void Awake()
	{
		//grid = GetComponent<Grid>();
		//manager = GetComponent<PathManager>();
	}

	
	// Update is called once per frame
	/*void Update () {
		if(Input.GetButtonDown("Jump"))
		{
			print("Updating Pathfinding");
			FindPath(seeker.position, target.position);
		}
	}*/
	
	/*IEnumerator FindPath(Vector3 start, Vector3 target)*/
	public void FindPath(PathRequest request, Action<PathResult> callback)
	{
		Vector3[] waypoints = new Vector3[0];
		bool result = false;
		
		Stopwatch sw = new Stopwatch();
		sw.Start();
		
		//Node startNode = grid.WorldtoNode(start);
		//Node targetNode = grid.WorldtoNode(target);
		Node startNode = grid.WorldtoNode(request.start);
		Node targetNode = grid.WorldtoNode(request.end);
		
		if(!startNode.walkable || !targetNode.walkable)
		{
			//manager.Finished(waypoints, result);
			callback(new PathResult(waypoints, result, request.callback));
		}
		
		Heap<Node> open = new Heap<Node>(grid.MaxSize());
		HashSet<Node> closed = new HashSet<Node>();
		
		open.Add(startNode);
		
		
		while(open.Count > 0)
		{
			Node current = open.Pop();
			
			closed.Add(current);
			
			if(current == targetNode)
			{
				sw.Stop();
				result = true;
				print("Path found in " + sw.ElapsedMilliseconds + " millis");
				break;
			}
			
			foreach(Node neighbor in grid.GetNeighbors(current))
			{
				if(!neighbor.walkable || closed.Contains(neighbor))
					continue;
				
				int newG = current.g + GetDistance(current, neighbor) + neighbor.penalty;
				if(newG < neighbor.g || !open.Contains(neighbor))
				{
					neighbor.g = newG;
					neighbor.h = GetDistance(neighbor, targetNode);
					neighbor.parent = current;
					
					if(!open.Contains(neighbor))
						open.Add(neighbor);
					else
						open.UpdateItem(neighbor);
				}
			}
		}
		//yield return null;
		
		if(result)
		{
			waypoints = RetracePath(startNode, targetNode);
			result = waypoints.Length > 0;
		}
		
		//manager.Finished(waypoints, result);
		callback(new PathResult(waypoints, result, request.callback));
	}
	
	Vector3[] RetracePath(Node start, Node end)
	{
		List<Node> path = new List<Node>();
		
		Node current = end;
		
		while(current!=start)
		{
			path.Add(current);
			current = current.parent;
		}
		
		Vector3[] waypoints = SimplifyPath(path);
		Array.Reverse(waypoints);
		return waypoints;
		//grid.path = path;
		
	}
	
	Vector3[] SimplifyPath(List<Node> path)
	{
		List<Vector3> waypoints = new List<Vector3>();
		Vector2 directionOld = Vector2.zero;
		
		for(int i = 1;  i < path.Count; i++)
		{
			Vector2 directionNew = new Vector2(path[i-1].x - path[i].x,
				path[i-1].y - path[i].y);
				
			if(directionNew!=directionOld)
			{
				waypoints.Add(path[i].position);
			}
			directionOld = directionNew;
		}
		
		return waypoints.ToArray();
	}
	
	int GetDistance(Node A, Node B)
	{
		int distX = Mathf.Abs(A.x - B.x);
		int distY = Mathf.Abs(A.y - B.y);
		
		if(distX > distY)
			return 14 * distY + 10*(distX - distY);
		else 
			return 14 * distX + 10*(distY - distX);
	}
	
	void OnDrawGizmos()
	{	
		if(grid.grid!=null)
		{
			//Gizmos.color = Color.blue;
			//Gizmos.DrawCube(targetNode.position, Vector3.one * (1 - 0.1f));
		}
		
	}
}

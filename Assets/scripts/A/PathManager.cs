using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class PathManager : MonoBehaviour {

	
	static PathManager instance;
	Queue<PathResult> results = new Queue<PathResult>();
	
	pathfinding p;
	
	void Awake()
	{
		instance = this;
		p = GetComponent<pathfinding>();
	}
	
	public static void RequestPath(PathRequest request)
	{
		ThreadStart thread = delegate 
		{ 
			instance.p.FindPath(request, instance.Finished);
		};
		
		thread.Invoke();
	}
	
	
	
	/*void TryProcessNext()
	{
		if(!processing && q.Count>0)
		{
			current = q.Dequeue();
			processing = true;
			instance.p.StartFindPath(current.start, current.end);
		}
	}*/
	
	public void Finished(PathResult result)
	{
		lock(results)
		{
			results.Enqueue(result);
		}
	}
	
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(results.Count > 0)
		{
			int c = results.Count;
			lock(results)
			{
				for(int i = 0; i < c; i++)
				{
					PathResult result = results.Dequeue();
					result.callback(result.path, result.success);
				}
			}
		}
	}
}

public struct PathRequest
	{
		public Vector3 start;
		public Vector3 end;
		public Action<Vector3[], bool> callback;
		
		public PathRequest(Vector3 s, Vector3 e, Action<Vector3[], bool> c)
		{
			start = s;
			end = e;
			callback = c;
		}
	}

	public struct PathResult
	{
		public Vector3[] path;
		public bool success;
		public Action<Vector3[], bool> callback;
		
		public PathResult(Vector3[] p, bool s, Action<Vector3[], bool> c)
		{
			path = p;
			success = s;
			callback = c;
		}
	}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {
	
	public Transform target;
	public float speed = 0.5f;
	public float turnD = 5f;
	public float turnSpeed = 3.0f;
	public float stoppingD = 10f;
	
	const float TargetMoveThres = 0.5f;
	const float MinPathUpdateTime = 0.2f;
	
	//public Vector3[] path;
	//int targetIndex;
	
	Path path;

	// Use this for initialization
	void Start () {
		//PathManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
		StartCoroutine("UpdatePath");
	}
	
	public void OnPathFound(Vector3[] waypoints, bool result)
	{
		if(result)
		{
			path = new Path(waypoints, transform.position, turnD, stoppingD);
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}
	
	IEnumerator FollowPath()
	{
		bool following = true;
		int pathIndex = 0;
		transform.LookAt(path.lookPoints[pathIndex]);
		//Vector3 current = path[0];
		
		float speedPercent = 1f;
		
		while(following)
		{
		/*	if(transform.position == current)
			{
				Debug.Log("Made it to the point! going to the next, boss!");
				targetIndex++;
				if(targetIndex>=path.Length)
				{
					yield break;
				}
				else
				{
					current = path[targetIndex];
				}
			}
			
			transform.position = Vector3.MoveTowards(transform.position, current, 
				Time.deltaTime * speed);
			transform.LookAt(new Vector3(current.x, transform.position.y, current.z));
			*/
			
			Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
			while(path.turnBoundaries[pathIndex].HasCrossed(pos2D))
			{
				if(pathIndex == path.finishLineIndex)
				{
					following = false;
					break;
				}
				else
				{
					pathIndex++;
				}
				
			}
			
			if(following)
			{
				if(pathIndex >= path.slowDownIndex && stoppingD > 0)
				{
					speedPercent = 
						Mathf.Clamp01(
						path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D)/stoppingD
						);
						
					if(speedPercent < 0.01)
						following = false;	
				}
				
				Quaternion target = 
					Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
					
				transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * turnSpeed);
				transform.Translate(
					Vector3.forward * Time.deltaTime * speed * speedPercent, 
					Space.Self);
			}
			yield return null;
		}
	}
	
	IEnumerator UpdatePath()
	{
		
		if(Time.timeSinceLevelLoad < 0.5f)
		{
			yield return new WaitForSeconds(0.5f);
		}
		
		PathManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
		
		float squareMoveT = TargetMoveThres * TargetMoveThres;
		Vector3 prevPos = target.position;
		
		while(true)
		{
			yield return new WaitForSeconds(MinPathUpdateTime);
			if((target.position - prevPos).sqrMagnitude > squareMoveT)
			{
				PathManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
				prevPos = target.position;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void OnDrawGizmos()
	{
		if(path!=null)
		{
			/*for(int i = targetIndex; i < path.Length; i++)
			{
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);
				
				if(i == targetIndex)
				{
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else
				{
					Gizmos.DrawLine(path[i-1], path[i]);
				}
			}*/
			path.DrawWithGizmos();
		}
	}
}

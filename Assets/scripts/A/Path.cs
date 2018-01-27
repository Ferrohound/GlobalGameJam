using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path {

	public readonly Vector3[] lookPoints;
	public readonly Line[] turnBoundaries;
	public readonly int finishLineIndex;
	public readonly int slowDownIndex;
	
	public Path(Vector3[] waypoints, Vector3 startpos, float turnDst, float stopD)
	{
		lookPoints = waypoints;
		turnBoundaries = new Line[lookPoints.Length];
		finishLineIndex = turnBoundaries.Length-1;
		
		Vector2 prev = V3toV2(startpos);
		
		for(int i = 0; i <lookPoints.Length; i++)
		{
			Vector2 current = V3toV2(lookPoints[i]);
			Vector2 dirToCurrent = (current - prev).normalized;
			Vector2 turnBoundaryPoint = (i == finishLineIndex) ? current : current - dirToCurrent * turnDst;
			turnBoundaries[i] = new Line(turnBoundaryPoint, prev - dirToCurrent*turnDst);
			prev = turnBoundaryPoint;
		}
		
		float dFromEnd = 0;
		for(int i = lookPoints.Length - 1; i > 0; i--)
		{
			dFromEnd+=Vector3.Distance(lookPoints[i], lookPoints[i-1]);
			if(dFromEnd > stopD)
			{
				slowDownIndex = i;
				break;
			}
		}
		
	}
	
	Vector2 V3toV2(Vector3 v)
	{
		return new Vector2(v.x, v.z);
	}
	
	public void DrawWithGizmos()
	{
		Gizmos.color = Color.black;
		foreach(Vector3 p in lookPoints)
		{
			Gizmos.DrawCube(p + Vector3.up, Vector3.one);
		}
		
		Gizmos.color = Color.blue;
		foreach(Line l in turnBoundaries)
		{
			l.DrawWithGizmos(10);
		}
	}
}

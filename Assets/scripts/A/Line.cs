using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Line {
	
	const float VerticalLineGradient = 1e5f;
	
	float gradient;
	float yIntercept;
	
	Vector2 p1;
	Vector2 p2;
	
	float pGradient;
	
	bool approachSide;
	
	public Line(Vector2 point, Vector2 Perpendicular)
	{
		float dx = point.x - Perpendicular.x;
		float dy = point.y - Perpendicular.y;
		
		pGradient = (dx==0) ? VerticalLineGradient:(dy/dx);
		
		//gradient of a line multiplied by a line perpendicular to it = -1
		gradient = (pGradient == 0) ? VerticalLineGradient : (-1/pGradient);
		
		//y = mx+c
		//c = y - mx
		yIntercept = point.y - gradient * point.x;
		
		p1 = point;
		p2 = p1 + new Vector2(1, gradient);
		
		approachSide = false;
		approachSide = GetSide(Perpendicular);
	}
	
	//return true if the point is one one side of ---|---
	public bool GetSide(Vector2 p)
	{
		return (p.x - p1.x) * (p2.y - p1.y) > (p.y - p1.y) * (p2.x -p1.x);
	}
	
	public bool HasCrossed(Vector2 p)
	{
		return GetSide(p) != approachSide;
	}
	
	public float DistanceFromPoint(Vector2 p)
	{
		float yIntPerp = p.y - pGradient * p.x;
		float xIntersect = (yIntPerp - yIntercept)/(gradient - pGradient);
		float yIntersect = gradient * xIntersect + yIntercept;
		
		return Vector2.Distance(p, new Vector2(xIntersect, yIntersect));
	}
	
	public void DrawWithGizmos(float length)
	{
		Vector3 LineD = new Vector3(1, 0, gradient).normalized;
		Vector3 LineCentre = new Vector3(p1.x, 0, p1.y) + Vector3.up;
		Gizmos.DrawLine(LineCentre - LineD * length/2, LineCentre + LineD * length/2);
	}
}

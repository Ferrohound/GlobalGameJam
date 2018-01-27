using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node : IHeapItem<Node> {

	public bool walkable;
	public Vector3 position;
	public int g, h, f, x, y;
	public Node parent;
	//movement penalty
	public int penalty;
	int index;
	
	public Node(bool _walkable, Vector3 pos, int _x, int _y)
	{
		walkable = _walkable;
		position = pos;
		x = _x;
		y = _y;
		penalty = 0;
	}
	
	public int fCost
	{
		get
		{
			return g+h;
		}
	}
	
	public int HeapIndex
	{
		get
		{
			return index;
		}
		set
		{
			index = value;
		}
	}
	
	public int CompareTo(Node other)
	{
		int compare = fCost.CompareTo(other.fCost);
		if(compare == 0)
		{
			compare = h.CompareTo(other.h);
		}
		
		return -compare;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Heap<T> where T : IHeapItem<T> {
	
	T[] items;
	int count;
	
	public Heap(int maxHeapSize)
	{
		items = new T[maxHeapSize];
		count = 0;
	}
	
	public void Add(T item)
	{
		item.HeapIndex = count;
		items[count] = item;
		PercolateUp(item);
		count++;
	}
	
	public T Pop()
	{
		T head = items[0];
		count--;
		items[0] = items[count];
		items[0].HeapIndex = 0;
		PercolateDown(items[0]);
		return head;
	}
	
	void PercolateDown(T item)
	{
		while(true)
		{
			int left = item.HeapIndex * 2 + 1;
			int right = item.HeapIndex * 2 + 2;
			int swap;
			
			if(left < count)
			{
				swap = left;
				
				if(right < count)
				{
					if(items[left].CompareTo(items[right]) < 0)
					{
						swap = right;
					}
				}
				
				if(item.CompareTo(items[swap]) < 0)
				{
					Swap(item, items[swap]);
				}
				else
					return;
			}
			
			else
				return;
		}
	}
	
	public void UpdateItem(T item)
	{
		PercolateUp(item);
	}
	
	public int Count
	{
		get
		{
			return count;
		}
	}
	
	public bool Contains(T item)
	{
		return Equals(items[item.HeapIndex], item);
	}
	
	void PercolateUp(T item)
	{
		int parentIndex = (item.HeapIndex-1)/2;
		
		while(true)
		{
			T parent = items[parentIndex];
			
			//higher priority returns 1, same returns 0, lower returns -1
			if(item.CompareTo(parent) > 0)
			{
				Swap(item, parent);
			}
			else
				break;
			
			parentIndex = (item.HeapIndex-1)/2;
		}
	}
	
	void Swap(T A, T B)
	{
		items[A.HeapIndex] = B;
		items[B.HeapIndex] = A;
		
		int tmp = A.HeapIndex;
		A.HeapIndex = B.HeapIndex;
		B.HeapIndex = tmp;
	}

}

public interface IHeapItem<T> : IComparable<T>
{
	int HeapIndex
	{
		get;
		set;
	}
}

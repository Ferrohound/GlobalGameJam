using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {
	
	public Transform target;
	public float radius = 1;
	public float speed = 1;
	public float heightAngle = 90;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		float y = radius * Mathf.Cos(heightAngle);
		float x = radius * Mathf.Cos(Time.time * speed) * 
			Mathf.Sin(heightAngle);
		float z = radius * Mathf.Sin(Time.time * speed) * 
			Mathf.Sin(heightAngle);
			
		Vector3 newPos = new Vector3(x, y, z);
		transform.position = newPos + target.position;
		transform.LookAt(target);
	}
}

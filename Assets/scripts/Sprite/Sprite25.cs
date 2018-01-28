using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode()]
public class Sprite25 : MonoBehaviour {
	
	Material mat;
	Camera cam;
	
	public Texture front;
	public Texture side;
	public Texture back;
	
	Texture current;
	
	void Awake()
	{
		mat = GetComponent<Renderer>().material;
		cam = Camera.main;
		current = front;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		float DP = Vector3.Dot(cam.transform.forward, transform.forward);
		if(DP <= -0.5f && current!=front)
		{
			mat.SetTexture("_MainTex", front);
			current = front;
		}
		
		if(DP < 0.5f && DP > -0.5f && current!=side)
		{
			mat.SetTexture("_MainTex", side);
			current = side;
		}
		
		if(DP >= 0.5f && current!=back)
		{
			mat.SetTexture("_MainTex", back);
			current = back;
		}
	}
	
	void OnDrawGizmos()
	{
		/*Gizmos.color = Color.red;
		Gizmos.DrawRay(cam.transform.position, cam.transform.forward);
		
		Gizmos.color = Color.blue;
		Gizmos.DrawRay(transform.position, transform.forward);*/
	}
}

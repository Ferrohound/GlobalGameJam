using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmit : MonoBehaviour {
	
	public Camera cam;
	Ray ray;
	public LayerMask posessable;

	// Use this for initialization
	void Start () 
	{
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Gizmos.color = Color.red;
		//Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
		ray = new Ray(cam.transform.position, cam.transform.forward);
		RaycastHit hit;
		//Gizmos.DrawRay(cam.transform.position, ray.direction);
		//Debug.Log("Direction of ray: " + ray.direction);
		
		 if (Physics.Raycast(ray, out hit))
		 {
			 Debug.Log("Hit!");
            
			if(hit.transform.gameObject.tag == "possessable")
			{
				Debug.Log("Looks like a target!");
			}
			else
			{
				Debug.Log(hit.transform.tag);
			}
		 }
        /*else
            print("I'm looking at nothing!");*/
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		if(cam!=null)
			Gizmos.DrawRay(cam.transform.position, ray.direction * 100);
	}
}

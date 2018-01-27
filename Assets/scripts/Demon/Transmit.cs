using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Transmit : MonoBehaviour {
	
	public Camera cam;
	Ray ray;
	Ray reflectedRay;
	RaycastHit target;
	public LayerMask posessable;
	
	public int maxReflections = 2;
	public float maxStepDistance = 50;

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
		
		//Debug.DrawLine(cam.transform.position, cam.transform.forward, Color.red);
		//Gizmos.DrawRay(cam.transform.position, ray.direction);
		//Debug.Log("Direction of ray: " + ray.direction);
		//do this when mouse is pressed
		if(Input.GetMouseButton/*Down*/(0))
		{
			//don't even really need any of this, I guess...
			//just shoot yourself in the direction of the ray
			
			if(ReflectRays(cam.transform.position, cam.transform.forward, maxReflections))
			{
				if(transform.parent!=null)
				{
					//fix this
					transform.parent.gameObject.layer = LayerMask.NameToLayer("possessable");
				}
				transform.parent = target.transform;
				transform.localPosition = Vector3.up;
				target.transform.gameObject.layer = (1<<1);
			}
			
			 /*if (Physics.Raycast(ray, out hit))
			 {
				 //Debug.Log("Hit!");
				
				if(hit.transform.gameObject.tag == "possessable")
				{
					Debug.Log("Looks like a target!");
					
				}
				else
				{
					reflectedRay = new Ray(hit.point, 
						Vector3.Reflect(ray.direction, hit.normal));
						
					//Debug.DrawLine(hit.point, reflectedRay.direction, Color.blue);
					Debug.DrawLine(hit.point, hit.normal, Color.green);
				}
			 }*/
		}
        /*else
            print("I'm looking at nothing!");*/
	}
	
	bool ReflectRays(Vector3 position, Vector3 direction, int remaining)
	{
		if(remaining == 0)
			return false;
		
		Vector3 original = position;
		Ray r = new Ray(position, direction);
		RaycastHit hit;
		
		if (Physics.Raycast(r, out hit, maxStepDistance))
        {
			if(hit.transform.gameObject.tag == "possessable")
			{
				//transform.parent = hit.transform;
				//transform.localPosition = Vector3.up;
				target = hit;
				return true;
			}
			
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
        }
		
		else
        {
            position += direction * maxStepDistance;
        }
		
		Debug.DrawLine(original, position, Color.green);
		
		return ReflectRays(position, direction, remaining-1);
		
	}
	
	/*Vector3 ReflectRay(Vector3 originalDirection, Vector3 Hitpoint, Vector3 normal)
	{
		return (originalDirection - 2 * (Vector3.Dot(originalDirection, normal.normalized)) * normal);
	}*/	
	
	void OnDrawGizmos()
	{
		/*Handles.color = Color.red;
		Handles.ArrowHandleCap(0, this.transform.position + this.transform.forward * 0.25f, this.transform.rotation, 0.5f, EventType.Repaint);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(this.transform.position, 0.25f);
		
		Gizmos.color = Color.red;
		if(cam!=null)
			Gizmos.DrawRay(cam.transform.position, ray.direction * 100);*/
	}
}

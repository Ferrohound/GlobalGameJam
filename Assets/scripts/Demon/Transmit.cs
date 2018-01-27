using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Transmit : MonoBehaviour {
	
	public Camera cam;
    private LineRenderer lr;
	Ray ray;
	Ray reflectedRay;
	RaycastHit target;
	public LayerMask posessable;
    public Transform wheretoshootfrom;
    public GameObject pp;
	public int maxReflections = 2;
	public float maxStepDistance = 50;
    private List<Vector3> bounces;
    private bool haveshoot = false;
    private GameObject pproj;
    // Use this for initialization
    void Start () 
	{
		cam = Camera.main;
        
        
	}
	
	// Update is called once per frame
	void Update () 
	{
        lr = GetComponent<LineRenderer>();
        bounces = new List<Vector3>();
        //Gizmos.color = Color.red;
        //Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        ray = new Ray(cam.transform.position, cam.transform.forward);
		RaycastHit hit;
        bool temp = ReflectRays(cam.transform.position, cam.transform.forward, maxReflections);
        //Debug.DrawLine(cam.transform.position, cam.transform.forward, Color.red);
        //Gizmos.DrawRay(cam.transform.position, ray.direction);
        //Debug.Log("Direction of ray: " + ray.direction);
        //do this when mouse is pressed
        if (Input.GetMouseButton/*Down*/(0) && !haveshoot)
		{
            //don't even really need any of this, I guess...
            //just shoot yourself in the direction of the ray
            haveshoot = true;
            
            pproj = Instantiate(pp, wheretoshootfrom.position, wheretoshootfrom.rotation);
            pproj.GetComponent<bounce2point>().pointstofollow = new List<Vector3>(bounces);
            
            
			
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
        if ( pproj!=null &&pproj.GetComponent<bounce2point>().gottem == true)
        {
            if (transform.parent != null)
            {
                //fix this
                transform.parent.gameObject.layer = LayerMask.NameToLayer("possessable");

            }
            Debug.Log("Hitsdsdsdwfes!");
            
            transform.parent = pproj.GetComponent<bounce2point>().target.transform;
            transform.parent.gameObject.layer= LayerMask.NameToLayer("unpossessable");
            transform.localPosition = Vector3.up;
            
            
            haveshoot = false;
            Destroy(pproj);
            pproj = null;
        }
        else if (pproj != null && pproj.GetComponent<bounce2point>().gottem != true &&  pproj.GetComponent<bounce2point>().end == true)
        {
            Debug.Log("Hitsdsdsdwsdsdsdsdsdsfes!");
            haveshoot = false;
            Destroy(pproj);
            pproj = null;
        }
        /*else
            print("I'm looking at nothing!");
         if (bounces.Count>=2)
           Debug.Log(bounces[0] + " " + bounces[1]);
            */
        lr.positionCount = bounces.Count + 1;
        lr.SetPosition(0, transform.position);
        for (int i = 1; i <= bounces.Count; i++)
            lr.SetPosition(i, bounces[i-1]);


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
                bounces.Add(hit.transform.position);
                return true;
			}
			
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
            bounces.Add(position);
        }
		
		else
        {
            position += direction * maxStepDistance;
            bounces.Add(position);
        }
        

        //Debug.DrawLine(original, position, Color.green);
		
		return ReflectRays(position, direction, remaining-1);
		
	}
	
	/*Vector3 ReflectRay(Vector3 originalDirection, Vector3 Hitpoint, Vector3 normal)
	{
		return (originalDirection - 2 * (Vector3.Dot(originalDirection, normal.normalized)) * normal);
	}*/	
	
	void OnDrawGizmos()
	{
		Handles.color = Color.red;
		Handles.ArrowHandleCap(0, this.transform.position + this.transform.forward * 0.25f, this.transform.rotation, 0.5f, EventType.Repaint);
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(this.transform.position, 0.25f);
		
		Gizmos.color = Color.red;
		if(cam!=null)
			Gizmos.DrawRay(cam.transform.position, ray.direction * 100);
	}
}

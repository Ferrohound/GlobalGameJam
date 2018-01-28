using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoint : MonoBehaviour {

    // Use this for initialization
    private Unit un;
    public List<Transform> waypoints;
    int i = 0;
	void Start () {
        un = GetComponent<Unit>();
        if (waypoints.Count != 0) un.target = waypoints[i];
	}
	
	// Update is called once per frame
	void Update () {
        if (waypoints.Count == 0) return;
        if (Vector3.Distance(waypoints[i].position,transform.position)<=3f)
            i++;
        if (i >= waypoints.Count)
        {
            i = 0;
        }
        if (waypoints.Count != 0) un.target = waypoints[i];
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounce2point : MonoBehaviour {

    // Use this for initialization
    public float speed = 10f;
    public List<Vector3> pointstofollow;
    private int i=0;
    public bool end = false;
    public bool gottem = false;
    public GameObject target;
	void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        if (i >= pointstofollow.Count) { end = true; return; }
        float step = speed * Time.deltaTime;
        Vector3 target = pointstofollow[i];
        transform.position = Vector3.MoveTowards(transform.position, target , step);
        if (Vector3.Distance(transform.position,pointstofollow[i]) <= 0.5f)
        {
            i++;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //this will be add later
        if (other.gameObject.layer == LayerMask.NameToLayer("possessable")) { gottem = true; target = other.gameObject; }
        
    }
}

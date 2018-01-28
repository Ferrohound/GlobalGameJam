using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {
	public float timer = 2;

	// Use this for initialization
	void Start () {
		Destroy(this, timer);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortal : MonoBehaviour {
	
	public GameObject aura;
	public float timer = 5;
	float elapsed;
	bool p;

	// Use this for initialization
	void Start () {
		aura.SetActive(false);
		p = false;
		elapsed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.layer == LayerMask.NameToLayer("possessed"))
		{
			aura.SetActive(true);
			SelfDestruct(timer);
			p = true;
		}
		else
		{
			aura.SetActive(false);
			if(p)
			{
				SelfDestruct(timer - elapsed);
			}
		}
		
		if(p)
			elapsed+=Time.deltaTime;
	}
	
	//self-destruct in a given period of time
	//instantiate something?
	void SelfDestruct(float time)
	{
		Destroy(this, time);
	}
}

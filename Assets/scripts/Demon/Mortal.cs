using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortal : MonoBehaviour {
	
	public GameObject aura;
	public Transmit Player;
	public GameObject deathAnim;
	public float timer = 5;
	float elapsed;
	public bool p;

	// Use this for initialization
	void Start () {
		Player = GameObject.Find("Demon").GetComponent<Transmit>();
		aura.SetActive(false);
		p = false;
		elapsed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.layer == LayerMask.NameToLayer("unpossessable"))
		{
			aura.SetActive(true);
			p = true;
		}
			
		/*if(p && gameObject.layer != LayerMask.NameToLayer("unpossessable"))
		{
			StartCoroutine("SelfDestruct", (timer - elapsed));
		}*/
		if (p && elapsed < timer)
		{
			elapsed+=Time.deltaTime;
		}
		else if (p && elapsed > timer)
		{
			//show damage and reset timer
			if(Player.host == this.transform)
			{
				Destroy(Player.gameObject);
				elapsed = 0;
			}
			Instantiate(deathAnim, transform.position, transform.rotation);
			Destroy(this.gameObject);
		}
	}
	
	//self-destruct in a given period of time
	//instantiate something?
	/*IEnumerator SelfDestruct(float time)
	{
		/*Debug.Log("Death pending");
		while (true)
        {
            yield return new WaitForSeconds(time);
        }
		
		Debug.Log("Goodbye...");
		Instantiate(deathAnim, transform);
		yield return null;
		Destroy(this.gameObject);
	}*/
}

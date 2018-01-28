using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NPCGlow))]
public class Mortal : MonoBehaviour {
	
	//camera shake
	CameraShake shake;
	
	public GameObject aura;
	public Transmit Player;
	public GameObject deathAnim;
	public float timer = 5;
	float elapsed;
	public bool p;
	float radius = 3;
	Vector3 original;
	
	[Range(0,3)]
	public int state = 0;
	NPCGlow glow;

	// Use this for initialization
	void Start () {
		shake = Camera.main.GetComponent<CameraShake>();
		
		if(aura == null)
		{
			aura = (GameObject)Instantiate(Resources.Load("Aura"));
		}
		
		if(deathAnim == null)
		{
			deathAnim = (GameObject)Instantiate(Resources.Load("Death"));
		}
		
		original = transform.position;
		Player = GameObject.Find("Demon").GetComponent<Transmit>();
		glow = GetComponent<NPCGlow>();
		switch(state)
		{
			case 0:
				glow.GlowColor = Color.red;
			break;
			
			case 1:
				glow.GlowColor = Color.yellow;
			break;
			
			case 2:
				glow.GlowColor = Color.green;
			break;
			
			case 3:
				glow.GlowColor = Color.blue;
			break;
		}
		aura.SetActive(false);
		p = false;
		elapsed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(state == 3)
		{
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			
			Vector3 movement = Player.transform.forward * moveVertical + 
				Player.transform.right * moveHorizontal;
				
			if(Vector3.Distance(original, transform.position + (movement * Time.deltaTime * 3)) < 
				radius)
				transform.Translate(movement * Time.deltaTime * 3);
		}
		
		if(gameObject.layer == LayerMask.NameToLayer("unpossessable") && !p)
		{
			shake.ShakeCamera(3/(2*state+1), timer);
			aura.SetActive(true);
			p = true;
		}

		if (p && elapsed < timer)
		{
			elapsed+=Time.deltaTime;
		}
		else if (p && elapsed > timer)
		{
			
			if(state != 3)
				state--;
			
			if(state < 0)
			{
				Instantiate(deathAnim, transform.position, transform.rotation);
				
				//player still on you, game over
				if(gameObject.layer == LayerMask.NameToLayer("unpossessable"))
				{
					Player.transform.position = new Vector3(-7.22f, 2.594f, 20.49f);
					Player.host = null;
					SceneManager.LoadScene("Level01");
				}
				else
				{
					Destroy(this.gameObject);
				}
			}
			
			//show damage and reset timer
			switch(state)
			{
				case 0:
					glow.GlowColor = Color.red;
				break;
				
				case 1:
					glow.GlowColor = Color.yellow;
				break;
				
				case 2:
					glow.GlowColor = Color.green;
				break;
				
				case 3:
					glow.GlowColor = Color.blue;
				break;
			}
			
			p = false;
			elapsed = 0;
			
			//ideally fade out
			if(gameObject.layer == LayerMask.NameToLayer("possessable"))
				aura.SetActive(false);
		}
		
		if(gameObject.layer == LayerMask.NameToLayer("possessable") && p)
		{
			state--;
			if(state < 0)
			{
				Instantiate(deathAnim, transform.position, transform.rotation);
				Destroy(this.gameObject);
			}
			
			switch(state)
			{
				case 0:
					glow.GlowColor = Color.red;
				break;
				
				case 1:
					glow.GlowColor = Color.yellow;
				break;
				
				case 2:
					glow.GlowColor = Color.green;
				break;
				
				case 3:
					glow.GlowColor = Color.blue;
				break;
			}
			
			p = false;
			elapsed = 0;
			shake.shakeDuration = 0;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUnloader : MonoBehaviour {

	public string SceneName;

	void OnTriggerEnter(Collider col)
	{
		if(col.tag != "Player")
			return;
		
		SceneManager.UnloadSceneAsync(SceneName);
		Destroy(this);
	}
	
}

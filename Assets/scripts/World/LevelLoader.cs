using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
	
	public string SceneName;
	public GameObject player;
	bool used = false;

	void OnTriggerEnter(Collider col)
	{
		if(!used)
		{
			if(col.tag != "Player")
				return;
			
			used = true;
			
			player = col.transform.gameObject;
			
			StartCoroutine("LoadAsyncScene");
		}
		
	}

	IEnumerator LoadAsyncScene()
    {
		// The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the Scene by build //number.
        Scene sceneToLoad = SceneManager.GetSceneByName(SceneName);
		
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive);
		
		//SceneManager.MoveGameObjectToScene
		
		while (!asyncLoad.isDone)
        {
            yield return null;
        }
		
		//SceneManager.MoveGameObjectToScene(player, sceneToLoad);
		
		// get the scene we just loaded into the background
        //Scene newScene = SceneManager.GetSceneByName(SceneName);
 
         // move the gameobject from scene A to scene B
         //SceneManager.MoveGameObjectToScene(player, newScene);
		 
		 Destroy(this);
		
	}
}

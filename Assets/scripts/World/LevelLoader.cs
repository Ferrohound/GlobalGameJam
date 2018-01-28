using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
	
	public string SceneName;
	public GameObject player;

	void OnTriggerEnter(Collider col)
	{
		if(col.tag != "Player")
			return;
		
		player = col.transform.gameObject;
		
		StartCoroutine("LoadAsyncScene");
		
	}

	IEnumerator LoadAsyncScene()
    {
		// The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the Scene by build //number.
        Scene sceneToLoad = SceneManager.GetSceneByName(SceneName);
		
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName);
		SceneManager.MoveGameObjectToScene(player, sceneToLoad);
		
		//SceneManager.MoveGameObjectToScene
		
		while (!asyncLoad.isDone)
        {
            yield return null;
        }
		
	}
}

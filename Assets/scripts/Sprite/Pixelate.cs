using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()]
public class Pixelate : MonoBehaviour {
	
	private Material _pixelMat;
	[Range(1, 200)]
	public float xRes; 
	[Range(1, 200)]
	public float yRes;
	
	void OnEnable()
	{
		_pixelMat = new Material(Shader.Find("Hidden/Pixelate"));
	}
	
	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		_pixelMat.SetFloat("_Columns", xRes);
		_pixelMat.SetFloat("_Rows", yRes);
		Graphics.Blit(src, dst, _pixelMat);
	}
}

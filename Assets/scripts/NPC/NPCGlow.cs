﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGlow : MonoBehaviour {

	public Color GlowColor;
	public float LerpFactor = 10;
	public float range = 15;
	
	public Transform player;

	public Renderer[] Renderers
	{
		get;
		private set;
	}

	public Color CurrentColor
	{
		get { return _currentColor; }
	}

	private List<Material> _materials = new List<Material>();
	private Color _currentColor;
	private Color _targetColor;

	void Start()
	{
		player = GameObject.Find("Player").transform;
		
		Renderers = GetComponentsInChildren<Renderer>();

		foreach (var renderer in Renderers)
		{	
			_materials.AddRange(renderer.materials);
		}
			
		if(Vector3.Distance(transform.position, player.position)<3.5f)
		{
			_targetColor = Color.clear;
		}
	}

	private void OnMouseEnter()
	{
		if(Vector3.Distance(transform.position, player.position)<3.5f)
		{
			_targetColor = GlowColor;
			enabled = true;
		}
	}
	
	private void OnMouseOver()
	{
		if(Vector3.Distance(transform.position, player.position)<3.5f)
		{
			_targetColor = GlowColor;
			enabled = true;
		}
		else
		{
			_targetColor = Color.clear;
		}
	}

	private void OnMouseExit()
	{
		_targetColor = Color.clear;
		enabled = true;
	}
	
/*	void OnTriggerEnter(Collider col)
	{
		if(col.tag!="Player")
		{
			Debug.Log("Not you!");
			return;
		}
		
		Debug.Log("Glowing");
		_targetColor = GlowColor;
		enabled = true;
		
	}*/
	
	void OnTriggerStay(Collider col)
	{
		//Debug.Log("Tag is " + col.tag);
		/*if(col.tag!="Player")
		{
			//D/ebug.Log("Not you!");
			return;
		}
		
		Debug.Log("Glowing");
		_targetColor = GlowColor;
		enabled = true;
		*/
	}
	
	void OnTriggerExit(Collider col)
	{
		/*if(col.tag!="Player")
			return;
		
		_targetColor = Color.clear;*/
		
	}

	/// <summary>
	/// Loop over all cached materials and update their color, disable self if we reach our target color.
	/// </summary>
	private void Update()
	{
		if(Vector3.Distance(transform.position, player.position) < range)
		{
			Debug.Log("In range");
			_targetColor = GlowColor;
		}
		else
		{
			_targetColor = Color.clear;
		}
	
		_currentColor = Color.Lerp(_currentColor, _targetColor, Time.deltaTime * LerpFactor);
			//_targetColor;

		for (int i = 0; i < _materials.Count; i++)
		{
			_materials[i].SetColor("_OutlineColor", _currentColor);
		}

		//if (_currentColor.Equals(_targetColor))
		//{
			//enabled = false;
		//}
	}
}

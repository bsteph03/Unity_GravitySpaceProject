using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class shaderEffectCamera : MonoBehaviour {

	[SerializeField]public Material mat;

	void OnRenderImage(RenderTexture src, RenderTexture dest)//Catch Image before being displayed
	{
		Graphics.Blit (src, dest, mat);
	}
}
//https://www.youtube.com/watch?v=C0uJ4sZelio
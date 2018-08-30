using System;
using ForestOfChaosLib.Attributes;
using UnityEngine;

[CreateAssetMenu]
public class ShapeSettings: ScriptableObject
{
	[NoFoldout] public NoiseLayer[] NoiseLayers;
	public             float        PlanetRadius = 1;

	[Serializable]
	public class NoiseLayer
	{
		public             bool          Enabled = true;
		[NoFoldout] public NoiseSettings NoiseSettings;
		public             bool          UseFirstLayerAsMask;
	}
}

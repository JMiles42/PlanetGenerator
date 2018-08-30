using System;
using UnityEngine;

[Serializable]
public class NoiseSettings
{
	public               float   BaseRoughness = 1;
	public               Vector3 Centre;
	public               float   MinValue;
	[Range(1, 8)] public int     NumLayers   = 1;
	public               float   Persistence = .5f;
	public               float   Roughness   = 2;
	public               float   Strength    = 1;
}

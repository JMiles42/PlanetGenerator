using UnityEngine;

public class NoiseFilter
{
	private readonly Noise         noise = new Noise();
	private readonly NoiseSettings settings;

	public NoiseFilter(NoiseSettings settings)
	{
		this.settings = settings;
	}

	public float Evaluate(Vector3 point)
	{
		float noiseValue = 0;
		var   frequency  = settings.BaseRoughness;
		float amplitude  = 1;

		for(var i = 0; i < settings.NumLayers; i++)
		{
			var v = noise.Evaluate((point * frequency) + settings.Centre);
			noiseValue += (v + 1) * .5f * amplitude;
			frequency  *= settings.Roughness;
			amplitude  *= settings.Persistence;
		}

		noiseValue = Mathf.Max(0, noiseValue - settings.MinValue);

		return noiseValue * settings.Strength;
	}
}

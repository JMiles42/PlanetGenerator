using UnityEngine;

public class ShapeGenerator
{
	private readonly NoiseFilter[] noiseFilters;
	private readonly ShapeSettings settings;

	public ShapeGenerator(ShapeSettings settings)
	{
		this.settings = settings;
		noiseFilters  = new NoiseFilter[settings.NoiseLayers.Length];

		for(var i = 0; i < noiseFilters.Length; i++)
			noiseFilters[i] = new NoiseFilter(settings.NoiseLayers[i].NoiseSettings);
	}

	public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
	{
		float firstLayerValue = 0;
		float elevation       = 0;

		if(noiseFilters.Length > 0)
		{
			firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);

			if(settings.NoiseLayers[0].Enabled)
				elevation = firstLayerValue;
		}

		for(var i = 1; i < noiseFilters.Length; i++)
		{
			if(settings.NoiseLayers[i].Enabled)
			{
				var mask = settings.NoiseLayers[i].UseFirstLayerAsMask? firstLayerValue : 1;
				elevation += noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;
			}
		}

		return pointOnUnitSphere * settings.PlanetRadius * (1 + elevation);
	}
}

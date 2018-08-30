﻿using UnityEngine;

public class TerrainFace
{
	private readonly Vector3        axisA;
	private readonly Vector3        axisB;
	private readonly Vector3        localUp;
	private readonly Mesh           mesh;
	private readonly int            resolution;
	private readonly ShapeGenerator shapeGenerator;

	public TerrainFace(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp)
	{
		this.shapeGenerator = shapeGenerator;
		this.mesh           = mesh;
		this.resolution     = resolution;
		this.localUp        = localUp;
		axisA               = new Vector3(localUp.y, localUp.z, localUp.x);
		axisB               = Vector3.Cross(localUp, axisA);
	}

	public void ConstructMesh()
	{
		var vertices  = new Vector3[resolution                      * resolution];
		var triangles = new int[(resolution - 1) * (resolution - 1) * 6];
		var triIndex  = 0;

		for(var y = 0; y < resolution; y++)
		{
			for(var x = 0; x < resolution; x++)
			{
				var i                 = x + (y * resolution);
				var percent           = new Vector2(x, y) / (resolution - 1);
				var pointOnUnitCube   = localUp + ((percent.x - .5f) * 2 * axisA) + ((percent.y - .5f) * 2 * axisB);
				var pointOnUnitSphere = pointOnUnitCube.normalized;
				vertices[i] = shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere);

				if((x != (resolution - 1)) && (y != (resolution - 1)))
				{
					triangles[triIndex]     =  i;
					triangles[triIndex + 1] =  i + resolution + 1;
					triangles[triIndex + 2] =  i              + resolution;
					triangles[triIndex + 3] =  i;
					triangles[triIndex + 4] =  i              + 1;
					triangles[triIndex + 5] =  i + resolution + 1;
					triIndex                += 6;
				}
			}
		}

		mesh.Clear();
		mesh.vertices  = vertices;
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
	}
}

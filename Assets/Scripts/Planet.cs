using ForestOfChaosLib.Attributes;
using UnityEngine;

public class Planet: MonoBehaviour
{
	private const                              int            CUBE_FACE_COUNT = 6;
	public                                     bool           AutoUpdate      = true;
	[ShowAsComponent]                  public  ColourSettings ColourSettings;
	[HideInInspector]                  public  bool           ColourSettingsFoldout;
	[SerializeField] [HideInInspector] private MeshFilter[]   MeshFilters;
	[Range(2, 256)]                    public  int            Resolution = 10;
	private                                    ShapeGenerator ShapeGenerator;
	[ShowAsComponent] public                   ShapeSettings  ShapeSettings;
	[HideInInspector] public                   bool           ShapeSettingsFoldout;
	private                                    TerrainFace[]  TerrainFaces;

	private void Initialize()
	{
		ShapeGenerator = new ShapeGenerator(ShapeSettings);

		if((MeshFilters == null) || (MeshFilters.Length == 0))
			MeshFilters = new MeshFilter[CUBE_FACE_COUNT];

		TerrainFaces = new TerrainFace[CUBE_FACE_COUNT];
		Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

		for(var i = 0; i < CUBE_FACE_COUNT; i++)
		{
			if(MeshFilters[i] == null)
			{
				var meshObj = new GameObject("mesh");
				meshObj.transform.parent                            = transform;
				meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
				MeshFilters[i]                                      = meshObj.AddComponent<MeshFilter>();
				MeshFilters[i].sharedMesh                           = new Mesh();
			}

			TerrainFaces[i] = new TerrainFace(ShapeGenerator, MeshFilters[i].sharedMesh, Resolution, directions[i]);
		}
	}

	[ContextMenu("Generate Planet")]
	public void GeneratePlanet()
	{
		Initialize();
		GenerateMesh();
		GenerateColours();
	}

	public void OnShapeSettingsUpdated()
	{
		if(AutoUpdate)
		{
			Initialize();
			GenerateMesh();
		}
	}

	public void OnColourSettingsUpdated()
	{
		if(AutoUpdate)
		{
			Initialize();
			GenerateColours();
		}
	}

	private void GenerateMesh()
	{
		foreach(var face in TerrainFaces)
			face.ConstructMesh();
	}

	private void GenerateColours()
	{
		foreach(var m in MeshFilters)
			m.GetComponent<MeshRenderer>().sharedMaterial.color = ColourSettings.PlanetColour;
	}
}

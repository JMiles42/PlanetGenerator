using System;
using ForestOfChaosLib.Editor;
using UnityEditor;
using Object = UnityEngine.Object;

//[CustomEditor(typeof(Planet))]
public class PlanetEditor: FoCsEditor<Planet>
{
	private         Editor colourEditor;
	private         Editor shapeEditor;
	public override bool   ShowContextMenuButtons => false;

	protected override void DoExtraDraw()
	{
		DrawContextMenuButtons();
		DrawSettingsEditor(Target.ShapeSettings,  Target.OnShapeSettingsUpdated,  ref Target.ShapeSettingsFoldout,  ref shapeEditor);
		DrawSettingsEditor(Target.ColourSettings, Target.OnColourSettingsUpdated, ref Target.ColourSettingsFoldout, ref colourEditor);
	}

	//public override void OnInspectorGUI()
	//{
	//	using (var check = Disposables.ChangeCheck())
	//	{
	//		base.OnInspectorGUI();
	//		if (check.changed) Target.GeneratePlanet();
	//	}
	//
	//	//if (FoCsGUI.Layout.Button("Generate Planet")) Target.GeneratePlanet();
	//}

	private void DrawSettingsEditor(Object settings, Action onSettingsUpdated, ref bool foldout, ref Editor editor)
	{
		if(settings != null)
		{
			foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);

			using(Disposables.IndentZeroed())
			{
				using(var check = Disposables.ChangeCheck())
				{
					if(foldout)
					{
						CreateCachedEditor(settings, null, ref editor);
						editor.OnInspectorGUI();

						if(check.changed)
							onSettingsUpdated?.Invoke();
					}
				}
			}
		}
	}
}

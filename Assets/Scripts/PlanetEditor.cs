using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Planet))]
public class PlanetEditor : Editor
{
    Planet planet;
    Editor shapeEditor;
    Editor colourEditor;
    private void OnEnable()
    {
        planet = (Planet)target;
    }

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if (check.changed)
            {
                planet.generatePlanet();
            }
        }
        if (GUILayout.Button("Generate Planet"))
        {
            planet.generatePlanet();
        }

        drawSettingsEditor(planet.GetShapeSettings(),planet.onShapeUpdated,ref shapeEditor);
        drawSettingsEditor(planet.GetColourSettings(),planet.onColoursUpdated,ref colourEditor);


    }

    void drawSettingsEditor(Object settings,System.Action onSettingsUpdated,ref Editor editor)
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            EditorGUILayout.InspectorTitlebar(true, settings);
            CreateCachedEditor(settings,null,ref editor);
            editor.OnInspectorGUI();

            if (check.changed)
            {
                if(onSettingsUpdated != null)
                {
                    onSettingsUpdated();
                }
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class GlobalWind : MonoBehaviour
{
    [SerializeField]
    [Range(0, 3)]
    private float speed = 1;
    private float prevSpeed = 1;

    [SerializeField]
    [Range(0, 3)]
    private float scale = 1;
    private float prevScale = 1;

    [SerializeField]
    [Range(0, 3)]
    private float strength = 1;
    private float prevStrength = 1;


    [SerializeField]
    private Material[] materials = new Material[0];


    void Start()
    {
        UpdateMaterials();
    }

    
    void Update()
    {
        if (CheckVariables())
        {
            UpdateMaterials();
        }
    }

    void UpdateMaterials()
    {
        if (materials.Length > 0)
        {
            foreach (Material m in materials)
            {
                if (m == null)
                {
                    Debug.LogWarning("[Global Wind] No material assigned");
                    break;
                }

                if (m.shader.name == "Shader Graphs/S_Grass" || m.shader.name == "Shader Graphs/S_Vegetation" || m.shader.name == "Shader Graphs/S_VegetationAdvanced")
                {
                    m.SetFloat("GlobalWind_Strength", strength);
                    m.SetFloat("GlobalWind_Scale", scale);
                    m.SetFloat("GlobalWind_Speed", speed);
                }
                else
                {
                    Debug.LogError("[Global Wind] " + m.name + "'s shader: " + m.shader.name + " is not compatible with Global Wind");
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning("[Global Wind] Material list is empty");
        }

        prevStrength = strength;
        prevScale = scale;
        prevSpeed = speed;
    }

    bool CheckVariables()
    {
        if (strength < prevStrength - .01f || strength > prevStrength + .01f)
            return true;

        if (scale < prevScale - .01f || scale > prevScale + .01f)
            return true;

        if (speed < prevSpeed - .01f || speed > prevSpeed + .01f)
            return true;


        return false;
    }
}

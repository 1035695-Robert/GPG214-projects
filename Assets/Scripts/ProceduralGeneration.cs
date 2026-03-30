using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;


[Serializable]
public class ProceduralPrefabs
{
    public GameObject prefab;
    public int numberOfPrefabsInstances;
}

public class ProceduralGeneration : MonoBehaviour
{
    public List<ProceduralPrefabs> prefabs = new List<ProceduralPrefabs>();
    public Vector3 genrationAreaSize = new Vector3(100f, 0f, 100f);

    public Transform parentContainer;

    public float absoluteGroundLevel = 0f;

    public float size = 2f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (parentContainer == null)
        {
            parentContainer = transform.root;
        }
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, absoluteGroundLevel, gameObject.transform.position.z);

        Generate();
    }
    void Generate()
    {
        foreach (var item in prefabs)
        {
            for (int i = 0; i < item.numberOfPrefabsInstances; i++)
            {
                Vector3 randomPosition = GetRandomPositionInGenerationArea();

                Quaternion RandomRotation = GetRandomRotation();

                Instantiate(item.prefab, randomPosition, RandomRotation, parentContainer.transform);
            }
        }
    }
        Vector3 GetRandomPositionInGenerationArea()
        {

            float x = Mathf.RoundToInt(UnityEngine.Random.Range(-genrationAreaSize.x / 2, genrationAreaSize.x / 2) / size) * size;
            float z = Mathf.RoundToInt(UnityEngine.Random.Range(-genrationAreaSize.z / 2, genrationAreaSize.z / 2) / size) * size;

            Vector3 randomPosition = new Vector3(x, 0f, z);

            return transform.position + randomPosition;
        }
        Quaternion GetRandomRotation()
        {
            float rotationY = UnityEngine.Random.Range(0, 4) * 90f;
            return Quaternion.Euler(0f, rotationY, 0f);
        }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, genrationAreaSize);
    }
}

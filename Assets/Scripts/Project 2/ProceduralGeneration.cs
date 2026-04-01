using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;


//[Serializable]
//public class ProceduralPrefabs
//{
//    public GameObject prefab;
//    public int numberOfPrefabsInstances;
//}

public class ProceduralGeneration : MonoBehaviour
{
    //public List<ProceduralPrefabs> prefabs = new List<ProceduralPrefabs>();

    public GameObject beltPrefab;
    public int numberOfBeltPrefabs;

    public GameObject boxLoaderPrefab;
    public int numberOfBoxLoaderPrefabs;

    public Vector3 genrationAreaSize = new Vector3(100f, 0f, 100f);

    public Transform parentContainer;

    public float absoluteGroundLevel = 0f;

    public float size = 2f;

    List<GameObject> beltList = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (parentContainer == null)
        {
            parentContainer = transform.root;
        }
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, absoluteGroundLevel, gameObject.transform.position.z);

        GenerateBeltPlacement();
    }
    void GenerateBeltPlacement()
    {


        for (int i = 0; i < numberOfBeltPrefabs; i++)
        {
            Vector3 randomPosition = GetRandomPositionInGenerationArea();

            Quaternion RandomRotation = GetRandomRotation();

            GameObject belt = Instantiate(beltPrefab, randomPosition, RandomRotation, parentContainer.transform);
            beltList.Add(belt);
        }
        BoxLoaderPlacement();


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

    void BoxLoaderPlacement()
    {
        for (int i = 0; i < numberOfBoxLoaderPrefabs; i++)
        {
            GameObject[] beltArray = beltList.ToArray();
            int randomIndex = UnityEngine.Random.Range(0, beltArray.Length);
            GameObject selectedBelt = beltArray[randomIndex];
            

            GameObject newBoxLoader = Instantiate(boxLoaderPrefab, 
                new Vector3(
                selectedBelt.transform.position.x - 2,
                2,
                selectedBelt.transform.position.z),
                Quaternion.identity);


            newBoxLoader.transform.LookAt(new Vector3(
                    selectedBelt.transform.position.x,
                    2,
                    selectedBelt.transform.position.z));
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, genrationAreaSize);
    }
}
//place boxloader next to and facing
//face towards belt

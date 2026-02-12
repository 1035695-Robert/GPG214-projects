using UnityEngine;

public class BoxLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string prefabPath = "Prefabs/SmallBox";

        GameObject prefab = Resources.Load<GameObject>(prefabPath); 

        if (prefab != null )
        {
            Instantiate(prefab, new Vector3(0,3,0), Quaternion.identity);
        }
        else
        {
            Debug.LogError("Fail to load prefab path" + prefabPath);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

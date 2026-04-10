using UnityEngine;

public class BeltPlacer : MonoBehaviour
{

    public GameObject conveyorBelt;
    [SerializeField] private float size = 2;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.name == "Void")
                    PlaceCubeNear(hitInfo.point);

            }
        }
    }

    public void PlaceCubeNear(Vector3 point)
    {
        var finalposition = CalculateSnappedPosition(point);
        GameObject newBelt = Instantiate(conveyorBelt, finalposition, Quaternion.identity);
        newBelt.name = conveyorBelt.name;
        EventManager.ItemTextureLoad.Invoke(newBelt);

    }

    public Vector3 CalculateSnappedPosition(Vector3 position)
    {
        position -= transform.position;

        float x = Mathf.RoundToInt(position.x / size) * size;
        float z = Mathf.RoundToInt(position.z / size) * size;


        Vector3 result = new Vector3(x, 0.5f, z);

        result += transform.position;

        return result;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (float x = 0; x < 40; x += size)
        {
            for (float z = 0; z < 40; z += size)
            {
                var point = CalculateSnappedPosition(new Vector3(x, 0f, z));
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
}


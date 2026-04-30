using UnityEngine;
using UnityEngine.UIElements;

public class BeltPlacer : MonoBehaviour
{

    public GameObject conveyorBelt;
    public GameObject markers;
    [SerializeField] private float size = 2;
    public CameraMovement cameraRotation;

   
    private void Start()
    {
        markers = Resources.Load<GameObject>("marker");
        for (float x = 0; x < 32; x += size)
        {
            for (float z = 0; z < 32; z += size)
            {
                var point = CalculateSnappedPosition(new Vector3(x, 0f, z));
                Instantiate(markers, point, Quaternion.identity,this.transform);
                StaticBatchingUtility.Combine(this.gameObject);

            }
        }
    }
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
        var finalRotation = CalculateSnappedRotation();
        GameObject newBelt = Instantiate(conveyorBelt, finalposition, finalRotation);
        newBelt.transform.parent = this.transform;
        
        newBelt.name = conveyorBelt.name;

        StaticBatchingUtility.Combine(this.gameObject);

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
    public Quaternion CalculateSnappedRotation()
    {
        float Y = Mathf.Round(cameraRotation.rotationY / 90f) * 90f;
        Debug.Log(cameraRotation.rotationX);
        Debug.Log(Y);
        Quaternion rotationY = Quaternion.Euler(0, Y, 0f);

        return rotationY;
    }
}


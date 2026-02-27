using Unity.VisualScripting;
using UnityEngine;

public class selectedBelt : MonoBehaviour
{

    private void OnMouseDown()
    {
        Debug.Log("clicked item");
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = Color.yellow;
    }
}

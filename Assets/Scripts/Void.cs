using UnityEngine;

public class Void : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Box")
        {
            collision.gameObject.SetActive(false);
        }
    }
}

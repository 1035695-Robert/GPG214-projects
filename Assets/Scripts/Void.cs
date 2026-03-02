using UnityEngine;

public class Void : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter(Collision collision)
    {
       if(collision.transform.tag == "BoxItem")
        {
            collision.gameObject.SetActive(false);
        }
    }
}

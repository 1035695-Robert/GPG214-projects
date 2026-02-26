using UnityEngine;
using UnityEngine.VFX;

public class testPoolLoader : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.A))
        {
            Spawn();
        }
    }
    void Spawn()
    {
        GameObject box = BoxPoolManager.instance.GetPooledBox();

        if (box != null)
        {
        box.transform.position = transform.position;
            box.SetActive(true);

            //additional customization for the bullet can be done here
        }
    }
}

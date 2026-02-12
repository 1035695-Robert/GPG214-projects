using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    public TextAsset boxTextJSON;

    [System.Serializable]
    public class Box
    {
        public string name;
        public string colour;
        //add other data types later
    }
    [System.Serializable]
    public class BoxList
    {
        public Box[] box;
    }
    public BoxList myBoxList = new BoxList();
    void Start()
    {
        myBoxList = JsonUtility.FromJson<BoxList>(boxTextJSON.text);
    }
}

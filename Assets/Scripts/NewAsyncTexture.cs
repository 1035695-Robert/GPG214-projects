using NUnit.Framework;
using SimpleFactory;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class NewAsyncTexture : MonoBehaviour
{
    [SerializeField] private Material BoxLoaderM;
    [SerializeField] private Material beltMaterial;
    [SerializeField] private Material beltTexture;

    public Sprite splitBelt;
    public Sprite belt;
    public Sprite boxLoader;

    public Texture2D splitBeltNormal;
    public Texture2D beltNormal;
    public Texture2D boxLoaderNormal;

    public Texture2D splitBeltAO;
    public Texture2D beltAO;
    public Texture2D boxLoaderARM;

    Texture2D boxLoaderTex;
    Texture2D beltTex;
    Texture2D splitTex;

    string L1 = "LOD1";
    string L2 = "LOD2";

    private void OnEnable()
    {
        EventManager.ItemTextureLoad += TextureLoad;
    }
    private void OnDisable()
    {
        EventManager.ItemTextureLoad -= TextureLoad;
    }
    private void Awake()
    {
        StartCoroutine(LoadTexture());
        DontDestroyOnLoad(gameObject);
    }


    private IEnumerator LoadTexture()
    {
        var requestSplit = Resources.LoadAsync<Sprite>("textures/split");
        var requestBelt = Resources.LoadAsync<Sprite>("textures/PushArrow");
        var requestBoxLoader = Resources.LoadAsync<Sprite>("textures/BoxLoader");

        var requestBeltN = Resources.LoadAsync<Texture2D>("textures/PushArrow_n");
        var requestSplitN = Resources.LoadAsync<Texture2D>("textures/split_n");
        var requestBoxLoaderN = Resources.LoadAsync<Texture2D>("textures/BoxLoader_n");

        var requestSplitAO = Resources.LoadAsync<Texture2D>("textures/split_AO");
        var requestBeltAO = Resources.LoadAsync<Texture2D>("textures/PushArrow_AO");
        var requestBoxLoaderARM = Resources.LoadAsync<Texture2D>("textures/BoxLoader_arm");

        splitBelt = requestSplit.asset as Sprite;
        splitTex = TextureFromSprite(splitBelt);
        belt = requestBelt.asset as Sprite;
        beltTex = TextureFromSprite(belt);
        boxLoader = requestBoxLoader.asset as Sprite;
        boxLoaderTex = TextureFromSprite(boxLoader);

        boxLoaderNormal = requestBoxLoaderN.asset as Texture2D;
        beltNormal = requestBeltN.asset as Texture2D;
        splitBeltNormal = requestSplitN.asset as Texture2D;

        splitBeltAO = requestSplitAO.asset as Texture2D;
        beltAO = requestBeltAO.asset as Texture2D;
        boxLoaderARM = requestBoxLoaderARM.asset as Texture2D;


        yield return null;
        //this could be done more compact.
    }
    public void TextureLoad(GameObject gameItem)
    {
        Debug.Log(gameItem.name);
        string item = gameItem.name.ToString();
        switch (item)
        {
            case "ConveyorBelt":
                LoadTextureToBelt(gameItem);
                break;

            case "BoxLoader":
                LoadTextureToBoxLoader(gameItem);
                break;

            case "SplitBelts":
                LoadTextureToBeltSplit(gameItem);
                break;

            case "Obstacles":
                LoadObstactleTexture(gameItem);
                break;
        }
    }
    public void LoadTextureToBelt(GameObject obj)
    {//need to convert to texture from sprite


        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();

        renderer.material.EnableKeyword("_NORMALMAP");
        renderer.material.EnableKeyword("_OCCLUSIONMAP");

        renderer.material.SetTexture("_BaseMap", beltTex);
        renderer.material.SetTexture("_BumpMap", beltNormal);
        //renderer.material.SetFloat("_BumpScale", 2f);
        renderer.material.SetTexture("_OcclusionMap", beltAO);

        Transform LOD1 = obj.transform.Find(L1);
        LOD1.GetComponent<MeshRenderer>().material.SetTexture("_BaseMap", beltTex);

        Transform LOD2 = obj.transform.Find(L2);
        LOD2.GetComponent<Renderer>().material.color = new Color32(72, 70, 70, 1);

        obj.GetComponent<Renderer>().allowOcclusionWhenDynamic = true;
    }
    public void LoadTextureToBeltSplit(GameObject obj)
    {
        //need to convert to texture from sprite
        Texture2D splitTex = TextureFromSprite(splitBelt);

        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();

        renderer.material.EnableKeyword("_NORMALMAP");
        renderer.material.EnableKeyword("_OCCLUSIONMAP");

        renderer.material.SetTexture("_BaseMap", splitTex);
        renderer.material.SetTexture("_BumpMap", splitBeltNormal);
        //renderer.material.SetFloat("_BumpScale", 2f);
        renderer.material.SetTexture("_OcclusionMap", splitBeltAO);

        Transform LOD1 = obj.transform.Find(L1);
        LOD1.GetComponent<MeshRenderer>().material.SetTexture("_BaseMap", splitTex);

        Transform LOD2 = obj.transform.Find(L2);
        LOD2.GetComponent<Renderer>().material.color = new Color32(72, 70, 70, 1);

        obj.GetComponent<Renderer>().allowOcclusionWhenDynamic = true;
    }

    public void LoadTextureToBoxLoader(GameObject obj)
    {
        //need to convert to texture from sprite
        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();

        renderer.material.EnableKeyword("_NORMALMAP");
        renderer.material.EnableKeyword("_OCCLUSIONMAP");
        renderer.material.EnableKeyword("_METALLICSPECGLOSSMAP");

        renderer.material.SetTexture("_BaseMap", boxLoaderTex);
        renderer.material.SetTexture("_BumpMap", boxLoaderNormal);
        // renderer.material.SetFloat("_BumpScale", 2f);
        //renderer.material.SetTexture("_OcclusionMap", boxLoaderARM);
        renderer.material.SetTexture("_MetallicGlossMap", boxLoaderARM);

        Transform LOD1 = obj.transform.Find(L1);
        LOD1.GetComponent<MeshRenderer>().material.SetTexture("_BaseMap", boxLoaderTex);

        Transform LOD2 = obj.transform.Find(L2);
        LOD2.GetComponent<Renderer>().material.color = new Color32(79,26, 15 ,255);

        obj.GetComponent<Renderer>().allowOcclusionWhenDynamic = true;
    }


    void LoadObstactleTexture(GameObject obj)
    {
        obj.GetComponent<Renderer>().material.color = Color.black;
    }

    public static Texture2D TextureFromSprite(Sprite sprite)
    {

        if (sprite.rect.width != sprite.texture.width)
        {

            Texture2D newTex = new Texture2D((int)sprite.textureRect.width, (int)sprite.textureRect.height);
            Color[] pixels = sprite.texture.GetPixels(
                (int)sprite.textureRect.x,
                (int)sprite.textureRect.y,
                (int)sprite.textureRect.width,
                (int)sprite.textureRect.height);

            newTex.SetPixels(pixels);
            newTex.Apply();
            return newTex;
        }
        else
        {
            return sprite.texture;
        }
    }

}

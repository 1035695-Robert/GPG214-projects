using System.Collections;
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

    GameObject[] objects;
    IEnumerator Start()
    {
        objects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        yield return StartCoroutine(LoadTexture());
        LoadTextureToBelt();
        LoadTextureToBeltSplit();
        LoadTextureToBoxLoader();
    }

    private IEnumerator LoadTexture()
    {
        var requestSplit = Resources.LoadAsync<Sprite>("textures/split");
        var requestBelt = Resources.LoadAsync<Sprite>("textures/PushArrow");
        var requestBoxLoader = Resources.LoadAsync<Sprite>("textures/BoxLoader");

        var requestBeltN = Resources.LoadAsync<Texture2D>("textures/PushArrow_n");
        var requestSplitN = Resources.LoadAsync<Texture2D>("textures/split_n");
        var requestBoxLoaderN = Resources.LoadAsync<Texture2D>("textures/BoxLoader_n");

        var requestSplitAO = Resources.LoadAsync<Sprite>("textures/split_AO");
        var requestBeltAO = Resources.LoadAsync<Sprite>("textures/PushArrow_AO");
        var requestBoxLoaderARM = Resources.LoadAsync<Texture2D>("textures/BoxLoader_arm");

        splitBelt = requestSplit.asset as Sprite;
        belt = requestBelt.asset as Sprite;
        boxLoader = requestBoxLoader.asset as Sprite;

        boxLoaderNormal = requestBeltN.asset as Texture2D;
        beltNormal = requestBeltN.asset as Texture2D;
        splitBeltNormal = requestSplitN.asset as Texture2D;

        splitBeltAO = requestSplitAO.asset as Texture2D;
        beltAO = requestBeltAO.asset as Texture2D;
        boxLoaderARM = requestBoxLoaderARM.asset as Texture2D;

        yield return null;
        //this could be done more compact.
    }

    private void LoadTextureToBelt()
    {//need to convert to texture from sprite
        Texture2D beltTex = TextureFromSprite(belt);

        foreach (GameObject obj in objects)
        {
            if (obj.name == "ConveyorBelt")
            {
                MeshRenderer renderer = obj.GetComponent<MeshRenderer>();

                renderer.material.SetTexture("_BaseMap", beltTex);
                renderer.material.SetTexture("_BumpMap", beltNormal);
                renderer.material.SetFloat("_BumpScale", 2f);
                renderer.material.SetTexture("_OcclusionMap", beltAO);
            }
        }
    }
    private void LoadTextureToBeltSplit()
    {
        //need to convert to texture from sprite
        Texture2D splitTex = TextureFromSprite(splitBelt);

        foreach (GameObject obj in objects)
        {
            if (obj.name == "SplitBelts")
            {

                MeshRenderer renderer = obj.GetComponent<MeshRenderer>();

                renderer.material.SetTexture("_BaseMap", splitTex);
                renderer.material.SetTexture("_BumpMap", beltNormal);
                renderer.material.SetFloat("_BumpScale", 2f);
                renderer.material.SetTexture("_OcclusionMap", splitBeltAO);
            }
        }
    }
    private void LoadTextureToBoxLoader()
    {                
        //need to convert to texture from sprite
        Texture2D boxLoaderTex = TextureFromSprite(boxLoader);

        foreach (GameObject obj in objects)
        {
            if (obj.name == "BoxLoader")
            {
                MeshRenderer renderer = obj.GetComponent<MeshRenderer>();

                renderer.material.SetTexture("_BaseMap", boxLoaderTex);
                renderer.material.SetTexture("_BumpMap", boxLoaderNormal);
                renderer.material.SetFloat("_BumpScale", 2f);
                renderer.material.SetTexture("_OcclusionMap", boxLoaderARM);
            }
        }
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

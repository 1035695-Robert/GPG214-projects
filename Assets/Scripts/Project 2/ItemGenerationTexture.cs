using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Rendering;

public class ItemGenerationTexture : MonoBehaviour
{
    public Texture2D LevelTexture;

    public GameObject objectToSpawn;

    public float SpawnDepth;
    public float spacing;
    public float beltPixelSize;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnBeltFromImage();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnBeltFromImage()
    {
        if (LevelTexture == null)
        {
            Debug.Log("no texture");
            return;
        }
        bool[,] occupiedPixels = new bool[LevelTexture.width, LevelTexture.height];

        int beltCount = 0;
        for (float y = 0; y < LevelTexture.height; y += beltPixelSize)
        {
            for (float x = 0; x < LevelTexture.width; x += beltPixelSize)
            {//check to see if i can spawn the belt
                if (CanSpawnBelt(LevelTexture, Mathf.FloorToInt(x), Mathf.FloorToInt(y), occupiedPixels))
                {
                    //if i can mark it as occupied.
                    MarkOccupied(Mathf.FloorToInt(x), Mathf.FloorToInt(y), occupiedPixels);

                    Vector3 spawnPosition = new Vector3(x * spacing, y * spacing, SpawnDepth)+ transform.position;
                    Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

                    beltCount++;
                    Debug.Log("coin Spawned in " + beltCount);
                }

            }
        }
    }

    //this checks to see if there is enough surrounding space to spawn belt
    bool CanSpawnBelt(Texture2D image, int startX, int startY, bool[,] occupied)
    {
        int redPixelCount = 0;
        for (int y = 0; y < Mathf.CeilToInt(beltPixelSize); y++)
        {
            for (int x = 0; x < Mathf.CeilToInt(beltPixelSize); x++)
            {
                int pixelX = startX + x;
                int pixelY = startY + y;

                if (pixelX >= image.width || pixelY >= image.height)
                {
                    continue;
                }
                if (occupied[pixelX, pixelY])
                {
                    return false;
                }
                Color pixelColor = image.GetPixel(pixelX, pixelY);
                if (isRed(pixelColor))
                {
                    redPixelCount++;
                }
            }

        }

        return redPixelCount >= beltPixelSize;
    }
    // sets the location provided and ensures neighbours are set to occupied
    void MarkOccupied(int startX, int startY, bool[,] occupied)
    {
        for (int y = 0; y < Mathf.CeilToInt(beltPixelSize); y++)
        {
            for (int x = 0; x < Mathf.CeilToInt(beltPixelSize); x++)
            {
                int pixelX = startX + x;
                int pixelY = startY + y;

                if (pixelX >= occupied.GetLength(0) || pixelY >= occupied.GetLength(1))
                {
                    continue;
                }
                occupied[pixelX, pixelY] = true;
            }
        }
    }

    private bool isRed(Color color)
    {
        return color.r > 0 && color.g < 1 && color.b < 1;
    }
}

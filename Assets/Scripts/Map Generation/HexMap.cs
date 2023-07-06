using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TerrainObjectData
{
    public string terrainObjectName;
    public GameObject terrainObject;
    public int lowerSpawnRate;
    public int upperSpawnRate;
    public List<string> acceptableTileSpawns;
}

public class HexMap : MonoBehaviour
{
    //Parent Game Object
    public GameObject parent;

    // Hex Tiles
    public GameObject deepSea;
    public GameObject shallowSea;
    public GameObject sand;
    public GameObject plains;
    public GameObject mountains;

    //Contains all of the data for the creation of terrain objects
    public List<TerrainObjectData> terrainObjectData;

    //Terrain Game Object Control Variables
    // Terrain Starter and Ender Variables (Lowest Terrain to Highest Terrain)
    public bool generateTerrainObjects = true;
    private bool placedProperly = true;
    public float deepSeaEnd;
    public float shallowSeaEnd;
    public float sandyEnd;
    public float plainsEnd;
    public float mountainsEnd;
    // Map Vars
    public int mapWidth = 25;
    public int mapHeight = 12;

    // Noise Adjustment Variables
    public float scale = 1.4f;
    public float tileXOffset = 34.5f;
    public float tileZOffset = 30f;

    //Script References
    //public NoiseManager noiseManager;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateHexTileMap());
    }

    IEnumerator CreateHexTileMap()
    {
        float xPos = Random.Range(-10000f, 10000f);
        float zPos = Random.Range(-10000f, 10000f);

        for (int x = 0; x <= mapWidth; x++)
        {
            for (int z = 0; z <= mapHeight; z++)
            {
                float sampleX = (x + xPos);
                float sampleZ = (z + zPos);

                float yNoise = Mathf.PerlinNoise(sampleX * 0.07f, sampleZ * 0.07f);

                if (yNoise >= -5)
                {
                    if (yNoise < deepSeaEnd)
                    {
                        Vector3 hexPosition = new Vector3(x * tileXOffset + (z % 2 == 0 ? 0 : tileXOffset / 2), 0, z * tileZOffset);
                        if (CheckCollisionWithBarrier(hexPosition))
                        {
                            Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
                            GameObject hex = Instantiate(deepSea, hexPosition, rotation);
                            hex.tag = "Deep Sea";
                            hex.transform.SetParent(parent.transform);
                        }
                    }
                    else if (yNoise < shallowSeaEnd)
                    {
                        Vector3 hexPosition = new Vector3(x * tileXOffset + (z % 2 == 0 ? 0 : tileXOffset / 2), 0, z * tileZOffset);
                        if (CheckCollisionWithBarrier(hexPosition))
                        {
                            Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
                            GameObject hex = Instantiate(shallowSea, hexPosition, rotation);
                            hex.tag = "Shallow Sea";
                            hex.transform.SetParent(parent.transform);
                        }
                    }
                    else if (yNoise < sandyEnd)
                    {
                        Vector3 hexPosition = new Vector3(x * tileXOffset + (z % 2 == 0 ? 0 : tileXOffset / 2), 0, z * tileZOffset);
                        if (CheckCollisionWithBarrier(hexPosition))
                        {
                            Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
                            GameObject hex = Instantiate(sand, hexPosition, rotation);
                            hex.tag = "Sand";
                            hex.transform.SetParent(parent.transform);
                            if (generateTerrainObjects)
                            {
                                generateObjects(tileXOffset, tileZOffset, x, z, "Sand");
                            }
                        }
                    }
                    else if (yNoise < plainsEnd)
                    {
                        Vector3 hexPosition = new Vector3(x * tileXOffset + (z % 2 == 0 ? 0 : tileXOffset / 2), 0, z * tileZOffset);
                        if (CheckCollisionWithBarrier(hexPosition))
                        {
                            Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
                            GameObject hex = Instantiate(plains, hexPosition, rotation);
                            hex.tag = "Plains";
                            hex.transform.SetParent(parent.transform);
                            if (generateTerrainObjects)
                            {
                                generateObjects(tileXOffset, tileZOffset, x, z, "Plains");
                            }
                        }
                    }else if (yNoise < mountainsEnd)
                    {
                        Vector3 mountainHexPosition = new Vector3(x * tileXOffset + (z % 2 == 0 ? 0 : tileXOffset / 2), 0, z * tileZOffset);
                        if (CheckCollisionWithBarrier(mountainHexPosition))
                         {
                            Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
                            GameObject hex = Instantiate(mountains, mountainHexPosition, rotation);
                            hex.tag = "Mountains";
                            hex.transform.SetParent(parent.transform);
                            if (generateTerrainObjects)
                            {
                                generateObjects(tileXOffset, tileZOffset, x, z, "Mountains");
                            }
                        }
                    }
                    yield return null;
                }
            }
        }
    }

    bool CheckCollisionWithBarrier(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapBox(position, Vector3.one * 0.5f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Barrier"))
            {
                return true;
            }
        }
        return false;
    }

    public void generateObjects(float tileXOffset, float tileZOffset, int x, int z, string hexTag)
    {
        // Iterate over each TerrainObjectData in the terrainObjectData list
        foreach (TerrainObjectData terrainData in terrainObjectData)
        {
            // Check if the hexTag is included in the acceptableTileSpawns list of the current TerrainObjectData
            if (terrainData.acceptableTileSpawns.Contains(hexTag))
            {
                // Generate objects for the current TerrainObjectData
                GenerateObjectsForTerrainData(terrainData, tileXOffset, tileZOffset, x, z);
            }
        }
    }

    private void GenerateObjectsForTerrainData(TerrainObjectData terrainData, float tileXOffset, float tileZOffset, int x, int z)
    {
        float maxHeight = 0f;
        int size = 0;

        int objectCount = Random.Range(terrainData.lowerSpawnRate, terrainData.upperSpawnRate);

        Vector3 rayOrigin = new Vector3(x, 20f, z);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hitInfo))
        {
            maxHeight = hitInfo.point.y;
            print("Raycast Works");
        }

        for (int y = 0; y < objectCount; y++)
        {
            GameObject terrainObjectPrefab = terrainData.terrainObject;
            if (terrainObjectPrefab != null)
            {
                float maxMeshHeight = 1f;
                MeshFilter meshFilter = terrainObjectPrefab.GetComponent<MeshFilter>();
                if (meshFilter != null)
                {
                    maxMeshHeight = meshFilter.sharedMesh.bounds.size.y;
                }

                GameObject terrainObject = Instantiate(terrainObjectPrefab);
                terrainObject.transform.position = new Vector3(Random.Range(x * tileXOffset - tileXOffset / 2, x * tileXOffset + tileXOffset / 2), maxHeight + maxMeshHeight / 2, Random.Range(z * tileZOffset - tileZOffset / 2, z * tileZOffset + tileZOffset / 2));
                print("Terrain Object Placed");
                terrainObject.transform.SetParent(parent.transform);

                Collider[] colliders = Physics.OverlapSphere(terrainObject.transform.position, 2f);
                size = colliders.Length;

                bool placedProperly = false;

                foreach (Collider collider in colliders)
                {
                    if (terrainData.acceptableTileSpawns.Contains(collider.tag))
                    {
                        placedProperly = true;
                        print("Object Placed Correctly");
                        // Do something with the placed terrain object
                        break;
                    }
                }   

                if (!placedProperly)
                {
                    // Delete the object if it's not placed correctly
                    Destroy(terrainObject);
                    print("Object Deleted");
                }
            }
        }
    }



    //Function Used to Determine if the Object was placed correctly. If not it is deleted, if it is correct, it will remain
    public void CheckPlacement(GameObject gameObject)
    {
        if (placedProperly == true)
        {
            return;
        } 
        else {
            Destroy(gameObject);
            print(gameObject + "Destroyed");
        }
    }
}
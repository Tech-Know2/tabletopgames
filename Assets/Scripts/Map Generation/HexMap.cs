using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HexMap : MonoBehaviour
{
    //Parent Game Object
    [SerializeField] private GameObject parent;

    // Hex Tiles
    [SerializeField] private GameObject deepSea;
    [SerializeField] private GameObject shallowSea;
    [SerializeField] private GameObject sand;
    [SerializeField] private GameObject plains;
    [SerializeField] private GameObject mountains;

    // Terrain Game Objects
    [SerializeField] private GameObject bush;
    [SerializeField] private GameObject tree;
    [SerializeField] private GameObject rock;
    [SerializeField] private GameObject cacti;
    [SerializeField] private GameObject boulder;

    // Terrain Game Object Control Variables
    // Terrain Starter and Ender Variables (Lowest Terrain to Highest Terrain)
    [SerializeField] private bool generateTerrainObjects = true;
    [SerializeField] private float deepSeaEnd;
    [SerializeField] private float shallowSeaEnd;
    [SerializeField] private float sandyEnd;
    [SerializeField] private float plainsEnd;
    [SerializeField] private float mountainsEnd;

    // Map Vars
    [SerializeField] private int mapWidth = 25;
    [SerializeField] private int mapHeight = 12;
    [SerializeField] private int mapRadius = 5;

    // Noise Adjustment Variables
    [SerializeField] private float scale = 1.4f;
    [SerializeField] private float tileXOffset = 34.5f;
    [SerializeField] private float tileZOffset = 30f;

    private Vector3[] hexDirections = {
        new Vector3(1, 0, 0),
        new Vector3(0.5f, 0, Mathf.Sqrt(3) / 2),
        new Vector3(-0.5f, 0, Mathf.Sqrt(3) / 2),
        new Vector3(-1, 0, 0),
        new Vector3(-0.5f, 0, -Mathf.Sqrt(3) / 2),
        new Vector3(0.5f, 0, -Mathf.Sqrt(3) / 2)
    };

    //Script References
    //public NoiseManager noiseManager;

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(CreateHexTileMap());
    }

    private IEnumerator CreateHexTileMap()
    {
        float xOffset = Random.Range(-10000f, 10000f);
        float zOffset = Random.Range(-10000f, 10000f);

        for (int q = -mapRadius; q <= mapRadius; q++)
        {
            int r1 = Mathf.Max(-mapRadius, -q - mapRadius);
            int r2 = Mathf.Min(mapRadius, -q + mapRadius);
            for (int r = r1; r <= r2; r++)
            {
                float x = q * tileXOffset * 1.5f;
                float z = (r + q * 0.5f) * tileZOffset * Mathf.Sqrt(3);

                float sampleX = (x + xOffset) * scale;
                float sampleZ = (z + zOffset) * scale;

                float yNoise = Mathf.PerlinNoise(sampleX, sampleZ);

                if (yNoise >= -5)
                {
                    TerrainType terrainType = DetermineTerrainType(yNoise);
                    GameObject hexTile = InstantiateTerrain(terrainType, x, z);

                    if (!CheckCollision(hexTile))
                    {
                        if (terrainType != TerrainType.DeepSea && generateTerrainObjects)
                        {
                            PlaceTerrainObjects(tileXOffset, tileZOffset, x, z, terrainType);
                        }
                    }
                    else
                    {
                        Destroy(hexTile);
                        PrintTerrainDestroyed(terrainType);
                    }
                }

                yield return null;
            }
        }
    }

    private bool CheckCollision(GameObject hexTile)
    {
        Collider[] colliders = Physics.OverlapBox(hexTile.transform.position, hexTile.transform.localScale / 2);

        foreach (Collider collider in colliders)
        {
            if (!collider.CompareTag("Barrier"))
            {
                return true;
            }
        }
        return false;
    }

    private TerrainType DetermineTerrainType(float yNoise)
    {
        if (yNoise < deepSeaEnd)
        {
            return TerrainType.DeepSea;
        }
        else if (yNoise < shallowSeaEnd)
        {
            return TerrainType.ShallowSea;
        }
        else if (yNoise < sandyEnd)
        {
            return TerrainType.Sand;
        }
        else if (yNoise < plainsEnd)
        {
            return TerrainType.Plains;
        }
        else if (yNoise < mountainsEnd)
        {
            return TerrainType.Mountains;
        }

        return TerrainType.Default;
    }

    private GameObject InstantiateTerrain(TerrainType terrainType, float x, float z)
    {
        GameObject hexTile = null;
        switch (terrainType)
        {
            case TerrainType.DeepSea:
                hexTile = Instantiate(deepSea);
                hexTile.tag = "Deep Sea";
                break;
            case TerrainType.ShallowSea:
                hexTile = Instantiate(shallowSea);
                hexTile.tag = "Shallow Sea";
                break;
            case TerrainType.Sand:
                hexTile = Instantiate(sand);
                hexTile.tag = "Sand";
                break;
            case TerrainType.Plains:
                hexTile = Instantiate(plains);
                hexTile.tag = "Plains";
                break;
            case TerrainType.Mountains:
                hexTile = Instantiate(mountains);
                hexTile.tag = "Mountains";
                break;
            default:
                break;
        }

        if (hexTile != null)
        {
            hexTile.transform.position = new Vector3(x, 0, z);
            hexTile.transform.SetParent(parent.transform);
        }

        return hexTile;
    }

    private void PlaceTerrainObjects(float tileXOffset, float tileZOffset, float x, float z, TerrainType terrainType)
    {
        switch (terrainType)
        {
            case TerrainType.Sand:
                placeCacti(tileXOffset, tileZOffset, x, z);
                break;
            case TerrainType.Plains:
                placeBush(tileXOffset, tileZOffset, x, z);
                placeTree(tileXOffset, tileZOffset, x, z);
                placeRock(tileXOffset, tileZOffset, x, z);
                break;
            case TerrainType.Mountains:
                placeBoulder(tileXOffset, tileZOffset, x, z);
                placeRock(tileXOffset, tileZOffset, x, z);
                break;
            default:
                break;
        }
    }

    private void PrintTerrainDestroyed(TerrainType terrainType)
    {
        switch (terrainType)
        {
            case TerrainType.DeepSea:
                print("Deep Sea Destroyed");
                break;
            case TerrainType.ShallowSea:
                print("Shallow Sea Destroyed");
                break;
            case TerrainType.Sand:
                print("Sand Destroyed");
                break;
            case TerrainType.Plains:
                print("Plains Destroyed");
                break;
            case TerrainType.Mountains:
                print("Mountains Destroyed");
                break;
            default:
                break;
        }
    }


    private void placeCacti(float tileXOffset, float tileZOffset, float x, float z)
    {
        float maxHeight = 0f;
        int size = 0;

        float cactiCount = Random.Range(3f,9f);

        Vector3 rayOrigin = new Vector3(x, 20f, z);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hitInfo))
        {
            maxHeight = hitInfo.point.y;
            print("Raycast Works");
        }

        for(int y = 0; y < cactiCount; y++)
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            float maxMeshHeight = 1f;

            if(meshFilter != null) {
                maxMeshHeight = meshFilter.mesh.bounds.size.y;
                print(maxMeshHeight);
            }

            GameObject Cacti = Instantiate(cacti);
            Cacti.transform.position = new Vector3(Random.Range(x*tileXOffset - tileXOffset/2, x*tileXOffset + tileXOffset/2), maxHeight + maxMeshHeight/2, Random.Range(z*tileZOffset - tileZOffset/2, z*tileZOffset + tileZOffset/2));
            print("Cacti Planted");
            Cacti.transform.SetParent(parent.transform);  

            Collider[] colliders = Physics.OverlapSphere(Cacti.transform.position, 2f);
            size = colliders.Length;

            foreach (Collider collider in colliders) 
            {
                if (collider.CompareTag("Sand")) {
                    print("Cacti Placed Correctly");
                } else if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea") || collider.CompareTag("Plains") || collider.CompareTag("Mountains")){
                    Destroy(Cacti);
                    print("Cacti Destroyed");
                } else if (size == 0)
                {
                    Destroy(Cacti);
                    print("Void Cacti Destroyed");
                }
            }
        }
    }

    private void placeBush (float tileXOffset, float tileZOffset, float x, float z)
    {
        float maxHeight = 1f;
        int size = 0;
        float bushCount = Random.Range(3f,12f);

        Vector3 rayOrigin = new Vector3(x, 20f, z);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hitInfo))
        {
            maxHeight = hitInfo.point.y;
            print("Raycast Works");
        }
        for(int y = 0; y < bushCount; y++)
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            float maxMeshHeight = 1f;
            if(meshFilter != null) 
            {
                maxMeshHeight = meshFilter.mesh.bounds.size.y;
                print(maxMeshHeight);
            }
                            
            GameObject Bush = Instantiate(bush);
            Bush.transform.position = new Vector3(Random.Range(x*tileXOffset - tileXOffset/2, x*tileXOffset + tileXOffset/2), maxHeight + maxMeshHeight/2, Random.Range(z*tileZOffset - tileZOffset/2, z*tileZOffset + tileZOffset/2));
            print("Bush Planted");
            Bush.transform.SetParent(parent.transform); 
             
            Collider[] colliders = Physics.OverlapSphere(Bush.transform.position, 2f);
            size = colliders.Length;
            foreach (Collider collider in colliders) 
            {
                if (collider.CompareTag("Plains")) 
                {
                    print("Bush Placed Correctly");
                } else if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea") || collider.CompareTag("Sand"))
                {
                    Destroy(Bush);
                    print("Bush Destroyed");
                } else if (size == 0)
                {
                    Destroy(Bush);
                    print("Void Bush Destroyed");
                }
            }
        }
    }

    private void placeTree (float tileXOffset, float tileZOffset, float x, float z)
    {
        float maxHeight = 1f;
        int size = 0;
                
        float treeCount = Random.Range(3f,10f);

        Vector3 rayOrigin = new Vector3(x, 20f, z);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hitInfo))
        {
            maxHeight = hitInfo.point.y;
            print("Raycast Works");
        }
        for(int y = 0; y < treeCount; y++)
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            float maxMeshHeight = 1f;
            if(meshFilter != null) 
            {
                maxMeshHeight = meshFilter.mesh.bounds.size.y;
                print(maxMeshHeight);
            }
                            
            GameObject Tree = Instantiate(tree);
            Tree.transform.position = new Vector3(Random.Range(x*tileXOffset - tileXOffset/2, x*tileXOffset + tileXOffset/2), maxHeight + maxMeshHeight/2, Random.Range(z*tileZOffset - tileZOffset/2, z*tileZOffset + tileZOffset/2));
            print("Tree Planted");
            Tree.transform.SetParent(parent.transform);  

            Collider[] colliders = Physics.OverlapSphere(Tree.transform.position, 2f);
            size = colliders.Length;
            foreach (Collider collider in colliders) 
            {
                if (collider.CompareTag("Plains")) 
                {
                    print("Tree Placed Correctly");
                } else if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea") || collider.CompareTag("Sand"))
                {
                    Destroy(Tree);
                    print("Tree Destroyed");
                } else if (size == 0)
                {
                    Destroy(Tree);
                    print("Void Tree Destroyed");
                }
            }
        }
    }

    private void placeRock (float tileXOffset, float tileZOffset, float x, float z)
    {
        float maxHeight = 1f;
        int size = 0;
                
        float rockCount = Random.Range(3f,10f);

        Vector3 rayOrigin = new Vector3(x, 20f, z);
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, Vector3.down, out hitInfo))
        {
            maxHeight = hitInfo.point.y;
            print("Raycast Works");
        }
        for(int y = 0; y < rockCount; y++)
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            float maxMeshHeight = 1f;
            if(meshFilter != null) 
            {
                maxMeshHeight = meshFilter.mesh.bounds.size.y;
                print(maxMeshHeight);
            }
                            
            GameObject Rock = Instantiate(rock);
            Rock.transform.position = new Vector3(Random.Range(x*tileXOffset - tileXOffset/2, x*tileXOffset + tileXOffset/2), maxHeight + maxMeshHeight/2, Random.Range(z*tileZOffset - tileZOffset/2, z*tileZOffset + tileZOffset/2));
            print("Tree Planted");
            Rock.transform.SetParent(parent.transform);  

            Collider[] colliders = Physics.OverlapSphere(Rock.transform.position, 2f);
            size = colliders.Length;
            foreach (Collider collider in colliders) 
            {
                if (collider.CompareTag("Plains")) 
                {
                    print("Rock Placed Correctly");
                } else if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea") || collider.CompareTag("Sand"))
                {
                    Destroy(Rock);
                    print("Rock Destroyed");
                } else if (size == 0)
                {
                    Destroy(Rock);
                    print("Void Rock Destroyed");
                }
            }
        }
    }

    private void placeBoulder(float tileXOffset, float tileZOffset, float x, float z)
    {
        float maxHeight = 0f;
        int size = 0;

        float boulderCount = Random.Range(3f,4f);

        Vector3 rayOrigin = new Vector3(x, 20f, z);
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, Vector3.down, out hitInfo))
        {
            maxHeight = hitInfo.point.y;
            print("Raycast Works");
        }
        for(int y = 0; y < boulderCount; y++)
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            float maxMeshHeight = 1f;
            if(meshFilter != null) 
            {
                maxMeshHeight = meshFilter.mesh.bounds.size.y;
                print(maxMeshHeight);
            }
                            
            GameObject Boulder = Instantiate(boulder);
            Boulder.transform.position = new Vector3(Random.Range(x*tileXOffset - tileXOffset/2, x*tileXOffset + tileXOffset/2), maxHeight + maxMeshHeight/2, Random.Range(z*tileZOffset - tileZOffset/2, z*tileZOffset + tileZOffset/2));
            print("Boulder Placed");
            Boulder.transform.SetParent(parent.transform); 

            Collider[] colliders = Physics.OverlapSphere(Boulder.transform.position, maxMeshHeight);
            size = colliders.Length;
            foreach (Collider collider in colliders) 
            {
                if (collider.CompareTag("Mountains")) 
                {
                    print("Boulder Placed Correctly");
                } else if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea") || collider.CompareTag("Sand"))
                {
                    Destroy(Boulder);
                    print("Boulder Destroyed");
                } else if (size == 0)
                {
                    Destroy(Boulder);
                    print("Void Boulder Destroyed");
                }
            }
        }
    }

    public enum TerrainType
    {
        Default,
        DeepSea,
        ShallowSea,
        Sand,
        Plains,
        Mountains
    }
}
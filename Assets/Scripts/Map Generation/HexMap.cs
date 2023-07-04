using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //Terrain Game Objects
    public GameObject bush;
    public GameObject tree;
    public GameObject rock;
    public GameObject cacti;
    public GameObject boulder;

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
                                placeCacti(tileXOffset, tileZOffset, x, z);
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
                                placeBush(tileXOffset, tileZOffset, x, z);
                                placeBush(tileXOffset, tileZOffset, x, z);
                                placeTree(tileXOffset, tileZOffset, x, z);
                                placeRock(tileXOffset, tileZOffset, x, z);
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
                                placeBoulder(tileXOffset, tileZOffset, x, z);
                                placeRock(tileXOffset, tileZOffset, x, z);
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

    void placeCacti(float tileXOffset, float tileZOffset, int x, int z)
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
                    placedProperly = true;
                    print("Cacti Placed Correctly");
                } else if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea") || collider.CompareTag("Plains") || collider.CompareTag("Mountains")){
                    placedProperly = false;
                    CheckPlacement(Cacti);
                } else if (size == 0)
                {
                    placedProperly = false;
                    CheckPlacement(Cacti);
                }
            }
        }
    }

    void placeBush (float tileXOffset, float tileZOffset, int x, int z)
    {
        float maxHeight = 1f;
        int size = 0;
        float bushCount = Random.Range(3f,8f);

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
                    placedProperly = true;
                    print("Bush Placed Correctly");
                } else if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea") || collider.CompareTag("Sand"))
                {
                    placedProperly = false;
                    CheckPlacement(Bush);
                } else if (size == 0)
                {
                    placedProperly = false;
                    CheckPlacement(Bush);
                }
            }
        }
    }

    void placeTree (float tileXOffset, float tileZOffset, int x, int z)
    {
        float maxHeight = 1f;
        int size = 0;
                
        float treeCount = Random.Range(3f,7f);

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
                    placedProperly = true;
                    print("Tree Placed Correctly");
                } else if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea") || collider.CompareTag("Sand"))
                {
                    placedProperly = false;
                    CheckPlacement(Tree);
                } else if (size == 0)
                {
                    placedProperly = false;
                    CheckPlacement(Tree);
                }
            }
        }
    }

    void placeRock (float tileXOffset, float tileZOffset, int x, int z)
    {
        float maxHeight = 1f;
        int size = 0;
                
        float rockCount = Random.Range(3f,9f);

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
                    placedProperly = true;
                    print("Rock Placed Correctly");
                } else if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea") || collider.CompareTag("Sand"))
                {
                    placedProperly = false;
                    CheckPlacement(Rock);
                } else if (size == 0)
                {
                    placedProperly = false;
                    CheckPlacement(Rock);
                }
            }
        }
    }

    void placeBoulder(float tileXOffset, float tileZOffset, int x, int z)
    {
        float maxHeight = 0f;
        int size = 0;

        float boulderCount = Random.Range(3f,5f);

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
                    placedProperly = true;
                    print("Boulder Placed Correctly");
                } else if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea") || collider.CompareTag("Sand"))
                {
                    placedProperly = false;
                    CheckPlacement(Boulder);
                } else if (size == 0)
                {
                    placedProperly = false;
                    CheckPlacement(Boulder);
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
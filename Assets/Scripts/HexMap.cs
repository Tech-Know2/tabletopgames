using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    // Hex Tiles
    public GameObject deepSea;
    public GameObject shallowSea;
    public GameObject sand;
    public GameObject plains;
    public GameObject mountains;

    //Terrain Game Objects
    public GameObject bush;
    public GameObject tree;

    //Terrain Game Object Control Variables

    // Terrain Starter and Ender Variables (Lowest Terrain to Highest Terrain)
    public float deepSeaEnd;
    public float shallowSeaEnd;
    public float sandyEnd;
    public float plainsEnd;
    public float mountainsEnd;

    // Map Vars
    public int mapWidth = 25;
    public int mapHeight = 12;

    // Noise Adjustment Variables
    public float scale = 1f;

    public float tileXOffset = 34.5f;
    public float tileZOffset = 30f;

    // Start is called before the first frame update
    void Start()
    {
        createHexTileMap();
    }

    // Update is called once per frame
    public void createHexTileMap()
    {
        float xPos = Random.Range(-10000f, 10000f);
        float zPos = Random.Range(-10000f, 10000f);


        for (int x = 0; x <= mapWidth; x++)
        {
            for (int z = 0; z <= mapHeight; z++)
            {
                float sampleX = (x + xPos);
                float sampleZ = (z + zPos);

                float yNoise = Mathf.PerlinNoise(sampleX * 0.05f, sampleZ * 0.05f);

                if (yNoise >= -1)
                {
                    if (yNoise < deepSeaEnd)
                    {
                        GameObject Hex = Instantiate(deepSea);
                        Hex.transform.position = new Vector3(x * tileXOffset + (z % 2 == 0 ? 0 : tileXOffset / 2), 0, z * tileZOffset);
                        Hex.tag = "Deep Sea";                  
                    }
                    else if (yNoise < shallowSeaEnd)
                    {
                        GameObject Hex = Instantiate(shallowSea);
                        Hex.transform.position = new Vector3(x * tileXOffset + (z % 2 == 0 ? 0 : tileXOffset / 2), 0, z * tileZOffset);
                        Hex.tag = "Shallow Sea";
                    }
                    else if (yNoise < sandyEnd)
                    {
                        GameObject Hex = Instantiate(sand);
                        Hex.transform.position = new Vector3(x * tileXOffset + (z % 2 == 0 ? 0 : tileXOffset / 2), 0, z * tileZOffset);
                        Hex.tag = "Sand";
                    }
                    else if (yNoise < plainsEnd)
                    {
                        GameObject Hex = Instantiate(plains);
                        Hex.transform.position = new Vector3(x * tileXOffset + (z % 2 == 0 ? 0 : tileXOffset / 2), 0, z * tileZOffset);
                        Hex.tag = "Plains";

                        float maxHeight = 0f;

                        float bushCount = Random.Range(3f,12f);
                        float treeCount = Random.Range(3f,10f);

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

                            if(meshFilter != null) {
                                maxMeshHeight = meshFilter.mesh.bounds.size.y;
                                print(maxMeshHeight);
                            }
                            
                            GameObject Bush = Instantiate(bush);
                            Bush.transform.position = new Vector3(Random.Range(x*tileXOffset - tileXOffset/2, x*tileXOffset + tileXOffset/2), maxHeight + maxMeshHeight/2, Random.Range(z*tileZOffset - tileZOffset/2, z*tileZOffset + tileZOffset/2));
                            print("Bush Planted");

                            Collider[] colliders = Physics.OverlapSphere(Bush.transform.position, maxMeshHeight);

                            foreach (Collider collider in colliders) {
                                if (collider.CompareTag("Plains")) {
                                    print("Bush Placed Correctly");
                                } else if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea")){
                                    Destroy(Bush);
                                    print("Bush Destroyed");
                                }
                            }
                        }

                        for(int y = 0; y < treeCount; y++)
                        {
                            MeshFilter meshFilter = GetComponent<MeshFilter>();
                            float maxMeshHeight = 1f;

                            if(meshFilter != null) {
                                maxMeshHeight = meshFilter.mesh.bounds.size.y;
                                print(maxMeshHeight);
                            }
                            
                            GameObject Tree = Instantiate(tree);
                            Tree.transform.position = new Vector3(Random.Range(x*tileXOffset - tileXOffset/2, x*tileXOffset + tileXOffset/2), maxHeight + maxMeshHeight/2, Random.Range(z*tileZOffset - tileZOffset/2, z*tileZOffset + tileZOffset/2));
                            print("Tree Planted");

                            Collider[] colliders = Physics.OverlapSphere(Tree.transform.position, 5f);

                            foreach (Collider collider in colliders) {
                                if (collider.CompareTag("Plains")) {
                                    print("Tree Placed Correctly");
                                } else if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea")){
                                    Destroy(Tree);
                                    print("Tree Destroyed");
                                }
                            }
                        }
                    }
                    else if (yNoise < mountainsEnd)
                    {
                        GameObject Hex = Instantiate(mountains);
                        Hex.transform.position = new Vector3(x * tileXOffset + (z % 2 == 0 ? 0 : tileXOffset / 2), 0, z * tileZOffset);
                        Hex.tag = "Mountains";
                    }
                }
            }
        }
    }
}
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
        StartCoroutine(createHexTileMap());
    }

    IEnumerator createHexTileMap()
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
                        Hex.transform.SetParent(parent.transform); 
                        yield return null;              
                    }
                    else if (yNoise < shallowSeaEnd)
                    {
                        GameObject Hex = Instantiate(shallowSea);
                        Hex.transform.position = new Vector3(x * tileXOffset + (z % 2 == 0 ? 0 : tileXOffset / 2), 0, z * tileZOffset);
                        Hex.tag = "Shallow Sea";
                        Hex.transform.SetParent(parent.transform);  
                        yield return null;
                    }
                    else if (yNoise < sandyEnd)
                    {
                        GameObject Hex = Instantiate(sand);
                        Hex.transform.position = new Vector3(x * tileXOffset + (z % 2 == 0 ? 0 : tileXOffset / 2), 0, z * tileZOffset);
                        Hex.tag = "Sand";
                        Hex.transform.SetParent(parent.transform);  

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

                            foreach (Collider collider in colliders) {
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
                        yield return null;
                    }
                    else if (yNoise < plainsEnd)
                    {
                        GameObject Hex = Instantiate(plains);
                        Hex.transform.position = new Vector3(x * tileXOffset + (z % 2 == 0 ? 0 : tileXOffset / 2), 0, z * tileZOffset);
                        Hex.tag = "Plains";
                        Hex.transform.SetParent(parent.transform);  

                        float maxHeight = 0f;
                        int size = 0;

                        float bushCount = Random.Range(3f,12f);
                        float treeCount = Random.Range(3f,10f);
                        float rockCount = Random.Range(3f,10f);

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
                            Bush.transform.SetParent(parent.transform);  

                            Collider[] colliders = Physics.OverlapSphere(Bush.transform.position, 2f);
                            size = colliders.Length;

                            foreach (Collider collider in colliders) {
                                if (collider.CompareTag("Plains")) {
                                    print("Bush Placed Correctly");
                                } else if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea") || collider.CompareTag("Sand")){
                                    Destroy(Bush);
                                    print("Bush Destroyed");
                                } else if (size == 0)
                                {
                                    Destroy(Bush);
                                    print("Void Bush Destroyed");
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
                            Tree.transform.SetParent(parent.transform);  

                            Collider[] colliders = Physics.OverlapSphere(Tree.transform.position, 2f);
                            size = colliders.Length;

                            foreach (Collider collider in colliders) {
                                if (collider.CompareTag("Plains")) {
                                    print("Tree Placed Correctly");
                                } else if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea") || collider.CompareTag("Sand")){
                                    Destroy(Tree);
                                    print("Tree Destroyed");
                                } else if (size == 0)
                                {
                                    Destroy(Tree);
                                    print("Void Tree Destroyed");
                                }
                            }
                        }

                        for(int y = 0; y < rockCount; y++)
                        {
                            MeshFilter meshFilter = GetComponent<MeshFilter>();
                            float maxMeshHeight = 1f;

                            if(meshFilter != null) {
                                maxMeshHeight = meshFilter.mesh.bounds.size.y;
                                print(maxMeshHeight);
                            }
                            
                            GameObject Rock = Instantiate(rock);
                            Rock.transform.position = new Vector3(Random.Range(x*tileXOffset - tileXOffset/2, x*tileXOffset + tileXOffset/2), maxHeight + maxMeshHeight/2, Random.Range(z*tileZOffset - tileZOffset/2, z*tileZOffset + tileZOffset/2));
                            print("Rock Placed");
                            Rock.transform.SetParent(parent.transform);  

                            Collider[] colliders = Physics.OverlapSphere(Rock.transform.position, 2f);
                            size = colliders.Length;

                            foreach (Collider collider in colliders) {
                                if (collider.CompareTag("Plains")) {
                                    print("Rock Placed Correctly");
                                } else if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea") || collider.CompareTag("Sand")){
                                    Destroy(Rock);
                                    print("Rock Destroyed");
                                } else if (size == 0)
                                {
                                    Destroy(Rock);
                                    print("Void Rock Destroyed");
                                }
                            }
                        }
                        yield return null;
                    }
                    else if (yNoise < mountainsEnd)
                    {
                        GameObject Hex = Instantiate(mountains);
                        Hex.transform.position = new Vector3(x * tileXOffset + (z % 2 == 0 ? 0 : tileXOffset / 2), 0, z * tileZOffset);
                        Hex.tag = "Mountains";
                        Hex.transform.SetParent(parent.transform);  

                        float maxHeight = 0f;
                        int size = 0;

                        float boulderCount = Random.Range(3f,4f);
                        float rockCount = Random.Range(3f,8f);

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

                            if(meshFilter != null) {
                                maxMeshHeight = meshFilter.mesh.bounds.size.y;
                                print(maxMeshHeight);
                            }
                            
                            GameObject Boulder = Instantiate(boulder);
                            Boulder.transform.position = new Vector3(Random.Range(x*tileXOffset - tileXOffset/2, x*tileXOffset + tileXOffset/2), maxHeight + maxMeshHeight/2, Random.Range(z*tileZOffset - tileZOffset/2, z*tileZOffset + tileZOffset/2));
                            print("Boulder Placed");
                            Boulder.transform.SetParent(parent.transform);  

                            Collider[] colliders = Physics.OverlapSphere(Boulder.transform.position, maxMeshHeight);
                            size = colliders.Length;

                            foreach (Collider collider in colliders) {
                                if (collider.CompareTag("Mountains")) {
                                    print("Boulder Placed Correctly");
                                } else if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea") || collider.CompareTag("Sand")){
                                    Destroy(Boulder);
                                    print("Boulder Destroyed");
                                } else if (size == 0)
                                {
                                    Destroy(Boulder);
                                    print("Void Boulder Destroyed");
                                }
                            }
                        }

                        for(int y = 0; y < rockCount; y++)
                        {
                            MeshFilter meshFilter = GetComponent<MeshFilter>();
                            float maxMeshHeight = 1f;

                            if(meshFilter != null) {
                                maxMeshHeight = meshFilter.mesh.bounds.size.y;
                                print(maxMeshHeight);
                            }
                            
                            GameObject Rock = Instantiate(rock);
                            Rock.transform.position = new Vector3(Random.Range(x*tileXOffset - tileXOffset/2, x*tileXOffset + tileXOffset/2), maxHeight + maxMeshHeight/2, Random.Range(z*tileZOffset - tileZOffset/2, z*tileZOffset + tileZOffset/2));
                            print("Rock Placed");
                            Rock.transform.SetParent(parent.transform);  

                            Collider[] colliders = Physics.OverlapSphere(Rock.transform.position, 2f);
                            size = colliders.Length;

                            foreach (Collider collider in colliders) {
                                if (collider.CompareTag("Mountains")) {
                                    print("Rock Placed Correctly");
                                } else if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea") || collider.CompareTag("Sand")){
                                    Destroy(Rock);
                                    print("Rock Destroyed");
                                } else if (size == 0)
                                {
                                    Destroy(Rock);
                                    print("Void Rock Destroyed");
                                }
                            }
                        }
                        yield return null;
                    }
                }
            }
        }
    }
}
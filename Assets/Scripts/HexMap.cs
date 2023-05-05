using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    //Parent Game Object
     private GameObject parent;

    // Hex Tiles
    private GameObject deepSea;
    private GameObject shallowSea;
    private GameObject sand;
    private GameObject plains;
    private GameObject mountains;

    //Terrain Game Objects
    private GameObject bush;
    private GameObject tree;
    private GameObject rock;
    private GameObject cacti;
    private GameObject boulder;

    //Terrain Game Object Control Variables

    // Terrain Starter and Ender Variables (Lowest Terrain to Highest Terrain)
    private float deepSeaEnd;
    private float shallowSeaEnd;
    private float sandyEnd;
    private float plainsEnd;
    private float mountainsEnd;

    // Map Vars
    private int mapWidth = 25;
    private int mapHeight = 12;

    // Noise Adjustment Variables
    private float scale = 1f;
    private float tileXOffset = 34.5f;
    private float tileZOffset = 30f;

    // Start is called before the first frame update
    void Start()
    {
        parent = new GameObject("HexMap");
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

                        placeCacti(tileXOffset, tileZOffset, cacti, parent.transform, x, z);
                    }
                    else if (yNoise < plainsEnd)
                    {
                        GameObject Hex = Instantiate(plains);
                        Hex.transform.position = new Vector3(x * tileXOffset + (z % 2 == 0 ? 0 : tileXOffset / 2), 0, z * tileZOffset);
                        Hex.tag = "Plains";
                        Hex.transform.SetParent(parent.transform);
                        
                        placeBushes(tileXOffset, tileZOffset, bush, parent.transform, x, z);
                        placeTrees(tileXOffset, tileZOffset, tree, parent.transform, x, z);
                        placeRocks(tileXOffset, tileZOffset, rock, parent.transform, x, z);
                    }
                    else if (yNoise < mountainsEnd)
                    {
                        GameObject Hex = Instantiate(mountains);
                        Hex.transform.position = new Vector3(x * tileXOffset + (z % 2 == 0 ? 0 : tileXOffset / 2), 0, z * tileZOffset);
                        Hex.tag = "Mountains";
                        Hex.transform.SetParent(parent.transform);

                        placeBoulders(tileXOffset, tileZOffset, rock, parent.transform, x, z);
                        placeRocks(tileXOffset, tileZOffset, rock, parent.transform, x, z);

                        yield return null;
                    }
                }
            }
        }
    }

    IEnumerator placeCacti(float tileXOffset, float tileZOffset, GameObject cacti, Transform parentTransform, int x, int z)
    {
        float cactiCount = Random.Range(3f, 9f);
        Vector3 rayOrigin = new Vector3(x * tileXOffset, 100f, z * tileZOffset); // raise the raycast origin to a high altitude

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            float maxHeight = hitInfo.point.y;
            Debug.Log("Raycast Works");

            for (int i = 0; i < cactiCount; i++)
            {
                float maxMeshHeight = 1f;
                MeshFilter meshFilter = cacti.GetComponent<MeshFilter>();
                if (meshFilter != null)
                {
                    maxMeshHeight = meshFilter.mesh.bounds.size.y;
                    Debug.Log(maxMeshHeight);
                }

                Vector3 cactiPosition = new Vector3(Random.Range(x * tileXOffset - tileXOffset / 2f, x * tileXOffset + tileXOffset / 2f),
                                                    maxHeight,
                                                    Random.Range(z * tileZOffset - tileZOffset / 2f, z * tileZOffset + tileZOffset / 2f));
                Collider[] colliders = Physics.OverlapSphere(cactiPosition, 2f, LayerMask.GetMask("Ground", "Sand", "Deep Sea"));

                if (colliders.Length == 0)
                {
                    GameObject cactiInstance = Instantiate(cacti, cactiPosition, Quaternion.identity, parentTransform);
                    Debug.Log("Cacti Planted");
                }
                else
                {
                    bool canPlaceCacti = false;
                    foreach (Collider collider in colliders)
                    {
                        if (collider.CompareTag("Sand") || collider.CompareTag("Ground"))
                        {
                            canPlaceCacti = true;
                            break;
                        }
                        else if (collider.CompareTag("Deep Sea"))
                        {
                            Debug.Log("Cacti Destroyed");
                            break;
                        }
                    }

                    if (canPlaceCacti)
                    {
                        GameObject cactiInstance = Instantiate(cacti, cactiPosition, Quaternion.identity, parentTransform);
                        Debug.Log("Cacti Planted");
                    }
                    else
                    {
                        Debug.Log("Void Cacti Destroyed");
                    }
                }
            }
        }

        yield return null;
    }

    IEnumerator placeRocks(float tileXOffset, float tileZOffset, GameObject rock, Transform parentTransform, int x, int z)
    {
        Vector3 tileCenter = new Vector3(x * tileXOffset, 100f, z * tileZOffset);

        if (Physics.Raycast(tileCenter + Vector3.up * 100f, Vector3.down, out RaycastHit hitInfo, 1000f))
        {
            float maxHeight = hitInfo.point.y;

            float rocksCount = Random.Range(3f, 9f);
            for (int i = 0; i < rocksCount; i++)
            {

                float maxMeshHeight = 1f;
                MeshFilter meshFilter = rock.GetComponent<MeshFilter>();
                if (meshFilter != null)
                {
                    maxMeshHeight = meshFilter.mesh.bounds.size.y;
                }

                Vector3 rockPosition = new Vector3(Random.Range(tileCenter.x - tileXOffset / 2f, tileCenter.x + tileXOffset / 2f),
                                                   maxHeight + maxMeshHeight / 2f,
                                                   Random.Range(tileCenter.z - tileZOffset / 2f, tileCenter.z + tileZOffset / 2f));

                Collider[] colliders = Physics.OverlapSphere(rockPosition, 2f);
                bool canPlaceRock = true;

                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("Water"))
                    {
                        canPlaceRock = false;
                        break;
                    }
                }

                if (canPlaceRock)
                {
                    GameObject rockInstance = Instantiate(rock, rockPosition, Quaternion.identity, parentTransform);
                    Debug.Log("Rocks Planted");
                }
                else
                {
                    Debug.Log("Rocks Destroyed");
                }
            }
        }

        yield return null;
    }


    IEnumerator placeBushes(float tileXOffset, float tileZOffset, GameObject bush, Transform parentTransform, int x, int z)
    {
        Vector3 rayOrigin = new Vector3(x, 100f, z);
        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hitInfo))
        {
            float maxHeight = hitInfo.point.y;
            float bushCount = Random.Range(3f, 12f);

            for (int y = 0; y < bushCount; y++)
            {
                MeshFilter meshFilter = bush.GetComponent<MeshFilter>();
                float maxMeshHeight = 1f;

                if (meshFilter != null)
                {
                    maxMeshHeight = meshFilter.mesh.bounds.size.y;
                }

                Vector3 bushPosition = new Vector3(Random.Range(x * tileXOffset - tileXOffset / 2, x * tileXOffset + tileXOffset / 2),
                                                   maxHeight + maxMeshHeight / 2,
                                                   Random.Range(z * tileZOffset - tileZOffset / 2, z * tileZOffset + tileZOffset / 2));

                Collider[] colliders = Physics.OverlapSphere(bushPosition, 2f);

                bool canPlaceBush = true;
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea") || collider.CompareTag("Sand"))
                    {
                        canPlaceBush = false;
                        break;
                    }
                }

                if (canPlaceBush)
                {
                    GameObject bushInstance = Instantiate(bush, bushPosition, Quaternion.identity, parentTransform);

                    if (colliders.Length == 0 || colliders[0].CompareTag("Plains"))
                    {
                        Debug.Log("Bush placed correctly");
                    }
                    else
                    {
                        Destroy(bushInstance);
                        Debug.Log("Bush removed due to incorrect placement");
                    }
                }
                else
                {
                    Debug.Log("Bush removed due to incorrect placement");
                }
            }
        }

        yield return null;
    }

    IEnumerator placeTrees(float tileXOffset, float tileZOffset, GameObject tree, Transform parentTransform, int x, int z)
    {
        float treeCount = Random.Range(3f, 10f);

        for (int y = 0; y < treeCount; y++)
        {
            MeshFilter meshFilter = tree.GetComponent<MeshFilter>();
            float maxMeshHeight = 1f;

            if (meshFilter != null)
            {
                maxMeshHeight = meshFilter.mesh.bounds.size.y;
            }

            Vector3 treePosition = new Vector3(Random.Range(x * tileXOffset - tileXOffset / 2, x * tileXOffset + tileXOffset / 2),
                                               0,
                                               Random.Range(z * tileZOffset - tileZOffset / 2, z * tileZOffset + tileZOffset / 2));
            treePosition += new Vector3(0, maxMeshHeight / 2, 0);

            Collider[] colliders = Physics.OverlapSphere(treePosition, 2f);
            int size = colliders.Length;

            bool canPlaceTree = true;
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea") || collider.CompareTag("Sand"))
                {
                    canPlaceTree = false;
                    break;
                }
            }

            if (canPlaceTree)
            {
                GameObject treeInstance = Instantiate(tree, treePosition, Quaternion.identity, parentTransform);
                print("Tree Planted");
            }
            else
            {
                print("Tree Destroyed");
            }
        }
        yield return null;
    }

    IEnumerator placeBoulders(float tileXOffset, float tileZOffset, GameObject boulder, Transform parentTransform, int x, int z)
    {
        float boulderCount = Random.Range(3f, 4f);
        Vector3 rayOrigin = new Vector3(x * tileXOffset, 100f, z * tileZOffset); // raise the raycast origin to a high altitude

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hitInfo, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            float maxHeight = hitInfo.point.y;
            Debug.Log("Raycast Works");

            for (int i = 0; i < boulderCount; i++)
            {
                float maxMeshHeight = 1f;
                MeshFilter meshFilter = boulder.GetComponent<MeshFilter>();
                if (meshFilter != null)
                {
                    maxMeshHeight = meshFilter.mesh.bounds.size.y;
                    Debug.Log(maxMeshHeight);
                }

                Vector3 boulderPosition = new Vector3(Random.Range(x * tileXOffset - tileXOffset / 2f, x * tileXOffset + tileXOffset / 2f),
                                                    maxHeight + maxMeshHeight / 2f,
                                                    Random.Range(z * tileZOffset - tileZOffset / 2f, z * tileZOffset + tileZOffset / 2f));
                Collider[] colliders = Physics.OverlapSphere(boulderPosition, maxMeshHeight, LayerMask.GetMask("Mountains", "Deep Sea", "Shallow Sea", "Sand"));

                if (colliders.Length == 0)
                {
                    GameObject boulderInstance = Instantiate(boulder, boulderPosition, Quaternion.identity, parentTransform);
                    Debug.Log("Boulder Placed");
                }
                else
                {
                    bool canPlaceBoulder = false;
                    foreach (Collider collider in colliders)
                    {
                        if (collider.CompareTag("Mountains"))
                        {
                            canPlaceBoulder = true;
                            break;
                        }
                        else if (collider.CompareTag("Deep Sea") || collider.CompareTag("Shallow Sea") || collider.CompareTag("Sand"))
                        {
                            Debug.Log("Boulder Destroyed");
                            break;
                        }
                    }

                    if (canPlaceBoulder)
                    {
                        GameObject boulderInstance = Instantiate(boulder, boulderPosition, Quaternion.identity, parentTransform);
                        Debug.Log("Boulder Placed");
                    }
                    else
                    {
                        Debug.Log("Void Boulder Destroyed");
                    }
                }
            }
        }

        yield return null;
    }

}

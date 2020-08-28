using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    //
    public GameObject[] mapItems;
    public GameObject[] hillItems;
    private float xyDiff = 1;
    private float homeSiteX = -1.25f;
    private float homeSiteY = -8;
    //private List<Vector3> hasItemPositions = new List<Vector3>();

    private void Awake()
    {
        createHome();
        createObstacles();
        createGround();
        createBoundry();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
    }


    private void createItem(GameObject gameOb, Vector3 site, Quaternion rotation)
    {
        GameObject item = Instantiate(gameOb, site, rotation);
        //item.transform.SetParent(gameObject.transform);
        //hasItemPositions.Add(site);
    }

    private void createHome()
    {
        createItem(mapItems[4], new Vector3(45, -45, 0), Quaternion.identity);
    }

    private void createGround()
    {
        for (int x = 0; x < 50; x++)
        {
            for (int y = 0; y < 50; y++)
            {
                createItem(mapItems[0], new Vector3(x, y, 1), Quaternion.identity);
                createItem(mapItems[0], new Vector3(-x, -y, 1), Quaternion.identity);
                createItem(mapItems[0], new Vector3(x, -y, 1), Quaternion.identity);
                createItem(mapItems[0], new Vector3(-x, y, 1), Quaternion.identity);
            }
        }
    }

    private void createObstacles()
    {
        //generate Obstacles randomly, but not make a dead way
        // generate hill
        createHill(25, 35);
        createHill(-15, 37);
        createHill(-26, -18);
        createHill(-36, -20);
        createHill(44, 7);
        createHill(17, -23);
        createHill(3, 7);
        createHill(-29, 12);
        createHill(-11, -40);

        // generate fence
        for (int x = -33; x<3; x++)
        {
            createItem(mapItems[1], new Vector3(x, 24, 0), Quaternion.identity);
        }

        for (int y = -23; y < 1; y++)
        {
            createItem(mapItems[5], new Vector3(-8, y, 0), Quaternion.identity);
        }

        for (int y = -13; y < 20; y++)
        {
            createItem(mapItems[5], new Vector3(24, y, 0), Quaternion.identity);
        }

    }

    private void createHill(int x, int y)
    {
        Quaternion mirror = new Quaternion(0, 180, 0, 0);
        // generate grass
        createItem(hillItems[0], new Vector3(x, y, 0), Quaternion.identity);
        createItem(hillItems[0], new Vector3(x+1, y, 0), Quaternion.identity);
        createItem(hillItems[0], new Vector3(x+2, y, 0), Quaternion.identity);
        createItem(hillItems[0], new Vector3(x-1, y, 0), Quaternion.identity);
        createItem(hillItems[0], new Vector3(x, y-1, 0), Quaternion.identity);
        createItem(hillItems[0], new Vector3(x+1, y-1, 0), Quaternion.identity);
        createItem(hillItems[0], new Vector3(x+2, y-1, 0), Quaternion.identity);
        createItem(hillItems[0], new Vector3(x-1, y-1, 0), Quaternion.identity);

        // generate stone
        createItem(hillItems[1], new Vector3(x, y-2, 0), Quaternion.identity);
        createItem(hillItems[1], new Vector3(x+1, y-2, 0), Quaternion.identity);
        createItem(hillItems[1], new Vector3(x+2, y-2, 0), Quaternion.identity);
        createItem(hillItems[1], new Vector3(x-1, y-2, 0), Quaternion.identity);
        createItem(hillItems[1], new Vector3(x, y-3, 0), Quaternion.identity);
        createItem(hillItems[1], new Vector3(x+1, y-3, 0), Quaternion.identity);
        createItem(hillItems[1], new Vector3(x+2, y-3, 0), Quaternion.identity);
        createItem(hillItems[1], new Vector3(x-1, y-3, 0), Quaternion.identity);

        // generate bottom
        createItem(hillItems[2], new Vector3(x - 2, y - 3, 0), Quaternion.identity);
        createItem(hillItems[2], new Vector3(x + 3, y - 3, 0), mirror);

        // generate corner
        createItem(hillItems[3], new Vector3(x - 2, y, 0), Quaternion.identity);
        createItem(hillItems[3], new Vector3(x + 3, y, 0), mirror);

        // generate edge
        createItem(hillItems[4], new Vector3(x-2, y - 1, 0), Quaternion.identity);
        createItem(hillItems[4], new Vector3(x-2, y - 2, 0), Quaternion.identity);
        createItem(hillItems[4], new Vector3(x+3, y - 1, 0), mirror);
        createItem(hillItems[4], new Vector3(x+3, y - 2, 0), mirror);

    }

    private void createBoundry()
    {
        for (int x = 0; x < 50; x++)
        {
            createItem(mapItems[2], new Vector3(x, 50, 0), Quaternion.identity);
            createItem(mapItems[2], new Vector3(-x, 50, 0), Quaternion.identity);
            createItem(mapItems[2], new Vector3(x, -50, 0), Quaternion.identity);
            createItem(mapItems[2], new Vector3(-x, -50, 0), Quaternion.identity);
        }

        for (int y = 0; y < 50; y++)
        {
            createItem(mapItems[3], new Vector3(50, -y, 0), Quaternion.identity);
            createItem(mapItems[3], new Vector3(50, y, 0), Quaternion.identity);
            createItem(mapItems[3], new Vector3(-50, y, 0), Quaternion.identity);
            createItem(mapItems[3], new Vector3(-50, -y, 0), Quaternion.identity);
        }
    }
}

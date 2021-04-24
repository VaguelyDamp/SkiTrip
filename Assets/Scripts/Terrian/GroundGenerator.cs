using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour
{
    public int NumTilesLeft = 1;
    public int NumTilesRight = 1;
    public int NumTilesDeep = 10;

    public int tileWidth = 30;

    public GameObject tilePrefab;

    public void Generate()
    {
        for (int i = 0; i < NumTilesDeep; i++)
        {
            Instantiate(tilePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + (tileWidth / 2) + (i * tileWidth)), transform.rotation, this.transform);

            for (int j = 0; j < NumTilesLeft; j++)
            {
                Instantiate(tilePrefab, new Vector3(transform.position.x - (tileWidth) - (j * tileWidth), transform.position.y, transform.position.z + (tileWidth / 2) + (i * tileWidth)), transform.rotation, this.transform);
            }
            for (int j = 0; j < NumTilesRight; j++)
            {
                Instantiate(tilePrefab, new Vector3(transform.position.x + (tileWidth) + (j * tileWidth), transform.position.y, transform.position.z + (tileWidth / 2) + (i * tileWidth)), transform.rotation, this.transform);
            }
        }
    }
}

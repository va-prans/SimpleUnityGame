using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGround : MonoBehaviour
{
    public GonzalesMovement gonzales;
    public FireWave fireWave;
    public List<Tile> tiles = new List<Tile>();

    public Tile tilePrefab;
    public int initialTileCount = 10;

    public float spawnDistance = 10;

    float speedModifier = 1.02f;

    bool reachedLeftEnd;
    bool reachedRightEnd;

    float tileLeftX;
    float tileRightX;

    int leftTileCount;
    int rightTileCount;

    int maxLeftTileCount = 0;
    int maxRghtTileCount = 0;

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < initialTileCount; i++)
        {
            Tile newTile = Instantiate(tilePrefab);
            newTile.transform.parent = transform;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            tiles.Add(transform.GetChild(i).GetComponent<Tile>());
        }

        foreach (Tile tile in tiles)
        {
            tile.transform.localPosition = new Vector2(tileRightX, tile.transform.localPosition.y);
            tileRightX += tile.GetSpriteSize();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distanceFromLeftX = gonzales.transform.position.x - tileLeftX;
        float distanceFromRightX = tileRightX - gonzales.transform.position.x;
        if (gonzales.transform.position.y <= -10)
        {
            gonzales.transform.position = new Vector3(tileLeftX, 10);  
        }

        if (distanceFromLeftX < spawnDistance)
        {
            if (leftTileCount < maxLeftTileCount)
            {
                AddToLeftEnd();
            }
            else
            {
                // Once we reach the left end
                if (!reachedLeftEnd)
                {
                    reachedLeftEnd = true;
                    gonzales.moveSpeed *= speedModifier;
                    reachedRightEnd = false;
                    rightTileCount = 0;
                    maxRghtTileCount--;

                    fireWave.StartFireWave(false, new Vector2(tileLeftX - 12.4f, gonzales.transform.position.y), gonzales.moveSpeed);

                    if (maxRghtTileCount <= 0)
                    {
                        Tile tile = tiles[tiles.Count - 1];
                        spawnDistance = tile.GetSpriteSize() * 2;
                        tiles.Remove(tile);
                        tileRightX -= tile.GetSpriteSize();
                        Destroy(tile.gameObject);
                    }
                }

            }
        }
        else if (distanceFromRightX < spawnDistance)
        {
            if (rightTileCount < maxRghtTileCount)
            {
                AddToRightEnd();
            }
            else
            {
                // Once we reach the right end
                if (!reachedRightEnd)
                {
                    reachedRightEnd = true;
                    gonzales.moveSpeed *= speedModifier;
                    reachedLeftEnd = false;
                    leftTileCount = 0;
                    maxLeftTileCount--;

                    fireWave.StartFireWave(true, new Vector2(tileRightX + 1.4f, gonzales.transform.position.y), gonzales.moveSpeed);

                    if (maxLeftTileCount <= 0)
                    {
                        Tile tile = tiles[0];
                        spawnDistance = tile.GetSpriteSize() * 2;
                        tiles.Remove(tile);
                        tileLeftX += tile.GetSpriteSize();
                        Destroy(tile.gameObject);
                    }
                }

            }
        }
	}

    void AddToLeftEnd()
    {
        Tile lastTile = tiles[tiles.Count - 1];
        tiles.RemoveAt(tiles.Count - 1);
        tiles.Insert(0, lastTile);
        tileLeftX -= lastTile.GetSpriteSize();
        lastTile.transform.localPosition = new Vector2(tileLeftX, lastTile.transform.localPosition.y);
        tileRightX -= lastTile.GetSpriteSize();
        leftTileCount++;
    }

    void AddToRightEnd()
    {
        Tile firstTile = tiles[0];
        tiles.RemoveAt(0);
        tiles.Add(firstTile);
        firstTile.transform.localPosition = new Vector2(tileRightX, firstTile.transform.localPosition.y);
        tileLeftX += firstTile.GetSpriteSize();
        tileRightX += firstTile.GetSpriteSize();
        rightTileCount++;
    }
}

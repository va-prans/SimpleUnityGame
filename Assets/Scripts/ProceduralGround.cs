﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGround : MonoBehaviour
{
    public GonzalesMovement gonzales;
    public List<Tile> tiles = new List<Tile>();

    public Tile tilepPrefab;
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
            Tile newTile = Instantiate(tilepPrefab);
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

        if (distanceFromLeftX < spawnDistance)
        {
            if (leftTileCount < maxLeftTileCount)
            {
                AddToLeftEnd();
            }
            else
            {
                if (!reachedLeftEnd)
                {
                    gonzales.moveSpeed *= speedModifier;
                    reachedLeftEnd = true;
                    reachedRightEnd = false;
                    rightTileCount = 0;
                    maxRghtTileCount--;

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
                if (!reachedRightEnd)
                {
                    gonzales.moveSpeed *= speedModifier;
                    reachedLeftEnd = false;
                    reachedRightEnd = true;
                    leftTileCount = 0;
                    maxLeftTileCount--;

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBorders : MonoBehaviour
{
    public Transform minX, maxX, minY, maxY;

    public Vector2 ChooseRandomSpot()
    {
        Vector2 randomPos = new Vector2(
            Random.Range(minX.position.x + 5f, maxX.position.x - 5f),
            Random.Range(minY.position.y + 5f, maxY.position.y - 5f));

        return randomPos;
    }
}

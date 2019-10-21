using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Ball", menuName ="Ball")]
public class Ball : ScriptableObject
{
    public float speed;
    public float pointsForDestroy;
    public Sprite sprite;
}

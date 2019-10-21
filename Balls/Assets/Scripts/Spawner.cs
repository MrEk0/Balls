using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> itemsToSpawn;
    [SerializeField] float timeBetweenSpawns = 2f;
    [SerializeField] float screenOffset = 1f;

    float timeSinceLastSpawn = 0f;
    float startMultiplier = 1f;

    float minWidth;
    float maxWidth;

    private void OnEnable()
    {
        GameManager.instance.OnTimeDown += ChangeStartSpeed;
    }

    private void OnDisable()
    {
        GameManager.instance.OnTimeDown -= ChangeStartSpeed;
    }

    private void Awake()
    {
        minWidth = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x+screenOffset;
        maxWidth = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x-screenOffset;
    }


    // Update is called once per frame
    void Update()
    {
        if(timeSinceLastSpawn>timeBetweenSpawns)
        {
            Spawn();
            timeSinceLastSpawn = 0f;
        }
        timeSinceLastSpawn += Time.deltaTime;
    }

    private void Spawn()
    {
        int index = Random.Range(0, itemsToSpawn.Count);

        float randomPositionX = Random.Range(minWidth, maxWidth);

        Vector3 startPosition = new Vector3(randomPositionX, transform.position.y, 0);
        GameObject ball = Instantiate(itemsToSpawn[index], startPosition, transform.rotation);
        ball.GetComponent<BallItem>().startMultiplier = startMultiplier;

        GameManager.instance.AddBall(ball);
    }

    private void ChangeStartSpeed(float multiplier)
    {
        startMultiplier *= multiplier;
    }
}

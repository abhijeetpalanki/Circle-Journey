using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public static Queue<GameObject> Obstacles;

    public int poolSize = 50;
    public float speed = 10f;
    public float smooth = 5f;

    public Vector2 WidthRange = new Vector2(3f, 3f);
    public Vector2 HeightRange = new Vector2(2.3f, 4.3f);

    public Transform ObstacleContainer;
    public GameObject Obstacle;

    private Vector3 startPosition;
    private GameObject top, bottom;

    private float topHeight, topWidth;
    private float bottomHeight, bottomWidth;

    private float topInterval
    {
        get => (topWidth - smooth / speed) / speed;        
    }

    private float bottomInterval
	{
		get => (bottomWidth - smooth / speed) / speed;
	}

    private Vector3 topScale
    {
        get => new Vector3(topWidth, topHeight, 1f);
    }

    private Vector3 bottomScale
	{
		get => new Vector3(bottomWidth, bottomHeight, 1f);
	}

	private void Awake()
	{
		startPosition = new Vector3(15f, 0f, 0f);
        FillPool();
	}

	private void Start()
	{
		StartCoroutine(TopRandomGenerator());
        StartCoroutine(BottomRandomGenerator());
	}

	private void FillPool()
    {
        Obstacles = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject clone = Instantiate(Obstacle, startPosition, Quaternion.identity, ObstacleContainer);
            clone.SetActive(false);

            Obstacles.Enqueue(clone);
        }
    }

    private void UpdateSpeed()
    {
        ObstacleMover.Speed = speed;
    }

    private GameObject GetObstacle() 
    {
        GameObject clone = Obstacles.Dequeue();
        clone.transform.position = startPosition;
        UpdateSpeed();

        return clone;
    }

    private void UpdateTopTransform()
    {
        top.transform.localScale = topScale;
        top.transform.position = new Vector3(top.transform.position.x, 5 - top.transform.localScale.y / 2f, 0f);
	}

	private void UpdateBottomTransform()
	{
		bottom.transform.localScale = bottomScale;
		bottom.transform.position = new Vector3(bottom.transform.position.x, -5 + bottom.transform.localScale.y / 2f, 0f);
	}

    private IEnumerator TopRandomGenerator()
    {
        topWidth = WidthRange.x;

        while (true)
        {
            top = GetObstacle();
            topHeight = Random.Range(HeightRange.x, HeightRange.y);
            UpdateTopTransform();
            yield return new WaitForSeconds(topInterval);
            top.SetActive(true);
        }
	}

	private IEnumerator BottomRandomGenerator()
	{
		bottomWidth = WidthRange.x;

		while (true)
		{
			bottom = GetObstacle();
			bottomHeight = Random.Range(HeightRange.x, HeightRange.y);
			UpdateBottomTransform();
			yield return new WaitForSeconds(bottomInterval);
			bottom.SetActive(true);
		}
	}
}

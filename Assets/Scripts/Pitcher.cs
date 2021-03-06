﻿using UnityEngine;

public class Pitcher : MonoBehaviour
{
	public Target CirclePrefab;
	public Target SquarePrefab;

	public float BasicFrequency;
	public float MaxFrequency;

	private float _frequency;
	private float _timer;


	private void Start()
	{
		_frequency = BasicFrequency;
		_timer = BasicFrequency;

		ObjectPool.CreatePool(CirclePrefab, 5);
		ObjectPool.CreatePool(SquarePrefab, 5);
	}
	
	private void Update() 
	{
		if(_timer >= _frequency)
		{
			Vector2 circleSpawnPos = CalculateSpawnPosition();
			Vector2 squareSpawnPos = CalculateSpawnPosition();

			CirclePrefab.Spawn(transform, circleSpawnPos);
			SquarePrefab.Spawn(transform, squareSpawnPos);

			_timer = 0.0f;
		}

		_timer += Time.deltaTime;
	}

	private Vector2 CalculateSpawnPosition()
	{
		Vector3 spawnMinPos = transform.InverseTransformPoint(GetComponent<Collider2D>().bounds.max);
		Vector3 spawnMaxPos = transform.InverseTransformPoint(GetComponent<Collider2D>().bounds.min);

		float randValueY = Random.Range(spawnMinPos.y, spawnMaxPos.y);
		
		return new Vector3(0.0f, randValueY);
	}

	public void Accelerate()
	{
		float newFreq = _frequency - 0.05f * BasicFrequency * Statistics.Level;
		if(newFreq > MaxFrequency)
			_frequency = newFreq;
	}
}
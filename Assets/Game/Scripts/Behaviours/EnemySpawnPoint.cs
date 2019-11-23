using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum Enemies
{
	Skull,
	TargetDummy,
	Muncher,
	Bloop,
}

public class EnemySpawnPoint : MonoBehaviour
{
	public Enemies enemy;
}

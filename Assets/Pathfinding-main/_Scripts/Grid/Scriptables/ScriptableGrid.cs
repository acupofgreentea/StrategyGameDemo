using System.Collections.Generic;
using _Scripts.Tiles;
using UnityEngine;

namespace Tarodev_Pathfinding._Scripts.Grid.Scriptables
{
    public abstract class ScriptableGrid : ScriptableObject
    {
        [SerializeField] protected NodeBase nodeBasePrefab;
        [SerializeField, Range(0, 6)] private int _obstacleWeight = 3;
        [SerializeField] protected bool changeColor = true;
        public abstract Dictionary<Vector2, NodeBase> GenerateGrid(Transform parent);

        protected bool DecideIfObstacle() => true; //Random.Range(1, 20) > _obstacleWeight
    }
}
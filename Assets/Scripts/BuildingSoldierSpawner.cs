﻿using System.Collections;
using System.Collections.Generic;
using _Scripts.Tiles;
using UnityEngine;

public class BuildingSoldierSpawner : MonoBehaviour
{
    [SerializeField] private BuildingSoldierSpawnStatSO soldierSpawnStatSo;
    
    private BuildingBase buildingBase;

    private float lastSpawnTime;

    private readonly List<SoldierUnit> activeSoldiers = new List<SoldierUnit>();

    private SoldierPool soldierPool;

    private NodeBase spawnNode;

    private bool HasReachedMaxCapacity => activeSoldiers.Count >= soldierSpawnStatSo.MaxCount;
    public BuildingSoldierSpawner Init(BuildingBase buildingBase)
    {
        this.buildingBase = buildingBase;
        
        return this;
    }

    private IEnumerator Start()
    {
        soldierPool = SoldierPool.Instance;
        soldierPool.OnReturnToPool += HandleOnSoldierDie;
        lastSpawnTime = Time.time;
        SetSpawnPoint();
        yield return null;
    }

    private void SetSpawnPoint()
    {
        List<NodeBase> occupiedNodes = buildingBase.OccupiedNodes;
        
        List<NodeBase> allNeighBors = new List<NodeBase>();
        
        foreach (NodeBase nodeBase in occupiedNodes)
        {
            allNeighBors.AddRange(nodeBase.Neighbors);
        }

        spawnNode = allNeighBors.Find(x => x.IsAvailable && x.Walkable);
    }
    private void Update()
    {
        if (HasReachedMaxCapacity)
            return;

        if (!(Time.time >= lastSpawnTime + soldierSpawnStatSo.SpawnRate)) 
            return;
        
        lastSpawnTime = Time.time;

        SpawnSoldier();
    }

    private void SpawnSoldier()
    {
        UpdateSpawnPoint();
        SoldierUnit newSoldier = soldierPool.Get();

        activeSoldiers.Add(newSoldier);
        newSoldier.CurrentNode = spawnNode;
        Vector3 spawnPosition = spawnNode.transform.position;
        spawnPosition.z = 0;
        
        //I set spawnPosition's z axis to 0 because I want my soldiers to stay on top of grid so i can have priority select them before nodes.
        newSoldier.transform.position = spawnPosition;
    }

    private void UpdateSpawnPoint()
    {
        if(spawnNode.IsAvailable == false || spawnNode.Walkable == false)
            SetSpawnPoint();
    }

    private void HandleOnSoldierDie(SoldierUnit soldierUnit)
    {
        activeSoldiers.Remove(soldierUnit);
    }
}
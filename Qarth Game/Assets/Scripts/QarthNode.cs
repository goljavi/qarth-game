﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QarthNode : MonoBehaviour
{
    public Renderer rend;
    public Color originalColor;
    Material mat;
    List<Wall> walls;
    public bool inUse;

    // Start is called before the first frame update
    void Start()
    {
        rend.material.color = originalColor;
        mat = rend.material;
        walls = new List<Wall>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Selected(Player player)
    {
        if (walls.Count < 1) mat.color = player.playerColor;
    }

    public void Deselect()
    {
        if (walls.Count < 1) mat.color = originalColor;
    }

    public QarthNode WallConnect(Wall wall)
    {
        if (walls.Count < 1) mat.color = wall.parent.playerColor;
        walls.Add(wall);
        return this;
    }

    public QarthNode WallDisconnect(Wall wall)
    {
        walls.Remove(wall);
        if (walls.Count < 1) mat.color = originalColor;
        return this;
    }
}

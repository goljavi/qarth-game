using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QarthNode : MonoBehaviour
{
    public Color originalColor;

    public int fila;
    public int columna;

    Material mat;
    List<Wall> walls;
    public bool inUse;
    public bool nodePlayer1;
    Renderer rend;
    public GameObject[] linesNode;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentInParent<Renderer>();
        rend.material.color = originalColor;
        mat = rend.material;
        walls = new List<Wall>();
        for (int i = 0; i < linesNode.Length; i++)
        {
            linesNode[i].SetActive(false);
        }
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
        inUse = true;
        if (walls.Count < 1) mat.color = wall.parent.playerColor;
        walls.Add(wall);
        if (walls.Count > 0) walls.ForEach(x => { x.life += walls.Count - 1; });
        return this;
    }

    public QarthNode WallDisconnect(Wall wall)
    {
        inUse = false;
        walls.Remove(wall);
        if (walls.Count < 1) mat.color = originalColor;

        var count = walls.Count;
        if (walls.Count < 1) mat.color = originalColor;
        if (count > 0) walls.ForEach(x => x.life -= count);

        return this;
    }

    public bool DisapproveConnection(QarthNode node)
    {
        var exceptionCol = columna + node.columna != 7;
        return walls.Any(
           x => x.Node1 == node 
        || x.Node2 == node) 
        || node.columna == columna 
        || node.fila < fila - 1 
        || node.fila > fila + 1 
        || exceptionCol && node.columna > columna + 1
        || exceptionCol && node.columna < columna - 1;
    }
}

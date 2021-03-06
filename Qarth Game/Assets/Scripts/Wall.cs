﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public int life;
    public bool violetWall;
    public float speed;
    public float threshold;

    public AudioSource destroyAudiosrc;

    LinkedListNode<Wall> wallNode;

    public QarthNode Node1;
    public QarthNode Node2;

    QarthNode currentNodeDir;
    Vector3 currentDir;

    public Player parent;

    public Color colorViolet;

    void Update()
    {
        if (!Node1) return;
        if (Vector3.Distance(transform.position, currentDir) < threshold)
        {

            if (currentNodeDir == Node1)
            {
                currentNodeDir = Node2;
                currentDir = Node2.transform.position;
            }
            else
            {
                currentNodeDir = Node1;
                currentDir = Node1.transform.position;
            }
        }
        transform.position = Vector3.Lerp(transform.position, currentDir, Time.deltaTime * speed);

        if (life < 1) Disconnect();
    }

    public void SetWall(QarthNode Node1, QarthNode Node2, LinkedListNode<Wall> wallNode, Player _parent)
    {
        this.Node1 = Node1;
        this.Node2 = Node2;
        this.wallNode = wallNode;
        this.parent = _parent;
        transform.position = Node2.transform.position;
        currentDir = Node1.transform.position;
        currentNodeDir = Node1;
        GetComponent<TrailRenderer>().materials[0].SetColor("_BaseColor", _parent.playerColor);
        GetComponent<ParticleSystemRenderer>().material.SetColor("_BaseColor", _parent.playerColor);

        if(Node1.nodePlayer1 && !Node2.nodePlayer1 || !Node1.nodePlayer1 && Node2.nodePlayer1)
        {
            GetComponent<TrailRenderer>().materials[0].SetColor("_BaseColor", colorViolet);
            GetComponent<ParticleSystemRenderer>().material.SetColor("_BaseColor", colorViolet);
            violetWall = true;
        }

        this.Node1.WallConnect(this);
        this.Node2.WallConnect(this);
    }

    public void Disconnect()
    {
        Node1.WallDisconnect(this);
        Node2.WallDisconnect(this);
        UIManager.Instance.ChangeUI(parent.gameObject.GetComponent<MovementPlayers>().player1,parent.walls.Count);

        var wallNode = parent.walls.Find(this);
        if (wallNode != null) parent.walls.Remove(wallNode);

        destroyAudiosrc.Play();
        GetComponent<TrailRenderer>().enabled = false;
        GetComponent<ParticleSystem>().Stop();
        Invoke("RealDeath", 2f);
    }

    private void RealDeath()
    {
        Destroy(gameObject);
    }

    public void Hit()
    {
        life -= 1;
    }
}

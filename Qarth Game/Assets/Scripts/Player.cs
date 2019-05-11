using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public Rigidbody rb;
    public GameObject paredPrefab;
    public Color playerColor;
    public Renderer rend;

    [HideInInspector] public Material playerMat;
    QarthNode currentNode;
    QarthNode linkedNode;
    LinkedList<Wall> walls;

    // Start is called before the first frame update
    void Start()
    {
        playerMat = rend.material;
        playerMat.color = playerColor;
        walls = new LinkedList<Wall>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) Connect();

        if (Input.GetKeyDown(KeyCode.G)) Disconnect();
    }

    void Connect()
    {
        if (currentNode && linkedNode && currentNode != linkedNode)
        {
            ConnectNodes();
        }
        else if (currentNode)
        {
            linkedNode = currentNode;
            currentNode.Selected(this);
        }
        else
        {
            linkedNode.Deselect();
            linkedNode = null;
        }
    }

    void Disconnect()
    {
        if (walls.Count < 1) return;
        walls.First.Value.Disconnect();
        walls.RemoveFirst();
    }

    void ConnectNodes()
    {
        if (currentNode.inUse && linkedNode.inUse) return;
        var wall = Instantiate(paredPrefab).GetComponent<Wall>();
        wall.SetWall(currentNode, linkedNode, walls.AddLast(wall), this);
        linkedNode = null;
    }

    void FixedUpdate()
    {
        float mH = Input.GetAxis("Horizontal");
        float mV = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(mH * speed, rb.velocity.y, mV * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Node")
        {
            currentNode = other.gameObject.GetComponent<QarthNode>();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Node")
        {
            currentNode = null;
        }
    }
}

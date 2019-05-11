using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int wallLimit = 4;
    public float speed;
    public Rigidbody rb;
    public GameObject paredPrefab;
    public Color playerColor;
    public Renderer rend;

    [HideInInspector] public Material playerMat;
    QarthNode currentNode;
    QarthNode linkedNode;
    LinkedList<Wall> walls;
    LineRenderer _lr;
    bool _connecting;

    // Start is called before the first frame update
    void Start()
    {
        playerMat = rend.material;
        playerMat.color = playerColor;
        walls = new LinkedList<Wall>();
        _lr = GetComponent<LineRenderer>();
        _lr.enabled = false;
    }

    void SetupLine()
    {
        _lr.positionCount = 2;
        _lr.useWorldSpace = true;
    }

    // Update is called once per frame
    void Update()
    {
        WallLimitChecker();
        if (_connecting && linkedNode)
        {
            _lr.SetPosition(0, linkedNode.transform.position);
            _lr.SetPosition(1, transform.position);
        }
    }

    void WallLimitChecker()
    {
        if (walls.Count > wallLimit) Disconnect();
    }

    public void Connect()
    {
        if (currentNode && linkedNode && currentNode != linkedNode)
        {
            ConnectNodes();
        }
        else if (currentNode)
        {
            IsConnecting(true);
        }
        else if (linkedNode)
        {
            IsConnecting(false);
        }
    }

    public void IsConnecting(bool value)
    {
        if (value)
        {
            linkedNode = currentNode;
            currentNode.Selected(this);
            _connecting = true;
            _lr.enabled = true;
        }
        else
        {
            linkedNode.Deselect();
            linkedNode = null;
            _connecting = false;
            _lr.enabled = false;
        }
    }

    public void Disconnect()
    {
        if (walls.Count < 1) return;
        walls.First.Value.Disconnect();
        walls.RemoveFirst();
        _connecting = false;
    }

    void ConnectNodes()
    {
        if (currentNode.DisapproveConnection(linkedNode)) return;
        var wall = Instantiate(paredPrefab).GetComponent<Wall>();
        wall.SetWall(currentNode, linkedNode, walls.AddLast(wall), this);
        linkedNode = null;
        _connecting = false;
        _lr.enabled = false;
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

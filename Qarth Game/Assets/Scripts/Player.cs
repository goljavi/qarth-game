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
    private Renderer rend;

    public AudioSource useNodeAudiosrc;
    public AudioSource usingNodeAudiosrc;
    public AudioSource errorNodeAudiosrc;

    [HideInInspector] public Material playerMat;
    QarthNode currentNode;
    QarthNode linkedNode;
    public LinkedList<Wall> walls;
    LineRenderer _lr;
    bool _connecting;

    private void Awake()
    {
        rend = GetComponentInChildren<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMat = rend.material;
        playerMat.SetColor("_BaseColor", playerColor);
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        var main = ps.main;
        main.startColor = playerColor;
        GetComponentInChildren<TrailRenderer>().materials[0].SetColor("_BaseColor", playerColor);
        GetComponentInChildren<Renderer>().materials[0].SetColor("_ColorBase", playerColor);
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
        UIManager.Instance.ChangeUI(gameObject.GetComponent<MovementPlayers>().player1, walls.Count);
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
            usingNodeAudiosrc.Play();
            linkedNode = currentNode;
            currentNode.Selected(this);
            _connecting = true;
            _lr.enabled = true;
        }
        else
        {
            usingNodeAudiosrc.Stop();
            linkedNode.Deselect();
            linkedNode = null;
            _connecting = false;
            _lr.enabled = false;
        }
    }

    public void Disconnect()
    {
        if (walls.Count < 1)
        {
            errorNodeAudiosrc.Play();
            return;
        }
        walls.First.Value.Disconnect();
        walls.RemoveFirst();
        _connecting = false;
    }

    void ConnectNodes()
    {
        if (currentNode.DisapproveConnection(linkedNode))
        {
            errorNodeAudiosrc.Play();
            return;
        }
        if (!currentNode.inUse)
        {
            currentNode.nodePlayer1 = gameObject.GetComponent<MovementPlayers>().player1;
        }
        if (!linkedNode.inUse)
        {
            linkedNode.nodePlayer1 = gameObject.GetComponent<MovementPlayers>().player1;
        }
        useNodeAudiosrc.time = 1.2f;
        useNodeAudiosrc.Play();
        usingNodeAudiosrc.Stop();
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
            currentNode.GetComponentInChildren<Renderer>().material.SetFloat("_ActiveLight", 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Node")
        {
            other.gameObject.GetComponent<QarthNode>().GetComponentInChildren<Renderer>().material.SetFloat("_ActiveLight", 0);
            currentNode = null;
        }
    }
}

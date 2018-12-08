using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public bool reverseDiretion = false;
    public Color color = Color.white;

    [Range(0, 0.05f)]
    public float x = 0.05f;
    [Range(0, 1f)]
    public float minX = 0f;
    [Range(0, 1f)]
    public float maxX = 1f;

    [Range(0, 0.1f)]
    public float y = 0.05f;
    [Range(0, 1f)]
    public float minY = 0f;
    [Range(0, 1f)]
    public float maxY = 1f;

    private GameObject lightGo;

    private Mesh mesh;
    private Material material;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private Vector3 p1;
    private Vector3 p2;
    
    void Awake()
    {
        lightGo = GameObject.Find("Light");

    }

    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        material = new Material(Shader.Find("Sprites/Default"));
    }

    private void Update()
    {
        material.SetColor("_Color", color);
        meshRenderer.sharedMaterial = material;

        var scale = transform.lossyScale.x;

        var d = transform.position - lightGo.transform.position;
        var offset = d.normalized * Mathf.Clamp(d.magnitude * y, minY, maxY) * scale;
        offset.y = -Mathf.Clamp(Mathf.Abs(offset.y), minY * scale, maxY * scale);
        offset.x = Mathf.Clamp(offset.x, -1, 1);
        var center = transform.position + offset;

        var vertexs = new Vector3[4];
        p1 = transform.position + new Vector3(-0.135f, 0f) * scale;
        vertexs[0] = transform.InverseTransformPoint(p1);
        p2 = transform.position + new Vector3(0.105f, 0f) * scale;
        vertexs[1] = transform.InverseTransformPoint(p2);
        var p3 = center;
        p3.x -= Mathf.Clamp(d.magnitude * x, minX, maxX) * scale;
        vertexs[2] = transform.InverseTransformPoint(p3);
        var p4 = center;
        p4.x += Mathf.Clamp(d.magnitude * x, minX, maxX) * scale;
        vertexs[3] = transform.InverseTransformPoint(p4);

        var triangles = new int[6];
        triangles[0] = 0;
        triangles[1] = 3;
        triangles[2] = 1;
        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh = new Mesh();
        mesh.vertices = vertexs;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshFilter.sharedMesh = mesh;
    }

    private void OnDisable()
    {
        meshFilter.sharedMesh = null;
    }
}

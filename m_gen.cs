using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_gen : MonoBehaviour
{
    // Start is called before the first frame update

    //size_x
    public int size_x=0;
    //size_z
    public int size_z = 0;
    public float dis =1;
    
    //points
    Vector3[] points;
    //triangles
    public Dictionary<int,List<int>> in_tri;
    //vertex coordinates
   public Vector3[] vcord;
    List<int> triunghi;
    MeshFilter fil;

    void Start()
    {
        List<Vector2> uv = new List<Vector2>();
        var tw_aux= new List<Vector3>();
        //points in the mesh
        points = new Vector3[size_x * size_z];
        for (int i = 0; i < size_x; i++)
        {
            for (int j = 0; j < size_z; j++)
            {
                uv.Add(new Vector2(i * dis, j * dis));
                points[i * size_z + j] = new Vector3(i * dis, 0, j * dis);
                tw_aux.Add( transform.TransformPoint(points[i * size_z + j]));
            }
        }
        vcord = tw_aux.ToArray();
        Debug.Log(points.Length);
        

        //generating triangle list
        triunghi = new List<int>();
        for (int i = 0; i < (size_x-1)*size_z; i++)
        {
            if (i % size_x != size_x - 1)
            {
                triunghi.Add(i);
                triunghi.Add(i + 1);
                triunghi.Add(i + size_z);
            }
            if (i%size_x!=0)
            {
                triunghi.Add(i + size_z);
                triunghi.Add(i + size_z-1);
                triunghi.Add(i);
                
                
            }

        }
        
        Debug.Log(triunghi.ToString());
        fil = GetComponent<MeshFilter>();
        Debug.Log(triunghi.Count/3);
        
        fil.mesh = new Mesh();
        
        fil.mesh.vertices = points;fil.mesh.uv = uv.ToArray();
        fil.mesh.triangles = triunghi.ToArray();
        fil.mesh.RecalculateNormals();
        fil.mesh.RecalculateTangents();
        fil.mesh.RecalculateBounds();
        GetComponent<MeshCollider>().sharedMesh= fil.mesh;
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshCollider>().enabled = true;
    }



    // Update is called once per frame
    Vector3 old;
    void Update()
    {
        if(transform.position!=old)
        {
           for(int i=0;i<points.Length;i++)
            {
                vcord[i] = transform.TransformPoint(points[i]);
            }
        }
        old = transform.position;
    }
}

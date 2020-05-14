using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour
{
    private static Vector3[] iterationDirections = {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };

    private static Quaternion[] iterationRotation = {
        Quaternion.identity,
        Quaternion.Euler(0,0,-90f),
        Quaternion.Euler(0,0,90f),
        Quaternion.Euler(90f, 0f, 0f),
		Quaternion.Euler(-90f, 0f, 0f)
    };

    public Mesh mesh;
    public Material material;
    [SerializeField] private int maxDepth;
    private int depth;
    [Range(0,1)]
    [SerializeField] private float iterationScale;
    private void Start(){
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        if(depth < maxDepth){
            CreateIteration();
        }
    }

    private void CreateIteration(){
        for (int i = 0; i < iterationDirections.Length; i++)
        {
            new GameObject("Fractal Object").AddComponent<Fractal>().Initialize(this, i);
        }
    }

    private void Initialize(Fractal parent,int index){
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        iterationScale = parent.iterationScale;
        transform.parent = parent.transform;
        transform.localScale = Vector3.one * iterationScale;
        transform.localPosition = iterationDirections[index] * (0.5f + 0.5f * iterationScale);
        transform.localRotation= iterationRotation[index];
    }
}

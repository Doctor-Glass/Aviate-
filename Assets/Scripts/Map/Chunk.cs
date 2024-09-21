using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Chunk
{
    [SerializeField]
    public GameObject chunkObject;      // The GameObject for this chunk
    [SerializeField]
    public int depth;                   // The depth in the tree for this chunk
    [SerializeField]
    public float size;                  // The size for this chunk, as a multiple of the size of the root chunk
    [SerializeField]
    public Chunk parent;                // The parent of this chunk
    [NonSerialized]
    public Chunk[] children;            // The children of this chunk
    [SerializeField]
    public bool isVisible;              // Whether or not this chunk is visible

    // Constructor for a Chunk object
    public Chunk(Vector2 _position, Chunk _parent = null)
    {
        isVisible = true;

        if (_parent != null) {
            depth = _parent.depth + 1;
            size = _parent.size / 2;
            parent = _parent;
        }
        else {
            depth = 0;
            size = 1.0f;
            parent = null;
        }

        chunkObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
        if (parent != null) {
            chunkObject.transform.SetParent(parent.chunkObject.transform);
        }
        chunkObject.transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);
        chunkObject.transform.localPosition = _position;
    }

    // Method to generate the terrain object and terrain texture
    public void GenerateTerrain() 
    {
        
    }
    
    // Method to check the distance from an object (ie. camera) to this chunk and, if necessary, subdivide this chunk
    public bool CheckDist(Transform obj, float limit) 
    {
        Vector3 dist = this.chunkObject.transform.position - obj.transform.position;

        if (dist.sqrMagnitude < Mathf.Pow(limit, 2)) {
            return true;
        }

        return false;
    }
}

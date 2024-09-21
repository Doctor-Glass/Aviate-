using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class ChunkTree : MonoBehaviour
{
    public List<Chunk> chunkTree;   // Tree to store all of the chunks
    public float threshold;         // Threshold distance to subdivide a chunk
    public int limit;               // Limit on subdivisions

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Chunk chunk in chunkTree) {
            if (chunk.depth >= limit) {
                continue;
            }

            if (chunk.isVisible != true) {
                continue;
            }
            
            CheckDist(chunk);
        }
    }

    void SubdivideChunk(Chunk chunk) 
    {
        chunk.children = new Chunk[4];

        chunk.children[0] = new Chunk(new Vector2(-0.25f,  0.25f), chunk);
        chunk.children[1] = new Chunk(new Vector2( 0.25f,  0.25f), chunk);
        chunk.children[2] = new Chunk(new Vector2( 0.25f, -0.25f), chunk);
        chunk.children[3] = new Chunk(new Vector2(-0.25f, -0.25f), chunk);

        for (int i = 0; i < 4; i++) {
            chunk.children[i].GenerateTerrain();
            chunkTree.Add(chunk.children[i]);
        }

        chunk.isVisible = false;
    }

    void CondenseChunk(Chunk chunk) 
    {
        for (int i = 0; i < 4; i++) {
            if (chunk.children != null && chunk.children[i].isVisible == false) {
                CondenseChunk(chunk.children[i]);
            }

            Destroy(chunk.children[i].chunkObject);
            chunkTree.Remove(chunk.children[i]);
            chunk.children[i] = null;
        }

        chunk.isVisible = true;
    }

    void CheckDist(Chunk chunk) 
    {
        Vector3 dist = chunk.chunkObject.transform.position - this.transform.position;

        if (dist.sqrMagnitude < Mathf.Pow(threshold * chunk.size, 2)) {
            SubdivideChunk(chunk);
        }
        else if (chunk.depth != 0 && dist.sqrMagnitude > (Mathf.Pow(threshold * chunk.parent.size, 2) * 2)) {
            CondenseChunk(chunk.parent);
        }
    }
}

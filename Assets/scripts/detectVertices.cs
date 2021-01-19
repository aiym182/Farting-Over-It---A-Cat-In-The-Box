using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class detectVertices : MonoBehaviour {


	MeshFilter Mf;
	Vector3[] Vertices;
	EdgeCollider2D ec;
	Vector2[] twoDWorld;
	void Awake(){
		
		Mf = GetComponent<MeshFilter>();
		ec = GetComponent<EdgeCollider2D>();

		Vertices = Mf.mesh.vertices;
		twoDWorld = new Vector2[Vertices.Length];

	}
	
	void Start () {
		for(int i= 0; i <Vertices.Length; i ++){
		Vector3 world_v =transform.TransformPoint(Vertices[i]);
		 twoDWorld[i] = new Vector2(world_v.x,world_v.y);
		 
		}
		ec.points =twoDWorld;
	}
	

}

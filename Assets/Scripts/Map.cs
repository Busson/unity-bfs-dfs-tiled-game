using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {


	public GameObject tileRock;
	public GameObject tileHole;

	float tileSize = 0.32f;
	float tileScale = 3.0f;

	int [,] tiles = new int[10,20]{
		{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
		{1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
		{1,0,1,1,1,0,1,1,1,1,1,1,1,0,1,1,1,1,1,1},
		{1,0,1,1,0,0,1,1,1,1,1,1,0,0,0,0,1,1,1,1},
		{1,0,1,1,0,0,1,1,1,1,1,1,0,0,0,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
		{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
		{1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
		{1,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
		{1,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}
	};

	// Use this for initialization
	void Start () {
	    
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < 20; j++) {

				if (tiles [i, j] == 1)
					Instantiate (tileRock, new Vector2 (j*tileSize*tileScale,-1*i*tileSize*tileScale), Quaternion.identity);
				else if(tiles[i,j] == 0)
					Instantiate (tileHole, new Vector2 (j*tileSize*tileScale,-1*i*tileSize*tileScale), Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

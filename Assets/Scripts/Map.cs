using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	//tiles
	public GameObject tileRock;
	public GameObject tileHole;
	public GameObject tileDiamond;

	//tile estatico -> usado no metodo estatico
	private static GameObject tileRockStatic; 

	//tamanho do tile
	public static float tileSize = 0.32f;
	//escala do tile
	public static float tileScale = 3.0f;

	//tamanho do mapa: largura e altura
	public static int mapWidth=40;
	public static int mapHeight=15;

	//matriz que representa o mapa
	public static int[,] tiles;

	//método de inicializacao
	void Start () {

		// definicao do mapa
		tiles = new int[15,40]{
			{1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,2,1,1,1,1,1,1,1,0,2,1,1,1,1,0,2,1,1,1,1,1,0,1},
			{1,0,0,0,2,1,1,1,1,0,1,0,1,1,0,0,1,1,1,0,1,0,1,1,0,0,0,1,0,0,1,0,1,1,0,0,1,1,0,0},
			{2,0,2,0,0,0,0,0,1,1,2,0,0,0,0,0,0,1,1,0,2,0,2,0,0,0,0,1,1,1,2,0,0,0,0,0,0,1,1,0},
			{1,0,1,1,0,0,0,1,0,0,1,0,1,1,0,0,1,1,0,0,1,0,0,0,2,1,1,1,1,0,1,0,1,1,0,0,1,1,1,0},
			{1,1,1,1,1,0,2,1,0,0,1,0,2,1,1,1,1,1,0,1,1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,2,1,1},
			{0,0,1,1,1,1,1,1,1,0,0,0,0,0,1,1,1,1,0,1,2,0,0,0,0,1,1,1,1,0,0,1,0,0,0,0,0,1,0,1},
			{0,1,1,0,0,1,1,1,1,1,1,1,1,1,1,0,1,1,0,0,0,1,1,0,0,1,1,1,1,2,1,1,1,1,1,0,1,1,0,0},
			{0,0,1,2,0,1,1,1,1,0,0,0,2,1,0,0,0,1,1,1,1,1,1,1,1,1,1,1,0,0,1,1,1,1,0,1,1,0,0,2},
			{2,0,0,0,0,1,1,1,1,0,0,1,0,0,0,0,0,1,0,1,1,2,1,1,1,2,1,1,0,0,1,1,1,1,0,1,1,0,0,1},
			{1,1,1,1,1,1,2,1,1,1,1,1,1,1,1,2,1,1,0,2,1,1,1,0,0,0,1,1,0,0,1,1,2,0,0,1,1,0,0,1},
			{1,1,1,1,1,1,1,1,0,0,1,1,1,1,1,1,1,2,1,1,1,1,0,0,0,0,1,1,0,0,1,1,1,0,0,1,1,0,2,1},
			{1,0,0,0,2,1,1,1,1,0,1,0,1,1,0,0,1,1,1,0,1,1,0,0,0,1,1,1,0,0,1,1,1,1,1,1,1,0,1,1},
			{2,0,2,0,0,0,0,1,1,1,2,0,0,0,0,0,0,1,1,0,1,1,1,1,1,1,1,1,0,0,2,1,1,1,1,1,1,0,1,1},
			{1,0,1,1,0,0,0,1,0,0,1,0,1,1,0,0,1,1,0,0,1,2,1,1,0,0,0,2,0,0,1,1,0,0,0,2,1,0,1,1},
			{1,1,1,1,1,1,2,1,0,0,1,0,2,1,1,1,1,1,0,1,1,0,0,1,1,0,0,0,0,0,0,1,0,0,0,0,1,1,1,0}
		};


		tileRockStatic = tileRock;
	    
		//lendo o mapa para instanciar os tiles correspondentes:
		//1 - tile de pedra
		//0 - tile de buraco
		//2 - tile de diamante
		for (int i = 0; i < 15; i++) {
			for (int j = 0; j < 40; j++) {

				if (tiles [i, j] == 1)
					Instantiate (tileRock, new Vector2 (j*tileSize*tileScale,-1*i*tileSize*tileScale), Quaternion.identity).name=i+"-"+j;
				else if(tiles[i,j] == 0)
					Instantiate (tileHole, new Vector2 (j*tileSize*tileScale,-1*i*tileSize*tileScale), Quaternion.identity).name=i+"-"+j;
				else if(tiles[i,j] == 2)
					Instantiate (tileDiamond, new Vector2 (j*tileSize*tileScale,-1*i*tileSize*tileScale), Quaternion.identity).name=i+"-"+j;
			}
		}
	}

	//método para setar qualquer tile para pedra
	public static void SetTile(string objectName){

		//procura o tile q sera destruido
		GameObject tileToDestroy = GameObject.Find (objectName);
		//obtem a posicao do tile que sera destruido
		Vector3 position = tileToDestroy.transform.position;
		//destroi o tile
		Destroy (tileToDestroy);
		// instancia um novo tile de pedra na mesma posicao do anterior.
		Instantiate (tileRockStatic, position, Quaternion.identity).name = objectName;

	}
		
}

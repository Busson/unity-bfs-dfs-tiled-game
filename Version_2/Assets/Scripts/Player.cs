using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	//posicao real do player no mundo
	public static Vector3 realPosition;

	//posicao atual do player na matriz
	Vector2 position;

	// Use this for initialization
	void Start () {
		//pega a atual do player na matriz
		position = new Vector2 ( transform.position.x / (Map.tileSize * Map.tileScale) , -transform.position.y / (Map.tileSize * Map.tileScale) );
	}
	
	// Update is called once per frame
	void Update () {

		//obtem a posicao atual no mundo
		realPosition = transform.position;

		//seta baixo
		if(Input.GetKeyDown("down")){
			//antes de mover limpa o local anterior com 1 
			Map.tiles [(int)position.y, (int)position.x] = 1;
			//verifica se a posicao que eu vou é valida
			if(position.y < Map.mapHeight && Map.tiles[ (int)(position.y+1),(int)position.x]!=0)position.y++;
			//aplica a nova posicao
			transform.position = new Vector3 (position.x * Map.tileSize * Map.tileScale, -position.y * Map.tileSize * Map.tileScale, -1f);

		}
		//seta cima
		else if(Input.GetKeyDown("up")){

			Map.tiles [(int)position.y, (int)position.x] = 1;

			if(position.y > 0 && Map.tiles[ (int)(position.y-1),(int)position.x]!=0)position.y--;
			
			transform.position = new Vector3 (position.x * Map.tileSize * Map.tileScale, -position.y * Map.tileSize * Map.tileScale, -1f);

		}
		//seta esquerda
		else if(Input.GetKeyDown("left")){

			Map.tiles [(int)position.y, (int)position.x] = 1;

			if(position.x > 0 && Map.tiles[ (int)position.y,(int)(position.x-1)]!=0)position.x--;

			transform.position = new Vector3 (position.x * Map.tileSize * Map.tileScale, -position.y * Map.tileSize * Map.tileScale, -1f);

		}
		//seta direita
		else if(Input.GetKeyDown("right")){

			Map.tiles [(int)position.y, (int)position.x] = 1;

			if(position.x < Map.mapWidth && Map.tiles[ (int)position.y,(int)(position.x+1)]!=0)position.x++;

			transform.position = new Vector3 (position.x * Map.tileSize * Map.tileScale, -position.y * Map.tileSize * Map.tileScale, -1f);

		}

		//caso o tile seja 2, remove o diamante e coloca pedra
		if (Map.tiles [(int)position.y, (int)position.x] == 2) {
			//funcao estatica da classe Map que destroi um tile e poe um tile de pedra no lugar
			Map.SetTile ( ((int)position.y)+"-"+((int)position.x) );
		}

		//a posicao onde estou é marcada com 3, os bots procuram pelo tile 3
		Map.tiles [(int)position.y, (int)position.x] = 3;
	
	}
}

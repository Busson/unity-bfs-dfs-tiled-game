using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character1 : MonoBehaviour {

	public Color debugColor;

	public int SearchMetod;

	List<Vector2> traject;

	bool trajectTraced;

	//Estrutura usada pelo DFS e bFS
	List<Vector2> colorVectice;

	//Estrutura
	List<Vector2> callList; 

	//acmulador de tempo
	float accTime=0;


	//método que faz a chamada aos algoritmos BFS ou DFS
	void CalculeTraject(Vector2 position){
		//resetando as listas utilizadas;
		traject = new List<Vector2> ();
		colorVectice = new List<Vector2> ();
		callList = new List<Vector2> ();
		trajectTraced = false;

		//método de procura 0: DFS
		if (SearchMetod == 0)
			DFS (position);
		else //método de procura 1: BFS
			BFS (position,0);
	}


	//ALGORITMO VERCUSIVO BFS

	void BFS(Vector2 position, int index){

		//verificando se a posicao que estou na matriz é 2 (diamante)
		if (Map.tiles [(int)position.y, (int)position.x] == 3) {
			//caso entre neste if, já encontrei o diamante, devo terminar o algoritmo
			//insiro esta posicao no trajeto
			traject.Insert (0, position);

			//montar o restante da matriz de trajeto
			Vector2 searchVertice = position;


			for (int i = colorVectice.Count-1; i >= 0; i--) {
				if (colorVectice [i] == searchVertice) {
					if(!traject.Contains(callList[i])) { 
						traject.Insert (0, callList [i]);
						searchVertice = callList [i];
					}
				}
			}

			return;
		}

		//verifica se o vertice visitado já foi adicionado a lista de verticies visitados
		if (!colorVectice.Contains (position)) {
			colorVectice.Add (position);
			callList.Add (position);
		}


		//Os próximos 4 Ifs inserem os vertices adjacentes na lista de vertices visitados
		//o vertice atual é adicionado na lista de chamada para sinalizar a origem da trajetoria
		if (position.x > 0) {

			Vector2 vertice = new Vector2 ((position.x - 1), position.y);

			if (!colorVectice.Contains (vertice) && (Map.tiles [ (int)vertice.y, (int)vertice.x] != 0)) {
				colorVectice.Add (vertice);
				callList.Add (position);
			}
		}
		if (position.x < Map.mapWidth - 1) {

			Vector2 vertice = new Vector2 ((position.x + 1), position.y);
			
			if (!colorVectice.Contains (vertice) && (Map.tiles [ (int)vertice.y, (int)vertice.x] != 0)) {
				colorVectice.Add (vertice);
				callList.Add (position);
		//		Debug.Log ("add "+vertice);
			}
		}
		if (position.y > 0) {

			Vector2 vertice = new Vector2 (position.x, (position.y - 1));
			
			if (!colorVectice.Contains (vertice) && (Map.tiles [ (int)vertice.y, (int)vertice.x] != 0)) {
				colorVectice.Add (vertice);
				callList.Add (position);
			}
		}
		if (position.y < Map.mapHeight - 1) {

			Vector2 vertice = new Vector2 (position.x, (position.y + 1));

			if (!colorVectice.Contains (vertice) && (Map.tiles [ (int)vertice.y, (int)vertice.x] != 0)) {
				colorVectice.Add (vertice);
				callList.Add (position);
			}
		}

	    //faco o algoritmo BFS para o próximo item da lista, incrementa o indice
		index++;

		//se o indice exceder o tamanho da lista, finaliza o programa
		if (index >= colorVectice.Count)
			return;
		//chamada do BFS para o proximo item da lista.
		BFS (colorVectice [index], index);

	}

	void DFS(Vector2 position){

		//validar a posicao atual do vertice
		if (position.x < 0 || position.x >= Map.mapWidth)
			return;
		if (position.y < 0 || position.y >= Map.mapHeight)
			return;
		if (Map.tiles [ (int)position.y, (int)position.x] == 0)
			return;
		if (colorVectice.Contains (position))
			return;

		//colorir o vertice  visitado
		colorVectice.Add (position);

		//adicionar vertice atual na lista de trajeto
		traject.Add (position);

		//caso o tile atual seja o tile objetivo 2 (diamante)
		if (Map.tiles [(int)position.y, (int)position.x] == 3) {
			//marcar o trajeto como tracado
			trajectTraced = true;
			return;
		}
		//caso o tile atual seja um tile normal de pedra, continuar ...

		//chamadas recursivas para caminhar pelo mapa

		//direita
		if(!trajectTraced) DFS (new Vector2( (position.x+1), position.y ) );

		//baixo
		if(!trajectTraced) DFS (new Vector2( position.x, (position.y+1) ) );

		//esquerda
		if(!trajectTraced) DFS (new Vector2( (position.x-1), position.y ) );

		//cima
		if(!trajectTraced) DFS (new Vector2( position.x, (position.y-1) ) );


		//no fim do metodo, se o trajeto nao foi tracado, remove o tile atual do trajeto.
		if(!trajectTraced)traject.Remove(position);

	}

	void Update() {

		//posicao do bot igual a posicao do player
		if (transform.position == Player.realPosition) {
			Debug.Log ("Colisao!");
		}

		//metodo que desenha o trajeto na tela
		Debug_DrawTrajectory();


		if (traject!=null && traject.Count > 0) {

			//acumulando o tempo
			accTime+=Time.deltaTime;
			//o resto do método só executa quando o tempo acumulado for maior que 0.5s
			if (accTime > 0.5f)
				accTime = 0;
			else
				return;
			

			Vector3 newPosition = new Vector3 (traject [0].x * (Map.tileScale * Map.tileSize), -1 * traject [0].y * (Map.tileScale * Map.tileSize), -1f);

			transform.position = newPosition;

			Vector2 currentPosition = new Vector2( (transform.position.x/ (Map.tileScale * Map.tileSize) ) ,-1*(transform.position.y/ (Map.tileScale * Map.tileSize)) );

			if (traject [0] == currentPosition) {
				
	             traject.RemoveAt (0);
			}
				

		} else {

			Vector2 currentPosition = new Vector2( (transform.position.x/0.96f) ,-1*(transform.position.y/0.96f) );
			CalculeTraject (currentPosition);
		}
			
	}

	//método que desenha a lista do trajeto na tela.
	void Debug_DrawTrajectory(){

		//se a lista de trajeto for vazia, finaliza
		if (traject == null)
			return;

		for (int i = 0; i < traject.Count - 1; i++) {

			Vector3 vertInit = new Vector3 (traject [i].x * (Map.tileScale * Map.tileSize), -1* traject [i].y * (Map.tileScale * Map.tileSize), -5f);
			Vector3 vertEnd = new Vector3 (traject [i+1].x * (Map.tileScale * Map.tileSize), -1* traject [i+1].y * (Map.tileScale * Map.tileSize), -5f);

			Debug.DrawLine (vertInit, vertEnd, debugColor);
		}

	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


	/// <summary>
	/// Classe contendo métodos para gerar mapas de tower defense proceduralmente.
	/// </summary>
[System.Serializable]
public class MapGenerator
{
		/// <summary>
		/// pseudo-porcentagem de elementos bloqueadores na geração, quanto maior o valor mais "acidentado" o terreno.
		/// </summary>
		[Range(0,100)]
		public int obstacleFill;
		/// <summary>
		/// If true, when generating a path the path will not go past the X or Y of the Target.
		/// </summary>
		public bool isLimitedGen;
		/// <summary>
		/// The width of the zone.
		/// </summary>
		public int zoneWidth;
		/// <summary>
		/// The height of the zone.
		/// </summary>
		public int zoneHeight;
		/// <summary>
		/// The size of the generic tile, all tiles must be of same size;
		/// </summary>
		public  float tileSize = 0.32f;
		/// <summary>
		/// Tiles which the enemy will cross to reach the destination.
		/// </summary>
		public GameObject[] enemyRoadTiles;
		/// <summary>
		/// Decoration tiles that will be randomly used for looks.
		/// </summary>
		public GameObject[] decorationTiles;
		/// <summary>
		/// The Tile that will be where towers are placed.
		/// </summary>
		public GameObject towerSpawnTile;

	/// <summary>
	/// Transform that is the parent of all generated tiles. 
	/// </summary>
	public Transform MapHolder;

	/// <summary>
	/// Deletar essa porra dps
	/// </summary>
	public GameObject TEMPORARIOENEMY;


	/// <summary>
	/// Lista de caminhos possiveis de serem seguidos no momento.
	/// </summary>
	[HideInInspector]
	public List<Caminho> caminhos = new List<Caminho> ();

	/// <summary>
	/// Range around a point (x,y) the tower generation will search for rules.
	/// </summary>
	public int rangeTowerPlace = 1;
	/// <summary>
	/// Amount of roads nearby necessary to spawn a tower.
	/// </summary>
	public int roadCountPlace = 3;

	/// <summary>
	/// Max amount of towers nearby, ex: If there are two towers already placed, this one will not be placed.
	/// </summary>
	public int towerLimit = 2;



	private  byte[,] grid;// = new byte[zoneWidth, zoneHeight];

	private bool isDecorationGenerated = false;

	public MapGenerator ()
	{
		//grid = new byte[zoneWidth, zoneHeight];
	}

	/// <summary>
	/// Inicializa a grid se ela não tiver sido inicializada ainda, Retorna true se nao estava inicializada, false caso contrario
	/// </summary>
	/// <returns><c>true</c>, if grid was inicializared, <c>false</c> otherwise.</returns>
	private bool inicializarGrid(){
		if (grid == null) {
			grid = new byte[zoneWidth, zoneHeight];
			return true;
		}
		return false;
	}








	private List<Vector2> wayPoints = new List<Vector2>();



	// Use this for initialization
	void Start () {
		//CreatePlayableZone(Vector3.zero);
		//generatePath(0,3,5,8, null);

		//generatePath (5, 5, 2, 3);
	}


	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Marks the panda house.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	public void markPandaHouse(int x, int y){
		inicializarGrid ();
		grid [x, y] = 4;
	}
	public bool isUsed(int x, int y) {
		return grid [x, y] != 0;
	}

	/// <summary>
	/// Generates the rest of map that isn't being used.
	/// </summary>
	public void generateDecoration(){
		isDecorationGenerated = true;
		inicializarGrid ();
		tileGenerationInfo t = new tileGenerationInfo ();
		t.TileSize = tileSize;
		for (int i = 0; i < zoneWidth; i++) {
			for (int j = 0; j < zoneHeight; j++) {
				if (grid[i,j] == 0) {
					t.X = i;
					t.Y = j;
					t.TileObject = decorationTiles [Random.Range (0, decorationTiles.Length)];
					t.SpawnMe ();
					grid [i, j] = 2;
				}
			}
		}
	}

	////////////////////////////Lembrar de mover isso para cima





	/// <summary>
	/// Gets the amount of cells around that are of celltype.
	/// <para>0 = FreeSpace, 1 = roadSpace, 2 = DecorationSpace, 3 = TowerLocation 4 = pandaHouse </para>
	/// </summary>
	/// <returns>The surrounding cell type count.</returns>
	/// <param name="gridX">Grid x.</param>
	/// <param name="gridY">Grid y.</param>
	/// <param name="cellType">Cell type.</param>
	int GetSurroundingCellTypeCount(int gridX, int gridY, byte cellType) {
		int wallCount = 0;
		for (int neighbourX = gridX - rangeTowerPlace; neighbourX <= gridX + rangeTowerPlace; neighbourX ++) {
			for (int neighbourY = gridY - rangeTowerPlace; neighbourY <= gridY + rangeTowerPlace; neighbourY ++) {
				if (neighbourX >= 0 && neighbourX < zoneWidth && neighbourY >= 0 && neighbourY < zoneHeight) {
					if (neighbourX != gridX || neighbourY != gridY) {
						if (grid[neighbourX,neighbourY] == cellType) 
							wallCount++;
					}
				}
				/*else {
					wallCount ++;
				}*/
			}
		}

		return wallCount;
	}

	/// <summary>
	/// Generates the tower locations.
	/// </summary>
	public void generateTowerLocations() {
		inicializarGrid ();
		if (isDecorationGenerated) {
			throw new UnityException ("Nao pode gerar torres dps da decoração ter sido gerada.");
		}
		for (int i = 0; i < zoneWidth; i++) {
			for (int j = 0; j < zoneHeight; j++) {
				if (grid[i,j] != 0) {
					continue;
				}
				if (GetSurroundingCellTypeCount(i,j, 1) >= roadCountPlace &&
					GetSurroundingCellTypeCount(i,j, 3) <= towerLimit) {

					tileGenerationInfo info = new tileGenerationInfo ();
					info.X = i;
					info.Y = j;
					info.TileSize = tileSize;
					info.TileObject = towerSpawnTile;
					info.SpawnMe ();
					grid [i, j] = 3;
				}
			}
		}
	}

	//public void generatePath()
	/// <summary>
	/// Generates the path.
	/// </summary>
	/// <returns><c>true</c>, if path was generated, <c>false</c> otherwise.</returns>
	/// <param name="startX">Start x.</param>
	/// <param name="startY">Start y.</param>
	/// <param name="targetX">Target x.</param>
	/// <param name="targetY">Target y.</param>
	public bool generatePath(int startX, int startY, int targetX, int targetY, List<Vector2> usedSpace = null) {
		if (isDecorationGenerated) {
			throw new UnityException ("Tentando gerar Caminho após ter sido gerada a decoração. Isso dá ruin. pls stop.");
		}
		inicializarGrid ();
		int failLimit = 2;
		int currentFails = 0;
		bool found;
		if (usedSpace == null) {
			usedSpace = new List<Vector2> ();
		}
		//Inicaliza a tile
		tileGenerationInfo genInfo = new tileGenerationInfo ();
		genInfo.TileObject = enemyRoadTiles [Random.Range (0, enemyRoadTiles.Length)];
		genInfo.X = startX;
		genInfo.Y = startY;
		genInfo.TileSize = tileSize;
		//Debug.Log("Teste of hell: " + genInfo.ActualX + "Y : " + genInfo.ActualY);
		//int[,] grid = new int[zoneWidth, zoneHeight];
		//Posições que serão re-startadas com 0 apos gerado o caminho.
		List<Vector2> FreeLater;
		//List<Ponto> FreeLater = new List<Ponto>();
		List<Vector2> usedFree = new List<Vector2>();
		//Preenche espaço ja utilizado
		foreach (Vector2 pos in usedSpace) {
			if (pos.x >= 0 && pos.x < zoneWidth && pos.y >= 0 && pos.y < zoneHeight) {
				if (grid [(int)pos.x, (int)pos.y] == 0) {
					grid [(int)pos.x, (int)pos.y] = 1;
					usedFree.Add (new Vector2(pos.x, pos.y));
				} 
			}
		}
		if (grid [genInfo.X, genInfo.Y] == 1) {
			Debug.Log ("Trying to start from used position!");
			return false;
		}
		grid [genInfo.X, genInfo.Y] = 1;




		found = false;
		while (!found) {
			FreeLater = new List<Vector2> ();
			//Testa se passou limite de tentativas.
			if (currentFails >= failLimit)
				return false;
			Node firstRun;

			//Preenche com bloqueadores para gerar caminho bonito.
			//Dividido por 1000 para limitar quantia de bloqueadores.
			int realAmountOfObstacles = (int)((grid.GetLength(0) * grid.GetLength(1)) * (obstacleFill / 1000f) );
			Debug.Log ("Obstaculos: " + realAmountOfObstacles);
			for (int i = 0; i < realAmountOfObstacles; i++) {
				FreeLater.AddRange (generateObstacles (targetX, targetY));
			}


			//Implementação intermediate-betania  + my idea.
			//Cria Problemas para o caminho do breadthFirst, ele executa a ida para o caminho em 2 passos!.
			//Faz a pesquisa e a geração das tiles no caminho encontrado
			//Melhorado para Não ser tão aleatorio, cria como se fosse um quadrante para pesquisas iniciais

			int intermediateX;// = Random.Range (0, Mathf.Abs(targetX - startX ));
			int intermediateY;// = Random.Range (0, Mathf.Abs(targetY - startY ));
			do {
				intermediateX = Random.Range (0, Mathf.Abs(targetX - startX ));
				intermediateY = Random.Range (0, Mathf.Abs(targetY - startY ));
				//limitação X
				if (startX > targetX) {
					intermediateX = startX - intermediateX;
				} else {
					intermediateX = startX + intermediateX;
				}
				//Limitaçao Y
				if (startY > targetY) {
					intermediateY = startY - intermediateY;
				} else {
					intermediateY = startY + intermediateY;
				}

			} while (grid [intermediateX, intermediateY] == 1);

			//First run.
			Node last = greedySearch (startX, startY, intermediateX, intermediateY);
			firstRun = last;
			if (last != null) {
				while (last.parent != null) {
					grid [last.X, last.Y] = 1;
					last = last.parent;
				}
				//Debug.Log ("Last: " + last.X + " Y: " + last.Y);
				//genInfo.X = last.X;
				//genInfo.Y = last.Y;
				grid [last.X, last.Y] = 1;
			}
			else {
				currentFails++;
				FreeLater.AddRange (usedFree);
				foreach (Vector2 position in FreeLater) {
					grid [(int)position.x, (int)position.y] = 0;
				}
				Debug.Log ("No way found in first turn : " + currentFails);
				continue;
			}

			//Deixando o mesmo blocker para proxima parte da run.
			/*foreach (Vector2 position in FreeLater) {

				grid [(int)position.x, (int)position.y] = 0;
			}

			//Second run
			for (int i = 0; i < obstacleFill; i++) {
				FreeLater.AddRange (generateObstacles (targetX, targetY));
			}*/

			last = greedySearch (intermediateX, intermediateY, targetX, targetY);
			if (last != null) {

				//CRIA A PRIMEIRA LISTA DE PONTOS
				List<Ponto> firstHalf = new List<Ponto> ();
				//Debug.Log ("FirstX: " + firstRun.X + " FirstY: " + firstRun.Y);
				Ponto p = new Ponto(genInfo.intToActualPos(firstRun.X), genInfo.intToActualPos(firstRun.Y));
				firstHalf.Add(p);
				//Debug.Log ("Ponto: X" + p.X + " Y: " + p.Y);
				//SPreenche os espaços da "primeira" geração.
				while (firstRun.parent != null) {
					genInfo.X = firstRun.X;
					genInfo.Y = firstRun.Y;
					genInfo.SpawnMe ();
					//wayPoints.Add (vec);
					if (firstRun.parent.dir != firstRun.dir) {
						p = new Ponto(genInfo.intToActualPos(firstRun.parent.X), genInfo.intToActualPos(firstRun.parent.Y));
						firstHalf.Add(p);
					}
					//Já preenchido a grid anteriormente.
					grid [genInfo.X, genInfo.Y] = 1;
					firstRun = firstRun.parent;
				}
				{
					genInfo.X = firstRun.X;
					genInfo.Y = firstRun.Y;
					genInfo.SpawnMe ();
					grid [genInfo.X, genInfo.Y] = 1;

				}
				p = new Ponto(genInfo.intToActualPos(firstRun.X), genInfo.intToActualPos(firstRun.Y));
				if (!firstHalf.Contains(p)) {
					firstHalf.Add(p);
				}
				firstHalf.Reverse ();
				//FIM DA PRIMEIRA GERAção


				List<Ponto> secondHalf = new List<Ponto> ();

				p = new Ponto(genInfo.intToActualPos(last.X), genInfo.intToActualPos(last.Y));
				secondHalf.Add (p);
				while (last.parent != null) {
					genInfo.X = last.X;
					genInfo.Y = last.Y;
					genInfo.SpawnMe ();
					grid [genInfo.X, genInfo.Y] = 1;
					if (last.parent != null && last.parent.dir != last.dir) {
						p = new Ponto(genInfo.intToActualPos(last.parent.X), genInfo.intToActualPos(last.parent.Y));
						secondHalf.Add(p);
					}
					//Debug.Log("2 Quantia de Vec2: " +  wayPoints.Count);
					last = last.parent;

				}

				/*p = new Ponto(genInfo.intToActualPos(last.X), genInfo.intToActualPos(last.Y));
				if (!secondHalf.Contains(p)) {
					secondHalf.Add (p);
				}*/
				secondHalf.Reverse ();
				//Junta os dois caminhos.
				firstHalf.AddRange (secondHalf);
				//Fim segunda half.
				caminhos.Add (new Caminho (firstHalf));

				/*
				Debug.Log ("Vectors 2: :");
				foreach (Vector2 v in wayPoints) {
					Debug.Log ("X: " + v.x + " " + v.y);
				}
				*/
				FreeLater.AddRange (usedFree);
				foreach (Vector2 position in FreeLater) {
					grid [(int)position.x, (int)position.y] = 0;
				}
				return true;
			} else {
				while (firstRun != null) {
					grid [firstRun.X, firstRun.Y] = 0;
					firstRun = firstRun.parent;
				}
				currentFails++;
				FreeLater.AddRange (usedFree);
				foreach (Vector2 position in FreeLater) {
					grid [(int)position.x, (int)position.y] = 0;
				}

				Debug.Log ("No way found in Second turn : " + currentFails);
				continue;
			}
			/*foreach (Vector2 position in FreeLater) {
				grid [(int)position.x, (int)position.y] = 0;
			}*/

		}

		return true;
	}
	/// <summary>
	/// Gera obstaculos aleatorios na grid desta classe, garantido que targetX,Y
	/// </summary>
	/// <returns>The obstacles.</returns>
	/// <param name="targetX">Target x.</param>
	/// <param name="targetY">Target y.</param>
	private List<Vector2> generateObstacles(int targetX, int targetY){
		List<Vector2> usado = new List<Vector2> ();
		int maximumSize = (zoneWidth / 3);
		int minimumSize = 1;
		int randomChoice = Random.Range (0, 5);

		//Gera Linhas
		//Linha horizontal
		if (randomChoice == 1) {
			int Height = Random.Range (0, zoneHeight);
			int Start = Random.Range (0, zoneWidth);
			int Finish = Start + Random.Range (minimumSize, maximumSize);
			if (Finish > zoneWidth) {
				Finish = zoneWidth - 1;
			}

			for (int i = Start; i < Finish; i++) {
				if (grid[i,Height] == 0 && (i != targetX || Height != targetY)) {
					grid [i, Height] = 1;
					usado.Add(new Vector2(i, Height));
				}
			}
		}
		//Linha Vertical.
		if (randomChoice == 2) {
			int Width = Random.Range (0, zoneWidth);
			int Start = Random.Range (0, zoneHeight);
			int Finish = Start + Random.Range (minimumSize, maximumSize);
			if (Finish > zoneHeight) {
				Finish = zoneHeight - 1;
			}
			for (int i = Start; i < Finish; i++) {
				if (grid[Width,i] == 0 && (i != targetY || Width != targetX)) {
					grid [Width, i] = 1;
					usado.Add(new Vector2(Width, i));
				}
			}
		}

		//Diagonal 1
		if (randomChoice == 3) {
			int start = Random.Range (0, zoneHeight);
			int finish = start + (int)Random.Range (minimumSize, maximumSize);
			if(finish > zoneWidth){
				finish = zoneWidth - 1;
			}
			if(finish > zoneHeight){
				finish = zoneHeight - 1;
			}

			for (int i = start; i < finish; i++) {
				if (grid[i,i] == 0 && (i != targetY || i != targetX)) {
					grid [i, i] = 1;
					usado.Add(new Vector2(i, i));
				}
			}
		}

		//Diagonal2
		if (randomChoice == 4) {
			int start = Random.Range (0, zoneHeight);
			int finish = start - (int)Random.Range (minimumSize, maximumSize);
			if(finish < 0){
				finish = 0;
			}

			for (int i = start; i >= finish; i--) {
				if (grid[i,finish + (start-i)] == 0 && (i != targetY || i != targetX)) {
					grid [i, finish + (start-i)] = 1;
					usado.Add(new Vector2(i, finish + (start-i)));
				}
			}
		}
		return usado;
	}


	/// <summary>
	/// Pesquisa Uma rota de start até target, dentro de Grid.Retorna o node em Target ou null.
	/// </summary>
	/// <returns>Last Node in the tree at target  or null if no paths found.</returns>
	/// <param name="startX">Start x.</param>
	/// <param name="startY">Start y.</param>
	/// <param name="targetX">Target x.</param>
	/// <param name="targetY">Target y.</param>
	private Node breadthFirstSearch(int startX, int startY, int targetX, int targetY){
		//Não Consultar: http://lmgtfy.com/?q=breadth+first+search&l=1
		Node.alreadyPlaced = new List<Vector2> ();
		Node root = new Node( startX, startY, null);
		Node current = null;
		//Debug.Log ("Grid: " + grid);
		Queue<Node> Q = new Queue<Node>();
		root.Distance = 0;
		Q.Enqueue (root);
		int impedeLoopInfinito = 0;
		while (Q.Count > 0) {
			impedeLoopInfinito++;
			if (impedeLoopInfinito > 150000) {
				Debug.Log ("Deu muito ruin: " + Q.Count);
				return null;
			}
			current = Q.Dequeue();


			/*
			if (current.parent != null) {
				debuggerrr += "CurrentX: " + current.X + " CY: " + current.Y + " ParentX: " + current.parent.X + " Y: " + current.parent.Y   + "\n\r\n";
			}*/

			//File.AppendAllText("C:\\\\ProjetoSimone\\Benis.txt", debuggerrr);
			//Debug.Log ("Current: "+ current) ;
			if (current.X == targetX && current.Y == targetY) {
				return current;
			}
			current.GenerateChildrenNoBrothers (grid);

			foreach (Node n in current.AdjacentNodes) {
				n.Distance = current.Distance + 1;
				Q.Enqueue (n);
			}
		}
		return null;
	}

	/// <summary>
	/// Uses greedy to search for a path from start to finish!
	/// <para>> Método lindo S2 funciona muito bem.</para>
	/// </summary>
	/// <returns>The search.</returns>
	/// <param name="startX">Start x.</param>
	/// <param name="startY">Start y.</param>
	/// <param name="targetX">Target x.</param>
	/// <param name="targetY">Target y.</param>
	/// <param name="grid">Grid.</param>
	private Node greedySearch(int startX, int startY, int targetX, int targetY){
		//if (grid[startX, startY] == 1 || grid[targetX, targetY] == 1)
		//throw new UnityException ("Começando ou terminando em lugar utilizado!");
		Node start = new Node (startX, startY, null);
		PriorityQueue<Node>  Pq = new PriorityQueue<Node> ();
		int prioridade = Mathf.Abs ( targetX - startX) + Mathf.Abs (targetY - startY);
		Pq.Enqueue (start, prioridade);
		int impedeLoopInfinito = 0;
		int limiteExpancoes = grid.GetLength (0) * (grid.GetLength (1) * 2);
		while (Pq.Count > 0) {
			Node current;
			try {
				current = Pq.Dequeue ();
			} catch (UnityException ex) {
				Debug.Log ("Fila vaziaa" + ex.Message);
				return null;
			}
			impedeLoopInfinito++;

			if (impedeLoopInfinito > limiteExpancoes ) {
				Debug.Log ("Expandiu nodes demais!" + Pq.Count);
				return null;
			}


			if (current.X == targetX && current.Y == targetY) {
				return current;
			}

			current.GenerateChildren (grid);

			foreach (Node n in current.AdjacentNodes) {
				prioridade = Mathf.Abs ( targetX -  n.X) + Mathf.Abs (targetY -  n.Y);
				Pq.Enqueue (n, prioridade);
			}
		}
		return null;
	}




	/// <summary>
	/// Node utilizado para executar pesquisas de caminho.
	/// <para>>Os nodes branchs são criados dinamicamente conforme Limits.</para>
	/// </summary>
	private class Node{
		public static List<Vector2> alreadyPlaced = new List<Vector2> ();
		public int X;
		public int Y;
		public int Distance = -1;
		public Node parent;
		public directions dir;
		public List<Node> AdjacentNodes = new List<Node>();
		/// <summary>
		/// Initializes a new instance of the <see cref="MapGenerator+Node"/> class.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="limits">Limits of the grid.</param>
		public Node(int x, int y, Node _parent) {
			X = x;
			Y = y;
			parent = _parent;
		}

		/// <summary>
		/// Generates the children of this node.
		/// </summary>
		/// <param name="grid">Grid.</param>
		public void GenerateChildren(byte[,] grid){
			//Generates the Up node
			if (!(Y + 1 >= grid.GetLength(1))) {
				//Se for root ira apenas testar  a grid, caso contrario não pode ser o msemo que o pai,
				if (parent  == null) {
					if (grid[X, Y + 1] == 0) {
						Node temp = new Node (X, Y + 1, this);
						temp.dir = directions.up;
						AdjacentNodes.Add (temp);
					}
				} else if (!isParentPosition(X, Y + 1, this)) {
					//Grid must be free
					if (grid[X, Y + 1] == 0) {
						Node temp = new Node (X, Y + 1, this);
						temp.dir = directions.up;
						AdjacentNodes.Add (temp);
					}
				}
			}

			//Generates the Down node
			if (!(Y - 1 < 0)) {
				if (parent == null) {
					if (grid[X, Y - 1] == 0) {
						Node temp = new Node (X, Y - 1, this);
						temp.dir = directions.down;
						AdjacentNodes.Add (temp);
					}
				} else if (!isParentPosition(X, Y - 1, this)) { 
					//Grid must be free
					if (grid[X, Y - 1] == 0) {
						Node temp = new Node (X, Y - 1, this);
						temp.dir = directions.down;
						AdjacentNodes.Add (temp);
					}
				}
			}

			//Generates the Left node
			if (!(X - 1 < 0)) {
				if (parent == null) {
					if (grid[X - 1, Y] == 0) {
						Node temp = new Node (X - 1, Y, this);
						temp.dir = directions.left;
						AdjacentNodes.Add (temp);
					}
				} else  if (!isParentPosition(X - 1, Y, this)) {
					//Grid must be free
					if (grid[X - 1, Y] == 0) {
						Node temp = new Node (X - 1, Y, this);
						temp.dir = directions.left;
						AdjacentNodes.Add (temp);
						//AdjacentNodes.Add ();
					}
				}
			}

			//Generates the Right node
			if (!(X + 1 >= grid.GetLength(0))) {
				if (parent == null) {
					if (grid[X + 1, Y] == 0) {
						Node temp = new Node (X + 1, Y, this);
						temp.dir = directions.right;
						AdjacentNodes.Add (temp);
					}
				} else if (!isParentPosition(X + 1, Y, this)) {
					//Grid must be free
					if (grid[X + 1, Y] == 0) {
						Node temp = new Node (X + 1, Y, this);
						temp.dir = directions.right;
						AdjacentNodes.Add (temp);
					}
				}
			}
		}

		/// <summary>
		/// Generates the children no brothers.
		/// </summary>
		public void GenerateChildrenNoBrothers(byte[,] grid){
			Vector2 placed = new Vector2 (X, Y);
			alreadyPlaced.Add (placed);
			GenerateChildren (grid);
		}



		private bool isParentPosition(int x, int y, Node filho){
			/*Vector2 tester = new Vector2 (x, y);
			if (alreadyPlaced.Contains(tester)) {
				return true;
			}
			return false;
			*/
			Node oPai = filho;
			while (oPai.parent != null) {
				if (oPai.X == x && oPai.Y == y) {
					return true;
				}
				oPai = oPai.parent;
			}
			if (oPai.X == x && oPai.Y == y) {
				return true;
			}
			//Funciona desde que esteja garantido que o primeiro parent
			//é fixo na grid como used.
			return false;
		}
	}
}



	/// <summary>
	/// Keeps information on a tile while building the map zone.
	/// <para>Also used to spawn Tile Objects.</para>
	/// </summary>
struct tileGenerationInfo {
	

		/// <summary>
		/// Instanciates the Gameobject TileObject at this Struct's X and Y times tileSize.
		/// <para/>If TileObject is null this method will do nothing.
		/// </summary>
	public void SpawnMe() {
			if (TileObject == null) {
				Debug.Log ("Genio deu ruin. tileobject é null burro. / MapGenerator");
				return;
			}
			Vector2 spPoint = new Vector2 (X * TileSize, Y * TileSize);
		GameObject obj = (GameObject)GameObject.Instantiate (TileObject, spPoint, Quaternion.identity);

		//     GameObject go = Instantiate(A, new Vector3 (0,0,0), Quaternion.identity) as GameObject; 
		//go.transform.parent = GameObject.Find("Stage Scroll").transform;
	}

	public void SpawnMe(Transform t){
		if (TileObject == null) {
			Debug.Log ("Genio deu ruin. tileobject é null burro. / MapGenerator");
			return;
		}
		Vector2 spPoint = new Vector2 (X * TileSize, Y * TileSize);
		GameObject obj = (GameObject)GameObject.Instantiate (TileObject, spPoint, Quaternion.identity);
		obj.transform.parent = t;
	}

	public void SpawnMe (float _x, float _y, float z) {
		if (TileObject == null) {
			Debug.Log ("Genio deu ruin. tileobject é null burro. / MapGenerator");
			return;
		}
		Vector3 spPoint = new Vector3 (_x * TileSize, _y * TileSize, z);
	}

		public float TileSize {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the X position.
		/// </summary>
		/// <value>The x position.</value>
		public int X {
			get;
			set;
		}
		/// <summary>
		/// Gets the ingame position of X
		/// </summary>
		/// <value>The actual x.</value>
		public float ActualX {
			get{
				return X * TileSize;
			}
		}


		/// <summary>
		/// Gets or sets the y position.
		/// </summary>
		/// <value>The y position.</value>
		public int Y {
			get;
			set;
		}

		/// <summary>
		/// Gets the ingame position of Y
		/// </summary>
		/// <value>The actual y.</value>
		public float ActualY {
			get {
				return Y * TileSize;
			}
		}

		/// <summary>
		/// Transforms an int of a position in a grid to an actual position in the map using the tile size.
		/// </summary>
		/// <returns>The to actual position.</returns>
		/// <param name="valor">The eu sou retardado coordinate.</param>
		public float intToActualPos(int valor){
			return valor * TileSize;
		}


		/// <summary>
		/// Gets or sets the direction.
		/// </summary>
		/// <value>The direction.</value>
		public directions Direction {
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the tile object.
		/// </summary>
		/// <value>The tile object.</value>
		public GameObject TileObject {
			get;
			set;
		}
	}

	/// <summary>
	/// Indica uma orientação para seguir. horizontal e vertical X e Y.
	/// </summary>
	struct Orientation
	{
		private int _XValue;
		private int _YValue;

		public int XValue {
			get {
				return _XValue;
			}
			set { 
				_XValue = value;
			}
		}
		public int YValue {
			get {
				return _YValue;
			}
			set { 
				_YValue = value;
			}
		}
		public Orientation(int x, int y){
			_XValue = x;
			_YValue = y;
		}
	}

	/// <summary>
	/// General directions that can be chosen.
	/// </summary>
	public enum directions {
		right,
		down,
		up,
		left
	}

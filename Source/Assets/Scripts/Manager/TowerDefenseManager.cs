using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using System.Collections;
using System.Collections.Generic;




public class TowerDefenseManager : MonoBehaviour {
	/// <summary>
	/// Classe capaz de gerar métodos, todas as configurações devem ser definidas nesta classe.
	/// </summary>
	public MapGenerator generator;

	public int HouseSize = 1;
	/// <summary>
	/// Quantia de Gold no jogo.
	/// </summary>
	private int gold;
	/// <summary>
	/// Gets or sets the gold.
	/// </summary>
	/// <value>The gold.</value>
	public int Gold {
		get { 
			return gold;
		}
		set	{ 
			gold = value;
		}
	}
	private int wave;
	public int Wave {
		get { return wave; }
		set {
			wave = value;
			if (!gameOver) {
				//for (int i = 0; i < nextWaveLabels.Length; i++) {
				//	nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
				//}
			}
			//waveLabel.text = "WAVE: " + (wave + 1);
		}
	}
		
	/// <summary>
	/// Gets or sets the health.
	/// </summary>
	/// <value>The health.</value>
	public int Health {
		get { 
			return health;
		}
		set { 
			health = value;
		}
	}
	private int health;

	private int level;
	public int Level {
		get{ return level;}
		set{level = value;}
	}

	/// <summary>
	/// The instance (Singleton pattern).
	/// </summary>
	private static TowerDefenseManager instance;
	/// <summary>
	/// Gets the instance of this class.
	/// </summary>
	/// <value>The instance.</value>
	public static TowerDefenseManager Instance {
		get{
			if(instance == null){
				instance = FindObjectOfType(typeof(TowerDefenseManager)) as TowerDefenseManager;
			}
			return instance;
		}
	}
	public int LEVELTESTE = 1;

	public EventSystem getSystem () {
		return this.GetComponent<EventSystem> ();
	}

	void Start(){
		health = 10;
		createScene (LEVELTESTE);
		/*generator.generatePath (2, 2, 12, 15, new List<Vector2>());
		//generator.generatePath (22, 19, 2, 16, new List<Vector2>());
		generator.generateTowerLocations();
		generator.generateDecoration ();
		*/
	}
		
	void Update(){
		if (Input.GetKey(KeyCode.Space)) {
			if (generator.caminhos.Count > 0) {
				foreach (Caminho c  in generator.caminhos) {
					GameObject teste =  (GameObject)GameObject.Instantiate (generator.TEMPORARIOENEMY);
					TD_Enemy1Controller control = teste.GetComponent<TD_Enemy1Controller> ();
					c.followPath (control);
				}
			} else {
					Debug.Log ("No caminhos");
				}
		}
		
	}

	/// <summary>
	/// Cria uma cena de acordo com o nivel da fase.
	/// </summary>
	/// <param name="level">Level.</param>
	void createScene(int level){
		//List<Vector2> pandaHouse = new List<Vector2> ();
		List<Vector2> houseAround = new List<Vector2> ();
		generator.zoneHeight = generator.zoneWidth = F_MapSize (level);
		int middle = generator.zoneWidth / 2;
		for (int hSize = middle - HouseSize; hSize < middle + HouseSize; hSize++) {
			for (int vSize = middle - HouseSize; vSize < middle + HouseSize; vSize++) {
				generator.markPandaHouse (vSize, hSize);
				//pandaHouse.Add(new Vector2 (vSize, hSize));
				//Debug.Log ("House X[ " + vSize + " ]Y[ " + hSize + "] ");
			}
		}

		//Horizontal
		for (int i = middle - HouseSize; i < middle + HouseSize; i++) {
			houseAround.Add (new Vector2 (i, middle - HouseSize - 1));
			//Debug.Log ("Around: X[ " + i + " ]Y[ " + (middle - HouseSize) + "] ");
		}
		for (int i = middle - HouseSize; i < middle + HouseSize; i++) {
			houseAround.Add (new Vector2 (i, middle + HouseSize));
			//Debug.Log ("Around: X[ " + i + " ]Y[ " + (middle + HouseSize - 1) + "] ");
		}
		//Vertical
		for (int i = middle - HouseSize; i < middle + HouseSize; i++) {
			houseAround.Add (new Vector2 (middle + HouseSize, i));
			//Debug.Log ("Around: X[ " + (middle + HouseSize) + " ]Y[ " + i + "] ");
		}
		for (int i = middle - HouseSize; i < middle + HouseSize; i++) {
			houseAround.Add (new Vector2 (middle - HouseSize - 1, i));
			//Debug.Log ("Around: X[ " + (middle - HouseSize - 1) + " ]Y[ " + i + "] ");
		}


		int quantiaDeCaminhos = F_PathAMount (level);
		int caminhosCriados = 0;
		int pathSize = generator.zoneWidth / 4;
		int limitTest = generator.obstacleFill;
		while (caminhosCriados < quantiaDeCaminhos) {
			int option1 = (middle + pathSize) + Random.Range (0, generator.zoneWidth - (middle + pathSize + 2));
			int option2 = (middle - pathSize) - Random.Range (0, generator.zoneWidth - (middle + pathSize + 2));
			int xS = Random.Range(0,100) < 50 ? option1 : option2;
			option1 = (middle + pathSize) + Random.Range (0, generator.zoneWidth - middle - pathSize);
			option2 = (middle - pathSize) - Random.Range (0, generator.zoneWidth - middle - pathSize);
			Debug.Log ("op1: " + option1 + " opt2: " + option2 + " Size: " + generator.zoneWidth);
			int yS = Random.Range(0,100) < 50 ? option1 : option2;
			Debug.Log ("ys: " + yS + " xs" + xS);
			Vector2 selecionado = new Vector2(-1,-1);
			do {
				if(selecionado != null)
					houseAround.Remove(selecionado);
				selecionado = houseAround [Random.Range (0, houseAround.Count)];
			} while (generator.isUsed((int)selecionado.x, (int)selecionado.y) && houseAround.Count > 0);
			houseAround.Remove (selecionado);
			int xF = (int)selecionado.x;
			int yF = (int)selecionado.y;
			if (generator.generatePath (xS, yS, xF, yF, houseAround))
				caminhosCriados++;
			else {
				if (generator.obstacleFill == 5) {
					Debug.Log ("deu realmente ruin, sem caminhos mesmo desobstruido");
					return;
				}
				generator.obstacleFill = 5;
			}
		}
		generator.generateTowerLocations ();
		generator.generateDecoration ();
	}

	/// <summary>
	/// Gets the next point.
	/// </summary>
	/// <returns>The next point.</returns>
	/// <param name="start">Start.</param>
	/// <param name="size">Size.</param>
	/// <param name="limit">Limit.</param>
	private int getNextPoint (int start, int size, int limit) {
		int answer = size;
		if (Random.Range (0, 100) > 50)
			answer = -answer;
		while (start + answer > limit || start + answer < 0) {
			answer = Mathf.Abs (answer) - 1;
			if (Random.Range (0, 100) > 50)
				answer = -answer;
		}
		return answer;
	}


	#region funcoesDeCrescimento

	/// <summary>
	/// Returns the size of the map according to the Level. 
	/// </summary>
	/// <returns>The map size for a certain level/returns>
	/// <param name="_level">Level.</param>
	private int F_MapSize (int _level) {
		return _level < 15 ? 6 + (2 * _level) : 40;
	}
		
	/// <summary>
	/// Fs the path A mount.
	/// </summary>
	/// <returns>The path A mount.</returns>
	/// <param name="_level">Level.</param>
	private int F_PathAMount (int _level) {
		return (int)(1 + Mathf.Log(_level/2));
			///2 + loge(root(x))
	}


	/// <summary>
	/// Fs the size of the path.
	/// </summary>
	/// <returns>The path size.</returns>
	/// <param name="_level">Level.</param>
	private int F_PathSize(int _level){
		return (int)((Random.Range(_level/3, _level/1.2f)) + 4);
	}

	#endregion

}


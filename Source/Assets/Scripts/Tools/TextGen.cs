using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextGen : MonoBehaviour {


	private List<string> greetings;
	private List<string> descriptions;
	private List<string> objetives;
	private List<string> evals;

	public int Kills {
		get;
		set;
	}

	public int KillGold {
		get;
		set;
	}

	public int GoldGathered {
		get;
		set;
	}

	public float DistanceCovered {
		get;
		set;
	}

	// Use this for initialization
	void Start () {
		string composta;
		greetings.Add ("Bom noite nobre guerreiro, ");
		greetings.Add ("Olá arqueira");
		greetings.Add ("Oi, aproxime-se da fogueira");

		composta = string.Format ("Hoje conseguimos eliminar {0} inimigos, o que nos rendeu {1} pedaços de ouro, também conseguimos coletar {2} moedas.", Kills, KillGold, GoldGathered);
		descriptions.Add (composta);
		composta = string.Format ("A batalha de hoje foi ardua, conseguimos acumular{0} de ouro no total, viajamos por {1} metros e derrotamos {2} inimigos", KillGold + GoldGathered, DistanceCovered, Kills);




	}

	
	// Update is called once per frame
	void Update () {
	
	}
}

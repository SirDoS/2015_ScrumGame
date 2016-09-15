using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Representa um caminho pelo qual monstros podem seguir no tower defense.
/// </summary>
public class Caminho {


	/// <summary>
	/// Occurs when enemy reached target.
	/// </summary>
	public event System.EventHandler enemyReachedTarget;

	/// <summary>
	/// The way point the monster follows.
	/// </summary>
	List<Ponto> WayPoint  = new List<Ponto>();

	/// <summary>
	/// Cria um Novo caminho.
	/// </summary>
	/// <param name="_caminho">Lista de pontos que compôe o caminho.</param>
	public Caminho(List<Ponto> _caminho){
		WayPoint = _caminho;
		
	}

	/// <summary>
	/// Faz com que um walker siga este caminho até o final
	/// <para />Destroi o inimigo ao terminar o caminho.
	/// </summary>
	/// <param name="walker">Walker.</param>
	public void followPath (IWalker walker){
		if (WayPoint.Count >= 2) {
			Debug.Log (this.ToString());
			walker.reachedPoint += onReach;
			walker.Teleport (WayPoint [0].X, WayPoint [0].Y);
			walker.Walk (WayPoint [1].X, WayPoint [1].Y);
			walker.CurrentWaypoint = 1;

		} else {
			throw new UnityException ("Tem que ter no minimo DOIS PONTOS NUM WAYPOINT...");
		}
	}

	public override string ToString (){
		string resposta = "";
		for (int i = 0; i < WayPoint.Count; i++) {
			resposta += "W[" + i + "]: X__" + WayPoint [i].X + " Y__" + WayPoint[i].Y + "\n";
		}
		return resposta;
	}




	/// <summary>
	/// Ações executadas quando um objeto termina de chegar em um waypoint
	/// </summary>
	/// <param name="sender">Sender, deve ser um Walker.</param>
	/// <param name="e">E.</param>
	private void onReach(object sender, System.EventArgs e){
		IWalker currentWalker = ((IWalker)sender);
		if (currentWalker.CurrentWaypoint + 1 == WayPoint.Count) {
			currentWalker.destroyMe ();
			//enemyReachedTarget (this, System.EventArgs.Empty);
		} else {
			//Não chegou ao fim, continua andando.
			int nextWaypoint = currentWalker.CurrentWaypoint + 1;
			currentWalker.Walk (WayPoint[nextWaypoint].X, WayPoint[nextWaypoint].Y );
			currentWalker.CurrentWaypoint = nextWaypoint;
		}
	}

}

/// <summary>
/// Representa um ponto em um espaço 2D.(posição)
/// </summary>
public struct Ponto{
	

	/// <summary>
	/// Initializes a new instance of the <see cref="Ponto"/> struct.
	/// <para>Já definindo X e Y</para>
	/// </summary>
	/// <param name="_X">X.</param>
	/// <param name="_Y">Y.</param>
	public Ponto(float _X, float _Y){
		X = _X;
		Y = _Y;
	}

	/// <summary>
	/// posição no eixo X
	/// </summary>
	public float X;

	/// <summary>
	/// posição no eixo Y
	/// </summary>
	public float Y;

}


using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Interface representando os contratos necessários para que qualquer monstro siga um caminho. 
/// <para> /> A direção no método walk requer que este walker sempre estjea em um waypoint corretamente.
/// </summary>
public interface IWalker{

	/// <summary>
	/// Gets or sets the current waypoint.
	/// </summary>
	/// <value>The current waypoint.</value>
	int CurrentWaypoint{get; set;}


	/// <summary>
	/// Order this instance to walk to a point.
	/// </summary>
	/// <param name="x">X position to move to.</param>
	/// <param name="y">Y position to move to.</param>
	void Walk (float x, float y);

	/*
	/// <summary>
	/// Order this instance to walk to a point, giving orientation of directions.
	/// </summary>
	/// <param name="x">X position to move to.</param>
	/// <param name="y">Y position to move to.</param>
	/// <param name="dir">Orientation of the next point.</param>
	void Walk (float x, float y, directions dir);
	*/

	/// <summary>
	/// Teleport to the specified x and y.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	void Teleport (float x, float y);

	/// <summary>
	/// Occurs when reached next point.
	/// </summary>
	event EventHandler reachedPoint;

	/// <summary>
	/// Destroys this instance.
	/// </summary>
	/// <returns><c>true</c>, if me was destroyed, <c>false</c> otherwise.</returns>
	bool destroyMe ();

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Information on the level of the tower.
/// </summary>
[System.Serializable]
public class TowerLevel {
	/// <summary>
	/// </summary>
	public int cost;

	public Sprite visualization;
	/// <summary>
	/// The bullet prefab to shot.
	/// </summary>
	public GameObject bullet;
	/// <summary>
	/// The fire rate.
	/// </summary>
	public float fireRate;

	public float radius;
}


public class TowerData : MonoBehaviour {
	/// <summary>
	/// The current level.
	/// </summary>
	private TowerLevel currentLevel;

	/// <summary>
	/// Lista de niveis de uma tower.
	/// </summary>
	public List<TowerLevel> levels;

	/// <summary>
	/// Gets or sets the current level.
	/// </summary>
	/// <value>The current level.</value>
	public TowerLevel  CurrentLevel {
		get {
			return currentLevel;
		}
		set{
			currentLevel = value;
			int currentLevelIndex = levels.IndexOf(currentLevel);
			Sprite levelVisualization  = levels[currentLevelIndex].visualization;
			for (int i = 0; i < levels.Count; i++) {
				if (levelVisualization != null) {
					if (i == currentLevelIndex) {
						(this.GetComponentInChildren <SpriteRenderer>()).sprite = levels [i].visualization;
						(this.GetComponentInChildren <CircleCollider2D>()).radius = levels [i].radius;
						//levels[i].visualization.SetActive(true);						
					} else {
						//levels[i].visualization.SetActive(false);
					}
				}
			}
		}
	}

	void OnEnable() {
		CurrentLevel = levels[0];

	}

	public TowerLevel getNextLevel() {
		int currentLevelIndex = levels.IndexOf (currentLevel);
		int maxLevelIndex = levels.Count - 1;
		if (currentLevelIndex < maxLevelIndex) {
			return levels[currentLevelIndex+1];
		} else {
			return null;
		}
	}

	public void increaseLevel() {
		int currentLevelIndex = levels.IndexOf(currentLevel);
		if (currentLevelIndex < levels.Count - 1) {
			CurrentLevel = levels[currentLevelIndex + 1];
		}
	}



}

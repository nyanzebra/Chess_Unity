using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameNetwork;

public class UIManagerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI() {
		if (Application.loadedLevel == 2 && m_GameReady) {
			if (GUI.Button(new Rect(500, 150, 350, 200), "Chess Game")) {
				m_NetworkManager.joinServer();
				loadGame();
			}
		}
	}

	public void createGame() {
		m_GameReady = true;
		m_NetworkManager.startServer ();
	}

	public void loadMainMenu() {
		Application.LoadLevel (0);
		m_NetworkManager.endConnection ();
		m_GameReady = false;
	}

	public void loadCurrentGames() {
		Application.LoadLevel (2);
	}

	public void loadGame() {
		Application.LoadLevel(1);
	}

	private NetworkManager m_NetworkManager = new NetworkManager();
	private static bool m_GameReady;
}

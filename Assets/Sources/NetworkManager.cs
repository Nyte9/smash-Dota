using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {

	//Networking Variables 
	private bool menu = true;
	private bool startMenu = true;
	private bool connectedToLobby = false;
	private bool connectionMenu = false;
	private bool roomSelection = false;
	private bool roomCreation = false;
	private bool settings = false;
	public bool respawnMenu = false;
	public bool inGameMenu = false;
	public GUISkin MenuSkin;
	private string RoomName = "";
	private string name = "Name";
	private bool custimization = false;

	//Menu Variables
	public GameObject mainCanvas;
	public GameObject multiCanvas;

	// Use this for initialization
	void Start () {
		mainCanvas.SetActive(true);
		multiCanvas.SetActive(false);
	}

	void OnJoinedLobby()
	{
		mainCanvas.SetActive(false);
		multiCanvas.SetActive(true);
		connectedToLobby = true;
	}

	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Can't join random room!");
		PhotonNetwork.CreateRoom (null); //creates a room if there are none avalible
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void multiMenu(){
		print ("showing multiplayer menu");
		PhotonNetwork.ConnectUsingSettings ("0.1");
		startMenu = false;
		connectionMenu = true;
	}

	public void joinRoom(){
		print ("Opening room selection");
		roomSelection = true;
		multiCanvas.SetActive(false);
	}

	public void hostRoom(){
		print ("Hosting room");
		roomCreation = true;
		multiCanvas.SetActive(false);
	}

	void OnGUI() {

		if (menu) {
			
			GUI.skin = MenuSkin;
			/*
			if (!connectedToLobby && startMenu) {
				
				GUI.Box (new Rect (0, Screen.height / 16 * 1, Screen.width, Screen.height / 16 * 14), "");
				GUI.Box (new Rect (0, Screen.height / 16 * 1, Screen.width, Screen.height / 16 * 1), "StartMenu");

				if (GUI.Button (new Rect (0, Screen.height / 16 * 3, Screen.width, Screen.height / 16 * 1), "Online")) {
					PhotonNetwork.ConnectUsingSettings ("0.1");
					startMenu = false;
					connectionMenu = true;
				}

				if (GUI.Button (new Rect (0, Screen.height / 16 * 5, Screen.width, Screen.height / 16 * 1), "Settings")) {
					startMenu = false;
					settings = true;
				}

				if (GUI.Button (new Rect (0, Screen.height / 16 * 7, Screen.width, Screen.height / 16 * 1), "Quit")) {
					Application.Quit ();
				}
				
			}

			if (connectedToLobby && connectionMenu) {
				GUI.Box (new Rect (0, Screen.height / 16 * 1, Screen.width, Screen.height / 16 * 14), "");
				GUI.Box (new Rect (0, Screen.height / 16 * 1, Screen.width, Screen.height / 16 * 1), "lobbyMenu");
				if (GUI.Button (new Rect (0, Screen.height / 16 * 3, Screen.width, Screen.height / 16 * 1), "JoinRoom")) {
					roomSelection = true;
					connectionMenu = false;
				}
				if (GUI.Button (new Rect (0, Screen.height / 16 * 4, Screen.width, Screen.height / 16 * 1), "CreateRoom")) {
					roomCreation = true;
					connectionMenu = false;
				}
				if (GUI.Button (new Rect (0, Screen.height / 16 * 5, Screen.width, Screen.height / 16 * 1), "Back")) {
					startMenu = true;	
					connectionMenu = false;
					PhotonNetwork.Disconnect ();
				}
			}
			*/
			if (roomSelection) {
				GUI.Box (new Rect (0, Screen.height / 16 * 1, Screen.width, Screen.height / 16 * 14), "");
				GUI.Box (new Rect (0, Screen.height / 16 * 1, Screen.width, Screen.height / 16 * 1), "RoomMenu");
				if (GUI.Button (new Rect (0, Screen.height / 16 * 3, Screen.width, Screen.height / 16 * 1), "Join Random Room")) {
					PhotonNetwork.JoinRandomRoom ();
					roomSelection = false;
				}
				int j = 1;
				foreach (RoomInfo game in PhotonNetwork.GetRoomList()) {
					if (GUI.Button (new Rect (0, Screen.height / 16 * (3 + j), Screen.width, Screen.height / 16 * 1), "Join" + game.name + "  " + game.playerCount + "/" + game.maxPlayers)) {
						PhotonNetwork.JoinRoom (game.name);
						roomSelection = false;
					}
					j++;
				}
				if (GUI.Button (new Rect (0, Screen.height / 16 * (j + 3), Screen.width, Screen.height / 16 * 1), "Back")) {
					mainCanvas.SetActive(true);
					roomSelection = false;
					startMenu = true;
					connectedToLobby = false;
					PhotonNetwork.Disconnect ();
				}
			}
			
			if (roomCreation) {
				GUI.Box (new Rect (0, Screen.height / 16 * 1, Screen.width, Screen.height / 16 * 14), "");
				GUI.Box (new Rect (0, Screen.height / 16 * 1, Screen.width, Screen.height / 16 * 1), "RoomCreation");
				RoomName = GUI.TextField (new Rect (0, Screen.height / 16 * 3, Screen.width, Screen.height / 16 * 1), RoomName);
				if (GUI.Button (new Rect (0, Screen.height / 16 * 4, Screen.width, Screen.height / 16 * 1), "Create")) {
					PhotonNetwork.CreateRoom (RoomName);
					roomCreation = false;
				}
				if (GUI.Button (new Rect (0, Screen.height / 16 * (5), Screen.width, Screen.height / 16 * 1), "Back")) {
					mainCanvas.SetActive(true);
					startMenu = true;
					roomCreation = false;
					PhotonNetwork.Disconnect ();
				}
			}
			
			if (settings) {
				GUI.Box (new Rect (0, Screen.height / 16 * 1, Screen.width, Screen.height / 16 * 14), "");
				GUI.Box (new Rect (0, Screen.height / 16 * 1, Screen.width, Screen.height / 16 * 1), "StartMenu");
				if (GUI.Button (new Rect (0, Screen.height / 16 * 3, Screen.width, Screen.height / 16 * 1), "Graphics1")) {
					QualitySettings.SetQualityLevel (0, true);
					
				}
				if (GUI.Button (new Rect (0, Screen.height / 16 * 4, Screen.width, Screen.height / 16 * 1), "Graphics2")) {
					
					QualitySettings.SetQualityLevel (1, true);
					
				}
				if (GUI.Button (new Rect (0, Screen.height / 16 * 5, Screen.width, Screen.height / 16 * 1), "Graphics3")) {
					
					QualitySettings.SetQualityLevel (2, true);
					
				}
				if (GUI.Button (new Rect (0, Screen.height / 16 * 6, Screen.width, Screen.height / 16 * 1), "Graphics4")) {
					
					QualitySettings.SetQualityLevel (3, true);
					
				}
				if (GUI.Button (new Rect (0, Screen.height / 16 * 7, Screen.width, Screen.height / 16 * 1), "Graphics5")) {
					
					QualitySettings.SetQualityLevel (4, true);
					
				}
				if (GUI.Button (new Rect (0, Screen.height / 16 * 8, Screen.width, Screen.height / 16 * 1), "Graphics6")) {
					
					QualitySettings.SetQualityLevel (5, true);
				}
				
				if (GUI.Button (new Rect (0, Screen.height / 16 * 9, Screen.width, Screen.height / 16 * 1), "Back")) {
					startMenu = true;
					settings = false;
				}
			}


		}
	}
}

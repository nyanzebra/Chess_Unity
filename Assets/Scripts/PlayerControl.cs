using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Chess;

public class PlayerControl : MonoBehaviour {
	//public
	// Use this for initialization
	void Start () {
		try {
			m_PlayerCamera = GameObject.FindGameObjectWithTag("WhiteCamera").GetComponent<Camera>();
			m_OpponentCamera = GameObject.FindGameObjectWithTag("BlackCamera").GetComponent<Camera>();
			m_Camera = m_PlayerCamera;
	    	m_GameManager = gameObject.GetComponent<GameManager>();
			m_GameManager.initPieceMap();
			m_GameManager.initBoard();
		} catch (UnityEngine.UnityException e) {
			Debug.LogException(e);
		}
	}
	
	// Update is called once per frame
	void Update() {
		switchCamera ();
		if (ChessBoard.Instance.hasPiece()) {
			moveSelectedPiece();
		}
		if (ChessBoard.Instance.hasMovedPiece) {
			releasePiece();
		}
		if (isAbleToRelease ()) {
			releasePiece();
		} else if (selectCollidedObject () != null) {
			m_GameManager.selectPiece (selectCollidedObject ());
		}
	}
   
	//private
	private GameManager m_GameManager;
	private Camera m_PlayerCamera;
	private Camera m_OpponentCamera;
	private Camera m_Camera;
	private Vector3 m_Origin;
	private bool m_Rotating;
	private bool m_IsTurn;

	private bool isAbleToRelease() {
		GameObject gameobject = selectCollidedObject ();
		if (gameobject == null) {
			return false;
		}
		if (ChessBoard.Instance.hasPiece()) {
			if (ChessBoard.Instance.SelectedPiece.getGameObject() == gameobject) {
				if (ChessBoard.Instance.SelectedPiece.isHighlighted()) {
					return true;
				}
			}
			if (gameobject.tag == "Square") {
				foreach (ChessSquare square in ChessBoard.Instance.SelectedPiece.getPossibleMoves()) {
					if (square.getGameObject() == gameobject) {
						return false;
					}
				}
				return true;
			}
		}
		return false;
	}

	private void releasePiece() {
		ChessBoard.Instance.hideValidMoves();
		ChessBoard.Instance.releasePiece();
		ChessBoard.Instance.hasMovedPiece = false;
	}

	private void switchCamera() {
		if (Input.GetKeyDown ("left") || Input.GetKeyDown("right")) {
			if (m_PlayerCamera.enabled) {
				m_PlayerCamera.enabled = false;
				m_OpponentCamera.enabled = true; 
				m_Camera = m_OpponentCamera;
			} else {
				m_PlayerCamera.enabled = true;
				m_OpponentCamera.enabled = false;
				m_Camera = m_PlayerCamera;
			}
		}
	}

	void OnMouseDrag() {
		if (Input.GetMouseButtonDown(0)) {
			m_Origin = Input.mousePosition;
			m_Rotating = true;
		}
		if (!Input.GetMouseButton(0)) {
			m_Rotating = false;
		}
		if (m_Rotating) {
			Vector3 pos = m_PlayerCamera.ScreenToViewportPoint(Input.mousePosition - m_Origin);
			transform.RotateAround(transform.position, transform.right, - pos.y * 4.0f);
			transform.RotateAround(transform.position, Vector3.up, pos.x * 4.0f);
		}
	}

	private GameObject selectCollidedObject() {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				return hit.collider.gameObject;
			}
		}
		return null;
	}
	
	private void moveSelectedPiece() {
		if (selectCollidedObject () != null) {
			if (selectCollidedObject ().tag == ("Square")) {
				float x = selectCollidedObject ().transform.position.x;
				float y = selectCollidedObject ().transform.position.y;
				float z = selectCollidedObject ().transform.position.z;
				Vector3 coordinates = new Vector3 (x, y, z);
				m_GameManager.movePiece (coordinates);
			}
		}
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Chess;

public class GameManager : MonoBehaviour {
	//public
	public GameManager() {}

	public void initPieceMap() {
		m_Pieces = new List<GameObject>();
		initPawns();
		initBishops();
		initKings();
		initKnights();
		initQueens();
		initRooks();
	}

	public void initBoard() {
		GameObject[] squares = GameObject.FindGameObjectsWithTag("Square");
		foreach (GameObject square in squares) {

			uint i = uint.Parse(square.name[square.name.Length - 2].ToString());
			uint j = uint.Parse(square.name[square.name.Length - 1].ToString());
			ChessBoard.Instance.setSquare(square, i, j);

			foreach (GameObject piece in m_Pieces) {
				if (piece.renderer.transform.position.x == square.renderer.transform.position.x) {
					if (piece.renderer.transform.position.z == square.renderer.transform.position.z) {
						ChessPiece cpiece = ChessBoard.Instance.BuildChessPiece(piece);
						ChessBoard.Instance.getSquares()[i, j].setChessPiece(cpiece);
						ChessBoard.Instance.getSquares()[i, j].getChessPiece().setSquare(ChessBoard.Instance.getSquares()[i, j]);
					}
				}
			}
		}
	}

	public void selectPiece(GameObject piece) {
		if (piece.tag != "Square") {
			ChessBoard.Instance.hideValidMoves ();
			ChessBoard.Instance.releasePiece ();
			ChessBoard.Instance.selectPiece (piece);
			ChessBoard.Instance.SelectedPiece.highlight(Color.yellow);
			ChessBoard.Instance.showValidMoves ();
		}
	}

	//draw graveyard, promotion, and draw win/lose
	void OnGUI() {		
		GUI.Box (new Rect (10, 10, 200, 150), "Black Graveyard");
		for (uint i = 0; i < m_GraveyardBlack.tombs(); ++i) {
			string name = m_GraveyardBlack.tomb(i).Key;
			string value = m_GraveyardBlack.tomb (i).Value.ToString();
			string label = name + "\tx" + value;
			GUI.Label(new Rect (20, 40 + (20 * i), 100, 20), label);
		}

		GUI.Box (new Rect (Screen.width - 210, 10, 200, 150), "White Graveyard");
		for (uint i = 0; i < m_GraveyardWhite.tombs(); ++i) {
			string name = m_GraveyardWhite.tomb(i).Key;
			string value = m_GraveyardWhite.tomb (i).Value.ToString();
			string label = name + "\tx" + value;
			GUI.Label(new Rect (Screen.width - 190, 40 + (20 * i), 100, 20), label);
		}
	}

	public void movePiece(Vector3 position) {
		uint y = (uint) position.x / 15;
		uint x = (uint) - position.z / 15;
		foreach (ChessSquare square in ChessBoard.Instance.getSquares()) {
			if (square.withinSquare(x, y) && square.isHighlighted()) {
				ChessPiece current = ChessBoard.Instance.SelectedPiece;
				if (current.getSquare().hasEnemyPiece(square)) {
					GameObject clone = Instantiate(square.getChessPiece().getGameObject()) as GameObject;
					addToGraveyard(clone);
					Destroy(square.getChessPiece().getGameObject());
					square.setChessPiece(null);
				}
				if (current is Pawn) {
					Pawn pawn = (Pawn) current;
					if (pawn.isEnPassant()) {
						GameObject clone = Instantiate(pawn.enPassantSquare().getChessPiece().getGameObject()) as GameObject;
						addToGraveyard(clone);	
						Destroy(pawn.enPassantSquare().getChessPiece().getGameObject());
						pawn.enPassantSquare().setChessPiece(null);
					}
					pawn.moved();
				}
				ChessBoard.Instance.SelectedPiece.getSquare().setChessPiece(null);
				ChessBoard.Instance.SelectedPiece.setSquare(square);
				ChessBoard.Instance.hasMovedPiece = true;
				break;
			}
		}
	}

	private void addToGraveyard(GameObject piece) {
		if (piece.name.Contains("_W_")) {
			m_GraveyardWhite.bury(piece);
		} else {
			m_GraveyardBlack.bury(piece);
		}
	}

	//private
	private void initPawns() {
		GameObject[] pieces = GameObject.FindGameObjectsWithTag("Pawn");
		foreach (GameObject piece in pieces) {
			m_Pieces.Add(piece);
		}
	}
	private void initRooks() {
		GameObject[] pieces = GameObject.FindGameObjectsWithTag ("Rook");
		foreach (GameObject piece in pieces) {
			m_Pieces.Add (piece);
		}
	}
	private void initKnights() {
		GameObject[] pieces = GameObject.FindGameObjectsWithTag ("Knight");
		foreach (GameObject piece in pieces) {
			m_Pieces.Add (piece);
		}
	}
	private void initBishops() {
		GameObject[] pieces = GameObject.FindGameObjectsWithTag ("Bishop");
		foreach (GameObject piece in pieces) {
			m_Pieces.Add (piece);
		}
	}
	private void initQueens() {
		GameObject[] pieces = GameObject.FindGameObjectsWithTag ("Queen");
		foreach (GameObject piece in pieces) {
			m_Pieces.Add (piece);
		}
	}
	private void initKings() {
		GameObject[] pieces = GameObject.FindGameObjectsWithTag ("King");
		foreach (GameObject piece in pieces) {
			m_Pieces.Add (piece);
		}
	}
	private List<GameObject> m_Pieces;
	private bool m_UpdateGraveyard;
	private Graveyard m_GraveyardBlack = new Graveyard();
	private Graveyard m_GraveyardWhite = new Graveyard();
}

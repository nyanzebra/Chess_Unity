using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chess {
	public class ChessBoard {

		public static ChessBoard Instance {
			get {
				if (m_Instance == null) {
					m_Instance = new ChessBoard ();
				}
				return m_Instance;
			}
		}

		public ChessPiece SelectedPiece { get{ return m_SelectedPiece; } }

		public void selectPiece(GameObject piece) {
			uint z = (uint)piece.renderer.transform.position.x / 15;
			uint x = (uint)-piece.renderer.transform.position.z / 15;
			if (m_Squares [x, z].hasChessPiece ()) {
				m_SelectedPiece = m_Squares[x, z].getChessPiece();
			} 
		}

		public ChessPiece BuildChessPiece(GameObject piece) {
			if (piece.tag == "Pawn") {
				return new Pawn(piece);
			} else if (piece.tag == "Rook") {
				return new Rook(piece);
			} else if (piece.tag == "Knight") {
				return new Knight(piece);
			} else if (piece.tag == "Bishop") {
				return new Bishop(piece);
			} else if (piece.tag == "Queen") {
				return new Queen(piece);
			} else if (piece.tag == "King") {
				return new King(piece);
			} else {
				return null;
			}
		}

		public bool hasMovedPiece {
			get {
				return m_hasMovedPiece;
			}
			set {
				m_hasMovedPiece = value;
			}
		}

		public void releasePiece() {
			if (hasPiece ()) {
				m_SelectedPiece.unhighlight();
				m_SelectedPiece = null;
			}
		}

		public bool hasPiece() {
			return (m_SelectedPiece != null) ? true : false;
		}

		public void showValidMoves() {
			foreach (ChessSquare move in m_SelectedPiece.getPossibleMoves()) {
				if (move != null) {
					if (m_SelectedPiece.getSquare().hasEnemyPiece(move)) {
						move.highlight(Color.red);
						moveToCheck(move);
					} else if (!m_SelectedPiece.getSquare().hasFriendlyPiece(move)) {
						move.highlight(Color.blue);
					} else if (m_SelectedPiece is Pawn) {
						if (((Pawn) m_SelectedPiece).isEnPassant()) {
							((Pawn) m_SelectedPiece).enPassantSquare().highlight(Color.red);
						}
					}
				}
			}
		}

		private void moveToCheck(ChessSquare square) {
			if (square.getChessPiece() is King) {
				((King)square.getChessPiece()).Check = true;
			}
		}

		public void hideValidMoves() {
			foreach (ChessSquare move in m_Squares) {
				if (move != null) {
					move.unhighlight();
				}
			}
		}

		public void setSquare(GameObject square, uint row, uint column) {
			if (row >= 0 && row < 8) {
				if (column >= 0 && column < 8) {
					m_Squares[row, column] = new ChessSquare(square);
				}
			}
		}

		public ChessSquare[,] getSquares() { 
			return m_Squares; 
		}

		//private
		private ChessBoard () {
		}

		private ChessSquare[,] m_Squares = new ChessSquare[8, 8];
		//private ChessPiece[] = 
		private ChessPiece m_SelectedPiece;
		private bool m_hasMovedPiece = false;
		private static ChessBoard m_Instance = null;
	}
}


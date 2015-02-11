using System;
using UnityEngine;
using System.Collections.Generic;

namespace Chess {

	public abstract class ChessPiece {
		//public
		public ChessPiece(GameObject piece) {
			m_Piece = piece;
			m_Color = m_Piece.renderer.material.color;
			if (m_Piece.name.Contains ("_B_")) {
				m_Texture_Color = "Black";
			} else {
				m_Texture_Color = "White";
			}
		}

		public void highlight(Color color) {
			m_IsHighlighted = true;
			m_Piece.renderer.material.color = color;
		}

		public bool isHighlighted() {
			return m_IsHighlighted;
		}

		public GameObject getGameObject() {
			return m_Piece;
		}

		public void unhighlight() {
			m_IsHighlighted = false;
			m_Piece.renderer.material.color = m_Color;
		}

		public ChessSquare getSquare() {
			return m_Square; 
		}

		public string getColor() {
			return m_Texture_Color;
		}

		public void setSquare(ChessSquare square) {			
			m_Square = square; 
			float y = m_ChessNamesAndYValues[m_Piece.tag];
			m_Piece.renderer.transform.position = new Vector3(square.position().x, y, square.position().z);
			m_Square.setChessPiece(this);
		}

		abstract public List<ChessSquare> getPossibleMoves();

		//protected		
		protected GameObject m_Piece;
		protected string m_Texture_Color;
		protected ChessSquare m_Square;
		protected List<ChessSquare> m_PossibleMoves = new List<ChessSquare>();

		//private
		private bool m_IsHighlighted;
		private Color m_Color;
		private Dictionary<string, float> m_ChessNamesAndYValues = new Dictionary<string, float>()
		{
			{ "Pawn", -7 },
			{ "Rook", -6 },
			{ "Knight", -5 },
			{ "Bishop", 0 },
			{ "King", 0 },
			{ "Queen", -1 },
		};
	}
}


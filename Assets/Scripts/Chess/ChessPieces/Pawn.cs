using System;
using UnityEngine;
using System.Collections.Generic;

namespace Chess {
	public class Pawn : ChessPiece {
		//public
		public Pawn (GameObject piece) : base(piece) {
		}

		public override List<ChessSquare> getPossibleMoves() {
			m_PossibleMoves.Clear();
			return pawnMoves();
		}

		public bool isEnPassant() {
			return m_IsEnPassant;
		}

		public ChessSquare enPassantSquare(){
			return m_EnPassantSquare;
		}

		public void moved() {
			m_Moved = true;
		}

		//private
		private List<ChessSquare> pawnMoves() {
			m_IsEnPassant = false;
			m_EnPassantSquare = null;
			if (!m_Moved) {
				if (!m_Square.adjacent(Adjacent.e_Adjacent.UP).adjacent(Adjacent.e_Adjacent.UP).hasChessPiece()) {
					m_PossibleMoves.Add(m_Square.adjacent(Adjacent.e_Adjacent.UP).adjacent (Adjacent.e_Adjacent.UP));
				}
			}
			//up or down
			if (m_Square.adjacent(Adjacent.e_Adjacent.UP) != null && !m_Square.adjacent(Adjacent.e_Adjacent.UP).hasChessPiece()) {
				m_PossibleMoves.Add(m_Square.adjacent(Adjacent.e_Adjacent.UP));
			}
			//up_left or down_right
			if (m_Square.adjacent(Adjacent.e_Adjacent.UP_LEFT) != null && !m_Square.hasFriendlyPiece(m_Square.adjacent(Adjacent.e_Adjacent.UP_LEFT))) {
				if (m_Square.hasEnemyPiece(m_Square.adjacent(Adjacent.e_Adjacent.UP_LEFT))) {
					m_PossibleMoves.Add(m_Square.adjacent(Adjacent.e_Adjacent.UP_LEFT));
				}
			}
			//up_right or down_left
			if (m_Square.adjacent(Adjacent.e_Adjacent.UP_RIGHT) != null && !m_Square.hasFriendlyPiece(m_Square.adjacent(Adjacent.e_Adjacent.UP_RIGHT))) {
				if (m_Square.hasEnemyPiece(m_Square.adjacent(Adjacent.e_Adjacent.UP_RIGHT))) {
					m_PossibleMoves.Add(m_Square.adjacent(Adjacent.e_Adjacent.UP_RIGHT));
				}
			}
			//left or right
			if (m_Square.adjacent(Adjacent.e_Adjacent.UP_LEFT) != null) {
				if (m_Square.hasEnemyPiece(m_Square.adjacent(Adjacent.e_Adjacent.LEFT))) {
					if (!m_Square.adjacent(Adjacent.e_Adjacent.UP_LEFT).hasChessPiece()) {
						m_IsEnPassant = true;
						m_EnPassantSquare = m_Square.adjacent(Adjacent.e_Adjacent.LEFT);
						m_PossibleMoves.Add(m_Square.adjacent(Adjacent.e_Adjacent.UP_LEFT));
					}
				}
			}
			if (m_Square.adjacent(Adjacent.e_Adjacent.UP_RIGHT) != null) { 
				if (m_Square.hasEnemyPiece(m_Square.adjacent(Adjacent.e_Adjacent.RIGHT))) {
					if (!m_Square.adjacent(Adjacent.e_Adjacent.UP_RIGHT).hasChessPiece()) {
						m_IsEnPassant = true;
						m_EnPassantSquare = m_Square.adjacent(Adjacent.e_Adjacent.RIGHT);
						m_PossibleMoves.Add(m_Square.adjacent(Adjacent.e_Adjacent.UP_RIGHT));
					}
				}
			}
			return m_PossibleMoves;
		}
		private bool m_Moved = false;
		private bool m_IsEnPassant = false;
		private ChessSquare m_EnPassantSquare;
	}
}


using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chess {
	public class Rook : ChessPiece {
		//public 
		public Rook(GameObject piece) : base(piece) {
		}

		public override List<ChessSquare> getPossibleMoves() {
			m_PossibleMoves.Clear ();
			return rookMoves();
		}

		//private
		private List<ChessSquare> rookMoves() {
			for (Adjacent.e_Adjacent i = Adjacent.e_Adjacent.START_CARDINAL; i != Adjacent.e_Adjacent.END; i = Adjacent.nextCardinal(i)) {
				ChessSquare square = m_Square.adjacent(i);
				for (uint j = 0; j < 7; ++j) {
					if (square == null || m_Square.hasFriendlyPiece(square)) {
						break;
					}
					if (m_Square.hasEnemyPiece(square)) {
						m_PossibleMoves.Add(square);
						break;
					}
					m_PossibleMoves.Add(square);
					square = square.adjacent(i);
				}
			}
			return m_PossibleMoves;
		}
	}
}


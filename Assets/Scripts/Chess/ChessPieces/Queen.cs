using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chess {
	public class Queen : ChessPiece{
		public Queen(GameObject piece) : base(piece) {
		}

		public override List<ChessSquare> getPossibleMoves() {
			m_PossibleMoves.Clear();
			return queenMoves();
		}

		//private
		private List<ChessSquare> queenMoves() {
			for (Adjacent.e_Adjacent i = Adjacent.e_Adjacent.START; i != Adjacent.e_Adjacent.END; i = Adjacent.next(i)) {
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


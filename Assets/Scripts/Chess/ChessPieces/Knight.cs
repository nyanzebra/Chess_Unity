using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chess {
	public class Knight : ChessPiece {
		public Knight(GameObject piece) : base(piece) {
		}

		public override List<ChessSquare> getPossibleMoves() {
			m_PossibleMoves.Clear();
			return knightMoves();
		}
		//private
		private List<ChessSquare> knightMoves() {
			for (Adjacent.e_Adjacent i = Adjacent.e_Adjacent.START_INTER_CARDINAL; i != Adjacent.e_Adjacent.END; i = Adjacent.nextSemiCardinal(i)) {
				ChessSquare square = m_Square.adjacent(i);
				for (Adjacent.e_Adjacent j = Adjacent.e_Adjacent.START_CARDINAL; j != Adjacent.e_Adjacent.END; j = Adjacent.nextCardinal(j)) {
					if (square != null && square.adjacent(j) != null && !m_Square.isCardinal(square.adjacent(j))) {
						if (!m_Square.hasFriendlyPiece(square.adjacent(j))) {
							m_PossibleMoves.Add (square.adjacent(j));
						}
					}
				}
			}
			return m_PossibleMoves;
		}
	}
}


using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chess {
	public class King : ChessPiece {
		public King(GameObject piece) : base(piece) {
		}

		public override List<ChessSquare> getPossibleMoves() {
			m_PossibleMoves.Clear();
			return kingMoves();
		}

		public bool Check {
			get {
				return m_IsInCheck;
			}
			set {
				m_IsInCheck = value;
			}
		}

		public bool CheckMate {
			get {
				return m_IsInCheckMate;
			}
			set {
				m_IsInCheckMate = value;
			}
		}

		//private
		private bool m_IsInCheck;
		private bool m_IsInCheckMate;
		private List<ChessSquare> kingMoves() {
			for (Adjacent.e_Adjacent i = Adjacent.e_Adjacent.START; i != Adjacent.e_Adjacent.END; i = Adjacent.next(i)) {
				m_PossibleMoves.Add(m_Square.adjacent(i));
			}
			return m_PossibleMoves;
		}
	}
}


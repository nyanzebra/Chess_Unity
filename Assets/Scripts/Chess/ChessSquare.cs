using System;
using UnityEngine;

namespace Chess {
	public static class Adjacent {
		public enum e_Adjacent {
			UP,
			UP_LEFT,
			UP_RIGHT,
			LEFT,
			RIGHT,
			DOWN,
			DOWN_LEFT,
			DOWN_RIGHT,
			START,
			START_CARDINAL,
			START_INTER_CARDINAL,
			END,
		};
		public static e_Adjacent oppositeCardinal(e_Adjacent direction) {
			switch (direction) {
			case e_Adjacent.UP:
				return e_Adjacent.DOWN;
			case e_Adjacent.DOWN:
				return e_Adjacent.UP;
			case e_Adjacent.LEFT:
				return e_Adjacent.RIGHT;
			case e_Adjacent.RIGHT:
				return e_Adjacent.LEFT;
			case e_Adjacent.UP_LEFT:
				return e_Adjacent.DOWN_RIGHT;
			case e_Adjacent.UP_RIGHT:
				return e_Adjacent.DOWN_LEFT;
			case e_Adjacent.DOWN_LEFT:
				return e_Adjacent.UP_RIGHT;
			case e_Adjacent.DOWN_RIGHT:
				return e_Adjacent.UP_LEFT;
			default:
				return e_Adjacent.END;
			}
		}
		public static e_Adjacent nextCardinal(e_Adjacent direction) {
			switch (direction) {
			case e_Adjacent.START_CARDINAL:
				return e_Adjacent.RIGHT;
			case e_Adjacent.UP:
				return e_Adjacent.LEFT;
			case e_Adjacent.LEFT:
				return e_Adjacent.END;
			case e_Adjacent.RIGHT:
				return e_Adjacent.UP;
			default:
				return e_Adjacent.END;
			}
		}

		public static e_Adjacent nextSemiCardinal(e_Adjacent direction) {
			switch (direction) {
			case e_Adjacent.START_INTER_CARDINAL:
				return e_Adjacent.DOWN_RIGHT;
			case e_Adjacent.DOWN_RIGHT:
				return e_Adjacent.UP_RIGHT;
			case e_Adjacent.UP_RIGHT:
				return e_Adjacent.UP_LEFT;
			case e_Adjacent.UP_LEFT:
				return e_Adjacent.END;
			default:
				return e_Adjacent.END;
			}
		}
		public static e_Adjacent next(e_Adjacent direction) {
			switch (direction) {
			case e_Adjacent.START:
				return e_Adjacent.DOWN_LEFT;
			case e_Adjacent.DOWN_LEFT:
				return e_Adjacent.DOWN_RIGHT;
			case e_Adjacent.DOWN_RIGHT:
				return e_Adjacent.RIGHT;
			case e_Adjacent.RIGHT:
				return e_Adjacent.UP;
			case e_Adjacent.UP:
				return e_Adjacent.UP_LEFT;
			case e_Adjacent.UP_LEFT:
				return e_Adjacent.UP_RIGHT;
			case e_Adjacent.UP_RIGHT:
				return e_Adjacent.LEFT;
			case e_Adjacent.LEFT:
				return e_Adjacent.END;
			default:
				return e_Adjacent.END;
			}
		}
	}

	public class ChessSquare {
		//public
		public ChessSquare() {
			m_Row = 0;
			m_Row = 0;
		}

		public ChessSquare(GameObject square) {
			m_Square = square;
			m_Column = (int) m_Square.renderer.transform.position.x / 15;
			m_Row = (int) - m_Square.renderer.transform.position.z / 15;

			m_Highlight = GameObject.CreatePrimitive(PrimitiveType.Plane);
			float x = m_Square.transform.position.x;
			float y = m_Square.transform.position.y;
			float z = m_Square.transform.position.z;
			m_Highlight.transform.position = new Vector3(x, y, z);
			m_Highlight.tag = "Square";
			m_Highlight.transform.localScale = m_Square.transform.localScale;
			m_Highlight.renderer.enabled = false;
		}

		public ChessPiece getChessPiece() { 
			return m_ChessPiece; 
		}

		public void setChessPiece(ChessPiece piece) {
			m_ChessPiece = piece;
		}

		public bool hasChessPiece() {
			return (m_ChessPiece == null) ? false : true;
		}

		public bool hasEnemyPiece(ChessSquare square) {
			if (square != null && square.hasChessPiece()) {
				return (m_ChessPiece.getColor () != square.getChessPiece ().getColor ()) ? true : false;
			}
			return false;
		}

		public bool hasFriendlyPiece(ChessSquare square) {
			if (square != null && square.hasChessPiece()) {
				return (m_ChessPiece.getColor () == square.getChessPiece ().getColor ()) ? true : false;
			}
			return false;
		}

		public bool withinSquare(uint x, uint z) {
			if (m_Column == z) {
				if (m_Row == x) {
					return true;
				}
			}
			return false;
		}

		public Vector3 position() {
			return m_Square.renderer.transform.position;
		}

		public void highlight(Color color) {
			m_Highlight.renderer.material.SetColor("_Color", color);
			m_Highlight.renderer.enabled = true;
			m_Square.renderer.enabled = false;
		}

		public void unhighlight() {
			m_Highlight.renderer.material.SetColor("_Color", Color.blue);
			m_Highlight.renderer.enabled = false;
			m_Square.renderer.enabled = true;
		}

		public bool isHighlighted() {
			return m_Highlight.renderer.enabled;
		}

		public GameObject getGameObject() {
			return m_Square;
		}

		public bool isCardinal(ChessSquare square) {
			for (Adjacent.e_Adjacent i = Adjacent.e_Adjacent.START_CARDINAL; i != Adjacent.e_Adjacent.END; i = Adjacent.nextCardinal(i)) {
				if (square == adjacent(i)) {
					return true;
				}
			}
			return false;
		}

		public ChessSquare adjacent(Adjacent.e_Adjacent square) {
			if (ChessBoard.Instance.SelectedPiece.getColor () == "Black") {
				square = Adjacent.oppositeCardinal(square);
			}
			switch (square) {
			case Adjacent.e_Adjacent.UP:
				if (m_Row - 1 >= 0) {
					return ChessBoard.Instance.getSquares () [m_Row - 1, m_Column];
				} else {
					return null;
				}
			case Adjacent.e_Adjacent.UP_LEFT:
				if (m_Column - 1 >= 0 && m_Row - 1 >= 0) {
					return ChessBoard.Instance.getSquares () [m_Row - 1, m_Column - 1];
				} else {
					return null;
				}
			case Adjacent.e_Adjacent.UP_RIGHT:
				if (m_Column + 1 < 8 && m_Row - 1 >= 0) {
					return ChessBoard.Instance.getSquares () [m_Row - 1, m_Column + 1];
				} else {
					return null;
				}
			case Adjacent.e_Adjacent.LEFT:
				if (m_Column - 1 >= 0) {
					return ChessBoard.Instance.getSquares () [m_Row, m_Column - 1];
				} else {
					return null;
				}
			case Adjacent.e_Adjacent.RIGHT:
				if (m_Column + 1 < 8) {
					return ChessBoard.Instance.getSquares () [m_Row, m_Column + 1];
				} else {
					return null;
				}
			case Adjacent.e_Adjacent.DOWN:
				if (m_Row + 1 < 8) {
					return ChessBoard.Instance.getSquares () [m_Row + 1, m_Column];
				} else {
					return null;
				}
			case Adjacent.e_Adjacent.DOWN_LEFT:
				if (m_Column - 1 >= 0 && m_Row + 1 < 8) {
					return ChessBoard.Instance.getSquares () [m_Row + 1, m_Column - 1];
				} else {
					return null;
				}
			case Adjacent.e_Adjacent.DOWN_RIGHT:
				if (m_Column + 1 < 8 && m_Row + 1 < 8) {
					return ChessBoard.Instance.getSquares () [m_Row + 1, m_Column + 1];
				} else {
					return null;
				}
			default:
				return null;
			}
		}

		//private
		private ChessPiece m_ChessPiece;
		private GameObject m_Square;
		private GameObject m_Highlight;
		private int m_Row;
		private int m_Column;
	}
}


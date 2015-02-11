using System;
using UnityEngine;
using System.Collections.Generic;

namespace Chess {
	public class Graveyard {
		public Graveyard () {
		}

		public void bury(GameObject gameobject) {
			string grave = "";
			foreach (string name in m_Tombs.Keys) {
			 	if (gameobject.name.Contains(name)) {
					grave = name;
				}
			}
			if (m_Tombs.ContainsKey (grave)) {
				m_Tombs [grave] += 1;
			}
		}

		public int tombs () {
			return m_Tombs.Count;
		}

		public KeyValuePair<string, uint> tomb(uint i) {
			uint begin = 0;
			foreach (KeyValuePair<string, uint> pair in m_Tombs) {
				if (begin == i) {
					return pair;
				}
				++begin;
			}
			throw new Exception ("pair error");
		}

		private Dictionary<string, uint> m_Tombs = new Dictionary<string, uint>() {
			{"Pawn", 0},
			{"Rook", 0},
			{"Knight", 0},
			{"Bishop", 0},
			{"Queen", 0},
		};
	}
}


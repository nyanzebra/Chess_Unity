using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameNetwork {
	public class NetworkManager {
		//public
		public NetworkManager () {
		}

		public void startServer() {
			if (Network.peerType == NetworkPeerType.Disconnected) {
				Network.InitializeServer (2, m_ConnectionPort, false);
			}
		}

		public void endConnection() {
			if (Network.peerType == NetworkPeerType.Client || Network.peerType == NetworkPeerType.Server) {
				Network.Disconnect (200);
			}
		}

		public void joinServer() {
			if (Network.peerType == NetworkPeerType.Disconnected) {
				Network.Connect (m_ConnectionIP, m_ConnectionPort);
			}
		}

		//private
		private const string m_ConnectionIP = "127.0.0.1";
		private const int m_ConnectionPort = 25001;
	}
}


// 
//  PeerHandler.cs
//  
//  Author:
//       Kelly Elton <kelly.elton@skylabsonline.com>
// 
//  Copyright (c) 2012 Kelly Elton - Skylabs Corporation
//  All Rights Reserved.

using System;
using System.Net.Sockets;
using MonoTorrent.PeerSwarm;
using MonoTorrent;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using MonoTorrent.Common;
using System.IO;
using Octgn.Common.Sockets;
using Octgn.Data;

namespace Octgn.Peer
{
	public static class PeerHandler
	{
		public static PeerSwarmManager Swarm;
		public static S2SHandler S2SHandler { get; set; }
		static PeerHandler ()
		{
			S2SHandler = new S2SHandler(Prefs.S2SPort);
			StartPeerSwarm();
		}
		public static void MessageAllPeers(SocketMessage message)
		{
			foreach(var s in Swarm.Peers)
				S2SHandler.MessagePeer(message,s.ConnectionUri);
		}
		private static void StartPeerSwarm()
		{
			var sha = new SHA1CryptoServiceProvider();
			var hash = new InfoHash(sha.ComputeHash(Encoding.ASCII.GetBytes("Octgn")));
			var param = new MonoTorrent.Client.Tracker.AnnounceParameters
			{
				InfoHash = hash,
				BytesDownloaded = 0,
				BytesLeft = 0,
				BytesUploaded = 0,
				PeerId = "Octgn",
				Ipaddress = IPAddress.Any.ToString(),
				Port = Prefs.S2SPort,
				RequireEncryption = false,
				SupportsEncryption = true,
				ClientEvent = new TorrentEvent()
			};

			Swarm = new PeerSwarmManager( Prefs.S2SPort, param, hash, Path.Combine(Environment.CurrentDirectory, "DHTNodes.txt"));
			Swarm.AddTracker("udp://tracker.openbittorrent.com:80/announce");
			Swarm.AddTracker("udp://tracker.publicbt.com:80/announce");
			Swarm.AddTracker("udp://tracker.ccc.de:80/announce");
			Swarm.AddTracker("udp://tracker.istole.it:80/announce");
			Swarm.AddTracker("http://announce.torrentsmd.com:6969/announce");
			Swarm.PeersFound += SwarmPeersFound;
			Swarm.LogOutput += LogOutput;
			Swarm.Start();
		}
		private static void LogOutput(object sender , LogEventArgs e)
		{
			if (!e.DebugLog)
				Common.Log.L(e.Message);
			else Common.Log.D(e.Message);
		}

		private static void SwarmPeersFound(object sender , PeersFoundEventArgs e)
		{
			 
		}
	}
}


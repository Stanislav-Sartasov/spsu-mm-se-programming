using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using PeerToPeerChat.Chat;

namespace PeerToPeerChat.UnitTests
{
	public class ChatTests
	{
		[Test]
		public void ChatCreate()
		{
			var chat = new ChatClient(10203);
			chat.Dispose();
		}

		[Test]
		public void TwoChatsInteract()
		{
			var messageReceived = 0;

			var chat10204 = new ChatClient(10204);
			var chat10205 = new ChatClient(10205);
			chat10205.ConnectTo(IPEndPoint.Parse("127.0.0.1:10204"));

			chat10204.OnMessage += _ => messageReceived++;
			chat10205.OnMessage += _ => messageReceived++;

			Thread.Sleep(100);
			
			chat10204.Send("A");

			Thread.Sleep(100);

			chat10205.Send("B");

			Thread.Sleep(100);

			Assert.AreEqual(messageReceived, 4);

			chat10204.Dispose();
			chat10205.Dispose();
		}

		[Test]
		public void ThreeChatsInteract()
		{
			Interact(3, new List<int>());
		}

		[Test]
		public void FourChatsInteract()
		{
			Interact(4, new List<int>());
		}

		[Test]
		public void FiveChatsInteract()
		{
			Interact(5, new List<int>());
		}

		[Test]
		public void ThreeChatsInteractDestruction()
		{
			Interact(3, new List<int>{1,2});
		}

		[Test]
		public void FourChatsInteractDestruction()
		{
			Interact(4, new List<int>{2,0});
		}

		[Test]
		public void FiveChatsInteractDestruction()
		{
			Interact(5, new List<int>{1,3,4});
		}

		private void Interact(int chatCount, List<int> disposeSeq)
		{
			var messageReceived = 0;

			var chats = new List<ChatClient>();

			for (var i = 0; i < chatCount; i++)
			{
				chats.Add(new ChatClient(10502 + i));
				Thread.Sleep(100);
			}

			foreach (var chat in chats)
			{
				chat.OnMessage += _ => messageReceived++;

				if (chat == chats.FirstOrDefault())
					continue;

				chat.ConnectTo(IPEndPoint.Parse("127.0.0.1:10502"));
				Thread.Sleep(100);
			}

			foreach (var elem in disposeSeq)
			{
				chats[elem].Dispose();

				Thread.Sleep(100);
			}

			var index = 0;

			foreach (var chat in chats)
			{
				if (disposeSeq.Contains(index++))
					continue;

				chat.Send(Convert.ToString(index));

				Thread.Sleep(100);
			}

			chatCount -= disposeSeq.Count;

			Assert.AreEqual(messageReceived, chatCount * chatCount);

			index = 0;

			foreach (var chat in chats)
			{
				if (disposeSeq.Contains(index++))
					continue;

				chat.Dispose();
			}

			Thread.Sleep(100);
		}
	}
}

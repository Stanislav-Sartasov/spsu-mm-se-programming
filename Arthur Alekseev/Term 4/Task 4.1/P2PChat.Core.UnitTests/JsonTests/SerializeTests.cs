using System.Net;
using Newtonsoft.Json;
using P2PChat.Core.Message;
using P2PChat.Core.Message.Fallback;
using P2PChat.Core.Message.UserMessage;
using P2PChat.Core.Message.Wrapper;

namespace P2PChat.Core.UnitTests.JsonTests;

public class SerializeTests
{
	[Test]
	public void FallbackListenerRequestSerialization()
	{
		var jsonObject = new FallbackListenerRequest();

		var jsonString = jsonObject.ToJson();

		Console.WriteLine(FallbackListenerRequest.IsValid(jsonString));

		Assert.IsTrue(FallbackListenerRequest.IsValid(jsonString));

		var deserialized = FallbackListenerRequest.FromJson(jsonString);

		Assert.Pass();
	}

	[Test]
	public void ListenerResponseSerialization()
	{
		var jsonObject = new ListenerResponse(true, IPEndPoint.Parse("0.1.2.3:4"));

		var jsonString = jsonObject.ToJson();

		Assert.IsTrue(ListenerResponse.IsValid(jsonString));

		var deserialized = ListenerResponse.FromJson(jsonString);

		Assert.AreEqual(deserialized.HasFallback, jsonObject.HasFallback);
		Assert.AreEqual(deserialized.ListenerEndpoint.Endpoint.ToString(),
			jsonObject.ListenerEndpoint.Endpoint.ToString());
	}

	[Test]
	public void MyPortRequestSerialization()
	{
		var jsonObject = new MyPortRequest();

		var jsonString = jsonObject.ToJson();

		Assert.IsTrue(MyPortRequest.IsValid(jsonString));

		var deserialized = MyPortRequest.FromJson(jsonString);

		Assert.Pass();
	}

	[Test]
	public void SkipMessageSerialization()
	{
		var jsonObject = new SkipMessage();

		var jsonString = jsonObject.ToJson();

		Assert.IsTrue(SkipMessage.IsValid(jsonString));

		var deserialized = SkipMessage.FromJson(jsonString);

		Assert.Pass();
	}

	[Test]
	public void SkipRequestSerialization()
	{
		var jsonObject = new SkipRequest();

		var jsonString = jsonObject.ToJson();

		Assert.IsTrue(SkipRequest.IsValid(jsonString));

		var deserialized = SkipMessage.FromJson(jsonString);

		Assert.Pass();
	}

	[Test]
	public void UserMessageSerialization()
	{
		var jsonObject = new UserMessage("Hello!", "Name");

		var jsonString = jsonObject.ToJson();

		Assert.IsTrue(UserMessage.IsValid(jsonString));

		var deserialized = UserMessage.FromJson(jsonString);

		Assert.AreEqual(deserialized.Content, jsonObject.Content);
		Assert.AreEqual(deserialized.Name, jsonObject.Name);
	}

	[Test]
	public void IPEndPointWrapperSerialization()
	{
		var jsonObject = new IpEndPointWrapper(IPEndPoint.Parse("0.1.2.3:4"));

		var jsonString = JsonConvert.SerializeObject(jsonObject);

		var deserialized = JsonConvert.DeserializeObject<IpEndPointWrapper>(jsonString);

		Assert.AreEqual(deserialized.Endpoint.ToString(), jsonObject.Endpoint.ToString());
	}
}
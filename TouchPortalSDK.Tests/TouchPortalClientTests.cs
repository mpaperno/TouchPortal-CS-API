﻿using System;
using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using TouchPortalSDK.Models.Enums;
using TouchPortalSDK.Sockets;
using TouchPortalSDK.Utils;

namespace TouchPortalSDK.Tests
{
    public class TouchPortalClientTests
    {
        private Mock<ITouchPortalSocket> _touchPortalSocketMock;
        
        private TouchPortalClient _client;

        [SetUp]
        public void Setup()
        {
            _touchPortalSocketMock = new Mock<ITouchPortalSocket>(MockBehavior.Strict);
            _touchPortalSocketMock.SetupProperty(mock => mock.OnMessageCallBack);
            _touchPortalSocketMock.SetupProperty(mock => mock.OnClose);
            
            _client = new TouchPortalClient(default, _touchPortalSocketMock.Object, Mock.Of<ICustomUpdatesStorage>());
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" \t ")]
        public void UpdateActionData_NoDataId(string dataId)
        {
            var result = _client.UpdateActionData(dataId, default, default, DataType.Number, default);
            Assert.False(result);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" \t ")]
        public void UpdateActionData_WithDataId_NoInstanceId(string instanceId)
        {
            Dictionary<string, object> parameter = null;
            _touchPortalSocketMock.Setup(mock => mock.SendMessage(It.IsAny<Dictionary<string, object>>()))
                .Callback<Dictionary<string, object>>(dict => parameter = dict)
                .Returns(true);

            var result = _client.UpdateActionData("dataId", default, default, DataType.Number, instanceId);
            Assert.True(result);
            Assert.AreEqual("updateActionData", parameter["type"]);
            CollectionAssert.DoesNotContain(parameter.Keys, "instanceId");

            var data = (Dictionary<string, object>)parameter["data"];
            Assert.AreEqual("dataId", data["id"]);
            Assert.AreEqual(0.0, data["maxValue"]);
            Assert.AreEqual(0.0, data["minValue"]);
            Assert.AreEqual("Number", data["type"]);

            _touchPortalSocketMock.Verify(mock => mock.SendMessage(It.IsAny<Dictionary<string, object>>()), Times.Once);
        }

        [Test]
        public void UpdateActionData_WithDataId_WithInstanceId()
        {
            Dictionary<string, object> parameter = null;
            _touchPortalSocketMock.Setup(mock => mock.SendMessage(It.IsAny<Dictionary<string, object>>()))
                .Callback<Dictionary<string, object>>(dict => parameter = dict)
                .Returns(true);

            var result = _client.UpdateActionData("dataId", default, default, DataType.Number, "instanceId");
            Assert.True(result);
            Assert.AreEqual("updateActionData", parameter["type"]);
            Assert.AreEqual("instanceId", parameter["instanceId"]);

            var data = (Dictionary<string, object>)parameter["data"];
            Assert.AreEqual("dataId", data["id"]);
            Assert.AreEqual(0.0, data["maxValue"]);
            Assert.AreEqual(0.0, data["minValue"]);
            Assert.AreEqual("Number", data["type"]);

            _touchPortalSocketMock.Verify(mock => mock.SendMessage(It.IsAny<Dictionary<string, object>>()), Times.Once);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" \t ")]
        public void SettingUpdate_NoName(string name)
        {
            var result = _client.SettingUpdate(name, "value");
            Assert.False(result);
        }

        [Test]
        public void SettingUpdate_WithName()
        {
            Dictionary<string, object> parameter = null;
            _touchPortalSocketMock.Setup(mock => mock.SendMessage(It.IsAny<Dictionary<string, object>>()))
                .Callback<Dictionary<string, object>>(dict => parameter = dict)
                .Returns(true);

            var result = _client.SettingUpdate("name", "value");
            Assert.True(result);
            Assert.AreEqual("settingUpdate", parameter["type"]);
            Assert.AreEqual("name", parameter["name"]);
            Assert.AreEqual("value", parameter["value"]);

            _touchPortalSocketMock.Verify(mock => mock.SendMessage(It.IsAny<Dictionary<string, object>>()), Times.Once);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" \t ")]
        public void CreateState_NoId(string stateId)
        {
            var result = _client.CreateState(stateId, "displayName", "defaultValue");
            Assert.False(result);
        }

        [Test]
        public void CreateState_WithId()
        {
            Dictionary<string, object> parameter = null;
            _touchPortalSocketMock.Setup(mock => mock.SendMessage(It.IsAny<Dictionary<string, object>>()))
                .Callback<Dictionary<string, object>>(dict => parameter = dict)
                .Returns(true);

            var result = _client.CreateState("stateId", "displayName", "defaultValue");
            Assert.True(result);
            Assert.AreEqual("createState", parameter["type"]);
            Assert.AreEqual("stateId", parameter["id"]); 
            Assert.AreEqual("displayName", parameter["desc"]);
            Assert.AreEqual("defaultValue", parameter["defaultValue"]);

            _touchPortalSocketMock.Verify(mock => mock.SendMessage(It.IsAny<Dictionary<string, object>>()), Times.Once);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" \t ")]
        public void RemoveState_NoId(string stateId)
        {
            var result = _client.RemoveState(stateId);
            Assert.False(result);
        }

        [Test]
        public void RemoveState_WithId()
        {
            Dictionary<string, object> parameter = null;
            _touchPortalSocketMock.Setup(mock => mock.SendMessage(It.IsAny<Dictionary<string, object>>()))
                .Callback<Dictionary<string, object>>(dict => parameter = dict)
                .Returns(true);

            var result = _client.RemoveState("stateId");
            Assert.True(result);
            Assert.AreEqual("removeState", parameter["type"]);
            Assert.AreEqual("stateId", parameter["id"]);

            _touchPortalSocketMock.Verify(mock => mock.SendMessage(It.IsAny<Dictionary<string, object>>()), Times.Once);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" \t ")]
        public void StateUpdate_NoId(string stateId)
        {
            var result = _client.StateUpdate(stateId, "value");
            Assert.False(result);
        }

        [Test]
        public void StateUpdate_WithId()
        {
            Dictionary<string, object> parameter = null;
            _touchPortalSocketMock.Setup(mock => mock.SendMessage(It.IsAny<Dictionary<string, object>>()))
                .Callback<Dictionary<string, object>>(dict => parameter = dict)
                .Returns(true);

            var result = _client.StateUpdate("stateId", "value");
            Assert.True(result);
            Assert.AreEqual("stateUpdate", parameter["type"]);
            Assert.AreEqual("stateId", parameter["id"]);
            Assert.AreEqual("value", parameter["value"]);

            _touchPortalSocketMock.Verify(mock => mock.SendMessage(It.IsAny<Dictionary<string, object>>()), Times.Once);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" \t ")]
        public void ChoiceUpdate_NoId(string listId)
        {
            var result = _client.ChoiceUpdate(listId, new [] { "value" }, null);
            Assert.False(result);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" \t ")]
        public void ChoiceUpdate_WithId_WithOutInstanceId(string instanceId)
        {
            Dictionary<string, object> parameter = null;
            _touchPortalSocketMock.Setup(mock => mock.SendMessage(It.IsAny<Dictionary<string, object>>()))
                .Callback<Dictionary<string, object>>(dict => parameter = dict)
                .Returns(true);

            var result = _client.ChoiceUpdate("listId", new[] { "value" }, instanceId);
            Assert.True(result);
            Assert.AreEqual("choiceUpdate", parameter["type"]);
            Assert.AreEqual("listId", parameter["id"]);
            CollectionAssert.AreEquivalent(new [] { "value" }, (IEnumerable)parameter["value"]);
            CollectionAssert.DoesNotContain(parameter.Keys, "instanceId");

            _touchPortalSocketMock.Verify(mock => mock.SendMessage(It.IsAny<Dictionary<string, object>>()), Times.Once);
        }

        [Test]
        public void ChoiceUpdate_WithId_WithInstanceId()
        {
            Dictionary<string, object> parameter = null;
            _touchPortalSocketMock.Setup(mock => mock.SendMessage(It.IsAny<Dictionary<string, object>>()))
                .Callback<Dictionary<string, object>>(dict => parameter = dict)
                .Returns(true);

            var result = _client.ChoiceUpdate("listId", new[] { "value" }, "instanceId");
            Assert.True(result);
            Assert.AreEqual("choiceUpdate", parameter["type"]);
            Assert.AreEqual("listId", parameter["id"]);
            CollectionAssert.AreEquivalent(new[] { "value" }, (IEnumerable)parameter["value"]);
            Assert.AreEqual("instanceId", parameter["instanceId"]);

            _touchPortalSocketMock.Verify(mock => mock.SendMessage(It.IsAny<Dictionary<string, object>>()), Times.Once);
        }

        [Test]
        public void Close_WithException()
        {
            var expected = new Exception();
            _touchPortalSocketMock.Setup(mock => mock.CloseSocket());
            Exception result = null;
            _client.OnClosed = exception => result = exception;
            _client.Close(expected);
            Assert.AreSame(expected, result);
        }

        [Test]
        public void Close_WithoutException()
        {
            _touchPortalSocketMock.Setup(mock => mock.CloseSocket());
            Exception result = null;
            _client.OnClosed = exception => result = exception;
            _client.Close(null);
            Assert.Null(result);
        }

        [Test]
        public void Connect_Success()
        {
            _touchPortalSocketMock.Setup(mock => mock.Connect()).Returns(true);
            _touchPortalSocketMock.Setup(mock => mock.Pair()).Returns("{}");
            _touchPortalSocketMock.Setup(mock => mock.Listen()).Returns(true);

            var result = _client.Connect();
            Assert.True(result);

            _touchPortalSocketMock.Verify(mock => mock.Connect(), Times.Once);
            _touchPortalSocketMock.Verify(mock => mock.Pair(), Times.Once);
            _touchPortalSocketMock.Verify(mock => mock.Listen(), Times.Once);
        }

        [Test]
        public void Connect_CouldNotListen()
        {
            _touchPortalSocketMock.Setup(mock => mock.Connect()).Returns(true);
            _touchPortalSocketMock.Setup(mock => mock.Pair()).Returns("{}");
            _touchPortalSocketMock.Setup(mock => mock.Listen()).Returns(false);

            var result = _client.Connect();
            Assert.False(result);

            _touchPortalSocketMock.Verify(mock => mock.Connect(), Times.Once);
            _touchPortalSocketMock.Verify(mock => mock.Pair(), Times.Once);
            _touchPortalSocketMock.Verify(mock => mock.Listen(), Times.Once);
        }

        [Test]
        public void Connect_CouldNotPair()
        {
            _touchPortalSocketMock.Setup(mock => mock.Connect()).Returns(true);
            _touchPortalSocketMock.Setup(mock => mock.Pair()).Returns(null as string);

            var result = _client.Connect();
            Assert.False(result);

            _touchPortalSocketMock.Verify(mock => mock.Connect(), Times.Once);
            _touchPortalSocketMock.Verify(mock => mock.Pair(), Times.Once);
        }

        [Test]
        public void Connect_CouldNotConnect()
        {
            _touchPortalSocketMock.Setup(mock => mock.Connect()).Returns(false);

            var result = _client.Connect();
            Assert.False(result);

            _touchPortalSocketMock.Verify(mock => mock.Connect(), Times.Once);
        }
    }
}
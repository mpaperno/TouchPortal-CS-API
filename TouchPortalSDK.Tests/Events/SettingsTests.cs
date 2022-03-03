using AutoFixture;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using TouchPortalSDK.Clients;
using TouchPortalSDK.Interfaces;
using TouchPortalSDK.Messages.Events;
using TouchPortalSDK.Tests.Fixtures;
using Encoding = System.Text.Encoding;

namespace TouchPortalSDK.Tests.Events
{
    public class SettingsTests
    {
        [Theory]
        [AutoMoqData]
        public void InfoEvent(IFixture fixture, [Frozen]Mock<ITouchPortalEventHandler> eventHandler)
        {
            IMessageHandler messageHandler = fixture.Create<TouchPortalClient>();
            messageHandler.OnMessage(Encoding.UTF8.GetBytes("{\"type\":\"info\"}"));

            eventHandler.Verify(mock => mock.OnInfoEvent(It.IsAny<InfoEvent>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void CloseEvent(IFixture fixture, [Frozen] Mock<ITouchPortalEventHandler> eventHandler)
        {
            IMessageHandler messageHandler = fixture.Create<TouchPortalClient>();
            messageHandler.OnMessage(Encoding.UTF8.GetBytes("{\"type\":\"closePlugin\"}"));

            eventHandler.Verify(mock => mock.OnClosedEvent("TouchPortal sent a Plugin close event."), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void ListChangeEvent(IFixture fixture, [Frozen] Mock<ITouchPortalEventHandler> eventHandler)
        {
            IMessageHandler messageHandler = fixture.Create<TouchPortalClient>();
            messageHandler.OnMessage(Encoding.UTF8.GetBytes("{\"type\":\"listChange\"}"));

            eventHandler.Verify(mock => mock.OnListChangedEvent(It.IsAny<ListChangeEvent>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void BroadcastEvent(IFixture fixture, [Frozen] Mock<ITouchPortalEventHandler> eventHandler)
        {
            IMessageHandler messageHandler = fixture.Create<TouchPortalClient>();
            messageHandler.OnMessage(Encoding.UTF8.GetBytes("{\"type\":\"broadcast\"}"));

            eventHandler.Verify(mock => mock.OnBroadcastEvent(It.IsAny<BroadcastEvent>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void SettingsEvent(IFixture fixture, [Frozen] Mock<ITouchPortalEventHandler> eventHandler)
        {
            IMessageHandler messageHandler = fixture.Create<TouchPortalClient>();
            messageHandler.OnMessage(Encoding.UTF8.GetBytes("{\"type\":\"settings\"}"));

            eventHandler.Verify(mock => mock.OnSettingsEvent(It.IsAny<SettingsEvent>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void ActionEvent_Action(IFixture fixture, [Frozen] Mock<ITouchPortalEventHandler> eventHandler)
        {
            IMessageHandler messageHandler = fixture.Create<TouchPortalClient>();
            messageHandler.OnMessage(Encoding.UTF8.GetBytes("{\"type\":\"action\"}"));

            eventHandler.Verify(mock => mock.OnActionEvent(It.IsAny<ActionEvent>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void ActionEvent_Up(IFixture fixture, [Frozen] Mock<ITouchPortalEventHandler> eventHandler)
        {
            IMessageHandler messageHandler = fixture.Create<TouchPortalClient>();
            messageHandler.OnMessage(Encoding.UTF8.GetBytes("{\"type\":\"up\"}"));

            eventHandler.Verify(mock => mock.OnActionEvent(It.IsAny<ActionEvent>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void ActionEvent_Down(IFixture fixture, [Frozen] Mock<ITouchPortalEventHandler> eventHandler)
        {
            IMessageHandler messageHandler = fixture.Create<TouchPortalClient>();
            messageHandler.OnMessage(Encoding.UTF8.GetBytes("{\"type\":\"down\"}"));

            eventHandler.Verify(mock => mock.OnActionEvent(It.IsAny<ActionEvent>()), Times.Once);
        }

        [Theory]
        [AutoMoqData]
        public void UnhandledEvent(IFixture fixture, [Frozen] Mock<ITouchPortalEventHandler> eventHandler)
        {
            IMessageHandler messageHandler = fixture.Create<TouchPortalClient>();
            messageHandler.OnMessage(Encoding.UTF8.GetBytes("{\"type\":\"unknown\"}"));

            eventHandler.Verify(mock => mock.OnUnhandledEvent(It.IsAny<string>()), Times.Once);
        }
    }
}

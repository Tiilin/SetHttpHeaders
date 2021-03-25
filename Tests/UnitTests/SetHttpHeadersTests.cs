using Microsoft.BizTalk.Message.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Winterdom.BizTalk.PipelineTesting;

namespace BizTalkComponents.PipelineComponents.SetHttpHeaders.Tests.UnitTests
{
    [TestClass]
    public class SetHttpHeadersTests
    {
        [TestMethod]
        public void Test()
        {
            IBaseMessage message = MessageHelper.CreateFromString("<test/>");
            message.Context.Write("property1", "namespace1", "filename1.txt");
            message.Context.Write("property2", "namespace2", "123");

            var component = new SetHttpHeaders()
            {
                Disabled = false,
                DestinationPath = "MyNamespace#HttpHeaders",
                HeaderNames = "FileName;StoreID",
                PromoteProperty = true,
                PropertyPaths = "namespace1#property1;namespace2#property2"
            };

            SendPipelineWrapper SendPipeline = PipelineFactory.CreateEmptySendPipeline();
            SendPipeline.AddComponent(component, PipelineStage.PreAssemble);

            IBaseMessage results = SendPipeline.Execute(message);

            Assert.AreEqual("FileName:filename1.txt StoreID:123", results.Context.Read("HttpHeaders", "MyNamespace"));
        }

    }
}

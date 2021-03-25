using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BizTalkComponents.Utils;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.Message.Interop;
using IComponent = Microsoft.BizTalk.Component.Interop.IComponent;
using System.Text;

namespace BizTalkComponents.PipelineComponents.SetHttpHeaders
{
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [System.Runtime.InteropServices.Guid("F005F9D0-8D47-11EB-85EF-29AA23E9CDFD")]
    [ComponentCategory(CategoryTypes.CATID_Any)]
    public partial class SetHttpHeaders : IComponent, IBaseComponent,
                                        IPersistPropertyBag, IComponentUI
    {
        [DisplayName("Disabled")]
        [Description("Disables the component")]
        [RequiredRuntime]
        public bool Disabled { get; set; }
        
        [DisplayName("Property Paths")]
        [Description("Paths to properties semi colon separated, i.e. http//:examplenamespace1#Myproperty1;http//:examplenamespace2#Myproperty2")]
        [RequiredRuntime]
        public string PropertyPaths { get; set; }

        [DisplayName("Header names")]
        [Description("Name of each http header seperated by semi colon, i.e. MyHeader1;MyHeader2")]
        [RequiredRuntime]
        public string HeaderNames { get; set; }

        [DisplayName("Destination path")]
        [Description("The property Path to where the returned value will be promoted to, i.e. http//:examplenamespace#Myproperty")]
        [RequiredRuntime]
        public string DestinationPath { get; set; }

        [DisplayName("Promote Property")]
        [Description("Specifies whether the property should be promoted or just written to the context.")]
        [RequiredRuntime]
        public bool PromoteProperty { get; set; }

        public IBaseMessage Execute(IPipelineContext pContext, IBaseMessage pInMsg)
        {
            string errorMessage;

            if (!Validate(out errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            //Component will not be executed if the property is set to true
            if (Disabled)
            {
                return pInMsg;
            }

            IBaseMessageContext context = pInMsg.Context;
            
            string[] PropertyPathList = PropertyPaths.Split(';');
            string[] HeaderNamesList = HeaderNames.Split(';');

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < PropertyPathList.Length; i++)
            {
                sb.Append(HeaderNamesList[i] + ":" + (string)context.Read(new ContextProperty(PropertyPathList[i])));

                if (i < PropertyPathList.Length - 1)
                {
                    sb.Append(" ");
                }
            }
            string HttpHeaders = sb.ToString();

            if (PromoteProperty)
            {
                pInMsg.Context.Promote(new ContextProperty(DestinationPath), HttpHeaders);
            }
            else
            {
                pInMsg.Context.Write(new ContextProperty(DestinationPath), HttpHeaders);
            }
            return pInMsg;
        }
    }
}

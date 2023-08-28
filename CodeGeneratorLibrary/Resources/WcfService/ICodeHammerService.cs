using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace CodeHammerServiceLibrary
{
    [ServiceContract]
    public interface ICodeHammerService
    {
        /// <summary>
        /// Codes the hammer <DtoClass> get all.
        /// </summary>
        /// <returns>return true if success</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/<DtoClass>DataContract")]
        List<<DtoClass>DataContract> CodeHammer<DtoClass>GetAllJson();

        /// <summary>
        /// Codes the hammer <DtoClass> get by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>return true if success</returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,

            UriTemplate = "/<DtoClass>DataContract/{id}")]
        <DtoClass>DataContract CodeHammer<DtoClass>GetByIDJson(string id);

        /// <summary>
        /// Codes the hammer <DtoClass> remove by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>return true if success</returns>
        [OperationContract]
        [WebInvoke(Method = "DELETE",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "/<DtoClass>DataContract/{id}")]
        int CodeHammer<DtoClass>RemoveByIDJson(string id);

        /// <summary>
        /// Codes the hammer <DtoClass> save.
        /// </summary>
        /// <param name="<DtoClassTemp>DataContract">The <DtoClassTemp> data contract.</param>
        /// <returns>return true if success</returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare,
           UriTemplate = "/<DtoClass>DataContract/")]
        int CodeHammer<DtoClass>SaveJson(<DtoClass>DataContract <DtoClassTemp>DataContract);

        /// <summary>
        /// Codes the hammer <DtoClass> set by identifier.
        /// </summary>
        /// <param name="<DtoClassTemp>DataContract">The <DtoClassTemp> data contract.</param>
        /// <param name="id">The identifier.</param>
        /// <returns>return true if success</returns>
        [OperationContract]
        [WebInvoke(Method = "PUT",
           ResponseFormat = WebMessageFormat.Json,
           RequestFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Bare,
           UriTemplate = "/<DtoClass>DataContract/{id}")]
        int CodeHammer<DtoClass>SetByIDJson(<DtoClass>DataContract <DtoClassTemp>DataContract, string id);
    }
}
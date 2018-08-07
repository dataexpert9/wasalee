using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wasalee.Controllers;

namespace Wasalee.Swagger
{
    public class FileUploadOperation : IOperationFilter
    {
        private readonly IEnumerable<string> _actionsWithUpload = new[]
        {
        //add your upload actions here!
        NamingHelpers.GetOperationId<FetchAnythingController>(nameof(FetchAnythingController.RequestItem))
    };

        public void Apply(Swashbuckle.AspNetCore.Swagger.Operation operation, OperationFilterContext context)
        {
            if (_actionsWithUpload.Contains(operation.OperationId))
            {
                
                //operation.Parameters.Remove(IParameter.Name.Contains(""))
                //operation.Parameters.Add(new NonBodyParameter
                //{
                //    Name = "file",
                //    In = "formData",
                //    Description = "Upload File",
                //    Required = true,
                //    Type = "file"
                //});
                //operation.Consumes.Add("multipart/form-data");
            }
        }
    }

    /// <summary>
    /// Refatoring friendly helper to get names of controllers and operation ids
    /// </summary>
    public class NamingHelpers
    {
        public static string GetOperationId<T>(string actionName) where T : Controller => $"{GetControllerName<T>()}{actionName}";

        public static string GetControllerName<T>() where T : Controller => typeof(T).Name.Replace(nameof(Controller), string.Empty);
    }
}

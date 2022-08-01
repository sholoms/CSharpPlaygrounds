//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

#pragma warning disable 108 // Disable "CS0108 '{derivedDto}.ToJson()' hides inherited member '{dtoBase}.ToJson()'. Use the new keyword if hiding was intended."
#pragma warning disable 114 // Disable "CS0114 '{derivedDto}.RaisePropertyChanged(String)' hides inherited member 'dtoBase.RaisePropertyChanged(String)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword."
#pragma warning disable 472 // Disable "CS0472 The result of the expression is always 'false' since a value of type 'Int32' is never equal to 'null' of type 'Int32?'
#pragma warning disable 1573 // Disable "CS1573 Parameter '...' has no matching param tag in the XML comment for ...
#pragma warning disable 1591 // Disable "CS1591 Missing XML comment for publicly visible type or member ..."
#pragma warning disable 8073 // Disable "CS8073 The result of the expression is always 'false' since a value of type 'T' is never equal to 'null' of type 'T?'"
#pragma warning disable 3016 // Disable "CS3016 Arrays as attribute arguments is not CLS-compliant"
#pragma warning disable 8603 // Disable "CS8603 Possible null reference return"

namespace ApiPlayground.controllers
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v13.0.0.0))")]
    public interface ICalculatorController
    {

        /// <param name="body">calculation</param>

        /// <returns>Successfully calculated</returns>

        System.Threading.Tasks.Task<CalculationResponse> CalculatePostAsync(CalculationRequest body);


        /// <returns>Successfully calculated</returns>

        System.Threading.Tasks.Task<CalculationResponse> CalculateGetAsync(string calculation);


        /// <returns>Successfully calculated</returns>

        System.Threading.Tasks.Task<FileResultResponse> CalculateFileAsync();


        /// <param name="body">add-to-file-request</param>

        /// <returns>Successfully calculated</returns>

        System.Threading.Tasks.Task<FileResultResponse> AddToFileAsync(AddToFileRequest body);

    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v13.0.0.0))")]

    public partial class CalculatorController : Controller
    {
        private ICalculatorController _implementation;

        public CalculatorController(ICalculatorController implementation)
        {
            _implementation = implementation;
        }

        /// <param name="body">calculation</param>
        /// <returns>Successfully calculated</returns>
        [Microsoft.AspNetCore.Mvc.HttpPost, Microsoft.AspNetCore.Mvc.Route("calculate")]
        public System.Threading.Tasks.Task<CalculationResponse> CalculatePost([Microsoft.AspNetCore.Mvc.FromBody] CalculationRequest body)
        {

            return _implementation.CalculatePostAsync(body);
        }

        /// <returns>Successfully calculated</returns>
        [Microsoft.AspNetCore.Mvc.HttpGet, Microsoft.AspNetCore.Mvc.Route("calculate")]
        public System.Threading.Tasks.Task<CalculationResponse> CalculateGet([Microsoft.AspNetCore.Mvc.FromQuery] string calculation)
        {

            return _implementation.CalculateGetAsync(calculation);
        }

        /// <returns>Successfully calculated</returns>
        [Microsoft.AspNetCore.Mvc.HttpGet, Microsoft.AspNetCore.Mvc.Route("file/results")]
        public System.Threading.Tasks.Task<FileResultResponse> CalculateFile()
        {

            return _implementation.CalculateFileAsync();
        }

        /// <param name="body">add-to-file-request</param>
        /// <returns>Successfully calculated</returns>
        [Microsoft.AspNetCore.Mvc.HttpPost, Microsoft.AspNetCore.Mvc.Route("file/add")]
        public System.Threading.Tasks.Task<FileResultResponse> AddToFile([Microsoft.AspNetCore.Mvc.FromBody] AddToFileRequest body)
        {

            return _implementation.AddToFileAsync(body);
        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class CalculationResponse
    {
        [Newtonsoft.Json.JsonProperty("result", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Result { get; set; }

        private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

        [Newtonsoft.Json.JsonExtensionData]
        public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class FileResultResponse
    {
        [Newtonsoft.Json.JsonProperty("results", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.List<CalculationResponse> Results { get; set; }

        private System.Collections.Generic.IDictionary<string, object> _additionalProperties = new System.Collections.Generic.Dictionary<string, object>();

        [Newtonsoft.Json.JsonExtensionData]
        public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties; }
            set { _additionalProperties = value; }
        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class CalculationRequest
    {
        /// <summary>
        /// Order Id
        /// </summary>
        [Newtonsoft.Json.JsonProperty("calculation", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Calculation { get; set; }

        /// <summary>
        /// leftToRight
        /// </summary>
        [Newtonsoft.Json.JsonProperty("ltr", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool Ltr { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class AddToFileRequest
    {
        [Newtonsoft.Json.JsonProperty("calculations", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.List<string> Calculations { get; set; }

    }


}

#pragma warning restore 1591
#pragma warning restore 1573
#pragma warning restore  472
#pragma warning restore  114
#pragma warning restore  108
#pragma warning restore 3016
#pragma warning restore 8603
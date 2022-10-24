using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class NoContentResponseDto
    {

        [JsonIgnore]
        public int StatusCode { get; set; }

        public List<String> Errors { get; set; }

        //Static Factory Metot Design Patern
        public static NoContentResponseDto Success(int statusCode)
        {
            return new NoContentResponseDto{ StatusCode = statusCode};
        }


        public static NoContentResponseDto Fail(int statusCode, List<string> errors)
        {
            return new NoContentResponseDto{ StatusCode = statusCode, Errors = errors };
        }
        public static NoContentResponseDto Fail(int statusCode, string errors)
        {
            return new NoContentResponseDto { StatusCode = statusCode, Errors = new List<string> { errors } };
        }
    }
}

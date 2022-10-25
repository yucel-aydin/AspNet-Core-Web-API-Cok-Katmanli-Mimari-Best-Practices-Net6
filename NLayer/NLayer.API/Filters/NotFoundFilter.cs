using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Filters
{
    /// <summary>
    /// Bir filter contructorda parametre alıyorsa kullanırken ServiceFilter üzerinden kullanılır.
    /// Ve ilgili tipide program.cs de eklemek gerekir.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();
            if (idValue == null)
            {
                await next.Invoke();
                return;
            }
              
            var id = (int)idValue;
            var anyEntity=await _service.AnyAsync(x=>x.Id==id);  
            if (anyEntity)
            {
                await next.Invoke();
                return;
            }
            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentResponseDto>.Fail(400, $"{typeof(T).Name}({id}) not found"));
          
        }
    }
}

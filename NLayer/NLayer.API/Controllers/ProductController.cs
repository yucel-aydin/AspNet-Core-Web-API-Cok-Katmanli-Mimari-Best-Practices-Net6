using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{

    public class ProductController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<Product> _service;
        public ProductController(IMapper mapper, IService<Product> service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var product = await _service.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(product.ToList());
            return CreateActionResult<List<ProductDto>>(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));  
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult<ProductDto>(CustomResponseDto<ProductDto>.Success(200, productsDto));

        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto));
            var productsDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult<ProductDto>(CustomResponseDto<ProductDto>.Success(201, productsDto));
        }


        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productDto));
            return CreateActionResult(CustomResponseDto<NoContentResponseDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(product);
            return CreateActionResult(CustomResponseDto<NoContentResponseDto>.Success(204));

        }
    }
}

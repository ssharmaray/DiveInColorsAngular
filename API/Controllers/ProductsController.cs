using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastucture.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.DTOs;
using AutoMapper;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        public readonly IGenericRepository<Product> _productsRepo;
        public readonly IGenericRepository<ProductBrand> _productsBrandRepo;
        public readonly IGenericRepository<ProductType> _productsTypeRepo;
        public readonly IMapper _mapper;
    
        public ProductsController(IGenericRepository<Product> productsRepo, 
                                    IGenericRepository<ProductBrand> productsBrandRepo,
                                    IGenericRepository<ProductType> productsTypeRepo,
                                    IMapper mapper)
        {
            _productsRepo = productsRepo;
            _productsBrandRepo = productsBrandRepo;
            _productsTypeRepo = productsTypeRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productsRepo.ListAsync(spec);
            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id); 
            var product = await _productsRepo.GetEntityWithSpec(spec);
            
            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

       [HttpGet("brands")]
       public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            return Ok(await _productsBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
       public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            return Ok(await _productsTypeRepo.ListAllAsync());
        }

    }

    
}
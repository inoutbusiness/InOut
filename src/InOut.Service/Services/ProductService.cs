using AutoMapper;
using InOut.Common;
using InOut.Domain.DTOs;
using InOut.Domain.Entities;
using InOut.Domain.Enums;
using InOut.Domain.Exceptions;
using InOut.Domain.Models.Product;
using InOut.Infrastructure.Repositories.Interfaces;
using InOut.Service.Services.Interfaces;
using System.Transactions;

namespace InOut.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;

        public ProductService(IProductRepository productRepository, IMapper mapper, IBrandRepository brandRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _brandRepository = brandRepository;
        }

        public ProductDto CreateProduct(ProductModel productModel)
        {
            if (_productRepository.GetProductIdByName(productModel.Name).IsIdValid())
                throw new AlreadyExistsException("Já existe o produto informado");

            ProductDto productDto;
            using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                var product = new Product()
                {
                    Name = productModel.Name,
                    Code = BuildProductSKUCode(productModel.Name, productModel.Type),
                    BrandId = _brandRepository.GetBrandIdByName(productModel.BrandName),
                    UnitOfMeasurement = UoMConverter(productModel.UnitOfMeasurement),
                    UnitPrice = productModel.UnitPrice,
                    Color = ColorConverter(productModel.Color),
                    InventoryId = productModel.InventoryId,
                    Type = ProductTypeConverter(productModel.Type),
                    Description = productModel.Description
                };

                var productCreated = _productRepository.Create(product);
                tc.Complete();

                productDto = _mapper.Map<ProductDto>(product);
            }

            return productDto;
        }

        public IEnumerable<ProductNameWithCode> GetProductNamesWithCode()
        {
            return _productRepository.GetAllProductNamesWithCode();
        }

        public string BuildProductSKUCode(string productName, string productType)
        {
            var productNameWords = productName.Split(' ').ToList();
            var productSKUCode = "";

            productNameWords.ForEach(x => productSKUCode += x.Count() > 2 ? x.Substring(0, 3) : x.Substring(0, 2));
            productSKUCode += productType.Substring(0, 4);
            productSKUCode += DateTime.Today.Day.ToString() + DateTime.Today.Month.ToString().Count();

            productSKUCode = productSKUCode.ToUpper();

            if (_productRepository.GetProductIdByCode(productSKUCode).IsIdValid())
            {
                // Ja existe o código
            }

            return productSKUCode;
        }

        /////////////////////////////////////////////////////
        // Melhor colocar os métodos abaixo em outro lugar???
        /////////////////////////////////////////////////////
        
        public EUnitOfMeasurement UoMConverter(string uom)
        {
            if (uom == "Meter")
            {
                return EUnitOfMeasurement.Meter;
            }
            else if (uom == "Liter")
            {
                return EUnitOfMeasurement.Liter;

            }
            else if (uom == "Kilo")
            {
                return EUnitOfMeasurement.Kilo;

            }
            else if (uom == "Gram")
            {
                return EUnitOfMeasurement.Gram;

            }
            else if (uom == "Unit")
            {
                return EUnitOfMeasurement.Unit;
            }

            return EUnitOfMeasurement.Others;
        }

        public EColor ColorConverter(string color)
        {
            if (color == "Amarelo")
            {
                return EColor.Yellow;
            }
            else if (color == "Vermelho")
            {
                return EColor.Red;
            }
            else if (color == "Roxo")
            {
                return EColor.Purple;
            }
            else if (color == "Azul")
            {
                return EColor.Blue;
            }
            else if (color == "Verde")
            {
                return EColor.Green;
            }
            else if (color == "Branco")
            {
                return EColor.White;
            }
            else if (color == "Preto")
            {
                return EColor.Black;
            }

            return EColor.Black;
        }

        public EProductType ProductTypeConverter(string productType)
        {
            if (productType == "Comida")
            {
                return EProductType.Food;
            }
            else if (productType == "Roupas")
            {
                return EProductType.Clothes;

            }
            else if (productType == "Eletronico")
            {
                return EProductType.Eletronics;

            }
            else if (productType == "Livro")
            {
                return EProductType.Books;

            }

            return EProductType.Others;
        }
    }
}
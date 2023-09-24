using AutoMapper;
using AutoMapperBenchmark.Dtos;
using AutoMapperBenchmark.Models;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace AutoMapperBenchmark;
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.SlowestToFastest)]
public class MappingBenchmark
{
    private IMapper _mapper;
    private Product[] _products;

    [Params(10, 100, 1000)]
    public int NumberOfElements { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var config = new MapperConfiguration( cfg =>
        {
            cfg.CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price + src.Price * 100 / src.Percentage));
        });
        _mapper = config.CreateMapper();
        _products = GenerateProducts();
    }

    [Benchmark]
    public ProductDto[] AutoMapperMapping()
    {
        return _products.Select(p => _mapper.Map<ProductDto>(p)).ToArray();
    }
    [Benchmark]
    public ProductDto[] ManualMapping()
    {
        return _products.Select(p => p.MapProductDto()).ToArray();
    }

    private Product[] GenerateProducts()
    {
        return Enumerable.Range(1 , NumberOfElements).Select(i => new Product(){ Id = i, Name = $"Product name {i}",Description = $"Product description {i}", Price = 45.50m, Percentage = 19}).ToArray();
    }

}
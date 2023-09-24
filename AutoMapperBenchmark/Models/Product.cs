using AutoMapperBenchmark.Dtos;

namespace AutoMapperBenchmark.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Percentage { get; set; }

    public ProductDto MapProductDto()
    {
        return new ProductDto()
        {
            Name = Name,
            Description = Description,
            Price = Price + Price * 100/ Percentage
        };
    }
}
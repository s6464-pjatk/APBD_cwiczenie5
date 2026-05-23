using System.ComponentModel.DataAnnotations;

namespace Cwiczenia5.Dtos;

public class PcCreateRequestDto
{
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string? Name { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public double? Weight { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int? Warranty { get; set; }

    [Required]
    public DateTime? CreatedAt { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int? Stock { get; set; }
}

public class PcUpdateRequestDto
{
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string? Name { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public double? Weight { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int? Warranty { get; set; }

    [Required]
    public DateTime? CreatedAt { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int? Stock { get; set; }
}

public class PcResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
}

public class PcWithComponentsResponseDto : PcResponseDto
{
    public List<PcComponentResponseDto> Components { get; set; } = new();
}

public class PcComponentResponseDto
{
    public int Amount { get; set; }
    public ComponentResponseDto Component { get; set; } = null!;
}

public class ComponentResponseDto
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ManufacturerResponseDto Manufacturer { get; set; } = null!;
    public ComponentTypeResponseDto Type { get; set; } = null!;
}

public class ManufacturerResponseDto
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public DateOnly FoundationDate { get; set; }
}

public class ComponentTypeResponseDto
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = null!;
    public string Name { get; set; } = null!;
}

using Domain.Models;

namespace Shared.Dto;

public class ContractDto : EntityDto
{
    public BusinessDto Business { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalSpaces { get; set; }
    public bool IsDeleted { get; set; }

    public ContractDto(Contract contract)
    {
        Id = contract.Id;
        Business = new BusinessDto(contract.Business);
        StartDate = contract.StartDate;
        EndDate = contract.EndDate;
        TotalSpaces = contract.TotalSpaces;
        IsDeleted = contract.IsDeleted;
    }

    public ContractDto()
    {
    }
}
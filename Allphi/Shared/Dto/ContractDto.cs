using Domain.Models;

namespace Shared.Dto;

public class ContractDto : EntityDto
{
    public BusinessDto Reserved { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalSpaces { get; set; }
    public bool IsDeleted { get; set; }

    public ContractDto(Contract contract)
    {
        Id = contract.Id;
        Reserved = new BusinessDto(contract.Business);
        StartDate = contract.StartDate;
        EndDate = contract.EndDate;
        TotalSpaces = contract.TotalSpaces;
        IsDeleted = contract.IsDeleted;
    }

    public ContractDto(BusinessDto reserved, DateTime startDate, DateTime endDate, int totalSpaces)
    {
        Reserved = reserved;
        StartDate = startDate;
        EndDate = endDate;
        TotalSpaces = totalSpaces;
        IsDeleted = false;
    }

    public ContractDto()
    {
    }
}
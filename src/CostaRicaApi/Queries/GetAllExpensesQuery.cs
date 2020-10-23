using CostaRicaApi.Dtos;
using MediatR;

namespace CostaRicaApi.Queries {
    public class GetAllExpensesQuery : IRequest<ListResponseDto<ShallowExpenseDto>> {
        
    }
}
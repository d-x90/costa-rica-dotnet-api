using CostaRicaApi.Dtos;
using MediatR;

namespace CostaRicaApi.Queries {
    public class GetExpenseByIdQuery : IRequest<ShallowExpenseDto> {
        public int Id {get; set;}

        public GetExpenseByIdQuery(int id)
        {
            Id = id;
        }
    }
}
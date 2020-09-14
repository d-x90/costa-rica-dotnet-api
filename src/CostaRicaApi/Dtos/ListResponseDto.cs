using System.Collections.Generic;

namespace CostaRicaApi.Dtos {
    public class ListResponseDto<T> {
        public List<T> Items { get; set; }
    }
}
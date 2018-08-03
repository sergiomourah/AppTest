using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AppTest.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddPedidoAsync(T item);
        Task<bool> UpdatePedidoAsync(T item);
        Task<bool> DeletePedidoAsync(long? id);
        Task<T> GetPedidoByIdAsync(long? id);
        Task<T> GetPedidoByClienteAsync(string cliente);
        Task<ObservableCollection<T>> GetPedidosAsync(bool forceRefresh = false);
    }
}

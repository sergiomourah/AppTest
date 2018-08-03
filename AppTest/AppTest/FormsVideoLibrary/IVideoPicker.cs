using System;
using System.Threading.Tasks;

namespace AppTest
{
    public interface IVideoPicker
    {
        Task<string> GetVideoFileAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumelService.Interface
{
    public interface ICsvHelperLoaderService
    {
        Task LoadAsync(string filePath);
    }
}

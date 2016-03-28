using DSLNG.PEAR.Services.Responses.FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSLNG.PEAR.Services.Interfaces
{
    public interface IProcessBlueprintServices
    {
        GetFilesResponse GetAll();

        GetFileResponse GetFile(int Id);
    }
}

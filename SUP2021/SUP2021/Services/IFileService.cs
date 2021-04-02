using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SUP2021.Services
{
    public interface IFileService
    {
        void SavePicture(string name, Stream data, string location = "temp");

    }
}

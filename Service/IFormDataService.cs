using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface IFormDataService
    {
        IEnumerable<FormData> GetFormData();
        FormData GetFormData(int id);
        bool SaveFormData(FormData data);
    }
}

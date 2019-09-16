using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface IFormDataRepository
    {
        FormData GetFormData();
        FormData GetFormData(int id);
        bool SaveFormData(FormData data);
    }
}

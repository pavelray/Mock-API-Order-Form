using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    interface IFormDataRepository
    {
        FormData GetFormData();
        bool SaveFormData();
    }
}

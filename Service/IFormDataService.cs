using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    interface IFormDataService
    {
        FormData GetFormData();
        bool SaveFormData();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Repository
{
    class FormDataRepository : IFormDataRepository
    {
        private IFormDataRepository _repo = null;
        public FormDataRepository(IFormDataRepository repo)
        {
            _repo = repo;
        }

        public FormData GetFormData()
        {
            throw new NotImplementedException();
        }

        public bool SaveFormData()
        {
            throw new NotImplementedException();
        }
    }
}

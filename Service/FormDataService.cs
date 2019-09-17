using System;
using System.Collections.Generic;
using System.Text;
using Model;
using Repository;

namespace Service
{
    public class FormDataService: IFormDataService
    {
        private IFormDataRepository _repository;

        public FormDataService(IFormDataRepository repository)
        {
            _repository = repository;
        }

        public bool SaveFormData(FormData data)
        {
           return _repository.SaveFormData(data);
        }

        public IEnumerable<FormData> GetFormData()
        {
            IEnumerable<FormData> data = _repository.GetFormData();

            return data;
        }

        public FormData GetFormData(int id)
        {
            FormData data =  _repository.GetFormData(id);

            return data;
        }

        
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using DatabaseFactory;
using Model;

namespace Repository
{
    public class FormDataRepository : IFormDataRepository
    {
        private DBFactory _dbFactory;
        private IDbConnection _connection;
      
        public FormDataRepository()
        {
            var temp = new DBFactory();

            _dbFactory = DBFactory.GetInstance();
            _connection = _dbFactory.GetConnection(DbServer.SQLSERVER);
        }

        public IEnumerable<FormData> GetFormData()
        {
            string query = $"SELECT * FROM FormData";
            List<FormData> formDatas = _dbFactory.GetData<FormData>(_connection, new CommandDefinition(query));

            List<FormData> data = new List<FormData>();

            foreach (FormData d in formDatas)
            {
                
                FormData formData = (new FormDataRepository()).GetFormData(d.Id);
                data.Add(formData);
            }

            return data;
        }

        public FormData GetFormData(int id)
        {
            FormData formData = null;

            try
            {
                string query = $"SELECT * FROM ShippingAddress WHERE FormId=@id";
                ShippingAddress shippingAddress = _dbFactory.GetData<ShippingAddress>(_connection, new CommandDefinition(query, new { id = id })).FirstOrDefault();

                query = $"SELECT * FROM BillingAddress WHERE FormId=@id";
                BillingAddress billingAddress = _dbFactory.GetData<BillingAddress>(_connection, new CommandDefinition(query, new { id = id })).FirstOrDefault();

                query = $"SELECT * FROM OrderDetails WHERE FormId=@id";
                OrderDetails orderDetails = _dbFactory.GetData<OrderDetails>(_connection, new CommandDefinition(query, new { id = id })).FirstOrDefault();

                query = $"SELECT * FROM Specifications WHERE FormId=@id";
                Specifications specifications = _dbFactory.GetData<Specifications>(_connection, new CommandDefinition(query, new { id = id })).FirstOrDefault();

                formData = new FormData();

                formData.Specifications = specifications;
                formData.ShippingAddress = shippingAddress;
                formData.BillingAddress = billingAddress;
                formData.OrderDetails = orderDetails;
                formData.Id = id;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }

            return formData;
        }

        public bool SaveFormData(FormData data)
        {
            data.Created_Date = DateTime.Now;

            try
            {
                string sql = @"Insert into FormData ([CreatedDate]) values (@CreatedDate);SELECT CAST(SCOPE_IDENTITY() as int)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CreatedDate", data.Created_Date);
                int formId = _dbFactory.ExecuteNonQuery(_connection, sql, parameters);

                int value = (new FormDataRepository()).InsertIntoShipping(data, formId);
                value = (new FormDataRepository()).InsertIntoBilling(data, formId);
                value = (new FormDataRepository()).InsertIntoOrderDetails(data, formId);
                value = (new FormDataRepository()).InsertIntoSpecification(data, formId);

                return true;
            }
            catch(Exception ex)
            {

            }
            return false;
        }

        int InsertIntoShipping(FormData data, int id)
        {
            string sql = @"Insert into ShippingAddress ([FullName]
                           ,[AddressLine1]
                           ,[AddressLine2]
                           ,[City]
                           ,[State]
                           ,[Country]
                           ,[Pincode]
                           ,[ContactNo]
                           ,[FormId]) values (
                            @FullName,
                            @AddressLine1,
                            @AddressLine2,
                            @City,
                            @State,
                            @Country,
                            @Pincode,
                            @ContactNo,
                            @FormId)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FullName", data.ShippingAddress.FullName);
            parameters.Add("@AddressLine1", data.ShippingAddress.AddressLine1);
            parameters.Add("@AddressLine2", data.ShippingAddress.AddressLine2);
            parameters.Add("@City", data.ShippingAddress.City);
            parameters.Add("@State", data.ShippingAddress.State);
            parameters.Add("@Country", data.ShippingAddress.Country);
            parameters.Add("@Pincode", data.ShippingAddress.Pincode);
            parameters.Add("@ContactNo", data.ShippingAddress.ContactNo);
            parameters.Add("@FormId", id);

           return  _dbFactory.ExecuteNonQuery(_connection, sql, parameters);
        }


        int InsertIntoBilling(FormData data, int id)
        {
            string sql = @"Insert into [BillingAddress] ([FullName]
                           ,[AddressLine1]
                           ,[AddressLine2]
                           ,[City]
                           ,[State]
                           ,[Country]
                           ,[Pincode]
                           ,[ContactNo]
                           ,[IsSameAsShipping]
                           ,[FormId]) values (
                            @FullName,
                            @AddressLine1,
                            @AddressLine2,
                            @City,
                            @State,
                            @Country,
                            @Pincode,
                            @ContactNo,
                            @IsSameAsShipping,
                            @FormId)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@FullName", data.BillingAddress.FullName);
            parameters.Add("@AddressLine1", data.BillingAddress.AddressLine1);
            parameters.Add("@AddressLine2", data.BillingAddress.AddressLine2);
            parameters.Add("@City", data.BillingAddress.City);
            parameters.Add("@State", data.BillingAddress.State);
            parameters.Add("@Country", data.BillingAddress.Country);
            parameters.Add("@Pincode", data.BillingAddress.Pincode);
            parameters.Add("@ContactNo", data.BillingAddress.ContactNo);
            parameters.Add("@IsSameAsShipping", data.BillingAddress.IsSameAsShipping);
            parameters.Add("@FormId", id);

            return _dbFactory.ExecuteNonQuery(_connection, sql, parameters);
        }

        int InsertIntoOrderDetails(FormData data, int id)
        {
            string sql = @"Insert into OrderDetails ([Price]
                            ,[Quantity]
                            ,[FormId]) values (
                            @Price,
                            @Quantity,
                            @FormId)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Price", data.OrderDetails.Price);
            parameters.Add("@Quantity", data.OrderDetails.Quantity);
            parameters.Add("@FormId", id);

            return _dbFactory.ExecuteNonQuery(_connection, sql, parameters);
        }

        int InsertIntoSpecification(FormData data, int id)
        {
            string sql = @"Insert into Specifications
                           ([ActualThickness]
                           ,[ActualLength]
                           ,[ActualWidth]
                           ,[SeriesName]
                           ,[Type]
                           ,[Warranty]
                           ,[FormId]) values (
                            @ActualThickness,
                            @ActualLength,
                            @ActualWidth,
                            @SeriesName,
                            @Type,
                            @Warranty,
                            @FormId)";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ActualThickness", data.Specifications.ActualThickness);
            parameters.Add("@ActualLength", data.Specifications.ActualLength);
            parameters.Add("@ActualWidth", data.Specifications.ActualWidth);
            parameters.Add("@SeriesName", data.Specifications.SeriesName);
            parameters.Add("@Type", data.Specifications.Type);
            parameters.Add("@Warranty", data.Specifications.Warranty);
            parameters.Add("@FormId", id);

            return _dbFactory.ExecuteNonQuery(_connection, sql, parameters);
        }
    }
}

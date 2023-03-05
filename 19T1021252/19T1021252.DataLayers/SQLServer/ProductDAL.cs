using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021252.DomainModels;

namespace _19T1021252.DataLayers.SQLServer
{
    /// <summary>
    /// Cài đặt chức năng xử lý dữ liệu liên quan đến mặt hàng
    /// </summary>
    public class ProductDAL : _BaseDAL, IProductDAL
    {
        public ProductDAL(string connectionString) : base(connectionString)
        {
        }

        public int Add(Product data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO Products(ProductName, SupplierID, CategoryID, Unit, Price, Photo)
                                    VALUES (@ProductName, @SupplierID, @CategoryID, @Unit, @Price, @Photo)
                                    SELECT SCOPE_IDENTITY()
                                    ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);

                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        public long AddAttribute(ProductAttribute data)
        {
            long result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"INSERT INTO ProductAttributes(AttributeName, AttributeValue, DisplayOrder, ProductID)
                                    VALUES (@AttributeName, @AttributeValue, @DisplayOrder, @ProductID)
                                    SELECT SCOPE_IDENTITY()
                                    ";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@AttributeName", data.AttributeName);
                cmd.Parameters.AddWithValue("@AttributeValue", data.AttributeValue);
                cmd.Parameters.AddWithValue("@DisplayOrder", data.DisplayOrder);
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);

                result = Convert.ToInt64(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        public long AddPhoto(ProductPhoto data)
        {
            throw new NotImplementedException();
        }

        public int Count(string searchValue = "", int categoryID = 0, int supplierID = 0)
        {
            int count = 0;

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT COUNT(*) 
                                    FROM Products
                                    WHERE ((@SearchValue = N'') OR (ProductName LIKE @SearchValue))
	                                    AND ((@CategoryID = 0) OR (CategoryID = @CategoryID)) 
	                                    AND ((@SupplierID = 0) OR (SupplierID = @SupplierID))";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);
                cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                cmd.Parameters.AddWithValue("@SupplierID", supplierID);

                count = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return count;
        }

        public bool Delete(int productID)
        {
            /*bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Employees WHERE EmployeeID = @EmployeeID AND NOT EXISTS(SELECT * FROM Orders WHERE EmployeeID = @EmployeeID)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@EmployeeID", productID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;*/
            throw new NotImplementedException();
        }

        public bool DeleteAttribute(long attributeID)
        {
            throw new NotImplementedException();
        }

        public bool DeletePhoto(long photoID)
        {
            throw new NotImplementedException();
        }

        public Product Get(int productID)
        {
            Product data = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Products WHERE ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ProductID", productID);
                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    data = new Product()
                    {
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        ProductName = Convert.ToString(dbReader["ProductName"]),
                        SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                        CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                        Unit = Convert.ToString(dbReader["Unit"]),
                        Price = Convert.ToDecimal(dbReader["Price"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                    };
                }
                cn.Close();
            }
            return data;
        }

        public ProductAttribute GetAttribute(long attributeID)
        {
            throw new NotImplementedException();
        }

        public ProductPhoto GetPhoto(long photoID)
        {
            throw new NotImplementedException();
        }

        public bool InUsed(int productID)
        {
            /*bool result = false;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT CASE WHEN EXISTS(SELECT * FROM Orders WHERE EmployeeID = @EmployeeID) THEN 1 ELSE 0 END";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@EmployeeID", id);

                result = Convert.ToBoolean(cmd.ExecuteScalar());

                cn.Close();
            }

            return result;*/
            throw new NotImplementedException();
        }

        public IList<Product> List(int page = 1, int pageSize = 0, string searchValue = "", int categoryID = 0, int supplierID = 0)
        {
            List<Product> data = new List<Product>();

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                        FROM 
                                        (
	                                        SELECT	*, ROW_NUMBER() OVER (ORDER BY ProductName) AS RowNumber
	                                        FROM	Products 
	                                        WHERE ((@SearchValue = N'') OR (ProductName LIKE @SearchValue))
	                                            AND ((@CategoryID = 0) OR (CategoryID = @CategoryID)) 
	                                            AND ((@SupplierID = 0) OR (SupplierID = @SupplierID))
                                        ) AS t
                                        WHERE (@PageSize = 0) OR (t.RowNumber BETWEEN (@Page - 1) * @PageSize + 1 AND @Page * @PageSize)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@Page", page);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                cmd.Parameters.AddWithValue("@SearchValue", searchValue);
                cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                cmd.Parameters.AddWithValue("@CategoryID", categoryID);



                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Product()
                    {
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        ProductName = Convert.ToString(dbReader["ProductName"]),
                        SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                        CategoryID = Convert.ToInt32(dbReader["CategoryID"]),
                        Unit = Convert.ToString(dbReader["Unit"]),
                        Price = Convert.ToDecimal(dbReader["Price"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                    });
                }
                dbReader.Close();
                cn.Close();
            }

            return data;
            //throw new NotImplementedException();
        }

        public IList<ProductAttribute> ListAttributes(int productID)
        {
            List<ProductAttribute> data = new List<ProductAttribute>();
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM ProductAttributes WHERE ProductID = @productID";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@productID", productID);

                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new ProductAttribute()
                    {
                        AttributeID = Convert.ToInt64(dbReader["AttributeID"]),
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        AttributeName = Convert.ToString(dbReader["AttributeName"]),
                        AttributeValue = Convert.ToString(dbReader["AttributeValue"]),
                        DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),
                    });
                }
                dbReader.Close();
                cn.Close();
            }
            return data;
        }

        public IList<ProductPhoto> ListPhotos(int productID)
        {
            List<ProductPhoto> data = new List<ProductPhoto>();
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM ProductPhotos WHERE ProductID = @productID";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@productID", productID);

                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new ProductPhoto()
                    {
                        PhotoID = Convert.ToInt64(dbReader["PhotoID"]),
                        ProductID = Convert.ToInt32(dbReader["ProductID"]),
                        Photo = Convert.ToString(dbReader["Photo"]),
                        Description = Convert.ToString(dbReader["Description"]),
                        DisplayOrder = Convert.ToInt32(dbReader["DisplayOrder"]),
                        IsHidden = Convert.ToBoolean(dbReader["IsHidden"]),
                    });
                }
                dbReader.Close();
                cn.Close();
            }
            return data;
        }

        public bool Update(Product data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Products
                                    SET ProductName = @ProductName, SupplierID = @SupplierID,
                                                        CategoryID = @CategoryID, Unit = @Unit, Price = @Price, Photo = @Photo
                                    WHERE ProductID = @ProductID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ProductID", data.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", data.ProductName);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@CategoryID", data.CategoryID);
                cmd.Parameters.AddWithValue("@Unit", data.Unit);
                cmd.Parameters.AddWithValue("@Price", data.Price);
                cmd.Parameters.AddWithValue("@Photo", data.Photo);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }

        public bool UpdateAttribute(ProductAttribute data)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePhoto(ProductPhoto data)
        {
            throw new NotImplementedException();
        }
    }
}

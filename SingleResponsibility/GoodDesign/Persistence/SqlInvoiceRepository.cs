using Microsoft.Data.SqlClient;
using SingleResponsibility.GoodDesign.Dto;
using SingleResponsibility.GoodDesign.Interfaces;

namespace SingleResponsibility.GoodDesign.Persistence;

// Persistence responsibility
public class SqlInvoiceRepository : IInvoiceRepository
{
    private readonly string _connectionString;
    public SqlInvoiceRepository(string connectionString) => _connectionString = connectionString;

    public int Save(string details, decimal total)
    {
        // No changes to the rest of the file are needed for this specific error.
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            using (SqlCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO solid.Invoice (Details, Total)
                    VALUES (@details, @total);
                    SELECT CAST(SCOPE_IDENTITY() AS int);"; // Add this line to retrieve the new ID
                cmd.Parameters.AddWithValue("@details", details);
                cmd.Parameters.AddWithValue("@total", total);

                //cmd.ExecuteNonQuery(); // ExecuteNonQuery only inserts the record without returning the ID

                // ExecuteScalar returns the Identity value for the inserted row
                return (int)cmd.ExecuteScalar();
            }
        }
    }
}
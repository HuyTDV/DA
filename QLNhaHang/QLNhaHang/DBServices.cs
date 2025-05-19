using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace QLNhaHang;

internal class DBServices
{
    private string conStr = @"Data Source=NITRO-5-TIGER\MSSQLSEVER;Initial Catalog=SqlQuanLyNhaHang;Integrated Security=True";
    private SqlConnection mySqlConnection;

    public DBServices() // Fixed constructor declaration  
    {
        mySqlConnection = new SqlConnection(conStr); // Fixed constructor body  
    }

    public DataTable GetData(string sSql)
    {
        try
        {
            SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter(sSql, mySqlConnection);
            DataTable myDataTable = new DataTable();
            mySqlDataAdapter.Fill(myDataTable);
            return myDataTable;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    public void runQuery(string sSql)
    {
        try
        {
            mySqlConnection.Open();
            SqlCommand mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();
            mySqlConnection.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
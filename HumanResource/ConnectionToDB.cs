
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Windows;

class ConnectionToDB
    {
   

     // internal static OracleConnection glb_cnn = new OracleConnection("data source = 192.168.0.11:1521/etcobrnh ;user id = etcois; password=elaghil");
    internal static OracleConnection glb_cnn = new OracleConnection("data source = localhost:1521/orclw ;user id = etcois; password=elaghil");
    internal static OracleCommand comm;
        internal static OracleTransaction trns;
        
        internal static bool isTrns = false;


        internal DataTable GetDataTable(string SqlSt)
        {
           // OleDbCommand MyComm = new OleDbCommand(SqlSt, glb_cnn);
            if (isTrns == true)
            {
                comm.CommandText = SqlSt;
            }
            else
            {
                comm = new OracleCommand(SqlSt, glb_cnn);
            }


        OracleDataReader MyDatareadre = null;
            DataTable MyTable = new DataTable();
            DataRow MyDataRow = null;
            int ColsCount = 0;

            try
            {
                if (glb_cnn.State == System.Data.ConnectionState.Closed)
                    glb_cnn.Open();


                MyDatareadre = comm.ExecuteReader();
                for (ColsCount = 0; ColsCount < MyDatareadre.FieldCount; ColsCount++)
                {
                    MyTable.Columns.Add(MyDatareadre.GetName(ColsCount), MyDatareadre.GetFieldType(ColsCount));
                }
                while (MyDatareadre.Read())
                {
                    MyDataRow = MyTable.NewRow();
                    for (ColsCount = 0; ColsCount < MyDatareadre.FieldCount; ColsCount++)
                    {
                        MyDataRow[ColsCount] = MyDatareadre.GetValue(ColsCount);

                    }
                    MyTable.Rows.Add(MyDataRow);
                }




                if (MyDatareadre != null)
                {
                    if (MyDatareadre.IsClosed)
                        MyDatareadre.Close();
                }

                MyDatareadre.Close();
                MyDatareadre.Dispose();
                return MyTable;
            }
            catch (Exception ErrGetData)
            {
                MessageBox.Show(ErrGetData.Source + Convert.ToChar(13) + ErrGetData.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;

            }

        }

        internal int TranDataToDB(string SqlSt)
        {
            try
            {

                if (glb_cnn.State == ConnectionState.Closed)
                    glb_cnn.Open();

                

                if (isTrns == false)
                {
                    comm = new OracleCommand(SqlSt, glb_cnn);
                    trns =  glb_cnn.BeginTransaction();
                    comm.Transaction = trns;
                   
                    isTrns = true;
                }
                else
                    comm.CommandText = SqlSt;

                return (comm.ExecuteNonQuery());
            }
            catch (Exception ErrGetData)
            {

                MessageBox.Show(ErrGetData.Source + Convert.ToChar(13) + ErrGetData.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return -1;
            }
        }

        internal void glb_commitTransaction()
        {
            trns.Commit();
            isTrns = false;

        }

        internal void glb_RollbackTransaction()
        {
            trns.Rollback();
            isTrns = false;

        }
    }


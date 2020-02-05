using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HumanResource.Employees
{
    /// <summary>
    /// Interaction logic for frmLogin.xaml
    /// </summary>
    public partial class frmPersonalAddressInfo : Window
    {
        public string strEmpid;
        public frmPersonalAddressInfo()
        {
            InitializeComponent();
            FillIDTYPE();
            FillMARITAL_STATUS();
            FillSex();
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
                this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ConnectionToDB cnn = new ConnectionToDB();
            int icheck = cnn.TranDataToDB("update employee set " +
                 "BIRTH_DATE=" + (dtpBIRTH_DATE.SelectedDate.ToString() == "" ? "null" : " to_date('" + dtpBIRTH_DATE.SelectedDate.Value.ToString("dd/MM/yyyy") + "','dd/mm/yyyy')") +
                ",BIRTH_PLACE='" + txtBIRTH_PLACE.Text.Trim() + "'" +
                ",MARITAL_STATUS='" + lstMARITAL_STATUS.Text.Trim() + "' " +
                ",SEX='" + lstSEX.Text.Trim() + "' " +
                ",NATIONALITY='" + txtNATIONALITY.Text.Trim() + "' " +
                ",CHILDREN=" + numCHILDREN.Value.ToString() +
                ",ADRESS='" + txtADRESS.Text.Trim() + "' " +
                ",PHONE='" + txtPHONE.Text.Trim() + "' " +
                ",CELLPHONE='" + txtCELLPHONE.Text.Trim() + "' " +
                ",email='" + txtEmail.Text.Trim() + "' " +
                ",IDTYPE='" + lstIDTYPE.Text.Trim() + "' " +
                ",ID_NO='" + txtID_NO.Text.Trim() + "' " +
                ",ID_DATE=" + (dtpID_DATE.SelectedDate.ToString() == "" ? "null" : " to_date('" + dtpID_DATE.SelectedDate.Value.ToString("dd/MM/yyyy") + "','dd/mm/yyyy')") +
                 ",ID_PLACE='" + txtID_PLACE.Text.Trim() + "' " +
                " where empid=" + strEmpid);

            if (icheck <= 0)
            {
                glb_function.MsgBox("حدث خطأ اثناء عملية الحفظ");
                return;
            }
            cnn.glb_commitTransaction();
            glb_function.MsgBox("تمت عملية الحفظ بنجاح");
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConnectionToDB cnn = new ConnectionToDB();
            System.Data.DataTable dtEmpPersonal = cnn.GetDataTable("select empid,to_char(BIRTH_DATE,'dd/mm/yyyy') BIRTH_DATE," +
                "BIRTH_PLACE,MARITAL_STATUS,SEX,NATIONALITY," +
                "CHILDREN,ADRESS,PHONE,CELLPHONE,email,IDTYPE," +
                "ID_NO,to_char(ID_DATE,'dd/mm/yyyy') ID_DATE,ID_PLACE " +
                " from employee where empid=" + strEmpid);

            if (dtEmpPersonal.Rows.Count > 0)
            {
                if (dtEmpPersonal.Rows[0]["BIRTH_DATE"].ToString() != "")
                    dtpBIRTH_DATE.SelectedDate = DateTime.ParseExact(dtEmpPersonal.Rows[0]["BIRTH_DATE"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                else
                    dtpBIRTH_DATE.SelectedDate = null;

                txtBIRTH_PLACE.Text = dtEmpPersonal.Rows[0]["BIRTH_PLACE"].ToString();
                lstMARITAL_STATUS.Text = dtEmpPersonal.Rows[0]["MARITAL_STATUS"].ToString();
                lstSEX.Text = dtEmpPersonal.Rows[0]["SEX"].ToString();
                txtNATIONALITY.Text = dtEmpPersonal.Rows[0]["NATIONALITY"].ToString();

                if (dtEmpPersonal.Rows[0]["CHILDREN"].ToString() == "")
                    numCHILDREN.Value = 0;
                else
                numCHILDREN.Value = Convert.ToInt32(dtEmpPersonal.Rows[0]["CHILDREN"].ToString());

                txtADRESS.Text = dtEmpPersonal.Rows[0]["ADRESS"].ToString();
                txtPHONE.Text = dtEmpPersonal.Rows[0]["PHONE"].ToString();
                txtCELLPHONE.Text = dtEmpPersonal.Rows[0]["CELLPHONE"].ToString();
                txtEmail.Text = dtEmpPersonal.Rows[0]["email"].ToString();
                lstIDTYPE.Text = dtEmpPersonal.Rows[0]["IDTYPE"].ToString();
                txtID_NO.Text = dtEmpPersonal.Rows[0]["ID_NO"].ToString();

                if (dtEmpPersonal.Rows[0]["ID_DATE"].ToString() != "")
                    dtpID_DATE.SelectedDate = DateTime.ParseExact(dtEmpPersonal.Rows[0]["ID_DATE"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                else
                    dtpID_DATE.SelectedDate = null;

                txtID_PLACE.Text = dtEmpPersonal.Rows[0]["ID_PLACE"].ToString();

            }
        }

        private void FillMARITAL_STATUS()
        {
            ConnectionToDB myconn = new ConnectionToDB();
            DataTable MyDataTable;
            MyDataTable = myconn.GetDataTable("Select distinct MARITAL_STATUS From employee where MARITAL_STATUS is not null ");
            lstMARITAL_STATUS.ItemsSource = MyDataTable.DefaultView;
            lstMARITAL_STATUS.DisplayMemberPath = "MARITAL_STATUS";


            lstMARITAL_STATUS.SelectedIndex = -1;
        }

        private void FillIDTYPE()
        {
            ConnectionToDB myconn = new ConnectionToDB();
            DataTable MyDataTable;
            MyDataTable = myconn.GetDataTable("Select distinct IDTYPE From employee where IDTYPE is not null ");
            lstIDTYPE.ItemsSource = MyDataTable.DefaultView;
            lstIDTYPE.DisplayMemberPath = "IDTYPE";


            lstIDTYPE.SelectedIndex = -1;
        }

        private void FillSex()
        {

            ConnectionToDB myconn = new ConnectionToDB();
            DataTable MyDataTable;
            MyDataTable = myconn.GetDataTable("Select distinct SEX From employee where sex is not null ");
            lstSEX.ItemsSource = MyDataTable.DefaultView;
            lstSEX.DisplayMemberPath = "SEX";


            lstSEX.SelectedIndex = -1;
        }

    }
}

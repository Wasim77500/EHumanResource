using System;
using System.Collections.Generic;
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
    public partial class frmAppointmentInfo : Window
    {
        public string strEmpid;
        public frmAppointmentInfo()
        {
            InitializeComponent();
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
                "INSURANCE_TYPE='" + txtINSURANCE_TYPE.Text.Trim() + "'" +
                ",GUARANTOR_NAME='" + txtGUARANTOR_NAME.Text.Trim() + "'" +
                ",GUARANTOR_TRADER_NAME='" + txtGUARANTOR_TRADER_NAME.Text.Trim() + "'" +
                ",GUARANTOR_ADDRESS='" + txtGUARANTOR_ADDRESS.Text.Trim() + "' " +
                ",GUARANTOR_PHONE='" + txtGUARANTOR_PHONE.Text.Trim() + "' " +
                ",GUARANTOR_CELLPHONE='" + txtGUARANTOR_CELLPHONE.Text.Trim() + "' " +
                ",INSURANCE_DATE=" + (dtpINSURANCE_DATE.SelectedDate.ToString() == "" ? "null" : " to_date('" + dtpINSURANCE_DATE.SelectedDate.Value.ToString("dd/MM/yyyy") + "','dd/mm/yyyy')") +
                ",SPECIALIZATION='" + txtSPECIALIZATION.Text.Trim() + "' " +
                ",QUALIFICATIONS='" + txtQUALIFICATIONS.Text.Trim() + "' " +
                ",JOB_EXPERIENCE='" + txtJOB_EXPERIENCE.Text.Trim() + "' " +
             ",DATE_APPOINTMENT=" + (dtpDATE_APPOINTMENT.SelectedDate.ToString() == "" ? "null" : " to_date('" + dtpDATE_APPOINTMENT.SelectedDate.Value.ToString("dd/MM/yyyy") + "','dd/mm/yyyy')") +
                ",BASIC_SAL_APPOINTMENT='" + nmbBASIC_SAL_APPOINTMENT.Value.ToString() + "' " +
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
            System.Data.DataTable dtEmpFinincialInfo = cnn.GetDataTable("select INSURANCE_TYPE,GUARANTOR_NAME," +
                                                      "  GUARANTOR_TRADER_NAME,GUARANTOR_ADDRESS, "+
                                                      "  GUARANTOR_PHONE, GUARANTOR_CELLPHONE, " +
                                                      "  to_char(INSURANCE_DATE, 'dd/mm/yyyy') INSURANCE_DATE, " +
                                                      "  SPECIALIZATION, QUALIFICATIONS, JOB_EXPERIENCE, " +
                                                      "  to_char(DATE_APPOINTMENT, 'dd/mm/yyyy') DATE_APPOINTMENT, BASIC_SAL_APPOINTMENT " +
                                                      "  from employee e where empid=" + strEmpid);

            if (dtEmpFinincialInfo.Rows.Count > 0)
            {
                txtINSURANCE_TYPE.Text = dtEmpFinincialInfo.Rows[0]["INSURANCE_TYPE"].ToString();
                txtGUARANTOR_NAME.Text = dtEmpFinincialInfo.Rows[0]["GUARANTOR_NAME"].ToString();
                txtGUARANTOR_TRADER_NAME.Text = dtEmpFinincialInfo.Rows[0]["GUARANTOR_TRADER_NAME"].ToString();
                txtGUARANTOR_ADDRESS.Text = dtEmpFinincialInfo.Rows[0]["GUARANTOR_ADDRESS"].ToString();
                txtGUARANTOR_PHONE.Text = dtEmpFinincialInfo.Rows[0]["GUARANTOR_PHONE"].ToString();
                txtGUARANTOR_CELLPHONE.Text = dtEmpFinincialInfo.Rows[0]["GUARANTOR_CELLPHONE"].ToString();
                if (dtEmpFinincialInfo.Rows[0]["INSURANCE_DATE"].ToString() != "")
                    dtpINSURANCE_DATE.SelectedDate = DateTime.ParseExact(dtEmpFinincialInfo.Rows[0]["INSURANCE_DATE"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                else
                    dtpINSURANCE_DATE.SelectedDate = null;


               
                txtSPECIALIZATION.Text = dtEmpFinincialInfo.Rows[0]["SPECIALIZATION"].ToString();
                txtQUALIFICATIONS.Text = dtEmpFinincialInfo.Rows[0]["QUALIFICATIONS"].ToString();
                txtJOB_EXPERIENCE.Text = dtEmpFinincialInfo.Rows[0]["JOB_EXPERIENCE"].ToString();
               
                if (dtEmpFinincialInfo.Rows[0]["DATE_APPOINTMENT"].ToString() != "")
                    dtpDATE_APPOINTMENT.SelectedDate = DateTime.ParseExact(dtEmpFinincialInfo.Rows[0]["DATE_APPOINTMENT"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                else
                    dtpDATE_APPOINTMENT.SelectedDate = null;


                if (dtEmpFinincialInfo.Rows[0]["BASIC_SAL_APPOINTMENT"].ToString() == "")
                    nmbBASIC_SAL_APPOINTMENT.Value = 0;
                else
                    nmbBASIC_SAL_APPOINTMENT.Value = Convert.ToInt32(dtEmpFinincialInfo.Rows[0]["BASIC_SAL_APPOINTMENT"].ToString());

               
            }
        }
    }
}

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
    public partial class frmNotYamani : Window
    {
        public string strEmpid="";
        public frmNotYamani()
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
                "PASSPORT_NO='" + txtPASSPORT_NO.Text.Trim() + "'" +
                ",VISA_STAT='" + txtVISA_STAT.Text.Trim() + "'" +
                 ",VISA_EXPIRED_DATE=" + (dtpVISA_EXPIRED_DATE.SelectedDate.ToString() == "" ? "null" : " to_date('" + dtpVISA_EXPIRED_DATE.SelectedDate.Value.ToString("dd/MM/yyyy") + "','dd/mm/yyyy')") +

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
            System.Data.DataTable dtEmpFinincialInfo = cnn.GetDataTable("select PASSPORT_NO,VISA_STAT,to_char(VISA_EXPIRED_DATE,'dd/mm/yyyy') VISA_EXPIRED_DATE " +
                " from employee where empid=" + strEmpid);

            if (dtEmpFinincialInfo.Rows.Count > 0)
            {
                txtPASSPORT_NO.Text = dtEmpFinincialInfo.Rows[0]["PASSPORT_NO"].ToString();
                txtVISA_STAT.Text = dtEmpFinincialInfo.Rows[0]["VISA_STAT"].ToString();
                if (dtEmpFinincialInfo.Rows[0]["VISA_EXPIRED_DATE"].ToString() != "")
                    dtpVISA_EXPIRED_DATE.SelectedDate = DateTime.ParseExact(dtEmpFinincialInfo.Rows[0]["VISA_EXPIRED_DATE"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                else
                    dtpVISA_EXPIRED_DATE.SelectedDate = null;

            }
        }
    }
}

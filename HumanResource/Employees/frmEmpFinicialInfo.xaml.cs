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
    public partial class frmEmpFinicialInfo : Window
    {
       public string strEmpid;
        public frmEmpFinicialInfo()
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
                "ADVNC_ACC_NO='"+txtADVNC_ACC_NO.Text.Trim()+"'" +
                ",LOAN_ACC_NO='"+txtLOAN_ACC_NO.Text.Trim()+"'" +
                ",LOAN_INSTALLMENT=" +numLOAN_INSTALLMENT.Value.ToString()+
                ",BANK_ACC_NO='"+txtBANK_ACC_NO.Text.Trim()+"' " +
                " where empid="+strEmpid);

            if(icheck <=0)
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
            System.Data.DataTable dtEmpFinincialInfo = cnn.GetDataTable("select ADVNC_ACC_NO,LOAN_ACC_NO,LOAN_INSTALLMENT,BANK_ACC_NO " +
                "from employee where empid=" + strEmpid);

            if(dtEmpFinincialInfo.Rows.Count >0)
            {
                txtADVNC_ACC_NO.Text = dtEmpFinincialInfo.Rows[0]["ADVNC_ACC_NO"].ToString();
                txtLOAN_ACC_NO.Text = dtEmpFinincialInfo.Rows[0]["LOAN_ACC_NO"].ToString();

                if (dtEmpFinincialInfo.Rows[0]["LOAN_INSTALLMENT"].ToString() == "")
                    numLOAN_INSTALLMENT.Value = 0;
                else
                    numLOAN_INSTALLMENT.Value  =Convert.ToInt32( dtEmpFinincialInfo.Rows[0]["LOAN_INSTALLMENT"].ToString());

                txtBANK_ACC_NO.Text = dtEmpFinincialInfo.Rows[0]["BANK_ACC_NO"].ToString();
            }
        }
    }
}

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
    public partial class frmEmployee : Window
    {
        string strtemp;

        class clsStat
        {
            public string Value { get; set; }
            public string Text { get; set; }
        }

        public frmEmployee()
        {
            InitializeComponent();
            strtemp = "";
            FillEmp();
            FillCombo();
        }

        private void FillStat()
        {
            List<clsStat> data = new List<clsStat>();
            clsStat newRow;


            newRow = new clsStat();
            newRow.Value = "1";
            newRow.Text = "فعال";
            data.Add(newRow);

            newRow = new clsStat();
            newRow.Value = "0";
            newRow.Text = "غير فعال";
            data.Add(newRow);

            lstStat.ItemsSource = data;
            lstStat.SelectedValuePath = "Value";
            lstStat.DisplayMemberPath = "Text";

        }

        private void FillCombo()
        {
            // strtemp = "";
            //FillEmp();
            FillOfficialdept();
            FillHafzDept();
            FillBranches();
            FillManagements();
            FillProfitCenter();

            FillStat();
           

            cbEMPNAME.SelectedIndex = -1;
        }
      
       
       

      
        
        private void FillProfitCenter()
        {
            ConnectionToDB myconn = new ConnectionToDB();
            DataTable MyDataTable;
            MyDataTable = myconn.GetDataTable("Select COST_CENTERNAME,COST_CENTERID From NEWACCOUNT_PROFIT_CENTERT ");
            lstProfit_Center.ItemsSource = MyDataTable.DefaultView;
            lstProfit_Center.SelectedValuePath = "COST_CENTERID";
            lstProfit_Center.DisplayMemberPath = "COST_CENTERNAME";
          

            lstProfit_Center.SelectedIndex = -1;
        }


        private void FillManagements()
        {
            ConnectionToDB myconn = new ConnectionToDB();
            DataTable MyDataTable;
            MyDataTable = myconn.GetDataTable("Select MNGMNT_NAME,MNGMNT_NO From MANAGEMENTS order by mngmnt_no");
            lstManagement.ItemsSource = MyDataTable.DefaultView;
           
            lstManagement.SelectedValuePath = "MNGMNT_NO";
            lstManagement.DisplayMemberPath = "MNGMNT_NAME";

            lstManagement.SelectedIndex = -1;
        }
        private void FillBranches()
        {
            ConnectionToDB myconn = new ConnectionToDB();
            DataTable MyDataTable;
            MyDataTable = myconn.GetDataTable("Select branch_no,branch_arabic From  branches ");
            if (MyDataTable != null)
            {


                lstBranches.ItemsSource = MyDataTable.DefaultView;

                lstBranches.SelectedValuePath = "branch_no".ToUpper();
                lstBranches.DisplayMemberPath = "branch_arabic".ToUpper();
                

                lstBranches.SelectedIndex = -1;

            }
        }




        private void FillOfficialdept()
        {
            ConnectionToDB myconn = new ConnectionToDB();
            DataTable MyDataTable;
            MyDataTable = myconn.GetDataTable("Select offdeptno,offdeptname From officialdept order by offdeptno");
            if (MyDataTable != null)
            {
                lstOFFDEPTID.ItemsSource = MyDataTable.DefaultView;
                lstOFFDEPTID.SelectedValuePath = "OFFDEPTNO".ToUpper();
                lstOFFDEPTID.DisplayMemberPath = "OFFDEPTNAME".ToUpper();
                

                lstOFFDEPTID.SelectedIndex = -1;

            }
        }

        private void FillHafzDept()
        {
            ConnectionToDB myconn = new ConnectionToDB();
            DataTable MyDataTable;
            MyDataTable = myconn.GetDataTable("Select OTHERDEPTNO,OTHERDEPTNAME From OTHERDEPT order by OTHERDEPTNO");
            if (MyDataTable != null)
            {


                
                lstOTHERDEPTID.ItemsSource = MyDataTable.DefaultView;
                lstOTHERDEPTID.SelectedValuePath = "OTHERDEPTNO";
                lstOTHERDEPTID.DisplayMemberPath = "OTHERDEPTNAME";
               

                lstOTHERDEPTID.SelectedIndex = -1;

            }
        }
        private void FillEmp()
        {
            ConnectionToDB myconn = new ConnectionToDB();
            DataTable MyDataTable;
            MyDataTable = myconn.GetDataTable("Select empid,empname From employee order by empname");
            if (MyDataTable != null)
            {

                cbEMPNAME.ItemsSource = MyDataTable.DefaultView;
                cbEMPNAME.SelectedValuePath = "empid".ToUpper();
                cbEMPNAME.DisplayMemberPath = "empname".ToUpper();
               





            }
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

        private void btnAccounts_Click(object sender, RoutedEventArgs e)
        {
            frmEmpFinicialInfo frm = new frmEmpFinicialInfo();
            frm.strEmpid = cbEMPNAME.SelectedValue.ToString();
            frm.ShowDialog();
        }

        private void btnPersonalAddress_Click(object sender, RoutedEventArgs e)
        {
            frmPersonalAddressInfo frm = new frmPersonalAddressInfo();
            frm.strEmpid = cbEMPNAME.SelectedValue.ToString();
            frm.ShowDialog();
        }

        private void btnQualification_Click(object sender, RoutedEventArgs e)
        {
            frmAppointmentInfo frm = new frmAppointmentInfo();
            frm.strEmpid = cbEMPNAME.SelectedValue.ToString();
            frm.ShowDialog();
        }

        private void btnNotYemen_Click(object sender, RoutedEventArgs e)
        {
            frmNotYamani frm = new frmNotYamani();
            frm.strEmpid = cbEMPNAME.SelectedValue.ToString();
            frm.ShowDialog();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckEntries())
                return;


            ConnectionToDB cnn = new ConnectionToDB();
            DataTable dtEmp = cnn.GetDataTable("select nvl(max(b.empid),0)+1 from employee b");
            string strEmpId = dtEmp.Rows[0][0].ToString();
            int icheck = cnn.TranDataToDB("insert into employee (empid" +
                ", empno" +
                ", empname" +
                ", stat" +
                ", offdeptid" +
                ", otherdeptid" +
                ", job" +
                ", basic_sal" +
                ", sal_attached" +
                ", hafz" +
                ", lived_expensiveness" +
                ", badalat" +
                ",insurance_stat" +
                ", tax_stat" +
                ", attendent_no" +
                ", branch_no" +
                ", mngmnt_no" +
                ", dept_no" +
                ", profit_cen_no" +
                ", created_user" +
                ", created_date" +
                ", badalat_petrol" +
                ",  badalat_job" +
                ", badalat_phone" +
                ", badalat_residence " +
                ") " +
                " values (" + strEmpId+
                ",'" +txtEMPNO.Text.Trim()+"'"+
                ",'" + cbEMPNAME.Text.Trim() + "'" +
                ",'" + lstStat.SelectedValue.ToString().Trim() + "'" +
                "," + lstOFFDEPTID.SelectedValue.ToString().Trim() + "" +
                "," +( lstOTHERDEPTID.SelectedIndex==-1?"null": lstOTHERDEPTID.SelectedValue.ToString().Trim()) + "" +
                ",'" + txtJOB.Text.Trim() + "'" +
                "," + nmbBASIC_SAL.Value .ToString() +
                "," + nmbSAL_ATTACHED.Value.ToString() +
                "," + nmbHAFZ.Value.ToString() +
                "," + nmbLIVED_EXPENSIVENESS.Value.ToString() +
                "," + nmbBADALAT.Value.ToString() +
                ",'" + nmbINSURANCE_STAT.Value.ToString() + "'" +
                ",'" + nmbTAX_STAT.Value.ToString() + "'" +
                "," + (txtATTENDENT_NO.Text.Trim()==""?"null": txtATTENDENT_NO.Text.Trim()) + 
                "," + lstBranches.SelectedValue.ToString() +
                "," + lstManagement.SelectedValue.ToString() +
                "," + lstDept.SelectedValue.ToString() +
                "," + lstProfit_Center.SelectedValue.ToString() +
                "," + glb_function.glb_strUserId +
                ",sysdate" +
                "," + nmbBdlt_petrol.Value.ToString() +
                "," + nmbBdltJob.Value.ToString() +
                "," + nmbBADALAT_PHONE.Value.ToString() +
                "," + nmbBADALAT_RESIDENCE.Value.ToString() +
                ")");

            if(icheck <=0)
            {
                glb_function.MsgBox("حدث خطأ اثناء عميلة الحفظ");
                return;
            }
            cnn.glb_commitTransaction();
            glb_function.MsgBox("تمت عملية الحفظ بنجاح");

            FillEmp();
            new glb_function().clearItems(this);
            GetData(strEmpId);
        }

        private bool CheckEntries()
        {
            if(cbEMPNAME.Text.Trim()=="")
            {
                glb_function.MsgBox("الرجاء ادخال اسم الموظف");
                cbEMPNAME.Focus();
                return false;
            }

            if(txtEMPNO.Text.Trim()=="")
            {
                glb_function.MsgBox("الرجاء ادخال رقم الموظف");
                txtEMPNO.Focus();
                return false ;

            }
            if(nmbBASIC_SAL.Value<=0)
            {
                glb_function.MsgBox("الرجاء ادخال الراتب");
                nmbBASIC_SAL.Focus();
                return false;
            }
            if(nmbSAL_ATTACHED.Value <=0)
            {
                glb_function.MsgBox("الرجاء ادخال الراتب المربوط");
                nmbSAL_ATTACHED.Focus();
                return false;
            }

            if(lstOFFDEPTID.SelectedIndex == -1)
            {
                glb_function.MsgBox("الرجاء اختيار الإدارة الرسمية");
                lstOFFDEPTID.Focus();
                return false;
            }

            if (lstBranches.SelectedIndex == -1)
            {
                glb_function.MsgBox("الرجاء اختيار الفرع");
                lstBranches.Focus();
                return false;
            }

            if (lstManagement.SelectedIndex == -1)
            {
                glb_function.MsgBox("الرجاء اختيار الأدارة");
                lstManagement.Focus();
                return false;
            }

            if (lstDept.SelectedIndex == -1)
            {
                glb_function.MsgBox("الرجاء اختيار القسم");
                lstDept.Focus();
                return false;
            }

            if (lstProfit_Center.SelectedIndex == -1)
            {
                glb_function.MsgBox("الرجاء اختيار مركز التكلفة");
                lstProfit_Center.Focus();
                return false;
            }

            if (lstStat.SelectedIndex == -1)
            {
                glb_function.MsgBox("الرجاء اختيار الحالة");
                lstStat.Focus();
                return false;
            }

            return true;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckEntries())
                return;


            ConnectionToDB cnn = new ConnectionToDB();
          
          
            int icheck = cnn.TranDataToDB("update employee "+
                " set "+
                " empno='" + txtEMPNO.Text.Trim() + "'" +
            
                ",stat='" + lstStat.SelectedValue.ToString().Trim() + "'" +
                ",offdeptid=" + lstOFFDEPTID.SelectedValue.ToString().Trim() + "" +
                ",OTHERDEPTID=" + (lstOTHERDEPTID.SelectedIndex == -1 ? "null" : lstOTHERDEPTID.SelectedValue.ToString().Trim()) + "" +
                ",JOB='" + txtJOB.Text.Trim() + "'" +
                ",BASIC_SAL=" + nmbBASIC_SAL.Value.ToString() +
                ",SAL_ATTACHED=" + nmbSAL_ATTACHED.Value.ToString() +
                ",HAFZ=" + nmbHAFZ.Value.ToString() +
                ",LIVED_EXPENSIVENESS=" + nmbLIVED_EXPENSIVENESS.Value.ToString() +
                ",BADALAT=" + nmbBADALAT.Value.ToString() +
                ",INSURANCE_STAT='" + nmbINSURANCE_STAT.Value.ToString() + "'" +
                ",TAX_STAT='" + nmbTAX_STAT.Value.ToString() + "'" +
                ",ATTENDENT_NO=" + ( txtATTENDENT_NO.Text.Trim()==""?"null": txtATTENDENT_NO.Text.Trim() )+
                ",BRANCH_NO=" + lstBranches.SelectedValue.ToString() +
                ",MNGMNT_NO=" + lstManagement.SelectedValue.ToString() +
                ",DEPT_NO=" + lstDept.SelectedValue.ToString() +
                ",PROFIT_CEN_NO=" + lstProfit_Center.SelectedValue.ToString() +
                ",CREATED_USER=" + glb_function.glb_strUserId +
                ",BADALAT_PETROL=" + nmbBdlt_petrol.Value.ToString() +
                ",BADALAT_JOB=" + nmbBdltJob.Value.ToString() +
                ",BADALAT_PHONE=" + nmbBADALAT_PHONE.Value.ToString() +
                ",BADALAT_RESIDENCE=" + nmbBADALAT_RESIDENCE.Value.ToString() +
                " where empid="+cbEMPNAME.SelectedValue.ToString());

            if (icheck <= 0)
            {
                glb_function.MsgBox("حدث خطأ اثناء عميلة التعديل");
                return;
            }
            cnn.glb_commitTransaction();
            glb_function.MsgBox("تمت عملية التعديل بنجاح");
            string strEmpId = cbEMPNAME.SelectedValue.ToString();
            FillEmp();
            
            new glb_function().clearItems(this);
            GetData(strEmpId);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            new glb_function().clearItems(this);
            btnSave.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnAccounts.IsEnabled = false;
            btnPersonalAddress.IsEnabled = false;
            btnQualification.IsEnabled = false;
            btnNotYemen.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbEMPNAME.Focus();
        }

        private void lstManagement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstManagement.SelectedValue == null)
            {
                lstDept.ItemsSource = null;
                return;
            }
                

            if (lstManagement.SelectedValue.ToString() != "System.Data.DataRowView" && lstManagement.SelectedValue.ToString() != "")
            {
                
                FillDepartments();
            }
            else
             lstDept.ItemsSource = null;
        }
        private void FillDepartments()
        {
            ConnectionToDB myconn = new ConnectionToDB();
            DataTable MyDataTable;
            MyDataTable = myconn.GetDataTable("Select DEPT_NAME,DEPT_NO From DEPARTEMENTS Where MNGMNT_NO = " + lstManagement.SelectedValue.ToString());
            lstDept.ItemsSource = MyDataTable.DefaultView;
            lstDept.DisplayMemberPath = "DEPT_NAME";
            lstDept.SelectedValuePath = "DEPT_NO";
            lstDept.SelectedIndex = -1;

        }

        private void cbEMPNAME_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbEMPNAME.SelectedValue == null)
                return;



            if (cbEMPNAME.SelectedValue.ToString() != "System.Data.DataRowView" && cbEMPNAME.SelectedValue.ToString() != "")
            {
                if (strtemp == "")
                {
                    strtemp = cbEMPNAME.SelectedValue.ToString();
                    new glb_function().clearItems(this);
                   
                   
                    GetData(strtemp);
                    strtemp = "";
                   

                   
                }


            }
        }
        private void GetData(string strPkid)
        {
            ConnectionToDB cnn = new ConnectionToDB();
            DataTable dtEmp = cnn.GetDataTable("select empid, empno, empname, stat, offdeptid, otherdeptid, job, basic_sal, "+
               " sal_attached, hafz, lived_expensiveness, badalat, "+
                " insurance_stat, tax_stat, attendent_no, "+
                " branch_no, mngmnt_no, dept_no, profit_cen_no, "+
                "   badalat_petrol, badalat_job, badalat_phone, badalat_residence "+
                " from employee where empid="+strPkid);

            cbEMPNAME.SelectedValue = dtEmp.Rows[0]["empid"].ToString();
            txtEMPNO.Text = dtEmp.Rows[0]["empno"].ToString();
            txtATTENDENT_NO.Text = dtEmp.Rows[0]["attendent_no"].ToString();
            txtJOB.Text = dtEmp.Rows[0]["job"].ToString();
            lstOFFDEPTID.SelectedValue = dtEmp.Rows[0]["offdeptid"].ToString();
            lstOTHERDEPTID.SelectedValue = dtEmp.Rows[0]["otherdeptid"].ToString();
            lstStat.SelectedValue = dtEmp.Rows[0]["stat"].ToString();
            nmbBASIC_SAL.Value = Convert.ToInt32(dtEmp.Rows[0]["basic_sal"].ToString());
            nmbSAL_ATTACHED.Value = Convert.ToInt32(dtEmp.Rows[0]["sal_attached"].ToString());
            nmbHAFZ.Value = Convert.ToInt32(dtEmp.Rows[0]["hafz"].ToString());

            nmbLIVED_EXPENSIVENESS.Value = Convert.ToInt32(dtEmp.Rows[0]["lived_expensiveness"].ToString());
            nmbBADALAT.Value = Convert.ToInt32(dtEmp.Rows[0]["badalat"].ToString());
            nmbINSURANCE_STAT.Value = Convert.ToInt32(dtEmp.Rows[0]["insurance_stat"].ToString());
            nmbTAX_STAT.Value = Convert.ToInt32(dtEmp.Rows[0]["tax_stat"].ToString());
            nmbBdlt_petrol.Value = Convert.ToInt32(dtEmp.Rows[0]["badalat_petrol"].ToString());
            nmbBdltJob.Value = Convert.ToInt32(dtEmp.Rows[0]["badalat_job"].ToString());
            nmbBADALAT_RESIDENCE.Value = Convert.ToInt32(dtEmp.Rows[0]["badalat_residence"].ToString());
            nmbBADALAT_PHONE.Value = Convert.ToInt32(dtEmp.Rows[0]["badalat_phone"].ToString());

            if (dtEmp.Rows[0]["branch_no"].ToString() == "")
                lstBranches.SelectedIndex = -1;
            else
            lstBranches.SelectedValue = dtEmp.Rows[0]["branch_no"].ToString();

            if (dtEmp.Rows[0]["mngmnt_no"].ToString() == "")
                lstManagement.SelectedIndex = -1;
            else
            {
                lstManagement.SelectedValue = dtEmp.Rows[0]["mngmnt_no"].ToString();
                lstManagement_SelectionChanged(null, null);//selectionChange Not Fired
            }
              


            if (dtEmp.Rows[0]["dept_no"].ToString() == "")
                lstDept.SelectedIndex = -1;
            else
                lstDept.SelectedValue = dtEmp.Rows[0]["dept_no"].ToString();

            if (dtEmp.Rows[0]["profit_cen_no"].ToString() == "")
                lstProfit_Center.SelectedIndex = -1;
            else
                lstProfit_Center.SelectedValue = dtEmp.Rows[0]["profit_cen_no"].ToString();


            btnUpdate.IsEnabled = true;
            btnAccounts.IsEnabled = true;
            btnPersonalAddress.IsEnabled = true;
            btnQualification.IsEnabled = true;
            btnNotYemen.IsEnabled = true;
            btnSave.IsEnabled = false;



        }

        private void cbEMPNAME_LostFocus(object sender, RoutedEventArgs e)
        {
            if(cbEMPNAME.SelectedIndex==-1)
            {
                string strEmpName = cbEMPNAME.Text.Trim();
               
                btnUndo_Click(null, null);
                
                cbEMPNAME.Text = strEmpName;
                
            }
        }
    }
}

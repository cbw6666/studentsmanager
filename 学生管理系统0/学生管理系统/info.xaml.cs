using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
namespace 学生管理系统
{
    /// <summary>
    /// info.xaml 的交互逻辑
    /// </summary>
    public partial class info : Window
    {
        public info()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string strclassid = combobox.Items[combobox.SelectedIndex].ToString();
            string strquery = "select stuid as '学号',stuname as '姓名',stugender as '性别',pwd as '密码',classid as '班号' from tb_stuinfo where classid='" + strclassid + "'";
            string strstuid = stuidbox.Text.Trim();
            string strstuname = stunamebox.Text.Trim();
            if (strstuid.Length > 0) {
                strquery += " and stuid='" + strstuid + "'";
            }
            if (strstuname.Length > 0) {
                strquery += " and stuname like'%" + strstuname + "%'";
            }
            try
            {
                MySqlHelper sqlhelper = new MySqlHelper();
                DataSet ds = sqlhelper.GetDataSet(MySqlHelper.Conn, CommandType.Text, strquery);
                DataTable dt = ds.Tables[0];
                dataGrid1.ItemsSource = dt.DefaultView;
                if (dt.Rows.Count == 0)
                    MessageBox.Show("没有符合条件的记录！");
            
             
              
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlHelper sqlhelper = new MySqlHelper();
                string strquery="select classid from tb_classINFO";
                DataSet datasteobj = sqlhelper.GetDataSet(MySqlHelper.Conn, CommandType.Text, strquery);
                DataTable dt=datasteobj.Tables[0];
                for(int i=0;i<dt.Rows.Count;i++){
                    DataRow dr=dt.Rows[i];
                    combobox.Items.Add(dr["classid"]);
                }
                if (dt.Rows.Count > 0) {
                    combobox.SelectedIndex = 0;
                }
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGrid1_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGrid1_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void delete_click(object sender, RoutedEventArgs e)
        {
          DataRowView mySelectedElement = (DataRowView)dataGrid1.SelectedItem;
          string strid = mySelectedElement.Row[0].ToString();
         MessageBoxResult re= MessageBox.Show("你确定要删除此条学生信息吗", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
          string strdelete = "delete from tb_stuinfo where stuid='" + strid + "'";
          if (re == MessageBoxResult.No) {
              return;
          }
          try
          {
               MySqlHelper sq = new MySqlHelper();
               sq.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text,strdelete);
               button1_Click(null, null);
             
          }
          catch (Exception ex) {
              MessageBox.Show(ex.Message);
             
             
          }
            
        }
    }
}

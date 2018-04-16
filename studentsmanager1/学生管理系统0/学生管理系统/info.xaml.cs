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
using MySql.Data;
using MySql.Data.MySqlClient;

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
            string strquery = "select stuid as '学号',stuname as '姓名',stugender as '性别',pwd as '年龄',classid as '班号' from tb_stuinfo where classid='" + strclassid + "'";
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
                modify.IsEnabled = true;
                DataTable dt = ds.Tables[0];
                dataGrid1.ItemsSource = dt.DefaultView;
                if (dt.Rows.Count == 0)
                    MessageBox.Show("没有符合条件的记录！");
            }
            catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
             
              
     
        
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            modify.IsEnabled = false;
            try
            {
                MySqlHelper sqlhelper = new MySqlHelper();
                string strquery="select classid from tb_classINFO";
                DataSet datasteobj = sqlhelper.GetDataSet(MySqlHelper.Conn, CommandType.Text, strquery);
                DataTable dt=datasteobj.Tables[0];
                for(int i=0;i<dt.Rows.Count;i++){
                    DataRow dr=dt.Rows[i];
                    combobox.Items.Add(dr["classid"]);
                    comboBox1.Items.Add(dr["classid"]);
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
    
            DataRowView  mySelectedElement = (DataRowView)dataGrid1.SelectedItem;
            mySelectedElement = (DataRowView)dataGrid1.SelectedItem;
            if (mySelectedElement == null)
            {
              return;
            }
            string strid = mySelectedElement.Row[0].ToString();
            string strname = mySelectedElement.Row[1].ToString();
            string strgender= mySelectedElement.Row[2].ToString();
            string strclassid= mySelectedElement.Row[4].ToString();
            string strpwd = mySelectedElement.Row[3].ToString();
            textstuid.Text = strid;
            textname.Text = strname;
            spwd.Text = strpwd;
            comboBox1.Text = strclassid;
            if (strgender == "女") {
                radioButton2.IsChecked = true;
            }
        }

        private void dataGrid1_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void delete_click(object sender, RoutedEventArgs e)
        {
          DataRowView mySelectedElement = (DataRowView)dataGrid1.SelectedItem;
          string strid = mySelectedElement.Row[0].ToString();
          if (strid == null) {
              return;
          }
         MessageBoxResult re= MessageBox.Show("你确定要删除此条学生信息吗", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
          string strdelete = "delete from tb_stuinfo where stuid='" + strid + "'";
          if (re == MessageBoxResult.No) {
              return;
          }
          
          try
          {
               //MySqlHelper sq = new MySqlHelper();
               //int n=sq.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text,strdelete);
               ////button1_Click(null, null);
              MySqlConnectionStringBuilder s = new MySqlConnectionStringBuilder();
              s.Server = "localhost";
              s.Port = 3306; //mysql端口号
              s.Database = "db_jiaowu";
              s.UserID = "root";
              s.Password = "14005cbw";
              MySqlConnection conn = new MySqlConnection(s.ConnectionString);
             // string strinsert = "insert into user_stu (user_stu,stu_pwd) values('" + strname + "','" + strpwd + "')";
              MySqlCommand comm = new MySqlCommand();
              comm.Connection = conn;
              comm.CommandText =strdelete;
              conn.Open();
              int n = comm.ExecuteNonQuery();
              conn.Close();
              //App w = new App();
              //w.DoEvents();
           
              button1_Click(null,null);
              MessageBox.Show("删除成功");
            //  button1_Click(null, null);
           
             
          }
          catch (Exception ex) {
              MessageBox.Show(ex.Message);
             
             
          }
            
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            string strclassid = comboBox1.Text.Trim();
            string strstuid = textstuid.Text.Trim();
            string strstuname = textname.Text.Trim();
            string strgender = "男";
            string strpwd = spwd.Text.Trim();
            if (radioButton2.IsChecked == true)
            {
                strgender = "女";
            }
            string strinsert = String.Format("insert into tb_stuinfo(stuid,stuname,stugender,pwd,classid) values('{0}','{1}','{2}','{3}','{4}')", strstuid, strstuname, strgender, strpwd, strclassid);
            try
            {
                MySqlHelper sqlhelper = new MySqlHelper();
                int n=sqlhelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, strinsert);
               button1_Click(null, null);
                MessageBox.Show("成功添加记录");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        private void modify_Click(object sender, RoutedEventArgs e)
        {
           //DataRowView mySelectedElement = (DataRowView)dataGrid1.SelectedItem;
           //string strstuid = mySelectedElement.Row[0].ToString();
            string strclassid = comboBox1.Text.Trim();
            string strstuid = textstuid.Text.Trim();
            string strstuname = textname.Text.Trim();
            string strgender = "男";
            string strpwd = spwd.Text.Trim();
            if (radioButton2.IsChecked == true)
            {
              strgender="女";
            }
            string strupdate = "update tb_stuinfo set classid='" + strclassid + "',stuname='"+strstuname+"',stugender='"+strgender+"',pwd='"+strpwd+"'where stuid='"+strstuid+"' ";
            try {
                MySqlHelper sqlhelper = new MySqlHelper();
                int n=sqlhelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text,strupdate);
                MessageBox.Show("修改成功");
                button1_Click(null, null);
            }
            catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
        }
    }
}

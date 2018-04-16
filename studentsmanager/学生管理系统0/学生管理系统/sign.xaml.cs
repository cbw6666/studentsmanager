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
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;


namespace 学生管理系统
{
    /// <summary>
    /// sign.xaml 的交互逻辑
    /// </summary>
    public partial class sign : Window
    {
        public sign()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            string strname = textBox_id.Text.Trim();
            string strpwd = passwordBox1.Password.Trim();
            //MySqlConnection conn = new MySqlConnection();
            //string strconn = "server=.;database=db_jiaowu";
            //conn.ConnectionString = strconn;
            //string strinsert = "insert into user_stu (user_stu,stu_pwd) values('" + strname + "','" + strpwd + "')";
            //MySqlCommand comm = new MySqlCommand();
            //comm.Connection = conn;
            //comm.CommandText = strinsert;
            //conn.Open();
            //int n = comm.ExecuteNonQuery();
            //conn.Close();
            //MessageBox.Show(n.ToString());

            MySqlConnectionStringBuilder s = new MySqlConnectionStringBuilder();
            s.Server = "localhost";
            s.Port = 3306; //mysql端口号
            s.Database = "db_jiaowu";
            s.UserID = "root";
            s.Password = "14005cbw";
            MySqlConnection conn = new MySqlConnection(s.ConnectionString);
            string strinsert = "insert into user_stu (user_stu,stu_pwd) values('" + strname + "','" + strpwd + "')";
            MySqlCommand comm = new MySqlCommand();
            comm.Connection = conn;
            comm.CommandText = strinsert;
            conn.Open();
            int n = comm.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("注册成功");


        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string strname = textBox_id.Text.Trim();
            string strpwd = passwordBox1.Password.Trim();
            MySqlConnectionStringBuilder s = new MySqlConnectionStringBuilder();
            s.Server = "localhost";
            s.Port = 3306; //mysql端口号
            s.Database = "db_jiaowu";
            s.UserID = "root";
            s.Password = "14005cbw";
            MySqlConnection conn = new MySqlConnection(s.ConnectionString);
            string strselect = "select * from user_stu where user_stu='"+strname + "'and stu_pwd='" + strpwd + "' ";
            MySqlDataAdapter adapter = new MySqlDataAdapter(strselect, conn);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                MessageBox.Show("登录成功");
                info frmobj = new info();
                frmobj.Show();
            }
            else {
                MessageBox.Show("登录失败");
            }
        }
    }
}

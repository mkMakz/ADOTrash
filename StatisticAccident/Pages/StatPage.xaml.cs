using StatisticAccident.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StatisticAccident.Pages
{
    /// <summary>
    /// Interaction logic for StatPage.xaml
    /// </summary>
    public partial class StatPage : Page
    {
        public StatPage()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT top 3 County, COUNT(*) as c FROM Accidents GROUP BY County Order by c desc;"
                + "SELECT top 3 Age, COUNT(*) as c FROM Accidents GROUP BY Age Order by c desc;" +
                "SELECT top 3 Gender, COUNT(*) as c FROM Accidents GROUP BY Gender Order by c desc;" +
                "SELECT top 3 DayOfWeek, COUNT(*) as c FROM Accidents GROUP BY DayOfWeek Order by c desc;";

            con.Open();

            SqlDataReader rd = cmd.ExecuteReader();
            StatAccident st = new StatAccident();
            int s = 0;
            while (rd.HasRows)
            {
                while (rd.Read())
                {
                    switch (s)
                    {
                        case 0:
                            st.TopCountry.Add(rd[0].ToString());
                            break;
                        case 1:
                            st.TopAge.Add(rd[0].ToString());
                            break;
                        case 2:
                            st.TopGender.Add(rd[0].ToString());
                            break;
                        case 3:
                            st.TopDayOfWeek.Add(rd[0].ToString());
                            break;
                    }

                    //st.TopAge.Add(rd[0].ToString());
                }
                s++;
                rd.NextResult();
            }
        }
    }
}

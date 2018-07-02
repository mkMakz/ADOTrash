using OfficeOpenXml;
using StatisticAccident.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
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
    /// Interaction logic for PanelPage.xaml
    /// </summary>
    public partial class PanelPage : Page
    {
        List<Accident> accidents = new List<Accident>();

        public PanelPage()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            string path = @"C:\Users\ОстроуховМ\Documents\Visual Studio 2015\Projects\StatisticAccident\StatisticAccident\2010 Fatalities by Type.xlsx";
            FileInfo template = new FileInfo(path);

            //ExcelPackage package = new ExcelPackage();
            using (ExcelPackage package = new ExcelPackage(template, true))
            {
                ExcelWorksheet workshet = package.Workbook.Worksheets[2];

                for (int i = 2; i < workshet.Dimension.End.Row; i++)
                {
                    Accident accident = new Accident();
                    accident.AccidentDay = DateTime.Parse(workshet.Cells[i, 1].Value.ToString());
                    accident.DayOfWeek = workshet.Cells[i, 2].Value.ToString();
                   
                    accident.County = workshet.Cells[i, 4].Value.ToString();
                    accident.Age = Int32.Parse(workshet.Cells[i, 12].Value.ToString());
                    accident.Gender = workshet.Cells[i, 13].Value.ToString();

                    accidents.Add(accident);
                }
            }

            //Step two

            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            try
            {
                con.Open();
                tbStatus.Text += con.State;
                foreach (Accident item in accidents)
                {

                    string sf = string.Format("INSERT INTO Accidents (AccidentDate, County, Age, Gender, DayOfWeek)" +
                        "Values ('{0}','{1}','{2}','{3}','{4}')", string.Format("{0:yyyy-MM-dd}",item.AccidentDay), item.County, item.Age, item.Gender, item.DayOfWeek);

                    cmd.CommandText = sf;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                tbStatus.Text += ex.Message;
            }
            finally
            {
                con.Close();
            }

        }
    }
}
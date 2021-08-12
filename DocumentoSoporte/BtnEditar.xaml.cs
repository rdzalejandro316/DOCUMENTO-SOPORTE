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

namespace DocumentoSoporte
{
    public partial class BtnEditar : Window
    {
        public int idemp = 0;
        public string cnEmp = "";
        public string cod_empresa = "";
        public string nomempresa = "";
        public dynamic SiaWin;

        public bool flag = false;
        public string idreg = "";
        public string numtrn = "";

        public BtnEditar()
        {
            InitializeComponent();
            TxFecIni.Text = DateTime.Now.ToString();
            TxFecFin.Text = DateTime.Now.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SiaWin = System.Windows.Application.Current.MainWindow;
            Title = "Edicion de Documento Soporte :" + cod_empresa + " - " + nomempresa;
        }

        private void BtnConsultar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "select * from CoCabSoporte where convert(date,fecha,103) between '" + TxFecIni.Text + "' and '" + TxFecFin.Text + "'; ";
                DataTable dt = SiaWin.Func.SqlDT(query, "tabla", idemp);
                if (dt.Rows.Count > 0)
                {
                    dataGrid.ItemsSource = dt.DefaultView;
                    TxTotal.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    MessageBox.Show("no existen documentos de soporte con ese rango de fecha", "alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    dataGrid.ItemsSource = null;
                    TxTotal.Text = "0";
                }
            }
            catch (Exception w)
            {
                MessageBox.Show("error BtnConsultar_Click:" + w);
            }
        }

        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedIndex >= 0)
            {
                DataRowView row = (DataRowView)dataGrid.SelectedItems[0];
                string id = row["idreg"].ToString().Trim();
                numtrn = row["num_trn"].ToString().Trim();
                if (MessageBox.Show("desea editar el documento:" + numtrn + " ?", "Alerta", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    flag = true;
                    idreg = id;                    
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("debe de seleccionar el documento que desea editar", "alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                flag = false;
            }
        }


    }
}

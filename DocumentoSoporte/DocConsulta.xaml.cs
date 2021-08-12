using Microsoft.Reporting.WinForms;
using Microsoft.Win32;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace DocumentoSoporte
{

    public partial class DocConsulta : Window
    {
        public int idemp = 0;
        public string cnEmp = "";
        public string cod_empresa = "";
        public string nomempresa = "";
        public dynamic SiaWin;
        public DocConsulta()
        {
            InitializeComponent();
            TxFecIni.Text = DateTime.Now.ToString();
            TxFecFin.Text = DateTime.Now.ToString();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SiaWin = System.Windows.Application.Current.MainWindow;
            Title = "Impresion Documento Soporte :" + cod_empresa + " - " + nomempresa;
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

        private void BtnImprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedIndex >= 0)
                {
                    DataRowView row = (DataRowView)dataGrid.SelectedItems[0];
                    int id = Convert.ToInt32(row["idreg"]);
                    imprimirPrograEgreso(id.ToString());
                }
                else
                {
                    MessageBox.Show("seleccione un documento para imprimir", "alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

            }
            catch (Exception w)
            {
                MessageBox.Show("error BtnImprimir_Click:" + w);
            }
        }


        public void imprimirPrograEgreso(string idreg)
        {
            try
            {
                string direccion_formato = @"/Contabilidad/DocumentoSoporte";
                string TituloReport = "Documento Soporte";

                List<ReportParameter> parameters = new List<ReportParameter>();

                ReportParameter paramcodemp = new ReportParameter();
                paramcodemp.Values.Add(cod_empresa);
                paramcodemp.Name = "codemp";
                parameters.Add(paramcodemp);

                ReportParameter paramcodtrn = new ReportParameter();
                paramcodtrn.Values.Add(idreg);
                paramcodtrn.Name = "idreg";
                parameters.Add(paramcodtrn);

                DataTable dt = SiaWin.DB.SqlDT("select sum(total) as total From CoCueSoporte where idregcab='" + idreg + "'", "tmp", idemp);
                decimal totalfac = dt.Rows.Count > 0 ? Convert.ToDecimal(dt.Rows[0]["total"]) : 0;
                string enletras = SiaWin.Func.enletras(totalfac.ToString());
                ReportParameter paramLetra = new ReportParameter();
                paramLetra.Values.Add(enletras);
                paramLetra.Name = "enletra";
                parameters.Add(paramLetra);


                SiaWin.Reportes(parameters, direccion_formato, TituloReporte: TituloReport, Modal: true, idemp: idemp, ZoomPercent: 50);
            }
            catch (Exception w)
            {
                MessageBox.Show("error en #imprimirPrograEgreso#:" + w);
            }
        }


        private void BtnExportar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var options = new Syncfusion.UI.Xaml.Grid.Converter.ExcelExportingOptions();
                options.ExcelVersion = ExcelVersion.Excel2013;
                var excelEngine = dataGrid.ExportToExcel(dataGrid.View, options);
                var workBook = excelEngine.Excel.Workbooks[0];

                SaveFileDialog sfd = new SaveFileDialog
                {
                    FilterIndex = 2,
                    Filter = "Excel 97 to 2003 Files(*.xls)|*.xls|Excel 2007 to 2010 Files(*.xlsx)|*.xlsx|Excel 2013 File(*.xlsx)|*.xlsx"
                };

                if (sfd.ShowDialog() == true)
                {
                    using (Stream stream = sfd.OpenFile())
                    {
                        if (sfd.FilterIndex == 1)
                            workBook.Version = ExcelVersion.Excel97to2003;
                        else if (sfd.FilterIndex == 2)
                            workBook.Version = ExcelVersion.Excel2010;
                        else
                            workBook.Version = ExcelVersion.Excel2013;
                        workBook.SaveAs(stream);
                    }

                    if (MessageBox.Show("Usted quiere abrir el archivo en excel?", "Ver archvo", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
            catch (Exception w)
            {
                MessageBox.Show("error al exportar:" + w);
            }
        }


    }
}

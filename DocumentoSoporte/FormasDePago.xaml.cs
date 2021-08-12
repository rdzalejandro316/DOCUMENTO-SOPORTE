using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DocumentoSoporte
{

    public partial class FormasDePago : Window
    {
        dynamic SiaWin;
        int idemp = 0;
        string cnEmp = string.Empty;
        string BusinessCode = "";


        public DataTable dtCue = new DataTable();
        public decimal totalPagar = 0;
        public bool flag = false;

        public FormasDePago()
        {
            InitializeComponent();
            SiaWin = Application.Current.MainWindow;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SiaWin = Application.Current.MainWindow;
                if (idemp <= 0) idemp = SiaWin._BusinessId;
                System.Data.DataRow foundRow = SiaWin.Empresas.Rows.Find(idemp);
                idemp = SiaWin._BusinessId;
                cnEmp = SiaWin.Func.DatosEmp(idemp);
                BusinessCode = foundRow["BusinessCode"].ToString().Trim();

                loadColumns();

                loadInfo();

                TxtTotalRecaudo.Text = totalPagar.ToString("C2");

                //if (string.IsNullOrEmpty(idreg))
                //{

                //    TxtTotalRecaudo.Text = totalPagar.ToString("C2");

                //    if (!string.IsNullOrWhiteSpace(predeterminarfpag))
                //    {
                //        string where = string.IsNullOrEmpty(puntov) ? "cod_pag='01'" : $"cod_pvt='{puntov}'";

                //        DataTable dt = SiaWin.Func.SqlDT($"select * from inmae_fpag where {where}; ", "formapago", idemp);
                //        if (dt.Rows.Count > 0)
                //        {
                //            string codpag = dt.Rows[0]["cod_pag"].ToString().Trim();
                //            string nompag = dt.Rows[0]["nom_pag"].ToString().Trim();
                //            string codcta = dt.Rows[0]["cod_cta"].ToString().Trim();
                //            insertGrid(codpag, nompag, codcta, totalPagar);
                //            TxtTotalRecaudo.Text = "0";
                //        }
                //    }

                //}
                //else
                //{
                //    string query = "select t1.cod_pag,t2.nom_pag,t1.vlr_pagado,t1.cod_cta,t1.doc_ref,t1.cod_ban,t1.cod_tpag  ";
                //    query += "from indet_fpag as t1 ";
                //    query += "inner join inmae_fpag t2 on t1.cod_pag = t2.cod_pag ";
                //    query += "where t1.idregcab='" + idreg + "' ";

                //    double val = 0;
                //    DataTable dt = SiaWin.Func.SqlDT(query, "formapago", idemp);
                //    if (dt.Rows.Count > 0)
                //    {
                //        foreach (System.Data.DataRow item in dt.Rows)
                //        {
                //            string codpag = item["cod_pag"].ToString().Trim();
                //            string nom_pag = item["nom_pag"].ToString().Trim();
                //            string cod_cta = item["cod_cta"].ToString().Trim();
                //            string cod_tpag = item["cod_tpag"].ToString().Trim();
                //            double valor = Convert.ToDouble(item["vlr_pagado"]);
                //            val += valor;
                //            string doc_ref = item["doc_ref"].ToString().Trim();
                //            string cod_ban = item["cod_ban"].ToString().Trim();
                //            dtCue.Rows.Add(codpag, nom_pag, valor, cod_cta, doc_ref, "", 0, "", cod_tpag);
                //        }

                //    }
                //    if (consulta) GridMain.IsEnabled = false;
                //    else TxtTotalRecaudo.Text = (totalPagar - val).ToString("C2");
                //}

            }
            catch (Exception w)
            {
                MessageBox.Show("errro al cargar formas de pago:" + w);
            }
        }

        public void loadColumns()
        {
            try
            {
                dtCue.Columns.Add("cod_pag");
                dtCue.Columns.Add("nom_pag");
                dtCue.Columns.Add("valor", typeof(decimal));
                dtCue.Columns.Add("cod_tpag");
                dataGrid.ItemsSource = dtCue.DefaultView;
            }
            catch (Exception w)
            {
                MessageBox.Show("error en loadColums:" + w);
            }

        }


        private void loadInfo()
        {
            try
            {
                DataTable dtfpago = SiaWin.Func.SqlDT("select RTRIM(cod_tpag) AS cod_tpag,RTRIM(nom_tpag) AS nom_tpag from inmae_tpago", "formapago", idemp);
                CBFormapag.DisplayMemberPath = "nom_tpag";
                CBFormapag.SelectedValuePath = "cod_tpag";
                CBFormapag.ItemsSource = dtfpago.DefaultView;
                if (dtfpago.Rows.Count > 0) CBFormapag.SelectedIndex = 0;

                DataTable dtFpag = SiaWin.Func.SqlDT("select RTRIM(cod_pag) AS cod_pag,RTRIM(nom_pag) AS nom_pag from inmae_fpag", "formapago", idemp);
                CBpagos.ItemsSource = dtFpag.DefaultView;
                CBpagos.SelectedValuePath = "cod_pag";
                CBpagos.DisplayMemberPath = "nom_pag";

            }
            catch (Exception w)
            {
                MessageBox.Show("error en loadInfo:" + w);
            }

        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.F5)
                {

                    BtnGrabar.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    return;
                }
            }
            catch (Exception w)
            {
                MessageBox.Show("error Window_PreviewKeyDown:" + w);
            }

        }

        private void Btnadd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CBpagos.SelectedIndex >= 0)
                {
                    System.Data.DataRow selectedDataRow = ((DataRowView)CBpagos.SelectedItem).Row;
                    string codigo = selectedDataRow["cod_pag"].ToString().Trim();
                    string name = selectedDataRow["nom_pag"].ToString().Trim();
                    string tipo = CBFormapag.SelectedValue.ToString().Trim();

                    insertGrid(codigo, name, tipo, 0);
                }
                else
                {
                    MessageBox.Show("Selecione una forma de pago");
                }
            }
            catch (Exception w)
            {
                MessageBox.Show("error al agregar:" + w);
            }
        }

        void insertGrid(string cod_pag, string nom_pag, string fpag, decimal val = 0)
        {
            dtCue.Rows.Add(cod_pag, nom_pag, fpag, val);
            sumaAbonos();
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedIndex >= 0)
                {
                    DataRowView row = (DataRowView)dataGrid.SelectedItems[0];
                    row.Delete();
                    sumaAbonos();
                }
                else
                {
                    MessageBox.Show("seleccione un medio de pago a eliminar", "alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            catch (Exception w)
            {
                MessageBox.Show("errro en la eliminacio:" + w);
            }
        }

        private void sumaAbonos()
        {
            try
            {
                decimal totalabono = 0;
                decimal.TryParse(dtCue.Compute("Sum(valor)", "").ToString(), out totalabono);
                TxtTotalPagado.Text = totalabono.ToString("C2");
                TxtTotalRecaudo.Text = Convert.ToDecimal(totalPagar - totalabono).ToString("C2"); ;
            }
            catch (Exception w)
            {
                MessageBox.Show("error en sumaAbonos():" + w);
            }


        }

        private void dataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (dataGrid.SelectedIndex >= 0)
                {
                    DataRowView row = (DataRowView)dataGrid.SelectedItems[0];
                    GridColumn colum = ((SfDataGrid)sender).CurrentColumn as GridColumn;


                    if (e.Key == Key.F8)
                    {
                        GridColumn Colum = ((SfDataGrid)sender).CurrentColumn as GridColumn;
                        if (Colum.MappingName == "valor")
                        {
                            decimal totalabono = 0;
                            decimal.TryParse(dtCue.Compute("Sum(valor)", "").ToString(), out totalabono);
                            System.Data.DataRow dr = dtCue.Rows[dataGrid.SelectedIndex];
                            dr.BeginEdit();
                            dr["valor"] = (totalPagar - totalabono);
                            dr.EndEdit();
                            e.Handled = true;
                        }
                        dataGrid.UpdateLayout();
                        sumaAbonos();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error en dataGrid_PreviewKeyDown_1" + ex.Message.ToString());
            }
        }

        private void dataGrid_CurrentCellEndEdit(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellEndEditEventArgs e)
        {
            try
            {

                GridColumn colum = ((SfDataGrid)sender).CurrentColumn as GridColumn;
                if (colum.MappingName == "valor")
                {
                    decimal totalabono = 0;

                    decimal.TryParse(dtCue.Compute("Sum(valor)", "").ToString(), out totalabono);
                    System.Data.DataRow dr = dtCue.Rows[dataGrid.SelectedIndex];
                    if (totalabono > totalPagar)
                    {
                        MessageBox.Show("El valor pagado es mayor al saldo...", "alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        dr.BeginEdit();
                        dr["valor"] = 0;
                        dr.EndEdit();
                    }

                    dataGrid.UpdateLayout();
                    sumaAbonos();
                }

            }
            catch (Exception w)
            {
                MessageBox.Show("error en dataGrid_CurrentCellEndEdit:" + w);
            }
        }


        private void BtnGrabar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool flag_valor = true;
                foreach (System.Data.DataRow dr in dtCue.Rows)
                {
                    double val_fpag = Convert.ToDouble(dr["valor"]);
                    if (val_fpag <= 0) flag_valor = false;
                }


                if (!flag_valor)
                {
                    MessageBox.Show("ingreso una forma de pago sin valor por favor elimine la forma de pago o agreguele un valor", "alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    dataGrid.SelectedIndex = 0;
                    dataGrid.Focus();
                    return;
                }




                var valor = TxtTotalRecaudo.Text;
                decimal value = decimal.Parse(valor, NumberStyles.Currency);

                if (value == 0)
                {
                    decimal abono = 0;
                    decimal.TryParse(dtCue.Compute("Sum(valor)", "").ToString(), out abono);
                    if (abono <= 0 || abono != totalPagar)
                    {
                        MessageBox.Show("Digita Valor a pagar o valor a abono es diferente al valor a pagar", "alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        dataGrid.SelectedIndex = 0;
                        dataGrid.Focus();
                        return;
                    }

                    flag = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tiene un saldo por pagar de:" + TxtTotalRecaudo.Text);
                }

            }
            catch (Exception w)
            {
                MessageBox.Show("error en Button_Click:" + w);
            }
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
        {

            flag = false;
            this.Close();
        }


    }
}

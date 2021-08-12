using DocumentoSoporte;
using DocumentoSoporte.Model;
using Microsoft.Reporting.WinForms;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SiasoftAppExt
{

    //Sia.PublicarPnt(10715,"DocumentoSoporte");
    //dynamic ww = ((Inicio)Application.Current.MainWindow).WindowExt(10715, "DocumentoSoporte");    
    //ww.ShowInTaskbar = false;
    //ww.Owner = Application.Current.MainWindow;
    //ww.WindowStartupLocation = WindowStartupLocation.CenterScreen;    
    //ww.ShowDialog();    

    public partial class DocumentoSoporte : Window
    {
        dynamic SiaWin;
        public int idemp = 0;
        string cnEmp = "";
        string cod_empresa = "";
        string nomempresa = "";

        public string idreg_editar = "";
        public string numtrn_editar = "";

        Documento Doc = new Documento();

        public DocumentoSoporte()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                SiaWin = System.Windows.Application.Current.MainWindow;
                if (idemp <= 0) idemp = SiaWin._BusinessId;

                LoadConfig();
                this.DataContext = Doc;

            }
            catch (Exception w)
            {
                MessageBox.Show("error al cargar:" + w);
            }
        }

        private void LoadConfig()
        {
            try
            {
                SiaWin = Application.Current.MainWindow;
                if (idemp <= 0) idemp = SiaWin._BusinessId;

                System.Data.DataRow foundRow = SiaWin.Empresas.Rows.Find(idemp);
                idemp = Convert.ToInt32(foundRow["BusinessId"].ToString().Trim());
                cnEmp = foundRow[SiaWin.CmpBusinessCn].ToString().Trim();
                cod_empresa = foundRow["BusinessCode"].ToString().Trim();
                nomempresa = foundRow["BusinessName"].ToString().Trim();
                this.Title = "Documento Soporte " + cod_empresa + "-" + nomempresa;

                Doc.fecha = DateTime.Now.ToString("dd/MM/yyyy");

                DataTable dt = SiaWin.Func.SqlDT("select ds_resol,ds_fecha,ds_inicial,ds_final,ds_prefijo,ds_consecutivo From Co_confi", "tabla", idemp);
                if (dt.Rows.Count > 0)
                {
                    Doc.resolucion = dt.Rows[0]["ds_resol"].ToString().Trim();
                    Doc.fec_resolucion = dt.Rows[0]["ds_fecha"].ToString().Trim();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("error en el load" + e.Message);
            }
        }

        private void tx_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                string tx = (sender as TextBox).Text;

                if (string.IsNullOrEmpty(tx)) return;

                string cod = "", nom = "", tit = "";
                string tbl = (sender as TextBox).Tag.ToString();

                switch (tbl)
                {
                    case "comae_ter":
                        cod = "cod_ter"; nom = "nom_ter"; tit = "Maestra de terceros";
                        break;
                    case "inmae_mer":
                        cod = "cod_mer"; nom = "nom_mer"; tit = "Maestra de vendedores";
                        break;
                }


                string query = "select * from " + tbl + " where  " + cod + "='" + tx + "' ";
                DataTable dt = SiaWin.Func.SqlDT(query, "tabla", idemp);
                if (dt.Rows.Count > 0)
                {
                    string code = dt.Rows[0][cod].ToString();
                    string name = dt.Rows[0][nom].ToString();
                    switch (tbl)
                    {
                        case "comae_ter":
                            Doc.cod_ter = code; Doc.nom_ter = name;
                            break;
                        case "inmae_mer":
                            Doc.cod_ven = code; Doc.nom_ven = name;
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("el codigo que ingreso no existe en la " + tit, "alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    switch (tbl)
                    {
                        case "comae_ter":
                            Doc.cod_ter = ""; Doc.nom_ter = "";
                            break;
                        case "inmae_mer":
                            Doc.cod_ven = ""; Doc.nom_ven = "";
                            break;
                    }
                }


            }
            catch (Exception w)
            {
                MessageBox.Show("error al buscar:" + w);
            }
        }

        private void Tx_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.F8 || e.Key == Key.Enter)
                {

                    string cod = "", nom = "", id = "", tit = "";
                    string tbl = (sender as TextBox).Tag.ToString();

                    switch (tbl)
                    {
                        case "comae_ter":
                            cod = "cod_ter"; nom = "nom_ter"; id = "idrow"; tit = "Maestra de terceros";
                            break;
                        case "inmae_mer":
                            cod = "cod_mer"; nom = "nom_mer"; id = "idrow"; tit = "Maestra de vendedores";
                            break;
                    }

                    dynamic winb = SiaWin.WindowBuscar(tbl, cod, nom, cod, id, Title, cnEmp, false, "", idEmp: idemp);
                    winb.ShowInTaskbar = false;
                    winb.Owner = Application.Current.MainWindow;
                    winb.Height = 400;
                    winb.ShowDialog();
                    int idtbl = winb.IdRowReturn;
                    string codetbl = winb.Codigo;
                    string nomtbl = winb.Nombre;

                    if (idtbl > 0)
                    {
                        switch (tbl)
                        {
                            case "comae_ter":
                                Doc.cod_ter = codetbl; Doc.nom_ter = nomtbl;
                                break;
                            case "inmae_mer":
                                Doc.cod_ven = codetbl; Doc.nom_ven = nomtbl;
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("el codigo que ingreso no existe en la " + tit, "alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        (sender as TextBox).Text = "";
                    }


                }


            }
            catch (Exception w)
            {
                MessageBox.Show("error al buscar:" + w);
            }
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string btn = (sender as Button).Content.ToString();

                if (btn == "Nuevo")
                {
                    (sender as Button).Content = "Guardar";
                    BtnSalir.Content = "Cancelar";
                    BtnEditar.IsEnabled = false;
                    PanelA.IsEnabled = true;
                    PanelB.IsEnabled = true;
                    Doc.RefGDCSource.Add(new Referencia() { referencia = "" });
                    dataGrid.CommitEdit();
                    dataGrid.UpdateLayout();
                    dataGrid.SelectedIndex = 0;
                    TxCodCli.Focus();
                }
                else
                {
                    #region validaciones


                    if (string.IsNullOrEmpty(Doc.cod_ter))
                    {
                        MessageBox.Show("debe de llenar el campo de terceros", "alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                    if (Doc.RefGDCSource.Count > 0)
                    {
                        bool fc = true;
                        bool fv = true;
                        bool fr = true;
                        foreach (var item in Doc.RefGDCSource)
                        {
                            if (item.cantidad == 0) fc = false;
                            if (item.val_uni == 0) fv = false;
                            if (string.IsNullOrEmpty(item.referencia)) fr = false;
                        }

                        if (!fc)
                        {
                            MessageBox.Show("el documento no permite cantidades en 0", "alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            return;
                        }
                        if (!fv)
                        {
                            MessageBox.Show("el documento no permite el valor unitario en 0", "alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            return;
                        }

                        if (!fr)
                        {
                            MessageBox.Show("el documento no permite que el campo referencia este vacio", "alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("debe de generar almenos un registro en el cuerpo del documento", "alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }



                    #endregion



                    FormasDePago wFpago = new FormasDePago();
                    wFpago.totalPagar = Doc.RefGDCSource.Total().total;
                    wFpago.ShowInTaskbar = false;
                    wFpago.Owner = Application.Current.MainWindow;
                    wFpago.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    wFpago.ShowDialog();

                    DataTable dtfpag = wFpago.dtCue;
                    if (!wFpago.flag)
                    {
                        MessageBox.Show("debe de ingresar la forma de pago para poder generar el documento soporte", "alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }



                    int id = document(idreg_editar, numtrn_editar, dtfpag);
                    if (id > 0)
                    {
                        string message = string.IsNullOrEmpty(idreg_editar) ? "se genero el documento exitosamente" : "se edito el documento:" + numtrn_editar + " exitosamente";
                        MessageBox.Show(message, "alerta", MessageBoxButton.OK, MessageBoxImage.Information);
                        imprimirPrograEgreso(id.ToString());
                        Doc.Clear();
                        BtnNuevo.Content = "Nuevo";
                        BtnSalir.Content = "Salir";
                        BtnEditar.IsEnabled = true;
                        PanelA.IsEnabled = false;
                        PanelB.IsEnabled = false;
                        PanelB.IsEnabled = false;
                        idreg_editar = "";
                        numtrn_editar = "";
                    }
                }

            }
            catch (Exception w)
            {
                MessageBox.Show("error BtnNuevo_Click:" + w);
            }
        }


        public int document(string idreg, string num_trn, DataTable dtFpag)
        {
            int bandera = -1;
            try
            {
                string message = string.IsNullOrEmpty(idreg) ? "Usted desea guardar el documento de soporte..?" : "usted desea editar el documento:" + num_trn;
                string caption = string.IsNullOrEmpty(idreg) ? "Generar Documento Soporte" : "Editar Documento Soporte";

                if (MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    if (!string.IsNullOrEmpty(idreg) || !string.IsNullOrEmpty(num_trn))
                    {
                        string delete = "delete CoCabSoporte where idreg='" + idreg + "';";
                        delete += "delete CoCueSoporte where idregcab='" + idreg + "';";

                        if (SiaWin.Func.SqlCRUD(delete, idemp) == true)
                        {
                            MessageBox.Show("documento eliminado exitosamente para volver a ser creado", "alerta", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }

                    using (SqlConnection connection = new SqlConnection(cnEmp))
                    {
                        connection.Open();
                        SqlCommand command = connection.CreateCommand();
                        SqlTransaction transaction = connection.BeginTransaction("Transaction");
                        command.Connection = connection;
                        command.Transaction = transaction;

                        string sqlcab = "";
                        string sqlcue = "";
                        string sqlfpag = "";

                        string sqlConsecutivo = @"declare @fecdoc as datetime;set @fecdoc = getdate();";
                        sqlConsecutivo = sqlConsecutivo + "declare @fecdocsecond as datetime;set @fecdocsecond = DATEADD(second,1,GETDATE()); ";
                        sqlConsecutivo = sqlConsecutivo + "declare @ini as char(4);declare @num as varchar(12); ";
                        sqlConsecutivo = sqlConsecutivo + "declare @iConsecutivo char(12) = '' ;declare @iFolioHost int = 0; ";
                        sqlConsecutivo = sqlConsecutivo + "UPDATE Co_confi SET ds_consecutivo=ISNULL(ds_consecutivo, 0) + 1 ; ";
                        sqlConsecutivo = sqlConsecutivo + "SELECT @iFolioHost = ds_consecutivo,@ini=rtrim(ds_prefijo) FROM Co_confi;";
                        sqlConsecutivo = sqlConsecutivo + "set @num=@iFolioHost; select @iConsecutivo=rtrim(@ini)+'-'+rtrim(convert(varchar,@num));";


                        string consecutivo = string.IsNullOrEmpty(idreg) ? "@iConsecutivo" : "'" + num_trn + "'";

                        string cod_ter = Doc.cod_ter.Trim();
                        string cod_ven = Doc.cod_ven.Trim();
                        string fecha = Doc.fecha.Trim();
                        string nota = Doc.nota.Trim();
                        string por_ica = Doc.por_ica.ToString("F", CultureInfo.InvariantCulture);
                        string por_riva = Doc.por_riva.ToString("F", CultureInfo.InvariantCulture);
                        string por_rft = Doc.por_rft.ToString("F", CultureInfo.InvariantCulture);

                        sqlcab = sqlConsecutivo + @"INSERT INTO CoCabSoporte (cod_cli,cod_ven,num_trn,fecha,por_ica,por_ret,por_riva,nota,userid) values ('" + cod_ter + "','" + cod_ven + "'," + consecutivo + ",'" + fecha + "'," + por_ica + "," + por_rft + "," + por_riva + ",'" + nota + "'," + SiaWin._UserId + ");DECLARE @NewID INT;SELECT @NewID = SCOPE_IDENTITY();";

                        foreach (var item in Doc.RefGDCSource)
                        {
                            if (item.cantidad > 0)
                            {

                                string referencia = item.referencia.Trim();
                                string detalle = item.detalle.Trim();
                                string cantidad = item.cantidad.ToString("F", CultureInfo.InvariantCulture);
                                string val_uni = item.val_uni.ToString("F", CultureInfo.InvariantCulture);
                                string val_ica = item.val_ica.ToString("F", CultureInfo.InvariantCulture);
                                string val_rft = item.val_rft.ToString("F", CultureInfo.InvariantCulture);
                                string val_riva = item.val_riva.ToString("F", CultureInfo.InvariantCulture);
                                string subtotal = item.subtotal.ToString("F", CultureInfo.InvariantCulture);
                                string total = item.total.ToString("F", CultureInfo.InvariantCulture);

                                sqlcue += @"INSERT INTO CoCueSoporte (idregcab,referencia,detalle,cantidad,vr_unit,val_ica,val_ret,val_riva,subtotal,total) values (@NewID,'" + referencia + "','" + detalle + "'," + cantidad + "," + val_uni + "," + val_ica + "," + val_rft + "," + val_riva + "," + subtotal + "," + total + ");";
                            }
                        }



                        if (dtFpag.Rows.Count > 0)
                        {
                            foreach (System.Data.DataRow row in dtFpag.Rows)
                            {

                                string cod_pag = row["cod_pag"].ToString().Trim();
                                string cod_tpag = row["cod_tpag"].ToString().Trim();
                                string valor = Convert.ToDecimal(row["valor"]).ToString("F", CultureInfo.InvariantCulture);

                                sqlfpag += $"INSERT INTO CoFpagSoporte (idregcab,cod_trn,num_trn,cod_pag,vlr_pagado,cod_tpag) VALUES (@NewID,'',{consecutivo},'{cod_pag}',{valor},'{cod_tpag}');";
                            }
                        }


                        command.CommandText = sqlcab + sqlcue + sqlfpag + @"select CAST(@NewId AS int);";
                        
                        
                        var r = new object();
                        r = command.ExecuteScalar();
                        transaction.Commit();
                        connection.Close();
                        bandera = Convert.ToInt32(r.ToString());
                    }
                }
                else
                {
                    bandera = -1;
                    dataGrid.Focus();
                }

                return bandera;
            }
            catch (Exception w)
            {
                MessageBox.Show("error al generar documento de soporte:" + w);
                return bandera;
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

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string btn = (sender as Button).Content.ToString();
                if (btn == "Cancelar")
                {
                    Doc.Clear();
                    (sender as Button).Content = "Salir";
                    BtnNuevo.Content = "Nuevo";
                    BtnEditar.IsEnabled = true;
                    idreg_editar = "";
                    numtrn_editar = "";
                    PanelA.IsEnabled = false;
                    PanelB.IsEnabled = false;
                    PanelB.IsEnabled = false;
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Usted desea salir?", "Confirmacion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception w)
            {
                MessageBox.Show("erro al salir:" + w);
            }
        }

        private void BtnImprimir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DocConsulta win = new DocConsulta();
                win.SiaWin = SiaWin;
                win.idemp = idemp;
                win.cnEmp = cnEmp;
                win.cod_empresa = cod_empresa;
                win.nomempresa = nomempresa;
                win.ShowInTaskbar = false;
                win.Owner = Application.Current.MainWindow;
                win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                win.ShowDialog();

            }
            catch (Exception w)
            {
                MessageBox.Show("error al imprimir:" + w);
            }
        }

        public void UpdateTot()
        {
            try
            {
                var tt = Doc.RefGDCSource.Total();

                if (tt.sub > 0)
                {
                    Doc.tot_cnt = tt.cnt;
                    Doc.tot_vlrunit = tt.cnt;
                    Doc.tot_vlrunit = tt.vlrunt;
                    Doc.tot_vlrtef = tt.vrtef;
                    Doc.tot_vlrica = tt.vrica;
                    Doc.tot_vlriva = tt.vrriva;
                    Doc.tot_subtotal = tt.sub;
                    Doc.tot_tot = tt.total;
                    Doc.tot_reg = Doc.RefGDCSource.Count;
                }

            }
            catch (Exception w)
            {
                MessageBox.Show("retenciones:" + w);
            }
        }

        private void dataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (dataGrid.IsReadOnly == true) return;
                if (e.Key == System.Windows.Input.Key.F5)
                {
                    BtnNuevo.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    return;
                }

                var data = ((DataGrid)sender).SelectedItem as Referencia;
                if (data == null) e.Handled = true;

                var uiElement = e.OriginalSource as UIElement;
                if ((e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Right || e.Key == Key.Tab)) //&& ((DataGrid)sender).CurrentColumn.DisplayIndex == 0)
                {
                    if (string.IsNullOrEmpty(data.referencia))
                    {
                        if (CheckCompras.IsChecked == true)
                        {
                            dynamic ww = SiaWin.WindowExt(9326, "InBuscarReferencia");  //carga desde sql
                            ww.Conexion = SiaWin.Func.DatosEmp(idemp);
                            ww.idEmp = idemp;
                            //ww.idBod = CmbBodOrigen.SelectedValue.ToString();
                            ww.UltBusqueda = "";
                            ww.ShowInTaskbar = false;
                            ww.Owner = Application.Current.MainWindow;
                            ww.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            ww.Height = 400;
                            ww.ShowDialog();
                            if (!string.IsNullOrEmpty(ww.Codigo))
                                data.referencia = ww.Codigo.ToString();
                            data.detalle = ww.Nombre.ToString();
                            ww = null;

                            if (string.IsNullOrEmpty(data.referencia))
                                MessageBox.Show("llene el campo de referencia", "alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show("el campo referencia debe de estar lleno", "alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            e.Handled = true;
                            return;
                        }
                    }

                    int column = ((DataGrid)sender).CurrentColumn.DisplayIndex + 1;
                    int columntot = ((DataGrid)sender).Columns.Count;

                    int fila1 = ((DataGrid)sender).SelectedIndex;
                    int fila = ((DataGrid)sender).Items.IndexOf(((DataGrid)sender).SelectedItem);

                    if ((e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab) && uiElement != null && (column < columntot))
                    {

                        if (!string.IsNullOrEmpty(data.referencia) && ((DataGrid)sender).CurrentColumn.DisplayIndex == columntot)
                        {
                            Int32 countref = Doc.RefGDCSource.Count;
                            if (countref == dataGrid.SelectedIndex + 1)
                            {
                                Doc.RefGDCSource.Add(new Referencia() { referencia = "" });
                                uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
                                dataGrid.SelectedIndex = dataGrid.SelectedIndex + 1;
                                dataGrid.CurrentCell = new DataGridCellInfo(dataGrid.Items[dataGrid.SelectedIndex], dataGrid.Columns[0]);
                                dataGrid.CommitEdit();
                                dataGrid.UpdateLayout();
                            }
                        }

                        if (((DataGrid)sender).CurrentColumn.DisplayIndex >= 0)
                        {
                            uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                            e.Handled = true;
                            return;
                        }
                    }

                    if (e.Key == Key.Right && ((DataGrid)sender).CurrentColumn.DisplayIndex == 0 && !string.IsNullOrEmpty(data.referencia))
                    {
                        uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                        e.Handled = true;
                    }

                    if (e.Key == Key.Left && uiElement != null && (column > 1))
                    {
                        uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Left));
                        e.Handled = true;
                    }

                    if ((e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Right || e.Key == Key.Tab) && uiElement != null && (column == columntot))
                    {
                        dataGrid.CommitEdit();
                        dataGrid.UpdateLayout();

                        int add = 0;
                        if (fila + 1 == Doc.RefGDCSource.Count)
                        {
                            Doc.RefGDCSource.Add(new Referencia() { referencia = "" });
                            add = 1;
                        }

                        if (add > 0) uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
                        dataGrid.CurrentCell = new DataGridCellInfo(dataGrid.Items[dataGrid.SelectedIndex + add], dataGrid.Columns[0]);
                        dataGrid.CommitEdit();
                        dataGrid.UpdateLayout();
                        dataGrid.SelectedIndex = dataGrid.SelectedIndex + add;
                        e.Handled = true;
                    }


                    if (e.Key == Key.Up && dataGrid.CurrentColumn.DisplayIndex == 0 && string.IsNullOrEmpty(data.referencia))
                    {
                        var selectedItem = dataGrid.SelectedItem as Referencia;
                        if (selectedItem != null)
                        {
                            uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Up));
                            dataGrid.SelectedIndex = dataGrid.SelectedIndex - 1;
                            Doc.RefGDCSource.Remove(selectedItem);
                            dataGrid.UpdateLayout();
                            var selectedItemnew = dataGrid.SelectedItem as Referencia;
                            if (selectedItemnew.cantidad > 0)
                            {
                                dataGrid.CurrentCell = new DataGridCellInfo(dataGrid.Items[dataGrid.SelectedIndex], dataGrid.Columns[2]);
                                dataGrid.CancelEdit();
                                dataGrid.UpdateLayout();
                            }
                            e.Handled = true;
                        }
                    }

                    if (e.Key == Key.Up)
                    {
                        var selectedItemnew = dataGrid.SelectedItem as Referencia;
                        if (selectedItemnew.cantidad > 0)
                        {
                            dataGrid.CurrentCell = new DataGridCellInfo(dataGrid.Items[dataGrid.SelectedIndex], dataGrid.Columns[2]);
                            dataGrid.CancelEdit();
                            dataGrid.UpdateLayout();
                        }
                    }

                    if (e.Key == Key.F3)  //eliminar registro
                    {
                        if (((DataGrid)sender).SelectedIndex == 0 && Doc.RefGDCSource.Count == 1) return;
                        if (MessageBox.Show("Borrar Registro actual?", "Siasoft", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                        {
                            var selectedItem = dataGrid.SelectedItem as Referencia;
                            if (selectedItem != null)
                            {
                                int fila1x = ((DataGrid)sender).SelectedIndex;
                                Int32 countrefx = Doc.RefGDCSource.Count;
                                if (((DataGrid)sender).SelectedIndex == 0 && Doc.RefGDCSource.Count > 1)
                                {
                                    uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
                                }
                                else
                                {
                                    if (((DataGrid)sender).SelectedIndex > 0 && Doc.RefGDCSource.Count > 1) uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Down));
                                    if (((DataGrid)sender).SelectedIndex == Doc.RefGDCSource.Count - 1) uiElement.MoveFocus(new TraversalRequest(FocusNavigationDirection.Up));
                                }
                                Doc.RefGDCSource.Remove(selectedItem);
                            }
                            e.Handled = true;
                        }
                    }

                }
            }
            catch (Exception w)
            {
                MessageBox.Show("error:" + w);
            }
        }

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                if (e.Column.Header.ToString() == "Cantidad" || e.Column.Header.ToString() == "VlUnit")
                {
                    foreach (Referencia item in Doc.RefGDCSource)
                    {
                        item.por_rft = Doc.por_rft;
                        item.por_ica = Doc.por_ica;
                        item.por_riva = Doc.por_riva;
                    }
                }

                if (CheckCompras.IsChecked == true)
                {
                    if (e.Column.Header.ToString() == "Referencia")
                    {
                        var data = ((DataGrid)sender).SelectedItem as Referencia;
                        DataTable dt = SiaWin.Func.SqlDT("select * from inmae_ref where cod_ref='" + data.referencia + "';", "ref", idemp);
                        //  MessageBox.Show("verifica la referencia " , "alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        if (dt.Rows.Count <= 0)
                        {
                            MessageBox.Show("la referencia " + data.referencia + " ingresada no existe por ende sera eliminada del registro", "alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            data.referencia = "";
                        }
                        else
                        {
                            //    MessageBox.Show("la referencia existe " + dt.Rows[0]["cod_ant"].ToString().Trim() + " /"+ dt.Rows[0]["nom_ref"].ToString().Trim(), "alerta", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            data.detalle = dt.Rows[0]["cod_ant"].ToString().Trim();
                        }
                    }
                }

                UpdateTot();
            }
            catch (Exception w)
            {
                MessageBox.Show("retenciones:" + w);
            }
        }

        private void DoubleTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            string name = (sender as DoubleTextBox).Name;
            decimal porcent = Convert.ToDecimal((sender as DoubleTextBox).Value);
            updateCuerpo(name, porcent);
        }

        public void updateCuerpo(string name, decimal porcent)
        {
            try
            {

                foreach (Referencia item in Doc.RefGDCSource)
                {
                    switch (name)
                    {
                        case "TxPorRtf":
                            item.por_rft = porcent;
                            break;
                        case "TxPorIca":
                            item.por_ica = porcent;
                            break;
                        case "TxPorRiva":
                            item.por_riva = porcent;
                            break;
                    }
                }

                UpdateTot();
            }
            catch (Exception w)
            {
                MessageBox.Show("error al asignar porcentajes al cuerpo:" + w);
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BtnEditar win = new BtnEditar();
                win.SiaWin = SiaWin;
                win.idemp = idemp;
                win.cnEmp = cnEmp;
                win.cod_empresa = cod_empresa;
                win.nomempresa = nomempresa;
                win.ShowInTaskbar = false;
                win.Owner = Application.Current.MainWindow;
                win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                win.ShowDialog();

                if (win.flag)
                {
                    string idreg = win.idreg;
                    idreg_editar = win.idreg;
                    numtrn_editar = win.numtrn;

                    string cabselc = "select cab.cod_cli,ter.nom_ter,cab.cod_ven,mer.nom_mer,cab.num_trn,cab.fecha,cab.por_ica,cab.por_ret,cab.por_riva,cab.nota ";
                    cabselc += "from CoCabSoporte as cab  ";
                    cabselc += "inner join comae_ter as ter on cab.cod_cli = ter.cod_ter ";
                    cabselc += "left join InMae_mer as mer on mer.cod_mer = cab.cod_ven ";
                    cabselc += "where cab.idreg='" + idreg + "';";

                    DataTable dtcab = SiaWin.Func.SqlDT(cabselc, "tabla", idemp);
                    if (dtcab.Rows.Count > 0)
                    {
                        Doc.Clear();

                        string codcli = dtcab.Rows[0]["cod_cli"].ToString().Trim();
                        string nomter = dtcab.Rows[0]["nom_ter"].ToString().Trim();
                        string codven = dtcab.Rows[0]["cod_ven"].ToString().Trim();
                        string nommer = dtcab.Rows[0]["nom_mer"].ToString().Trim();
                        string fec = dtcab.Rows[0]["fecha"].ToString().Trim();
                        decimal porica = Convert.ToDecimal(dtcab.Rows[0]["por_ica"]);
                        decimal porret = Convert.ToDecimal(dtcab.Rows[0]["por_ret"]);
                        decimal porriva = Convert.ToDecimal(dtcab.Rows[0]["por_riva"]);
                        string nota = dtcab.Rows[0]["nota"].ToString().Trim();

                        Doc.cod_ter = codcli;
                        Doc.nom_ter = nomter;
                        Doc.cod_ven = codven;
                        Doc.nom_ven = nommer;
                        Doc.fecha = fec;
                        Doc.nota = nota;
                        Doc.por_ica = porica;
                        Doc.por_rft = porret;
                        Doc.por_riva = porriva;

                        DataTable dtcue = SiaWin.Func.SqlDT("select * from CoCueSoporte where idregcab='" + idreg + "'  ", "tabla", idemp);
                        if (dtcue.Rows.Count > 0)
                        {
                            foreach (System.Data.DataRow item in dtcue.Rows)
                            {
                                Doc.RefGDCSource.Add(new Referencia()
                                {
                                    referencia = item["referencia"].ToString().Trim(),
                                    detalle = item["detalle"].ToString().Trim(),
                                    cantidad = Convert.ToDecimal(item["cantidad"]),
                                    val_uni = Convert.ToDecimal(item["vr_unit"]),
                                    por_rft = porret,
                                    val_rft = Convert.ToDecimal(item["val_ret"]),
                                    por_ica = porica,
                                    val_ica = Convert.ToDecimal(item["val_ica"]),
                                    por_riva = porriva,
                                    val_riva = Convert.ToDecimal(item["val_riva"]),
                                    subtotal = Convert.ToDecimal(item["subtotal"]),
                                    total = Convert.ToDecimal(item["total"])
                                });
                            }



                            UpdateTot();
                        }

                        BtnNuevo.Content = "Guardar";
                        BtnSalir.Content = "Cancelar";
                        PanelA.IsEnabled = true;
                        PanelB.IsEnabled = true;
                        dataGrid.CommitEdit();
                        dataGrid.UpdateLayout();
                        dataGrid.SelectedIndex = 0;
                        TxCodCli.Focus();
                    }
                }
            }
            catch (Exception w)
            {
                MessageBox.Show("error al abrir la pantalla de edicion:" + w);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = (sender as CheckBox).Name;
                foreach (CheckBox item in GridCheck.Children)
                {
                    item.IsChecked =
                        item.Name == name ? true : false;
                }
            }
            catch (Exception w)
            {
                MessageBox.Show("error al checeked:" + w);
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                bool flag = false;
                foreach (CheckBox item in GridCheck.Children)
                {
                    if (item.IsChecked == true) flag = true;
                }

                if (!flag)
                    CheckServicios.IsChecked = true;

            }
            catch (Exception w)
            {
                MessageBox.Show("error al uncheceked:" + w);
            }
        }



    }
}

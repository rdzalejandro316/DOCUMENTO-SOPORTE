using System;
using System.Collections.Generic;
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
using SrvEnvio = EnvioDocumentoSoporte.ServiceReference1;



namespace EnvioDocumentoSoporte
{

    public partial class MainWindow : Window
    {

        SrvEnvio.ServiceClient ServiceClient;
        string tokenEmpresa = "7b962c55cf214a20ba4a93fb90c2ebe792f9a99c";
        string tokenPassword = "5a10c39027f54bcea4530b57f96a3d08473b6a79";


        public MainWindow()
        {
            InitializeComponent();
            ServiceClient = new SrvEnvio.ServiceClient();
        }

        private void BtnEnviar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SrvEnvio.FacturaGeneral factura = new SrvEnvio.FacturaGeneral();
                factura.cantidadDecimales = "2";

                #region cliente

                SrvEnvio.Cliente cliente = new SrvEnvio.Cliente();
                cliente.email = "alejandro-rdz-@hotmail.com";

                SrvEnvio.Tributos tributos = new SrvEnvio.Tributos();
                tributos.codigoImpuesto = "ZY";

                cliente.detallesTributarios = new SrvEnvio.Tributos[1];
                cliente.detallesTributarios[0] = tributos;

                cliente.direccionCliente = new SrvEnvio.Direccion()
                {
                    ciudad = "Bogotá",
                    codigoDepartamento = "11",
                    departamento = "Bogota",
                    direccion = "crr 5 cll 20",
                    lenguaje = "es",
                    municipio = "11001",
                    pais = "CO"
                };

                cliente.direccionFiscal = new SrvEnvio.Direccion()
                {
                    ciudad = "Bogotá",
                    codigoDepartamento = "11",
                    departamento = "Bogota",
                    direccion = "crr 5 cll 20",
                    lenguaje = "es",
                    municipio = "11001",
                    pais = "CO"
                };

                cliente.informacionLegalCliente = new SrvEnvio.InformacionLegal()
                {

                    nombreRegistroRUT = "CONSORCIO ALIANZA SAN CRISTOBAL 4",
                    numeroIdentificacion = "901041710",
                    numeroIdentificacionDV = "5",
                    tipoIdentificacion = "31",
                };

                cliente.nombreRazonSocial = "pedro perez";
                cliente.notificar = "SI";
                cliente.numeroDocumento = "901041710";
                cliente.numeroIdentificacionDV = "5";


                SrvEnvio.Obligaciones obligaciones = new SrvEnvio.Obligaciones()
                {
                    obligaciones = "R-99-PN",
                    regimen = "49"
                };
                cliente.responsabilidadesRut = new SrvEnvio.Obligaciones[1];
                cliente.responsabilidadesRut[0] = obligaciones;

                cliente.tipoIdentificacion = "31";
                cliente.tipoPersona = "1";


                factura.cliente = cliente;
                #endregion


                #region detalle factura


                factura.detalleDeFactura = new SrvEnvio.FacturaDetalle[2];

                #region producto 1

                SrvEnvio.FacturaDetalle producto1 = new SrvEnvio.FacturaDetalle()
                {
                    cantidadPorEmpaque = "1",
                    cantidadReal = "1.00",
                    cantidadRealUnidadMedida = "WSD",
                    cantidadUnidades = "1.00",
                    codigoProducto = "P000001",
                    descripcion = "impresora ",
                    estandarCodigo = "999",
                    estandarCodigoProducto = "PHKA80"
                };

                SrvEnvio.FacturaImpuestos facturaImpuestos1 = new SrvEnvio.FacturaImpuestos()
                {
                    baseImponibleTOTALImp = "1003.00",
                    codigoTOTALImp = "01",
                    controlInterno = "",
                    porcentajeTOTALImp = "19.00",
                    unidadMedida = "94",
                    unidadMedidaTributo = "",
                    valorTOTALImp = "190.57",
                    valorTributoUnidad = ""
                };
                producto1.impuestosDetalles = new SrvEnvio.FacturaImpuestos[1];
                producto1.impuestosDetalles[0] = facturaImpuestos1;

                SrvEnvio.ImpuestosTotales impuestoTOTAL1 = new SrvEnvio.ImpuestosTotales
                {
                    codigoTOTALImp = "01",
                    montoTotal = "190.57"
                };
                producto1.impuestosTotales = new SrvEnvio.ImpuestosTotales[1];
                producto1.impuestosTotales[0] = impuestoTOTAL1;

                producto1.marca = "HKA";
                producto1.muestraGratis = "0";
                producto1.precioTotal = "1193.57";
                producto1.precioTotalSinImpuestos = "1003.00";
                producto1.precioVentaUnitario = "1003.00";
                producto1.secuencia = "1";
                producto1.unidadMedida = "WWSD";

                #endregion

                factura.detalleDeFactura[0] = producto1;

                #region producto 2

                SrvEnvio.FacturaDetalle producto2 = new SrvEnvio.FacturaDetalle()
                {
                    cantidadPorEmpaque = "1",
                    cantidadReal = "1.00",
                    cantidadRealUnidadMedida = "WSD",
                    cantidadUnidades = "1.00",
                    codigoProducto = "P000002",
                    descripcion = "impresora ",
                    estandarCodigo = "999",
                    estandarCodigoProducto = "PHKA801"
                };

                SrvEnvio.FacturaImpuestos facturaImpuestos2 = new SrvEnvio.FacturaImpuestos()
                {
                    baseImponibleTOTALImp = "1003.00",
                    codigoTOTALImp = "01",
                    controlInterno = "",
                    porcentajeTOTALImp = "19.00",
                    unidadMedida = "94",
                    unidadMedidaTributo = "",
                    valorTOTALImp = "190.57",
                    valorTributoUnidad = ""
                };
                producto2.impuestosDetalles = new SrvEnvio.FacturaImpuestos[1];
                producto2.impuestosDetalles[0] = facturaImpuestos2;

                SrvEnvio.ImpuestosTotales impuestoTOTAL2 = new SrvEnvio.ImpuestosTotales
                {
                    codigoTOTALImp = "01",
                    montoTotal = "190.57"
                };
                producto2.impuestosTotales = new SrvEnvio.ImpuestosTotales[1];
                producto2.impuestosTotales[0] = impuestoTOTAL2;

                producto2.marca = "HKA";
                producto2.muestraGratis = "0";
                producto2.precioTotal = "1193.57";
                producto2.precioTotalSinImpuestos = "1003.00";
                producto2.precioVentaUnitario = "1003.00";
                producto2.secuencia = "1";
                producto2.unidadMedida = "WWSD";

                #endregion

                factura.detalleDeFactura[1] = producto2;

                #endregion

                #region impuestos

                SrvEnvio.FacturaImpuestos impuestoGeneral1 = new SrvEnvio.FacturaImpuestos
                {
                    baseImponibleTOTALImp = "2006.00",
                    codigoTOTALImp = "01",
                    porcentajeTOTALImp = "19.00",
                    unidadMedida = "WSD",
                    valorTOTALImp = "381.14"
                };
                factura.impuestosGenerales = new SrvEnvio.FacturaImpuestos[1];
                factura.impuestosGenerales[0] = impuestoGeneral1;

                SrvEnvio.ImpuestosTotales impuestosTotales = new SrvEnvio.ImpuestosTotales()
                {
                    codigoTOTALImp = "01",
                    montoTotal = "381.14",
                };
                factura.impuestosTotales = new SrvEnvio.ImpuestosTotales[1];
                factura.impuestosTotales[0] = impuestosTotales;

                #endregion

                #region mediosDePago
                SrvEnvio.MediosDePago medioPago = new SrvEnvio.MediosDePago
                {
                    medioPago = "10",
                    metodoDePago = "2",
                    numeroDeReferencia = "01",
                    fechaDeVencimiento = "2021-08-20"
                };

                factura.mediosDePago = new SrvEnvio.MediosDePago[1];
                factura.mediosDePago[0] = medioPago;
                #endregion


                //factura.rangoNumeracion = "PREFIJO-DESDE";
                factura.moneda= "COP";
                factura.rangoNumeracion = "DA-1";
                factura.redondeoAplicado = "0.00";
                factura.tipoDocumento = "05";
                factura.tipoOperacion = "10";
                factura.totalBaseImponible = "2006.00";
                factura.totalBrutoConImpuesto = "2387.14";
                factura.totalMonto = "2387.14";
                factura.totalProductos = "2";
                factura.totalSinImpuestos = "2006.00";

                factura.consecutivoDocumento = "DA1";
                factura.fechaEmision = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                var docRespuesta = ServiceClient.Enviar(tokenEmpresa, tokenPassword, factura, "0");

                StringBuilder msgError = new StringBuilder();
                if (docRespuesta.mensajesValidacion != null)
                {

                    int nReturnMsg = docRespuesta.mensajesValidacion.Count();
                    for (int i = 0; i < nReturnMsg; i++)
                    {
                        msgError.Append(docRespuesta.mensajesValidacion[i].ToString() + Environment.NewLine);
                    }
                }
                
                //envio factura 
                if (docRespuesta.codigo == 200 || docRespuesta.codigo == 201)
                {                
                    this.rtxInformacion.Text += "Codigo: " + docRespuesta.codigo.ToString() + Environment.NewLine +
                         "Consecutivo Documento: " + docRespuesta.consecutivoDocumento + Environment.NewLine +
                         "Cufe: " + docRespuesta.cufe + Environment.NewLine +
                         "Mensaje: " + docRespuesta.mensaje + Environment.NewLine +
                         "Resultado: " + docRespuesta.resultado + Environment.NewLine;
                    //this.Close();
                }
                else
                {
                    //ActualizaDocFacturaElectronica(docRespuesta);
                    this.rtxInformacion.Text += "Codigo: " + docRespuesta.codigo.ToString() + Environment.NewLine +
                        "Mensaje: " + docRespuesta.mensaje + Environment.NewLine +
                        "Resultado: " + docRespuesta.resultado + Environment.NewLine +
                        "Errores  :" + msgError.ToString();
                }


            }
            catch (Exception w)
            {
                MessageBox.Show("error:" + w);
            }
        }


    }
}

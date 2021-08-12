using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentoSoporte.Model
{
    public class Documento : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private Ref RefgdcSource = new Ref();
        public Ref RefGDCSource
        {
            get { return RefgdcSource; }
            set { RefgdcSource = value; OnPropertyChanged("RefGDCSource"); }
        }


        string _fecha = DateTime.Now.ToString("dd/MM/yyyy");
        public string fecha { get { return _fecha; } set { _fecha = value; OnPropertyChanged("fecha"); } }

        string _cod_ter = "";
        public string cod_ter { get { return _cod_ter; } set { _cod_ter = value; OnPropertyChanged("cod_ter"); } }

        string _nom_ter = "";
        public string nom_ter { get { return _nom_ter; } set { _nom_ter = value; OnPropertyChanged("nom_ter"); } }

        string _cod_ven = "";
        public string cod_ven { get { return _cod_ven; } set { _cod_ven = value; OnPropertyChanged("cod_ven"); } }

        string _nom_ven = "";
        public string nom_ven { get { return _nom_ven; } set { _nom_ven = value; OnPropertyChanged("nom_ven"); } }

        string _nota = "";
        public string nota { get { return _nota; } set { _nota = value; OnPropertyChanged("nota"); } }

        decimal _por_rft = 0;
        public decimal por_rft { get { return _por_rft; } set { _por_rft = value; OnPropertyChanged("por_rft"); } }

        decimal _por_ica = 0;
        public decimal por_ica { get { return _por_ica; } set { _por_ica = value; OnPropertyChanged("por_ica"); } }

        decimal _por_riva = 0;
        public decimal por_riva { get { return _por_riva; } set { _por_riva = value; OnPropertyChanged("por_riva"); } }

        //------------- info documento

        string _resolucion = "";
        public string resolucion { get { return _resolucion; } set { _resolucion = value; OnPropertyChanged("resolucion"); } }

        string _fec_resolucion = "";
        public string fec_resolucion { get { return _fec_resolucion; } set { _fec_resolucion = value; OnPropertyChanged("fec_resolucion"); } }

        string _ini_resolucion = "";
        public string ini_resolucion { get { return _ini_resolucion; } set { _ini_resolucion = value; OnPropertyChanged("ini_resolucion"); } }

        string _fin_resolucion = "";
        public string fin_resolucion { get { return _fin_resolucion; } set { _fin_resolucion = value; OnPropertyChanged("fin_resolucion"); } }

        // -------------- totales

        decimal _tot_cnt = 0;
        public decimal tot_cnt { get { return _tot_cnt; } set { _tot_cnt = value; OnPropertyChanged("tot_cnt"); } }

        decimal _tot_vlrunit = 0;
        public decimal tot_vlrunit { get { return _tot_vlrunit; } set { _tot_vlrunit = value; OnPropertyChanged("tot_vlrunit"); } }

        decimal _tot_vlrtef = 0;
        public decimal tot_vlrtef { get { return _tot_vlrtef; } set { _tot_vlrtef = value; OnPropertyChanged("tot_vlrtef"); } }

        decimal _tot_vlrica = 0;
        public decimal tot_vlrica { get { return _tot_vlrica; } set { _tot_vlrica = value; OnPropertyChanged("tot_vlrica"); } }

        decimal _tot_vlriva = 0;
        public decimal tot_vlriva { get { return _tot_vlriva; } set { _tot_vlriva = value; OnPropertyChanged("tot_vlriva"); } }

        decimal _tot_subtotal = 0;
        public decimal tot_subtotal { get { return _tot_subtotal; } set { _tot_subtotal = value; OnPropertyChanged("tot_subtotal"); } }

        decimal _tot_tot = 0;
        public decimal tot_tot { get { return _tot_tot; } set { _tot_tot = value; OnPropertyChanged("tot_tot"); } }

        int _tot_reg = 0;
        public int tot_reg { get { return _tot_reg; } set { _tot_reg = value; OnPropertyChanged("tot_reg"); } }


        // -------------- metodos
        public void Clear()
        {
            this.cod_ter = string.Empty;
            this.nom_ter = string.Empty;
            this.cod_ven = string.Empty;
            this.nom_ven = string.Empty;
            this.nota = string.Empty;
            this.por_rft = 0;
            this.por_ica = 0;
            this.por_riva = 0;
            RefGDCSource.Clear();
            tot_cnt = 0;
            tot_vlrunit = 0;
            tot_vlrtef = 0;
            tot_vlrica = 0;
            tot_vlriva = 0;
            tot_subtotal = 0;
            tot_tot = 0;
            tot_reg = 0;
        }

        public class Ref : ObservableCollection<Referencia>
        {
            public (decimal cnt, decimal vlrunt, decimal sub, decimal vrtef, decimal vrica, decimal vrriva, decimal total) Total()
            {
                decimal _cnt = 0; decimal _vlrunt = 0;
                decimal _sub = 0;
                decimal _vrtef = 0;
                decimal _vrica = 0;
                decimal _vrriva = 0;
                decimal _total = 0;

                foreach (var item in this)
                {
                    _cnt += item.cantidad;
                    _vlrunt += item.val_uni;
                    _sub += item.subtotal;
                    _vrtef += item.val_rft;
                    _vrica += item.val_ica;
                    _vrriva += item.val_rft;
                    _total += item.total;
                }
                return (cnt: _cnt, vlrunt: _vlrunt, sub: _sub, vrtef: _vrtef, vrica: _vrica, vrriva: _vrriva, total: _total);
            }
        }



    }



}

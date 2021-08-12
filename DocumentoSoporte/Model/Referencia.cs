using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentoSoporte.Model
{
    public class Referencia : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        string _referencia = "";
        public string referencia { get { return _referencia; } set { _referencia = value; OnPropertyChanged("referencia"); } }

        string _detalle = "";
        public string detalle { get { return _detalle; } set { _detalle = value; OnPropertyChanged("detalle"); } }

        decimal _cantidad;
        public decimal cantidad
        {
            get { return _cantidad; }
            set
            {
                _cantidad = value; OnPropertyChanged("cantidad");
                subtotal = _cantidad * _val_uni;
                val_rft = ((subtotal * por_rft) / 100);
                val_ica = ((subtotal * por_ica) / 100);
                val_riva = ((subtotal * por_riva) / 100);
                total = subtotal - val_rft - val_ica - val_riva;
            }
        }



        decimal _val_uni;
        public decimal val_uni
        {
            get { return _val_uni; }
            set
            {
                _val_uni = value; OnPropertyChanged("val_uni");
                subtotal = _cantidad * _val_uni;
                val_rft = ((subtotal * por_rft) / 100);
                val_ica = ((subtotal * por_ica) / 100);
                val_riva = ((subtotal * por_riva) / 100);
                total = subtotal - val_rft - val_ica - val_riva;
            }
        }

        decimal _por_rft;
        public decimal por_rft
        {
            get { return _por_rft; }
            set
            {
                _por_rft = value; OnPropertyChanged("por_rft");
                subtotal = _cantidad * _val_uni;
                val_rft = ((subtotal * por_rft) / 100);
                val_ica = ((subtotal * por_ica) / 100);
                val_riva = ((subtotal * por_riva) / 100);
                total = subtotal - val_rft - val_ica - val_riva;
            }
        }

        decimal _val_rft;
        public decimal val_rft { get { return _val_rft; } set { _val_rft = value; OnPropertyChanged("val_rft"); } }

        decimal _por_ica = 0;
        public decimal por_ica
        {
            get { return _por_ica; }
            set
            {
                _por_ica = value; OnPropertyChanged("por_ica");
                subtotal = _cantidad * _val_uni;
                val_rft = ((subtotal * por_rft) / 100);
                val_ica = ((subtotal * por_ica) / 100);
                val_riva = ((subtotal * por_riva) / 100);
                total = subtotal - val_rft - val_ica - val_riva;
            }
        }

        decimal _val_ica = 0;
        public decimal val_ica { get { return _val_ica; } set { _val_ica = value; OnPropertyChanged("val_ica"); } }

        decimal _por_riva = 0;
        public decimal por_riva
        {
            get { return _por_riva; }
            set
            {
                _por_riva = value; OnPropertyChanged("por_riva");
                subtotal = _cantidad * _val_uni;
                val_rft = ((subtotal * por_rft) / 100);
                val_ica = ((subtotal * por_ica) / 100);
                val_riva = ((subtotal * por_riva) / 100);
                total = subtotal - val_rft - val_ica - val_riva;
            }
        }

        decimal _val_riva = 0;
        public decimal val_riva { get { return _val_riva; } set { _val_riva = value; OnPropertyChanged("val_riva"); } }

        decimal _subtotal;
        public decimal subtotal
        {
            get { return _subtotal; }
            set
            {
                _subtotal = value; OnPropertyChanged("subtotal");
                val_rft = ((subtotal * por_rft) / 100);
                val_ica = ((subtotal * por_ica) / 100);
                val_riva = ((subtotal * por_riva) / 100);
                total = subtotal - val_rft - val_ica - val_riva;
            }
        }

        decimal _total;
        public decimal total { get { return _total; } set { _total = value; OnPropertyChanged("total"); } }


    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test2
{
    public partial class ListItem : UserControl
    {
        public ListItem()
        {
            InitializeComponent();
        }

        public string _Kod;

        public string Kod
        {
            get { return _Kod; }
            set { _Kod = value; label1.Text = value; }
        }

        public string _Stroka;
        public string Stroka
        {
            get { return _Stroka; }
            set {_Stroka = value; label2.Text = value; }
        }

        public string _Stroka2;
        public string Stroka2
        {
            get { return _Stroka2; }
            set { _Stroka2 = value; label3.Text = value; }
        }
    }
}

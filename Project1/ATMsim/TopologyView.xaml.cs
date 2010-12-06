using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuickGraph;

namespace AtmSim
{
    /// <summary>
    /// Interaction logic for TopologyView.xaml
    /// </summary>
    public partial class TopologyView : Window
    {
        private IBidirectionalGraph<object, IEdge<object>> _Graph;
        public IBidirectionalGraph<object, IEdge<object>> Graph
        {
            get { return _Graph; }
        }

        public TopologyView()
        {
            var g = new BidirectionalGraph<object, IEdge<object>>();
            g.AddVertex("A");
            g.AddVertex("B");
            g.AddVertex("C");
            g.AddEdge(new Edge<object>("A", "B"));
            g.AddEdge(new Edge<object>("A", "C"));
            g.AddEdge(new Edge<object>("B", "C"));
            this._Graph = g;
            InitializeComponent();
        }
    }
}

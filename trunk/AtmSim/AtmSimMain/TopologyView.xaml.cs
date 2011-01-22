﻿using System;
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
using System.ComponentModel;
using QuickGraph;
using GraphSharp;
using GraphSharp.Controls;

namespace AtmSim
{
    public class TopologyLayout : GraphLayout<Topology.Node, Topology.Link, Topology> { }

    /// <summary>
    /// Interaction logic for TopologyView.xaml
    /// </summary>
    public partial class TopologyView : Window, INotifyPropertyChanged
    {
        private Topology graph = new Topology();
        public Topology Graph
        {
            get { return graph; }
            set
            {
                graph = value;
                NotifyPropertyChanged("Graph");
            }
        }

        public TopologyView(Manager manager)
        {
            //var g = new BidirectionalGraph<object, IEdge<object>>();
            //foreach (string node in manager.GetElements())
            //{
            //    g.AddVertex(node);
            //}
            //foreach (Edge<int> connection in manager.GetLinks())
            //{
            //    g.AddEdge(new Edge<object>(connection.Source, connection.Target));
            //}
            this.graph = manager.Topology;
            this.DataContext = this;
            InitializeComponent();
        }

        new public void Show()
        {
            if (this.graph.Vertices.Count() == 0)
                this.Close();
            else
                base.Show();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}

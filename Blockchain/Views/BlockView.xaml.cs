using Blockchain.Miner;
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

namespace Blockchain.Views
{
    /// <summary>
    /// Interaktionslogik für BlockView.xaml
    /// </summary>
    public partial class BlockView : UserControl
    {
        public BlockView()
        {
            InitializeComponent();
        }

        private void Mine_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is Blockchain.Models.Block block)
            {
                var nonce = MinerService.GetGoalNonce(block);
                
                block.SetNonce(nonce);
            }
        }
    }
}

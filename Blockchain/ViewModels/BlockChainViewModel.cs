using Blockchain.Commands;
using Blockchain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blockchain.ViewModels
{
    public class BlockChainViewModel
    {
        public ObservableCollection<Block> Blocks { get; set; } = new ObservableCollection<Block>();





        public ICommand AddBlockCommand { get; init; }

        public BlockChainViewModel()
        {

            Blocks.Add(new Block { Data = "Hallo Welt", Nonce = 1, Number = 1, PreviousHash = "000000000000000000000000000000000000000000000000000000000000000" });

            AddBlockCommand = new RelayCommand(AddBlockExecute);
        }

        private void AddBlockExecute(object obj)
        {
            var newBlock = new Block { Number = Blocks.Count + 1, PreviousHash = Blocks[Blocks.Count - 1].Hash };
            newBlock.PropertyChanged += ReloadBlock;
            Blocks.Add(newBlock);
        }

        private void ReloadBlock(object sender, PropertyChangedEventArgs e)
        {
            for (int i = 0; i < Blocks.Count; i++)
            {
                Blocks[i].PropertyChanged -= ReloadBlock;
            }

            for (int i = 0; i < Blocks.Count; i++)
            {
               

                if (i == 0)
                {
                    Blocks[i].SetHash();
                }
                else
                {
                    Blocks[i].PreviousHash = Blocks[i - 1].Hash;
                    Blocks[i].SetHash();
                }
            }


            for (int i = 0; i < Blocks.Count; i++)
            {
                Blocks[i].PropertyChanged += ReloadBlock;                
            }
        }
    }
}

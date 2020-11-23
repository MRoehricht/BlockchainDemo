using Blockchain.Miner;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Blockchain.Models
{
    public class Block : INotifyPropertyChanged
    {
        public int Number { get; set; }
        public int Nonce { get; set; }
        public string PreviousHash { get; set; }
        public string Hash { get; set; }
        private string data;
        public string Data
        {
            get { return data; }
            set
            {
                data = value;
                SetHash();
            }
        }
        public string Goal { get; }

        public bool IsBlockValid
        {
            get { return PreviousHash != null && PreviousHash.StartsWith(Goal) && Hash != null && Hash.StartsWith(Goal); }
        }

        public Block()
        {
            Goal = "0000";
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public byte[] GetBlockData()
        {
            string data = Number.ToString();
            data += Nonce.ToString();
            data += PreviousHash?.ToString();
            data += Data?.ToString();

            return Encoding.ASCII.GetBytes(data);
        }

        public void SetNonce(int nonce)
        {
            this.Nonce = nonce;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nonce)));
            SetHash();
        }

        public void SetHash()
        {
            Hash = MinerService.GetHash(this);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Hash)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsBlockValid)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PreviousHash)));
        }
    }
}

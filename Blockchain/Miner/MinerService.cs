using Blockchain.Models;
using System.Security.Cryptography;
using System.Text;

namespace Blockchain.Miner
{
    public static class MinerService
    {
        public static int GetGoalNonce(Block block)
        {
            string hash;
            block.Nonce = 0;

            using (SHA256 sHA256 = SHA256.Create())
            {
                do
                {
                    block.Nonce++;
                    var bytes = sHA256.ComputeHash(block.GetBlockData());
                    hash = ConvertByteArray(bytes);


                } while (!hash.StartsWith(block.Goal));
            }

            return block.Nonce;
        }

        public static string GetHash(Block block)
        {
            string hash;

            using (SHA256 sHA256 = SHA256.Create())
            {
                var bytes = sHA256.ComputeHash(block.GetBlockData());
                hash = ConvertByteArray(bytes);
            }

            return hash;
        }

        /// <summary>
        /// A method for displaying the generated hash in a legible manner.
        /// </summary>
        /// <param name="array">Hash DataArry</param>
        /// <returns></returns>
        public static string ConvertByteArray(byte[] array)
        {
            string output = string.Empty;

            for (int i = 0; i < array.Length; i++)
            {
                output += ($"{array[i]:X2}");

                if ((i % 4) == 3)
                {
                    output += (" ");
                }
            }

            return output;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachine.Models
{
    public class CoinBag
    {
        private Dictionary<int, int> _Bag;

        CoinBag()
        {
            _Bag = new Dictionary<int, int>(4);
            _Bag[1] = 0;
            _Bag[2] = 0;
            _Bag[5] = 0;
            _Bag[10] = 0;
        }

        public void PutCoin(int value, int count)
        {
            if (!_Bag.ContainsKey(value))
            {
                throw new Exception("Coin value not exist");
            }
            _Bag[value] += count;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace VendingMachine.Models
{
    public class CoinBag
    {
        private ConcurrentDictionary<int, int> _Bag;
        private int _LockedMoney;

        public CoinBag()
        {
            _Bag = new ConcurrentDictionary<int, int>();
            _Bag[1] = 0;
            _Bag[2] = 0;
            _Bag[5] = 0;
            _Bag[10] = 0;
            _LockedMoney = 0;
        }

        private int Summary()
        {
            int sum = 0;
            foreach(var coin in _Bag)
            {
                sum += coin.Key * coin.Value;
            }
            return sum;
        }

        public ConcurrentDictionary<int, int> GetBag()
        {
            return _Bag;
        }

        public void PutCoin(int value, int count)
        {
            if (!_Bag.ContainsKey(value))
            {
                throw new Exception("Coin value not exist");
            }
            _Bag[value] += count;
            _LockedMoney += value * count;
        }

        public bool isEnough(Drink Drink)
        {
            if (Summary() >= Drink.Price)
            {
                return true;
            }
            return false;
        }

        public bool IsThereAChange(int Price)
        {
            int lockedMoney = _LockedMoney - Price;
            if(lockedMoney <= 0)
            {
                return false;
            }
            List<int> lockedCoins = new List<int>();
            foreach(var coin in _Bag.Reverse())
            {
                while (_Bag[coin.Key] > 0 && coin.Key <= lockedMoney && lockedMoney >= 0)
                {
                    lockedMoney -= coin.Key;
                    _Bag[coin.Key]--;
                    lockedCoins.Add(coin.Key);
                }
            }
            if(lockedMoney > 0)
            {
                return false;
            }
            lockedCoins.ForEach(coin =>
            {
                _Bag[coin]++;
            });
            return true;
        }

        public Dictionary<int, int> getChange()
        {
            int change = _LockedMoney;
            var changeBag = new Dictionary<int, int>();
            if (change >= 0)
            {
                foreach (var coin in _Bag.Reverse())
                {
                    while (_Bag[coin.Key] > 0 && coin.Key <= change && change >= 0)
                    {
                        change -= coin.Key;
                        _Bag[coin.Key]--;
                        if (!changeBag.ContainsKey(coin.Key))
                        {
                            changeBag[coin.Key] = 0;
                        }
                        changeBag[coin.Key]++;
                    }
                }
            }
            _LockedMoney = 0;
            return changeBag;
        }

        public void calcChange(Drink Drink)
        {
            int change = _LockedMoney - Drink.Price;
            if (change >= 0)
            {
                _LockedMoney = 0;
                foreach (var coin in _Bag.Reverse())
                {
                    while (_Bag[coin.Key] > 0 && coin.Key <= change && change >= 0)
                    {
                        change -= coin.Key;
                        _LockedMoney += coin.Key;
                    }
                }
            }
        }

        public int GetLockedMoney()
        {
            return _LockedMoney;
        }
    }
}

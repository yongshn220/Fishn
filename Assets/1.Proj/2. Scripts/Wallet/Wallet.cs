using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fishn
{
    public static class Wallet
    {
        private static int _coral;
        public static int coral { get{return _coral;}}

        public static void SetCoral(int coral)
        {
            if (coral >= 0)
            {
                _coral = coral;
            }
        }

        public static bool HasEnough(int amount)
        {
            return (coral >= amount);
        }

        public static bool Use(int amount)
        {
            if (HasEnough(amount))
            {
                _coral = _coral - amount;
                return true;
            }
            return false;
        }

        public static void Earn(int amount)
        {
            if (amount > 0)
            {
                _coral += amount;
            }
        }
    }
}

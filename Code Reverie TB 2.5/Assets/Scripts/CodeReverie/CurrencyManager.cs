using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeReverie
{
    [DefaultExecutionOrder(-115)]
    public class CurrencyManager : ManagerSingleton<CurrencyManager>
    {
        public Dictionary<CurrencyType, int> currencies = new Dictionary<CurrencyType, int>();


        protected override void Awake()
        {
            base.Awake();



            if (ES3.KeyExists("currencies"))
            {
                currencies = ES3.Load<Dictionary<CurrencyType, int>>("currencies");
            }
            else
            {
                currencies = new Dictionary<CurrencyType, int>();
            
                Array currency = Enum.GetValues(typeof(CurrencyType));
            
                for (int i = 0; i < currency.Length; i++)
                {
                    if (!currencies.ContainsKey((CurrencyType)currency.GetValue(i)))
                    {
                        currencies.Add((CurrencyType)currency.GetValue(i), 0);

                        if ((CurrencyType)currency.GetValue(i) == CurrencyType.Lumies)
                        {
                            currencies[CurrencyType.Lumies] = 500000000;
                        }
                    
                    }
                }

            }
            
        }
        
        
        public bool CheckIfEnoughCurrency(CurrencyType currency, int amount)
        {

            if (GetCurrency(currency) >= amount)
            {
                return true;
            }
            
            return false;
        }
        
        
        public int GetCurrency(CurrencyType currency)
        {
            return currencies[currency];
        }
        
        
        public void UpdateCurrency(CurrencyType currency, int amount)
        {
            currencies[currency] += amount;
        }
        
        public void AddCurrency(CurrencyType currency, int amount)
        {
            currencies[currency] += amount;
        }
        
        
        public void RemoveCurrency(CurrencyType currency, int amount)
        {
            currencies[currency] -= amount;
        }



        public void Save()
        {
            ES3.Save("currencies", currencies);
        }

        public void Load()
        {
            
        }

        private void OnApplicationQuit()
        {
            Save();
        }
    }
}
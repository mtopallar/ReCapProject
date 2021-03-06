using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key);//Cast<> burada gerekmez.
        object Get(string key);//Bunu kullandığımızda sonradan Cast<> yapmak gerekir.
        void Add(string key, object value, int duration);
        bool IsAdd(string key);
        void Remove(string key);
        void RemoveByPattern(string pattern); //başı sonu önemli değil içinde x (mesela get) olanlar

    }
}

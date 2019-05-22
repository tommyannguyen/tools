﻿using System;

namespace Nca.Library.Interfaces
{
    public class CacheItem<T>
    {
        public CacheItem(T value, TimeSpan timeout)
        {
            Value = value;
            TimeOut = timeout;
        }
        public T Value { get; }
        public TimeSpan TimeOut { get; }
    }
}

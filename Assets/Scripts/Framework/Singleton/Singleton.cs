namespace Framework.Singleton
{
    using System;

    public class Singleton<T> where T : class, new()
    {
        private Singleton() { }

        private static readonly Lazy<T> instance = new Lazy<T>(() => new T());

        public static T Instance => instance.Value;
    }
}
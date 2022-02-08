using System;
using System.Collections.Generic;
using System.Text;

namespace FirstProjectCS
{
    public class SingletonCreator
    {
        private static readonly Dictionary<Type,object> instances = new Dictionary<Type,object>();

        public static object GetInstance(Type type)
        {
            if (instances.ContainsKey(type))
            {
                return instances[type];
            }
            else
            {
                object instance = Activator.CreateInstance(type,nonPublic:true);
                instances.Add(type, instance);
                return instance;
            }
        }

    }
}

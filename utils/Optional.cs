using System;
using System.Collections.Generic;
using System.Text;

namespace FirstProjectCS
{
    public class Optional 
    {
        private object _value;
        public bool IsPresent { get; private set; } = false;

        private Optional() { }

        public static Optional Empty()
        {
            return new Optional();
        }

        public static Optional Of (object theValue)
        {
            Optional retVal = new Optional();
            retVal.Set(theValue);
            return retVal;
        }

        private void Set(object theValue)
        {
            this._value = theValue;
            IsPresent = true;
        }
        public object Get()
        {
            return this._value;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace cs8_con_ObsoletePropertyAccessors
{
    class Class1
    {
        private int _ID;
        public int ID
        {
            get
            {
                return this._ID;
            }
            [Obsolete("No longer available. Please use Overloaded Constructor", error: false)]
            set
            {
                this._ID = value;
            }
        }

        public Class1()
        {

        }

        public Class1(int id)
        {

#pragma warning disable CS0618 // Type or member is obsolete
            ID = id;
#pragma warning restore CS0618 // Type or member is obsolete

            _ID = id;
        }
    }
}

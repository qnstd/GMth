namespace cngraphi.gmth
{
    /// <summary>
    /// Uint4 数据结构
    /// <para>作者：强辰</para>
    /// </summary>
    public struct Uint4
    {
        public uint x;
        public uint y;
        public uint z;
        public uint w;


        public Uint4(int v)
        {
            x = (uint)v;
            y = (uint)v;
            z = (uint)v;
            w = (uint)v;
        }

        public Uint4(uint v)
        {
            x = v;
            y = v;
            z = v;
            w = v;
        }

        public Uint4(uint x, uint y, uint z, uint w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }



        #region 运算符重载

        static public implicit operator Uint4(uint v) { return new Uint4(v); }
        static public explicit operator Uint4(int v) { return new Uint4(v); }

        // & 
        static public Uint4 operator &(Uint4 lhs, Uint4 rhs) { return new Uint4(lhs.x & rhs.x, lhs.y & rhs.y, lhs.z & rhs.z, lhs.w & rhs.w); }
        static public Uint4 operator &(Uint4 lhs, uint rhs) { return new Uint4(lhs.x & rhs, lhs.y & rhs, lhs.z & rhs, lhs.w & rhs); }
        static public Uint4 operator &(uint lhs, Uint4 rhs) { return new Uint4(lhs & rhs.x, lhs & rhs.y, lhs & rhs.z, lhs & rhs.w); }


        // ^
        static public Uint4 operator ^(Uint4 lhs, Uint4 rhs) { return new Uint4(lhs.x ^ rhs.x, lhs.y ^ rhs.y, lhs.z ^ rhs.z, lhs.w ^ rhs.w); }
        static public Uint4 operator ^(Uint4 lhs, uint rhs) { return new Uint4(lhs.x ^ rhs, lhs.y ^ rhs, lhs.z ^ rhs, lhs.w ^ rhs); }
        static public Uint4 operator ^(uint lhs, Uint4 rhs) { return new Uint4(lhs ^ rhs.x, lhs ^ rhs.y, lhs ^ rhs.z, lhs ^ rhs.w); }


        // ~
        static public Uint4 operator ~(Uint4 val) { return new Uint4(~val.x, ~val.y, ~val.z, ~val.w); }


        // |
        static public Uint4 operator |(Uint4 lhs, Uint4 rhs) { return new Uint4(lhs.x | rhs.x, lhs.y | rhs.y, lhs.z | rhs.z, lhs.w | rhs.w); }
        static public Uint4 operator |(Uint4 lhs, uint rhs) { return new Uint4(lhs.x | rhs, lhs.y | rhs, lhs.z | rhs, lhs.w | rhs); }
        static public Uint4 operator |(uint lhs, Uint4 rhs) { return new Uint4(lhs | rhs.x, lhs | rhs.y, lhs | rhs.z, lhs | rhs.w); }


        #endregion
    }

}
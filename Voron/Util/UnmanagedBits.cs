﻿using System;

namespace Voron.Util
{
    public unsafe class UnmanagedBits
    {
        private readonly int* _ptr;
        private readonly long _size;
        private readonly UnmanagedBits _pages;

        public UnmanagedBits(byte* ptr, long size, UnmanagedBits pages)
        {
            _ptr = (int*)ptr;
            _size = size;
            _pages = pages;
        }

	    public long Size
	    {
			get { return _size; }
	    }

        public bool this[int pos]
        {
            get
            {
                if(pos < 0 || pos >= _size)
                    throw new ArgumentOutOfRangeException("pos");

                return (_ptr[pos >> 5] & (1 << (pos & 31))) != 0;
            }
            set
            {
                if (pos < 0 || pos >= _size)
                    throw new ArgumentOutOfRangeException("pos");

                if (_pages != null)
                    _pages[pos >> 12] = true;

                if (value)
                    _ptr[pos >> 5] |= (1 << (pos & 31));
                else
                    _ptr[pos >> 5] &= ~(1 << (pos & 31));
            }
        }

		public void Clear()
		{
			for (int i = 0; i < _size; i++)
			{
				this[i] = false;
			}
		}
    }
}
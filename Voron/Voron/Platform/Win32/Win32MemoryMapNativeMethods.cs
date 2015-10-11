using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Voron.Platform.Win32
{
    public static unsafe class Win32MemoryMapNativeMethods
    {
        public static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        // ReSharper disable once InconsistentNaming - Win32
        public struct WIN32_MEMORY_RANGE_ENTRY
        {
            public void* VirtualAddress;
            public IntPtr NumberOfBytes;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public extern static bool PrefetchVirtualMemory(IntPtr hProcess, UIntPtr NumberOfEntries,
            WIN32_MEMORY_RANGE_ENTRY* VirtualAddresses, ulong Flags);

        [Flags]
        public enum FileMapProtection : uint
        {
            PageReadonly = 0x02,
            PageReadWrite = 0x04,
            PageWriteCopy = 0x08,
            PageExecuteRead = 0x20,
            PageExecuteReadWrite = 0x40,
            SectionCommit = 0x8000000,
            SectionImage = 0x1000000,
            SectionNoCache = 0x10000000,
            SectionReserve = 0x4000000,
        }



        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFileMapping(
            IntPtr hFile,
            IntPtr lpFileMappingAttributes,
            FileMapProtection flProtect,
            uint dwMaximumSizeHigh,
            uint dwMaximumSizeLow,
            [MarshalAs(UnmanagedType.LPStr)] string lpName);

        // ReSharper disable UnusedMember.Local
        [Flags]
        public enum NativeFileMapAccessType : uint
        {
            Copy = 0x01,
            Write = 0x02,
            Read = 0x04,
            AllAccess = 0x08,
            Execute = 0x20,
        }
        // ReSharper restore UnusedMember.Local

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool UnmapViewOfFile(byte* lpBaseAddress);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern byte* MapViewOfFileEx(IntPtr hFileMappingObject,
            NativeFileMapAccessType dwDesiredAccess,
            uint dwFileOffsetHigh,
            uint dwFileOffsetLow,
            UIntPtr dwNumberOfBytesToMap,
            byte* lpBaseAddress);


        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FlushFileBuffers(SafeFileHandle hFile);


        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FlushViewOfFile(byte* lpBaseAddress, IntPtr dwNumberOfBytesToFlush);
    }
}
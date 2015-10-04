using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


[Guid ("c33fa593-ee30-41f4-9d01-f1c8d105e633")]
[InterfaceType (ComInterfaceType.InterfaceIsIUnknown)]
[ComImport]
interface ITest
{
	// PreserveSig test
	[MethodImpl (MethodImplOptions.InternalCall | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Runtime)]
	int Test_Preserved (out int retVal);

	// non-PreserveSig test
	[MethodImpl (MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	int Test ();

	// test that HRESULTs != S_OK are actually raising a COMException
	[MethodImpl (MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
	void Test_Fail ();
}


class Program
{
	[DllImport ("libtest")]
	[return: MarshalAs (UnmanagedType.Interface)]
	static extern ITest getTestImpl ();

	static void Main ()
	{
		var obj = getTestImpl ();
		int res;
		obj.Test_Preserved (out res);
		Console.WriteLine (res == 42);
		Console.WriteLine (obj.Test() == 17);
		try {
			obj.Test_Fail ();
			Console.WriteLine (false);
		} catch (COMException) {
			Console.WriteLine (true);
		}
	}
}

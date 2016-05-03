using System;
using System.Collections.Generic;

class StackTraceDumper {

	public static void Main ()
	{
		try {
			throw new Exception ("Stacktrace with 1 frame");
		} catch (Exception e) {
			Console.WriteLine (e);
			Console.WriteLine ("Stacktrace:");
			Console.WriteLine (new System.Diagnostics.StackTrace(e));
		}

		Catch (() => {throw new Exception ("Stacktrace with 2 frames");});

		Catch (() => ThrowException ("Stacktrace with 3 frames", 1));

		Catch (() => ThrowException ("Stacktrace with 4 frames", 2));

		Catch (() => {
			var message = "Stack frame with method overload using ref parameter";
			ThrowException (ref message);
		});

		Catch (() => {
			int i;
			ThrowException ("Stack frame with method overload using out parameter", out i);
		});

		Catch (() => ThrowExceptionGeneric<double> ("Stack frame with 1 generic parameter"));

		Catch (() => ThrowExceptionGeneric<double,string> ("Stack frame with 2 generic parameters"));

		Catch (() => ThrowExceptionGeneric (12));

		Catch (() => InnerClass.ThrowException ("Stack trace with inner class"));

		Catch (() => InnerGenericClass<string>.ThrowException ("Stack trace with inner generic class"));

		Catch (() => InnerGenericClass<string>.ThrowException ("Stack trace with inner generic class and method generic parameter", "string"));

		Catch (() => InnerGenericClass<string>.ThrowException<string> ("Stack trace with inner generic class and generic overload", "string"));

		Catch (() => InnerGenericClass<string>.InnerInnerGenericClass<int>.ThrowException ("Stack trace with 2 inner generic class and generic overload"));

		Catch (() => InnerGenericClass<int>.InnerInnerGenericClass<string>.ThrowException ("Stack trace with 2 inner generic class and generic overload"));
	}

	public static void Catch (Action action)
	{
		try {
			action ();
		} catch (Exception e) {
			Console.WriteLine();
			Console.WriteLine (e);
			Console.WriteLine ("Stacktrace:");
			Console.WriteLine (new System.Diagnostics.StackTrace (e));
		}
	}

	public static void ThrowException (string message)
	{
		throw new Exception (message);
	}

	public static void ThrowException (ref string message)
	{
		throw new Exception (message);
	}

	public static void ThrowException (string message, int i)
	{
		if (i > 1)
			ThrowException (message, --i);

		throw new Exception (message);
	}

	public static void ThrowException (string message, out int o)
	{
		throw new Exception (message);
	}

	public static void ThrowExceptionGeneric<T> (string message)
	{
		throw new Exception (message);
	}

	public static void ThrowExceptionGeneric<T> (T a1)
	{
		throw new Exception ("Stack frame with generic method overload");
	}

	public static void ThrowExceptionGeneric<T> (List<string> a1)
	{
		throw new Exception ("Stack frame with generic method overload");
	}

	public static void ThrowExceptionGeneric<T> (List<T> a1)
	{
		throw new Exception ("Stack frame with generic method overload");
	}

	public static void ThrowExceptionGeneric<T1,T2> (string message)
	{
		throw new Exception (message);
	}

	class InnerClass {
		public static void ThrowException (string message)
		{
			throw new Exception (message);
		}
	}

	class InnerGenericClass<T> {
		public static void ThrowException (string message)
		{
			throw new Exception (message);
		}

		public static void ThrowException (string message, T arg)
		{
			Console.WriteLine ("Generic to string:" + arg.ToString());
			throw new Exception (message);
		}

		public static void ThrowException<T1> (string message, T1 arg)
		{
			throw new Exception (message);
		}

		public class InnerInnerGenericClass<T2> {
			public static void ThrowException (T message)
			{
				throw new Exception (message as string);
			}

			public static void ThrowException (T2 message)
			{
				throw new Exception (message as string);
			}
		}
	}
}
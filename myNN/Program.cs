using System;
using NeuralNetwork;

namespace myNN
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			while (true) {
				Network n = new Network ();
				n.train ();
				n.test ();
				Console.ReadKey ();
			}
			return;
		}
	}
}
	

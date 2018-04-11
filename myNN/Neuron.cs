using System;

namespace NeuralNetwork
{
	public abstract class Neuron
	{
		protected ParameterList<double> weights;
		protected double bais;
		protected string name;
		public double errorToBP = 0;

		public double getWeightAtIndex(int index){
			return this.weights [index];
		}

		public abstract void adjustWeights();

		public void printWeights(){
			Console.WriteLine ("\n\n============================================");
			Console.WriteLine ("Name : " + this.name);
			Console.WriteLine ("--------------------------------------------");
			Console.WriteLine ("Index \t | \t Weight");
			Console.WriteLine ("--------------------------------------------");
			int counter = 0;
			foreach (double item in this.weights.List) {
				Console.WriteLine (counter.ToString () + " \t | \t " + item.ToString ());
				counter++;
			}
			Console.WriteLine ("Bias \t | \t " + this.bais.ToString ());
		}

		public void randomizeWeights(){
			Random r = new Random (Guid.NewGuid().GetHashCode());
			for (int c = 0; c < this.weights.Length; c++) {
				weights [c] = r.NextDouble ();
			}
			bais = r.NextDouble ();
		}
	}
}


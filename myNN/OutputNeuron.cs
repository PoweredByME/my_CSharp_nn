using System;
using NeuralNetwork;

namespace NeuralNetwork
{
	

	public class OutputNeuron : Neuron{
		ParameterList<double> input;
		double error = 	0;
		double learningRate = 1;
		double summation = 0;
		bool expectedValueCalled = false;
		public double Error{
			get { return error;}
			set { this.error = value;}
		}

		public double Output{
			get { 
				summation = ParameterList<double>.vectorDot (weights, input) + bais;
				return NeuronActivation.Function(summation);
			}
		}

		public OutputNeuron(){
			weights = new ParameterList<double> ();
			input = new ParameterList<double> ();
			bais = 0;
			learningRate = 1;
			name = "outputNeuron";
		}
		public OutputNeuron(ParameterList<double> weights, string name = "outputNeuron", double bais = 0, double learningRate = 1){
			this.weights = weights;
			input = new ParameterList<double> (weights.Length);
			this.bais = bais;
			this.learningRate = learningRate;
			this.name = name;
		}
		public OutputNeuron(int numberOfInputs, string name = "outputNeuron" , double learningRate = 1){
			weights = new ParameterList<double> (numberOfInputs);
			input = new ParameterList<double> (numberOfInputs);
			bais = 0;
			this.learningRate = learningRate;
			this.name = name;
		}


		/// <summary>
		/// This function adjusts the weights of the Neuron.
		/// This function must be called after the expected value has been
		/// set.
		/// </summary>
		public override void adjustWeights(){
			if (expectedValueCalled != true) {
				throw new Exception ("Expeected Value not given");
			}
			errorToBP = this.error * NeuronActivation.Derivative (summation);
			double e = this.learningRate * errorToBP;		

			for (int c = 0; c < this.weights.Length; c++) {
				this.weights [c] -= this.input [c] * e;
			}
			bais -= e;
			expectedValueCalled = false;
		}

		/// <summary>
		/// This function adjusts the weights of the Neuron after getting the
		/// value which is expected out of the neuron
		/// </summary>
		public void adjustWeights(double expectedValue){
			if (expectedValueCalled != true) {
				this.expectedValue (expectedValue);
			}

			errorToBP = this.error * NeuronActivation.Derivative (summation);
			double e = this.learningRate * errorToBP;		

			for (int c = 0; c < this.weights.Length; c++) {
				this.weights [c] -= this.input [c] * e;
			}
			bais -= e;
			expectedValueCalled = false;
		}

		/// <summary>
		/// This function gets the input of the neuron
		/// </summary>
		/// <param name="input">Input List.</param>
		public void setInput(ParameterList<double> input){
			if (this.input.Length != input.Length) {
				throw new Exception ("Input length do not match.");
			}
			this.input = input;
		}

		/// <summary>
		/// This function gets the expected value, calculates the neurons output 
		/// and Error. Then it returns the error.
		/// </summary>
		/// <returns>The Error.</returns>
		/// <param name="expectedValue">Expected value.</param>
		public double expectedValue(double expectedValue){
			this.Error = - expectedValue + this.Output;
			expectedValueCalled = true;
			return this.Error;
		}

		/// <summary>
		/// This function gets the expected value, calculates the neurons output 
		/// and Error. Then it returns the error. It also returns the value of the 
		/// output via the ref variable.
		/// </summary>
		/// <returns>The Error.</returns>
		/// <param name="expectedValue">Expected value.</param>
		public double expectedValue(double expectedValue, ref double output){
			output = this.Output;
			this.Error = - expectedValue + output;
			expectedValueCalled = true;
			return this.Error;
		}
	}

}


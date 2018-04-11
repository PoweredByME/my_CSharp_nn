using System;
using System.Collections.Generic;

namespace NeuralNetwork
{
	public class HiddenNeuron : Neuron
	{
		ParameterList<double> input;
		double learningRate = 1;
		double summation = 0;
		bool ONHBS = false;
		Dictionary<Neuron, int> nextNeuronAndInputIndexInNeuron = new Dictionary<Neuron, int>();



		public HiddenNeuron(){
			weights = new ParameterList<double> ();
			input = new ParameterList<double> ();
			bais = 0;
			learningRate = 1;
			name = "hiddenNeuron";
		}
		public HiddenNeuron(ParameterList<double> weights, string name = "hiddenNeuron", double bais = 0, double learningRate = 1){
			this.weights = weights;
			input = new ParameterList<double> (weights.Length);
			this.bais = bais;
			this.learningRate = learningRate;
			this.name = name;
		}
		public HiddenNeuron(int numberOfInputs, string name = "hiddenNeuron" , double learningRate = 1){
			weights = new ParameterList<double> (numberOfInputs);
			input = new ParameterList<double> (numberOfInputs);
			bais = 0;
			this.learningRate = learningRate;
			this.name = name;
		}

		public HiddenNeuron(int numberOfInputs, Neuron[] neuronsAtOutput, string name = "hiddenNeuron" , double learningRate = 1){
			weights = new ParameterList<double> (numberOfInputs);
			input = new ParameterList<double> (numberOfInputs);
			bais = 0;
			this.learningRate = learningRate;
			this.name = name;
		}

		public double Output(int inputIndexInNextNeuron, Neuron nextNeuron){
			if (!ONHBS) {
				if (!nextNeuronAndInputIndexInNeuron.ContainsKey (nextNeuron)) {
					nextNeuronAndInputIndexInNeuron.Add (nextNeuron, inputIndexInNextNeuron);
				} else {
					throw new Exception ("This neuron already exist in the next neuron");
				}
			}
			summation = ParameterList<double>.vectorDot (weights, input) + bais;
			return NeuronActivation.Function(summation);
		}

		public double calcErrorProjectedFromNextNeurons(){
			double solution = 0;
			foreach(var pair in nextNeuronAndInputIndexInNeuron){
				solution += pair.Key.getWeightAtIndex (pair.Value) * pair.Key.errorToBP;
			}
			return solution;
		}

		public void outputNeuronsHaveBeenSet(){
			ONHBS = true;
		}

		public override void adjustWeights(){
			this.errorToBP = calcErrorProjectedFromNextNeurons() * NeuronActivation.Derivative (summation);
			double e = this.errorToBP * learningRate; 
				for (int c = 0; c < this.weights.Length; c++) {
				this.weights [c] -= this.input [c] * e;
			}
			bais -= e;
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

	}
}


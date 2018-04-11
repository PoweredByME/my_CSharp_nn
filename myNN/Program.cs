﻿using System;
using NeuralNetwork;

namespace myNN
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			double[,] input = new double[,] {
				{ 0, 0 },
				{ 0, 1 },
				{ 1, 0 },
				{ 1, 1 },
			};

			double[] output = new double[] {0 , 1, 0, 1 };

			double lr = 1;
			OutputNeuron o = new OutputNeuron (2,"outputNeuron", lr);
			HiddenNeuron[] h = new HiddenNeuron[] { new HiddenNeuron (2, "hn0",lr), new HiddenNeuron (2, "hn1",lr) };
			o.randomizeWeights ();
			foreach (var item in h) {
				item.randomizeWeights ();
			}
			int epochSize = 2000;
			for (int c = 0; c < epochSize; c++) {
				for(int i = 0; i < 4; i++) {
					h [0].setInput (new ParameterList<double> (new double[] { input [i, 0], input [i, 1] }));
					h [1].setInput (new ParameterList<double> (new double[] { input [i, 0], input [i, 1] }));
					o.setInput (new ParameterList<double> (new double[] {h[0].Output(0,(Neuron) o), h[1].Output(1,(Neuron) o)}));
					foreach (var item in h) {
						item.outputNeuronsHaveBeenSet ();
					}
					o.adjustWeights (output [i]);
					foreach (var item in h) {
						item.adjustWeights ();
					}
				}
				/*o.printWeights ();
				foreach (var item in h) {
					item.printWeights ();
				}*/
			}

			Console.WriteLine ("\n\n=================================================");
			Console.WriteLine ("Prediction");
			Console.WriteLine ("=================================================");
			for(int i = 0; i < 4; i++) {
				h [0].setInput (new ParameterList<double> (new double[] { input [i, 0], input [i, 1] }));
				h [1].setInput (new ParameterList<double> (new double[] { input [i, 0], input [i, 1] }));
				o.setInput (new ParameterList<double> (new double[] {h[0].Output(0,(Neuron) o), h[1].Output(1,(Neuron) o)}));
				Console.WriteLine (o.Output.ToString ());
			}

		}
	}
}

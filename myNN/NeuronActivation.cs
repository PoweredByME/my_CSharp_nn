using System;

namespace NeuralNetwork
{
	public static class NeuronActivation
	{
		public static double Function(double x){
			return sigmoid(x);
		}

		public static double Derivative(double x){
			return sigmoidDerivative(x);
		}

		static double sigmoid(double x){
			return 1.0 / (1.0 + Math.Exp(-x));
		}

		static double sigmoidDerivative(double x){
			x = sigmoid(x);
			return x * (1 - x);	
		}
	}
}


using System;

namespace NeuralNetwork
{
	public class Network
	{
		double lr = 1;
		OutputNeuron[] o ;
		HiddenNeuron[] h ;
		int epoch = 2000;

		double[,] input = new double[,] {
			{ 0, 0 },
			{ 0, 1 },
			{ 1, 0 },
			{ 1, 1 },
		};
		double[] output = new double[] { 1, 0, 0, 1 };



		public Network (){
			// init neurons
			o = denseOutputLayer (1, 10, lr);
			h = denseHiddenLayer (10, 2, lr);

			// randomizing the weights
			randomizeWeights ((Neuron[])o);
			randomizeWeights ((Neuron[])h);

			// printing weights
			//printWeights ((Neuron[])o);
			//printWeights ((Neuron[])h);
			initModel ();

		}
		public void train(){
			for (int c = 0; c < epoch; c++) {
				for (int i = 0; i < 4; i++) {
					Model (new double[] { input [i, 0], input [i, 1] }, new double[] { output [i] });
				}
			}
		}
		public void test(){
			Console.WriteLine ("\n\n=================================================");
			Console.WriteLine ("Prediction");
			Console.WriteLine ("=================================================");
			for(int i = 0; i < 4; i++) {
				Model (new double[] { input [i, 0], input [i, 1] }, new double[] { output [i] }, true);
			}	

		}

		void initModel(){
			foreach (var item in h) {
				item.setInput (getInputList(2));
			}
			o[0].setInput (getInputList(h, o[0]));
			lockLayer (h);
		}

		public void Model (double[] inputVec, double[] outputVec, bool test = false)
		{
			foreach (var item in h) {
				item.setInput (new ParameterList<double> (inputVec));
			}
			o [0].setInput (getInputList (h, o [0]));


			if (!test) {
				o [0].adjustWeights (outputVec [0]);
				foreach (var item in h) {
					item.adjustWeights ();
				}
				//printWeights ((Neuron[])o);
				//printWeights ((Neuron[])h);


			} else {
				Console.WriteLine ("Test : " + o [0].Output.ToString ());
			}
		}
			
























		/*UTILS*/

		void randomizeWeights(Neuron[] n){
			foreach (var item in n) {
				item.randomizeWeights ();
			}
		}

		void printWeights(Neuron[] n){
			foreach (var item in n) {
				item.printWeights ();
			}
		}

		OutputNeuron[] denseOutputLayer(int numberOfNeurons, int numberOfInputOfEachNeuron, double learningRate = 1,string layerName = "outputNeuron"){
			OutputNeuron[] on = new OutputNeuron[numberOfNeurons];
			for (int c = 0; c < numberOfNeurons; c++) {
				on [c] = new OutputNeuron (numberOfInputOfEachNeuron, layerName + c.ToString (), learningRate);
			}
			return on;
		}

		HiddenNeuron[] denseHiddenLayer(int numberOfNeurons, int numberOfInputOfEachNeuron, double learningRate = 1,string layerName = "hiddenNeuron"){
			HiddenNeuron[] on = new HiddenNeuron[numberOfNeurons];
			for (int c = 0; c < numberOfNeurons; c++) {
				on [c] = new HiddenNeuron (numberOfInputOfEachNeuron, layerName + c.ToString (), learningRate);
			}
			return on;
		}

		ParameterList<double> getInputList(int length){
			return new ParameterList<double> (length); 
		}

		ParameterList<double> getInputList(HiddenNeuron[] hiddenLayer, OutputNeuron nextNeuron){
			ParameterList<double> list = new ParameterList<double> (hiddenLayer.Length);
			for (int c = 0; c < list.Length; c++) {
				list [c] = hiddenLayer [c].Output (c, nextNeuron);
			}
			return list;
		}


		ParameterList<double> getInputList(HiddenNeuron[] hiddenLayer, HiddenNeuron nextNeuron){
			ParameterList<double> list = new ParameterList<double> (hiddenLayer.Length);
			for (int c = 0; c < list.Length; c++) {
				list [c] = hiddenLayer [c].Output (c, nextNeuron);
			}
			return list;
		}

		void lockLayer(HiddenNeuron[] h){
			foreach (var item in h) {
				item.outputNeuronsHaveBeenSet ();
			}
		}
	}
}


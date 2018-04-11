using System;
using System.Linq;

namespace NeuralNetwork
{
	public class ParameterList<T>{
		T[] list;
		int length = 0;
		public int Length{
			get{ return this.length;}
		}
		public T[] List{
			get { return this.list;}
		}
		public ParameterList(){}
		public ParameterList(T[] list){
			this.list = list;
			this.length = this.list.Length;
		}
		public void setNewList(T[] newList){
			this.list = newList;
			this.length = this.list.Length;
		}
		public void setList(T[] List){
			if (length == List.Length) {
				this.list = List;		
			} else {
				throw new Exception ("List size donot match");
			}
		}
		public ParameterList(int length){
			this.length = length;
			list = new T[this.length];
		}
		public T this[int index]{
			get{ return list[index];}
			set{ list [index] = value;}
		}

		public static double vectorDot(ParameterList<double> l1, ParameterList<double> l2){
			if (l1.Length != l2.Length) {
				throw new Exception ("Vector diamensions are not equal.");
			} else {
				double solution = 0; 
				for (int c = 0; c < l1.Length; c++) {
					solution += l1 [c] * l2 [c];
				}
				return solution;
			}
		}

	}
}


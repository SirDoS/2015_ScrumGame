using System;
	/// <summary>
	/// Fila com prioridade, necessário para implementar greedy.
	/// </summary>
public class PriorityQueue<T>
{
	Noooode first;
	Noooode last;
	private int quantia = 0;
	/// <summary>
	/// Quantia de elementos na fila
	/// </summary>
	/// <value>The count.</value>
	public int Count {
		get{return quantia; }
	}

	public PriorityQueue ()
	{	
	}

		/// <summary>
		/// Adiciona o elemento a fila e ordena de acordo com sua prioridade.
		/// </summary>
		/// <param name="elemento">Elemento.</param>
		/// <param name="prioridade">Prioridade.</param>
	public void Enqueue(T elemento, int prioridade){
		quantia++;
		if (first == null) {
			first = new Noooode (elemento, prioridade);
			last = first;
		} else {
			Noooode addme = new Noooode (elemento, prioridade);
			Noooode temp;
			//Modo Simples:
			temp = last;
			while (temp.proximo != null && temp.proximo.Prioridade > prioridade) {
				temp = temp.proximo;
			}
			if (temp.proximo == null) {
				temp.proximo = addme;
				addme.proximo = null;
				addme.anterior = temp;
				first = addme;
			} else {
				addme.proximo = temp.proximo;
				addme.anterior = temp;
				temp.proximo.anterior = addme;
				temp.proximo = addme;
			}
			/*
			int diffF = Math.Abs (prioridade - first.Prioridade);
			int diffL = Math.Abs (prioridade - last.Prioridade);
			if (diffF <= diffL) {
				temp = first;
				while (temp.anterior != null && temp.anterior.Prioridade <= prioridade) {
					temp = temp.anterior;
				}
				//Se prioridade do que esta sendo adicionado for mais prioritaria entao ele é o primeiro
				if (temp.Prioridade > prioridade) {
					temp.proximo = addme;
					addme.anterior = temp;
					first = addme;
					return;
				}
				//Caso contrário adicionar ele na posição adequada
				if (temp.anterior == null) {
					addme.proximo = temp;
					last = addme;
				} else {
					addme.anterior = temp.anterior;
					addme.proximo = temp;
					temp.anterior.proximo = addme;
					temp.anterior = addme;
				}
			} else {
				//Caso esteja mais proximo do fim.
				temp = last;

				while (temp.proximo != null &&  temp.proximo.Prioridade < prioridade) {
					temp = temp.proximo;
				}
				//Se prioridade do que esta sendo adicionado for menos prioritaria entao ele é o ultimo
				if (temp.Prioridade > prioridade) {
					temp.anterior = addme;
					addme.proximo = temp;
					last = addme;
					return;
				}
				addme.anterior = temp.anterior;
				addme.proximo = temp;
				if (temp.anterior != null) {
					temp.anterior.proximo = addme;
				}
				temp.anterior = addme;
			}*/
		}
	}

	/// <summary>
	/// Retira o elemento com maior prioridade da fila
	/// </summary>
	public T Dequeue(){
		if (first == null) {
			throw new Exception ("Fila Vazia: " + Count);
		}
		quantia--;
		Noooode temp = first;
		first = first.anterior;
		if (first == null) {
			last = null;
		} else {
			first.proximo = null;
		}
		return (T)temp.obj;
	}


		/// <summary>
		/// Representa os nodes desta fila
		/// </summary>
		private class Noooode{
			public Object  obj;
			public Noooode proximo;
			public Noooode anterior;
			public int Prioridade;

			public Noooode(Object _obj, int prio){
				obj = _obj;
				Prioridade = prio;
		}
	}
}
using UnityEngine;
using System.Collections;
using System;

//Asegurar que sea igual a la interface
public class Heap<T> where T : IHeapItem<T> {
	//template
	T[] items;
	int currentItemCount;

	//Restablecer el tamaño del heap
	public Heap(int maxHeapSize) {
		items = new T[maxHeapSize];
	}

	//Metodo para añadir items to the heap
	public void Add(T item) {
		item.HeapIndex = currentItemCount;
		items[currentItemCount] = item;
		SortUp(item);
		currentItemCount++;
	}

	//Metodo para remover el primer elemento del heap
	public T RemoveFirst() {
		T firstItem = items[0];
		currentItemCount--;
		items[0] = items[currentItemCount];
		items[0].HeapIndex = 0;
		SortDown(items[0]);
		return firstItem;
	}
	//Cambiar la prioridad del item.
	public void UpdateItem(T item) {
		SortUp(item);
	}

	public int Count {
		get {
			return currentItemCount;
		}
	}

	public bool Contains(T item) {
		return Equals(items[item.HeapIndex], item);
	}

	//Metodo para acomomar el item hacia abajo del heap
	void SortDown(T item) {
		while (true) {
			//Hoja izquierda y hoja derecha
			int childIndexLeft = item.HeapIndex * 2 + 1;
			int childIndexRight = item.HeapIndex * 2 + 2;
			int swapIndex = 0;

			//Recorrer los valores hacia abajo. Acomodar por prioridades.
			if (childIndexLeft < currentItemCount) {
				swapIndex = childIndexLeft;

				if (childIndexRight < currentItemCount) {
					if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0) {
						swapIndex = childIndexRight;
					}
				}

				if (item.CompareTo(items[swapIndex]) < 0) {
					Swap (item,items[swapIndex]);
				}
				else {
					return;
				}

			}
			else {
				return;
			}

		}
	}

	//Acomdar haci arriba
	void SortUp(T item) {
		//Referencia al padre
		int parentIndex = (item.HeapIndex-1)/2;

		while (true) {
			T parentItem = items[parentIndex];
			if (item.CompareTo(parentItem) > 0) {
				Swap (item,parentItem);
			}
			else {
				break;
			}

			parentIndex = (item.HeapIndex-1)/2;
		}
	}

	//Metodo para cambiar los valores del heap
	void Swap(T itemA, T itemB) {
		items[itemA.HeapIndex] = itemB;
		items[itemB.HeapIndex] = itemA;
		int itemAIndex = itemA.HeapIndex;
		itemA.HeapIndex = itemB.HeapIndex;
		itemB.HeapIndex = itemAIndex;
	}
}

//Interface para tener el orden de nodos
public interface IHeapItem<T> : IComparable<T> {
	int HeapIndex {
		get;
		set;
	}
}
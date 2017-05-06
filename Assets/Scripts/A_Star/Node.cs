using System.Collections;
using UnityEngine;

public class Node : IHeapItem<Node> {

	public bool walkable;
	public Vector3 worldPosition;
	//Tener la referncia del nodo en que esta el enemigo
	public int gridX;
	public int gridY;

	//Calculo de los costos.
	//gCost es la distancia del nodo en donde se empezo
	public int gCost;
	//hCost es la distancia hacia el nodo donde queremos llegar.
	public int hCost;
	//Tener la refencia al padre. Para saber de que nodo fue calculado su gCost y su fCost
	public Node parent;
	//Index del heap
	int heapIndex;

	//Constructor.
	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY) {
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
	}

	//fCost = gCost + hCost;
	public int fCost {
		get {
			return gCost + hCost;
		}
	}

	public int HeapIndex {
		get {
			return heapIndex;
		}
		set {
			heapIndex = value;
		}
	}

	//Compara los valores de fCost de los nodos
	public int CompareTo(Node nodeToCompare) {
		int compare = fCost.CompareTo(nodeToCompare.fCost);
		//Si los fCost son iguales, ocupar el hCost
		if (compare == 0) {
			compare = hCost.CompareTo(nodeToCompare.hCost);
		}
		return -compare;
	}
}

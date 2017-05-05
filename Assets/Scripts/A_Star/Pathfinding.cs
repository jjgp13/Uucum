using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pathfinding : MonoBehaviour {
	//Referencia al manager de paths
	PathRequestManager requestManager;
	//referencia al grid que construido
	Grid grid;

	void Awake() {
		//Obtener el script del manager
		requestManager = GetComponent<PathRequestManager>();
		//Obtener el script del grid al despertar
		grid = GetComponent<Grid> ();
	}

	//Buscar un path en el escenario
	public void StartFindPath(Vector3 startPos, Vector3 targetPos) {
		StartCoroutine(FindPath(startPos,targetPos));
	}

	//Implementación del algoritmo A*.
	IEnumerator FindPath(Vector3 startPos, Vector3 targetPos) {
		Vector3[] waypoints = new Vector3[0];
		bool pathSuccess = false;

		//Obtener el nodo en el cual se empezo y el nodo en donde esta el nodo a donde queremos llegar
		Node startNode = grid.NodeFromWorldPoint(startPos);
		Node targetNode = grid.NodeFromWorldPoint(targetPos);

		if (startNode.walkable || targetNode.walkable) {
			//Lista de nodos abiertos
			Heap<Node> openSet = new Heap<Node> (grid.MaxSize);
			//Hash de nodos cerrados
			HashSet<Node> closedSet = new HashSet<Node> ();

			//Incluir a la lista de nodos abiertos el nodo donde empezamos
			openSet.Add (startNode);

			//Mientras la lista de nodos abiertos no este vacia, hacer los calculos.
			while (openSet.Count > 0) {
				//El nodo actual es en el que empezamos (startNode)
				Node currentNode = openSet.RemoveFirst ();
				closedSet.Add (currentNode);

				//Si hemos llegado al nodo final, hemos encontrado nuestro camino.
				if (currentNode == targetNode) {
					pathSuccess = true;
					break;
				}
				//Por cada uno de los nodos en la lista de vecinos
				foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
					//Si el no es caminbale o el hash de cerrados, omitiarlo
					if (!neighbour.walkable || closedSet.Contains (neighbour)) {
						continue;
					}
					//Calcular el nuevo costo de los vecinos
					int newCostToNeighbour = currentNode.gCost + GetDistance (currentNode, neighbour);
					//Si el nuevo costo es menor al gCost del vecino o la lista de abiertos no esta en la lista de vecinos
					if (newCostToNeighbour < neighbour.gCost || !openSet.Contains (neighbour)) {
						//Calcular el fCost del vecino. Calculando el gCost y el hCost del vecino
						neighbour.gCost = newCostToNeighbour;
						neighbour.hCost = GetDistance (neighbour, targetNode);
						//El padre del nuevo nodo calcualo es el actual
						neighbour.parent = currentNode;

						//Si no esta en la lista de abiertos, añadirlo
						if (!openSet.Contains (neighbour))
							openSet.Add (neighbour);
					}
				}
			}
		}
		yield return null;
		if (pathSuccess) {
			waypoints = RetracePath(startNode,targetNode);
		}
		requestManager.FinishedProcessingPath(waypoints,pathSuccess);
	}

	//Regresar el camino que se obtuvo
	Vector3[] RetracePath(Node startNode, Node endNode) {
		//Lista de nodos que obtiene el camino
		List<Node> path = new List<Node>();
		Node currentNode = endNode;
		//Regresar al lisa de nodos del camino
		//Mietras no sea igual al nodo de inicio, agregarlo
		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}

		Vector3[] waypoints = SimplifyPath(path);
		Array.Reverse(waypoints);
		return waypoints;

	}

	Vector3[] SimplifyPath(List<Node> path) {
		List<Vector3> waypoints = new List<Vector3>();
		Vector2 directionOld = Vector2.zero;

		for (int i = 1; i < path.Count; i ++) {
			Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX,path[i-1].gridY - path[i].gridY);
			if (directionNew != directionOld) {
				waypoints.Add(path[i].worldPosition);
			}
			directionOld = directionNew;
		}
		return waypoints.ToArray();
	}


	//Obtener la distancia entre dos nodos
	//A es para el nodo donde comienza y b el nodo target
	int GetDistance(Node nodeA, Node nodeB) {
		//Valores absolutos de la distancia de los nodos en x and y
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
		//Si ls distancia en y es menor, regresar los movimeintos en diagonal hacia y axis
		//Siempre tomamos el de menor magnitud por que es el que nos regresa los movimientos en diagonal.
		if (dstX > dstY)
			return 14*dstY + 10* (dstX-dstY);
		//Si los movimientos en x son menores multiplicarlos por el costo diagnoal y despues por el costo directo
		return 14*dstX + 10 * (dstY-dstX);
	}
}

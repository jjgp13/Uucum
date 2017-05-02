using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	public bool displayGridGizmos;
	//Layer para detecar si hay una colisición y saber si esa area es caminable.
	public LayerMask unwalkableMask;
	//Tamaño del grid en unidades del 3dSpaces
	public Vector2 gridWorldSize;
	//tamaño del nodo
	public float nodeRadius;
	//Arreglo para almacenar los nodos
	Node[,] grid;

	//Referenicas a los tamaños del nodo 
	float nodeDiameter;
	int gridSizeX, gridSizeY;

	//Saber si el grid existe
	public static bool gridExists;

	void Awake() {
		gridExists = false;
		//Obtener el diametro del nodo
		nodeDiameter = nodeRadius*2;
		//Obtener su medidas a partir del tamaño que le asignamos en el inspector.
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
		//Crear el grid
		StartCoroutine(timeToCreateGrid());
	}
	//Tamaño maximo del grid
	public int MaxSize {
		get {
			return gridSizeX * gridSizeY;
		}
	}

	//Esperar 5 segundos a que se cree el escenario
	IEnumerator timeToCreateGrid(){
		yield return new WaitForSeconds (5);
		CreateGrid ();
		gridExists = true;
	}



	//Metodo para crear el grid con gizmos
	void CreateGrid() {
		grid = new Node[gridSizeX,gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x/2 - Vector3.forward * gridWorldSize.y/2;

		for (int x = 0; x < gridSizeX; x ++) {
			for (int y = 0; y < gridSizeY; y ++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint,nodeRadius,unwalkableMask));
				//Crear el nodo y almacenralo en el grid
				grid[x,y] = new Node(walkable,worldPoint, x,y);
			}
		}
	}

	//Metodo para obtener los vecinos del nodo.
	public List<Node> GetNeighbours(Node node) {
		//Estructura de los vecinos
		List<Node> neighbours = new List<Node>();

		//Recorrer de 3 en 3
		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				//Omitir si esta en el centro
				if (x == 0 && y == 0)
					continue;
				//Verificar si el grid esta dentro del los vecinos
				int checkX = node.gridX + x;
				int checkY = node.gridY + y;
				//Verificar si esta dentro del recuadro de vecinos y agregarlo
				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
					neighbours.Add(grid[checkX,checkY]);
				}
			}
		}
		//regresar la lista de vecinos
		return neighbours;
	}

	//Obtener el nodo segun sea la posición del objeto en el 3dSpace
	public Node NodeFromWorldPoint(Vector3 worldPosition) {
		float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y/2) / gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX-1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY-1) * percentY);
		return grid[x,y];
	}

	//lista de nodos que contiene el camino
	public Heap<Node> path;
	//Metodo para pintar los nodos en el grid
	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position,new Vector3(gridWorldSize.x,1,gridWorldSize.y));
		if (grid != null && displayGridGizmos) {
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable)?Color.white:Color.red;
				if (path != null)
				if (path.Contains(n))
					Gizmos.color = Color.black;
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter-.1f));
			}
		}
	}
}

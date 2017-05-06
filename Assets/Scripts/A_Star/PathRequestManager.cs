using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathRequestManager : MonoBehaviour {
	//Crear una cola para los pathRequest de los enemigos
	Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
	//Almacenar el path actual
	PathRequest currentPathRequest;

	//Para acceder a la instancia del path que se esta calculando
	static PathRequestManager instance;
	Pathfinding pathfinding;
	//Sabar si se esta procesando un path de un enemigo
	bool isProcessingPath;

	void Awake() {
		instance = this;
		pathfinding = GetComponent<Pathfinding>();
	}

	//Este se llama en el scprit de units.
	//Para obtener una ruta de un enemigo
	public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback) {
		PathRequest newRequest = new PathRequest(pathStart,pathEnd,callback);
		//una vez que se creo el path meterlo a la cola
		instance.pathRequestQueue.Enqueue(newRequest);
		//Proceser una vez calculado
		instance.TryProcessNext();
	}

	//Intentar procesar el siguietne
	void TryProcessNext() {
		//Si no se esta procesando y no hay paths procesando en la cola
		if (!isProcessingPath && pathRequestQueue.Count > 0) {
			currentPathRequest = pathRequestQueue.Dequeue();
			isProcessingPath = true;
			pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
		}
	}

	//Una vez que se haya acabo de calcular el path
	public void FinishedProcessingPath(Vector3[] path, bool success) {
		currentPathRequest.callback(path,success);
		isProcessingPath = false;
		TryProcessNext();
	}

	//Estructura para obtener el path
	struct PathRequest {
		public Vector3 pathStart;
		public Vector3 pathEnd;
		public Action<Vector3[], bool> callback;

		public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback) {
			pathStart = _start;
			pathEnd = _end;
			callback = _callback;
		}

	}
}
using UnityEngine;
using NavMeshPlus.Components;

public class NavMeshSurfaceManagement : MonoBehaviour
{
    public static NavMeshSurfaceManagement Instance { get; private set; }
    private NavMeshSurface _navMeshSurface;

	private void Awake()
	{
		Instance = this;
		_navMeshSurface = GetComponent<NavMeshSurface>();
		_navMeshSurface.hideEditorLogs = true;
	}
	public void RebakeNavMeshSurface()
	{
		_navMeshSurface.BuildNavMesh();
	}
}

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DynamicArrows : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private bool runOnStart;
    [SerializeField] private LineRenderer path;
    [SerializeField] private float pathHeightOffset = 1.25f;
    [SerializeField] private float spawnHeightOffset = 1.5f;
    [SerializeField] private float pathUpdateSpeed = 0.25f;
    public GameObject destinationPoint;
    private NavMeshTriangulation Triangulation;
    private Coroutine drawPathCoroutine;

    public delegate void DestinationEvent(GameObject d);
    public static DestinationEvent onDestinationActive;
    public static DestinationEvent onDestinationDeactive;
    public static DynamicArrows instance;
    private void Awake()
    {
       instance = this;
    }

    public void TakeOwner(Transform ownerPlayer)
    {
        player = ownerPlayer;
    }

    private void Start()
    {
        if(runOnStart) ShowPath();
    }
    void OnEnable()
    {
        onDestinationActive+= DestinationActive;
        onDestinationDeactive+= DestinationDeactive;
    }
    void OnDisable()
    {
        onDestinationActive-= DestinationActive;
        onDestinationDeactive-= DestinationDeactive;
    }

    void DestinationActive(GameObject gameObj){
        path.enabled = true;
        SetDestinationPointToDrawPath(gameObj);
    }
    void DestinationDeactive(GameObject gameObj){
        StopCoroutine(drawPathCoroutine);
            path.enabled = false;
    }


    public void ShowPath()
    {
        drawPathCoroutine = StartCoroutine(DrawPath());
    }
    /// <summary>
    /// Show path with respect to transform
    /// </summary>
    /// <param name="transform"></param> <summary>
    /// transform is a destination point
    /// </summary>
    /// <param name="transform"></param>
    public void ShowPath(GameObject transform)
    {
        drawPathCoroutine = StartCoroutine(DrawPath(transform));
    }

    public void SetDestinationPointToDrawPath(GameObject position)
    {
        destinationPoint = position;
        ShowPath(destinationPoint);
    }
    private IEnumerator DrawPath(GameObject dPoint = null)
    {
        if(dPoint == null) dPoint = destinationPoint;
        WaitForSeconds Wait = new WaitForSeconds(pathUpdateSpeed);
        NavMeshPath path = new NavMeshPath();

        while (dPoint != null)
        {
            if (NavMesh.CalculatePath(player.position, dPoint.transform.position, NavMesh.AllAreas, path))
            {
                this.path.positionCount = path.corners.Length;

                for (int i = 0; i < path.corners.Length; i++)
                {
                    this.path.SetPosition(i, path.corners[i] + Vector3.up * pathHeightOffset);
                }
                //Player.GetComponent<NavMeshAgent>().SetPath(path);
            }
            else
            {
                Debug.LogError($"Unable to calculate a path on the NavMesh between {player.position} and {dPoint.transform.position}!");
            }

            yield return Wait;
        }
    }
}
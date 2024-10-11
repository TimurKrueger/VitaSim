using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationPath: MonoBehaviour {
    public static NavigationPath Instance { get; private set; }

    [SerializeField] private Transform Player;
    [SerializeField] private LineRenderer Path;
    [SerializeField] private float PathHeightOffset = 1.0f;
    [SerializeField] private float SpawnHeightOffset = 1.5f;
    [SerializeField] private float PathUpdateSpeed = 0.25f;
    [SerializeField] private float StopDistanceBeforeTarget = 20f;

    private Transform CurrentTarget;
    private NavMeshTriangulation Triangulation;
    private Coroutine DrawPathCoroutine;

    private void Awake() {
        Triangulation = NavMesh.CalculateTriangulation();

        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        }
        else {
            Instance = this;
        }
    }

    public void SetNewTarget(Transform newTarget) {
        CurrentTarget = newTarget;

        if (DrawPathCoroutine != null) {
            StopCoroutine(DrawPathCoroutine);
        }

        DrawPathCoroutine = StartCoroutine(DrawPathToTarget());
    }
    
    private IEnumerator DrawPathToTarget() {
        WaitForSeconds Wait = new WaitForSeconds(PathUpdateSpeed);
        NavMeshPath path = new NavMeshPath();

        while(CurrentTarget != null) {
            if(HospitalManager.Instance.State == GameState.DestroyAllViruses) {
                Path.enabled = false; break;
            }
            if (HospitalManager.Instance.State == GameState.SubmitReportToChiefDoctor) {
                Path.enabled = true;
            }

            if (Interactor.Instance.IsInInteractionRange(CurrentTarget)) {
                Path.enabled = false;
            } else {
                Path.enabled = true;

                if (NavMesh.CalculatePath(Player.position, CurrentTarget.transform.position, NavMesh.AllAreas, path)) {
                    Path.positionCount = path.corners.Length;

                    for (int i = 0; i < path.corners.Length; i++) {
                        Path.SetPosition(i, path.corners[i] + Vector3.up * PathHeightOffset);
                    }
                } else {
                    Debug.Log("Unable to calculate path");
                }
            }
           
            yield return Wait;
        }
    }
}

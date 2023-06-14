using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public GameObject parent;
    public SphereCollider interactionCollider;

    Mesh mesh;
    public Vector3[] polygonPoints;
    public int[] polygonTriangles;

    public int polygonSides;
    public float radius;

    // Start is called before the first frame update
    void Start() {
        mesh = new Mesh();
        this.GetComponent<MeshFilter>().mesh = mesh;
        interactionCollider.radius = radius;
    }

    // Update is called once per frame
    void Update() {
        DrawCircle(polygonSides, radius);
    }

    private void OnTriggerEnter(Collider target) {    
        if (target.gameObject.layer == 10) {
            AIMovementController targetController = target.gameObject.GetComponentInParent<AIMovementController>();
            AIMovementController self = parent.GetComponent<AIMovementController>();
            if (!targetController.hasInteracted && !self.hasInteracted) {
                targetController.hasInteracted = true;
                self.hasInteracted = true;

            }
        }
    }

    void DrawCircle(int steps, float radius) {
        polygonPoints = GetCircumferencePoints(steps, radius).ToArray();
        polygonTriangles = DrawFilledTriangles(polygonPoints);
        mesh.Clear();
        mesh.vertices = polygonPoints;
        mesh.triangles = polygonTriangles;
    }

    List<Vector3> GetCircumferencePoints(int steps, float radius) {
        Vector3 parentPosition = parent.transform.position;
        List<Vector3> points = new List<Vector3>();
        float circumferenceProgressPerStep = (float)1 / steps;
        float TAU = 2 * Mathf.PI;
        float radianProgressPerStep = circumferenceProgressPerStep * TAU;

        for(int i = 0; i < steps; i++) {
            float currentRadian = radianProgressPerStep * i;
            points.Add(new Vector3((Mathf.Cos(currentRadian) * radius), -1, (Mathf.Sin(currentRadian) * radius)));
        }
        return points;
    }

    int[] DrawFilledTriangles(Vector3[] points) {
        int triangleAmount = points.Length - 2;
        List<int> newTriangles = new List<int>();
        for(int i = 0; i < triangleAmount; i++) {
            newTriangles.Add(0);
            newTriangles.Add(i + 2);
            newTriangles.Add(i + 1);
        }
        return newTriangles.ToArray();
    }

}

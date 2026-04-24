using UnityEngine;

public class CircleCreator : MonoBehaviour
{
    [SerializeField] public LineRenderer circleRenderer;
    [SerializeField] public int subdivisions = 10;
    [SerializeField] public float radius = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Drawcircle(subdivisions, radius);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Drawcircle(int steps, float radius)
    {
        circleRenderer.positionCount = steps;

        for(int currentStep = 0; currentStep < steps; currentStep++)
        {
            float circumfrenceProgress = (float) currentStep/steps;
            float currentRadian = circumfrenceProgress * 2 * Mathf.PI;
            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3(x, y, 0);

            circleRenderer.SetPosition(currentStep, currentPosition);
        }
    }
}

using UnityEngine;

public class FlowerController : MonoBehaviour
{
    public Vector3 rotationSpeed = new Vector3(0, 36, 0);
    public int direction = 1;

    private void Start()
    {
        int rndNum = UnityEngine.Random.Range(0, 2);
        if (rndNum == 0)
        {
            direction = 1;
        }
        else if (rndNum == 1) 
        {
            direction = -1;
        }
    }

    void Update()
    {
        transform.Rotate(rotationSpeed * direction * Time.deltaTime);
    }

}

using UnityEngine;

public class WallsRepeat : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 placeToTransform;

    private Vector3 placeToReset;

    private void Start()
    {
        placeToReset = this.gameObject.transform.position;
    }

    private void Update()
    {
        if (TerrainGenerator.instance.canMove)
        {
            this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * TerrainGenerator.instance.velocity, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Repeat")
        {
            this.gameObject.transform.localPosition = placeToTransform;
        }
    }
}
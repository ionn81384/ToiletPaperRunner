using UnityEngine;

public class Tile1Mechanic : MonoBehaviour
{
    public GameObject miniCube;
    public float max = 372;
    public float min = -372;


    // Start is called before the first frame update
    private void Start()
    {
        setTile();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (TerrainGenerator.instance.canMove)
        {
            miniCube.transform.parent.gameObject.transform
                .Translate(Vector3.forward * Time.deltaTime * TerrainGenerator.instance.velocity);
        }
    }

    public void setTile()
    {
        var newX = Random.Range(min, max);
        Vector3 newPosition = new Vector3(newX / 1000, 0.044f, 0f);
        miniCube.transform.localPosition = newPosition;
    }
}